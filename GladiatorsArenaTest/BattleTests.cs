﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using GladiatorsArena;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GladiatorsArenaTest
{
    [TestClass]
    public class BattleTests
    {
        /*
         * Template
         *  [TestMethod]
            public void Test()
            {
                // Arrange
                // Act
                // Assert
            }
         */

        [TestMethod]
        public void BattleCreatedWithFighters()
        {
            // Arrange
            using Fighter hercules = new Fighter("Hercules");
            using Fighter conan = new Fighter("Conan");

            List<Fighter> fightersList = new List<Fighter>
            {
                hercules,
                conan
            };
            // Act
            using Battle battle = new Battle(fightersList);
            // Assert
            Assert.IsFalse(battle.FightersList.Count <= 0);
        }

        [TestMethod]
        public void GetTheWinnerFromTheBattle()
        {
            // Arrange
            using Fighter hercules = new Fighter("Hercules");
            using Fighter jetLee = new Fighter("Jet Lee");
            using Fighter conan = new Fighter("Conan");

            List<Fighter> fightersList = new List<Fighter>
            {
                hercules,
                jetLee,
                conan
            };

            Battle battle = new Battle(fightersList);
            
            string expected = "Hercules";
            int damage = Battle.MaxFighterHealth - 1;
            // Act
            for (int i = 0; i < battle.FightersList.Count; i++)
            {
                battle.FightersList[i].Health = damage;
                damage += 2;
            }

            Fighter winner = battle.GetWinner();
            string actual = winner.Name;
            // Assert
            Assert.AreEqual<string>(expected, actual);
            Assert.IsTrue(winner.IsDead == false);
        }

        [TestMethod]
        public void BattleHasDeadFighterRemovedAndCounterDecremented()
        {
            // Arrange
            using Fighter hercules = new Fighter("Hercules");
            using Fighter jetLee = new Fighter("Jet Lee");
            using Fighter conan = new Fighter("Conan");

            List<Fighter> fightersList = new List<Fighter>
            {
                hercules,
                jetLee,
                conan
            };

            using Battle battle = new Battle(fightersList);
            battle.FighterListChanged += battle.FighterRemovedListener;

            int damage = Battle.MaxFighterHealth + 1;
            int counter = 5;

            string expected = $" Jet Lee is now out of the battle!{Environment.NewLine}" +
                                $" Conan is now out of the battle!{Environment.NewLine}" +
                                $"Hercules{Environment.NewLine}";
            int expectedCounter = 3;

            using StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Act
            battle.FightersList[1].Health = damage;
            if (battle.FightersList[1].IsDead) battle.CleanDeadFighter(ref counter);
            battle.FightersList[1].Health = damage;
            if (battle.FightersList[1].IsDead) battle.CleanDeadFighter(ref counter);

            battle.FightersList.ForEach(fighter => Console.WriteLine(fighter.Name));

            string actual = stringWriter.ToString();
            // Assert
            Assert.AreEqual<string>(expected, actual);
            Assert.IsTrue(battle.FightersList.Count == 1);
            Assert.AreEqual<int>(expectedCounter, counter);
        }
    }
}
