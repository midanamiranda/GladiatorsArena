﻿using System;
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

            Console.WriteLine("Welcome to the Gladiators Arena!!");
            Console.WriteLine("\nThe Fighters for today are:");

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
                    damage = dice.Next(1, 8);
                    battle.FightersList[i].Health = damage;
                    if (battle.FightersList[i].Health <= 0) break;
                }

                Console.WriteLine("");
            }
            while (Battle.IsStillOn);

            Fighter winner = battle.GetWinner();

            Console.WriteLine($"\n{winner.Name} Takes the Crown!!");
        }
    }
}
