using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void DeadFighterFoundAndBattleTurnedOff()
        {
            // Arrange
            using Fighter hercules = new Fighter("Hercules");

            List<Fighter> fightersList = new List<Fighter>
            {
                hercules
            };

            using Battle battle = new Battle(fightersList);

            string expected = $"Hercules has lost his life! The game is over.{Environment.NewLine}";
            int damage = Battle.MaxFighterHealth + 1;

            using StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Act            
            battle.FightersList.ForEach(fighter => fighter.DeadFighter += fighter.DeadFighterListener );           
            battle.FightersList[0].Health = damage;
            string actual = stringWriter?.ToString();
            // Assert
            Assert.AreEqual<string>(expected, actual);
            Assert.IsFalse(Battle.IsStillOn);
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
            int damage = 5;
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
        }
    }
}
