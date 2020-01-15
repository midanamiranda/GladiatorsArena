using System;
using System.Collections.Generic;

namespace GladiatorsArena
{
    public class Program
    {
        static void Main(string[] args)
        {
            Fighter hercules = new Fighter("Hercules");
            Fighter conan = new Fighter("Conan");
            Fighter jetLee = new Fighter("Jet Lee");

            List<Fighter> fightersList = new List<Fighter>
            {
                hercules,
                conan,
                jetLee
            };

            Battle battle = new Battle(fightersList);

            battle.FightersList.ForEach(fighter => fighter.AssigneHealthListeners());
            battle.FighterListChanged += delegate (object sender, FightersListEventArgs e)
            {
                Console.WriteLine($"{sender.GetType()} , {e.listChanged}");
            };

            Console.WriteLine("Welcome to the Gladiators Arena!!");
            Console.WriteLine("\nThe Fighters for today are:\n");

            battle.FightersList.ForEach(fighter => Console.WriteLine($"{fighter.Code} - {fighter.Name}"));

            Console.WriteLine("\nLet´s Fight!!\n");

            Random dice = new Random();
            int damage;
            int roundCount = 1;

            do
            {
                Console.WriteLine($"\nRound {roundCount++}\n");
                
                for(int i = 0; i < battle.FightersList.Count; i++)
                {
                    damage = dice.Next(1, Battle.MaxHealthLoss + 1);
                    battle.FightersList[i].Health = damage;
                    if (battle.FightersList[i].IsDead)
                    {
                        battle.CleanDeadFighter();
                        break;
                    }
                }

                Console.WriteLine("");
            }
            while (Battle.IsStillOn);

            Fighter winner = battle.GetWinner();

            Console.WriteLine($"\n{winner.Name} wins it and takes the Crown!!");
        }
    }
}
