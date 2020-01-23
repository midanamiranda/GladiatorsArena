using System;
using System.Collections.Generic;
using System.Text;

namespace GladiatorsArena
{
    public class Battle : IDisposable
    {
        // Events

        public event EventHandler<FightersListEventArgs> RemovedFighter;

        // Contructors

        public Battle(List<Fighter> fightersList)
        {
            FightersList = fightersList;
        }

        // Fields

        private static readonly Stack<Fighter> deadFighterTrack = new Stack<Fighter>();

        // Properties

        public static int MaxFighterHealth { get; set; } = 15;
        public static int MaxHealthLoss { get; set; } = 7;
        public static bool IsStillOn { get; set; } = true;

        private List<Fighter> fightersList;

        public List<Fighter> FightersList
        {
            get { return fightersList; }
            set
            {
                fightersList = value;
                if (deadFighterTrack.Count != 0)
                    RemovedFighter?.Invoke(this, new FightersListEventArgs(deadFighterTrack.Pop().Name));
                if (fightersList.Count <= 1)
                    IsStillOn = false;
            }
        }

        // Members

        /// <summary>
        /// Returns the fighter with the IsDead flag set to false
        /// </summary>
        /// <returns></returns>
        public Fighter GetWinner()
        {
            return this.fightersList.Find(fighter => fighter.IsDead == false);
        }

        /// <summary>
        /// Find the Dead Fighter, remove it from the Fighters List
        /// </summary>
        public void CleanDeadFighter()
        {
            Fighter deadFighter = this.FightersList.Find(fighter => fighter.IsDead == true);
            deadFighterTrack.Push(deadFighter);
            this.FightersList = this.FightersList.FindAll(fighter => fighter.IsDead != true);
        }

        /// <summary>
        /// Splits the FightersList in two sides to allow coordination in the attacks
        /// </summary>
        /// <param name="attackingFighters"></param>
        /// <param name="damagedFighters"></param>
        public void GenerateClash(HashSet<Fighter> attackingFighters, HashSet<Fighter> damagedFighters)
        {
            Random rnd = new Random();            
            int atkListSize = this.FightersList.Count % 2 == 0 ? this.FightersList.Count / 2 : (this.FightersList.Count / 2) + 1;
            List<int> fgts = new List<int>();

            do
            {
                int fgt = rnd.Next(0, this.FightersList.Count);
                try
                {
                    attackingFighters.Add(this.FightersList[fgt]);
                    fgts.Add(fgt);
                }
                catch (Exception) { }
            }
            while (attackingFighters.Count < atkListSize);

            do
            {
                int fgt = rnd.Next(0, this.FightersList.Count);
                if (!fgts.Contains(fgt))
                {
                    try
                    {
                        damagedFighters.Add(this.FightersList[fgt]);
                    }
                    catch (Exception) { }
                }
            }
            while (damagedFighters.Count < (this.FightersList.Count - atkListSize));

        }

        // Event Listeners

        /// <summary>
        /// Displays the DeadFighter to be removed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FighterRemovedListener(object sender, FightersListEventArgs e)
        {
            Console.WriteLine($"{e.removedFighter} has lost his life and is now out of the battle !!");
        }

        // IDisposable Members

        /// <summary>
        /// By the end of the method, if using the 'using' keyword,
        /// the battle list will be cleared
        /// and static field will be turned on
        /// </summary>
        public void Dispose()
        {
            this.FightersList.Clear();
            IsStillOn = true;
        }
    }
}
