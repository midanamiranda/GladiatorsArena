using System;
using System.Collections.Generic;
using System.Text;

namespace GladiatorsArena
{
    public class Battle : IDisposable
    {
        // Events

        public event EventHandler<FightersListEventArgs> FighterListChanged;

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
                    FighterListChanged(this, new FightersListEventArgs(deadFighterTrack.Pop().Name));
                if (fightersList.Count <= 1) 
                    IsStillOn = false;
            }
        }

        // Members

        /// <summary>
        /// Returns the fighter with the most health in the FighterList
        /// </summary>
        /// <returns></returns>
        public Fighter GetWinner()
        {
            return this.fightersList.Find(fighter => fighter.IsDead == false);
        }

        /// <summary>
        /// Find the Dead Fighter, remove it from the Fighters List and help manage the main program loop
        /// </summary>
        /// <param name="i"></param>
        public void CleanDeadFighter(ref int i)
        {
            Fighter deadFighter = this.FightersList.Find(fighter => fighter.IsDead == true);
            deadFighterTrack.Push(deadFighter);
            this.FightersList = this.FightersList.FindAll(fighter => fighter.IsDead != true);
            i -= 1;
        }

        //public void GenerateClash()
        //{
        //	HashSet<int> attackedFighters = new HashSet<int>(this.FightersList.Count / 2);
        //}

        // Event Listeners

        public void FighterRemovedListener(object sender, FightersListEventArgs e)
        {
            Console.WriteLine($" {e.removedFighter} is now out of the battle!");
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
