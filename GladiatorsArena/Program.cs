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

            Random dice = new Random();
            int damage;

            do
            {
                for(int i = 0; i < battle.FightersList.Count; i++)
                {
                    damage = dice.Next(1, 11);
                    battle.FightersList[i].Health = damage;
                    if (battle.FightersList[i].Health <= 0) break;
                }              
            }
            while (Battle.IsStillOn);

            Fighter winner = battle.GetWinner();

            Console.WriteLine($"{winner.Name} wins!!");
        }
    }
}
