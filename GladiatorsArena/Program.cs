using System;
using System.Collections.Generic;
using System.Linq;

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

            battle.FightersList.ForEach(fighter => fighter.HealthUpdate += fighter.HealthUpdateListener);
            battle.RemovedFighter += battle.FighterRemovedListener;

            Console.WriteLine("Welcome to the Gladiators Arena!!");
            
            Console.WriteLine($"\nRules:" +
                            $"\n1) Every fighter starts with health set to {Battle.MaxFighterHealth}" +
                            $"\n2) The maximun damage a fighter can take is {Battle.MaxHealthLoss}" +
                            $"\n3) If a fighter reaches health 0 it will be removed from the battle" +
                            $"\n4) The battle goes on until there is only one or no fighter left");
            
            Console.WriteLine("\nThe Fighters for today are:\n");

            battle.FightersList.ForEach(fighter => Console.WriteLine($"{fighter.Code} - {fighter.Name}"));

            Console.WriteLine("\nLet´s Fight!!\n");

            Random dice = new Random();
            int damage;

            do
            {
                HashSet<Fighter> attackingFighters = new HashSet<Fighter>();
                HashSet<Fighter> damagedFighters = new HashSet<Fighter>();

                battle.GenerateClash(attackingFighters, damagedFighters);

                List<Fighter> attackingFightersList = attackingFighters.ToList();
                List<Fighter> damagedFightersList = damagedFighters.ToList();

                for (int i = 0; i < damagedFightersList.Count; i++)
                {
                    damage = dice.Next(1, Battle.MaxHealthLoss + 1);

                    Console.WriteLine($"{attackingFightersList[i].Name} strikes {damagedFightersList[i].Name} !!");
                    damagedFightersList[i].Health = damage;

                    if (damagedFightersList[i].IsDead)
                        battle.CleanDeadFighter();
                    
                    Console.WriteLine("");
                }
            }
            while (Battle.IsStillOn);

            try
            {
                Fighter winner = battle.GetWinner();
                Console.WriteLine($"{winner.Name} is the only one left and takes the Crown!!");
            }
            catch (Exception)
            {
                Console.WriteLine("Hardcore Fight!! Everyone is dead.");
            }

        }
    }
}
