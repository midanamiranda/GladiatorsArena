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
        public void DeadFighterFound()
        {
            // Arrange
            using Fighter hercules = new Fighter("Hercules");

            List<Fighter> fightersList = new List<Fighter>
            {
                hercules
            };

            using Battle battle = new Battle(fightersList);
            battle.RemovedFighter += battle.FighterRemovedListener;

            string expected = $"Hercules has lost his life and is now out of the battle !!{Environment.NewLine}";
            int damage = Battle.MaxFighterHealth + 1;

            using StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Act
            battle.FightersList[0].Health = damage;
            battle.CleanDeadFighter();
            string actual = stringWriter?.ToString();
            // Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void BattleHasDeadFighterRemoved()
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
            battle.RemovedFighter += battle.FighterRemovedListener;

            int damage = Battle.MaxFighterHealth + 1;

            string expected = $"Jet Lee has lost his life and is now out of the battle !!{Environment.NewLine}" +
                                $"Conan has lost his life and is now out of the battle !!{Environment.NewLine}" +
                                $"Hercules{Environment.NewLine}";

            using StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Act
            battle.FightersList[1].Health = damage;
            if (battle.FightersList[1].IsDead) battle.CleanDeadFighter();
            battle.FightersList[1].Health = damage;
            if (battle.FightersList[1].IsDead) battle.CleanDeadFighter();

            battle.FightersList.ForEach(fighter => Console.WriteLine(fighter.Name));

            string actual = stringWriter.ToString();
            // Assert
            Assert.AreEqual<string>(expected, actual);
            Assert.IsTrue(battle.FightersList.Count == 1);
        }

        [TestMethod]
        public void ClashesListsWithAllFightersFromEvenPairedBattle()
        {
            // Arrange
            using Fighter hercules = new Fighter("Hercules");
            using Fighter conan = new Fighter("Conan");
            using Fighter jetLee = new Fighter("Jet Lee");
            using Fighter chuckNorris = new Fighter("Chuck Norris");
            using Fighter creed = new Fighter("Creed");
            using Fighter balboa = new Fighter("Balboa");

            List<Fighter> fightersList = new List<Fighter>
            {
                hercules,
                conan,
                jetLee,
                chuckNorris,
                creed,
                balboa
            };

            using Battle battle = new Battle(fightersList);

            int atkListSize = battle.FightersList.Count % 2 == 0 ? battle.FightersList.Count / 2 : (battle.FightersList.Count / 2) + 1;
            HashSet<Fighter> attackingFighters = new HashSet<Fighter>();
            HashSet<Fighter> damagedFighters = new HashSet<Fighter>();
            // Act
            battle.GenerateClash(attackingFighters, damagedFighters);
            // Assert
            Assert.AreEqual<int>(atkListSize, attackingFighters.Count);
            Assert.AreEqual<int>(battle.FightersList.Count - atkListSize, damagedFighters.Count);
        }

        [TestMethod]
        public void ClashesListsWithAllFightersFromNotEvenPairedBattle()
        {
            // Arrange
            using Fighter hercules = new Fighter("Hercules");
            using Fighter conan = new Fighter("Conan");
            using Fighter jetLee = new Fighter("Jet Lee");
            using Fighter chuckNorris = new Fighter("Chuck Norris");
            using Fighter creed = new Fighter("Creed");

            List<Fighter> fightersList = new List<Fighter>
            {
                hercules,
                conan,
                jetLee,
                chuckNorris,
                creed
            };

            using Battle battle = new Battle(fightersList);

            int atkListSize = battle.FightersList.Count % 2 == 0 ? battle.FightersList.Count / 2 : (battle.FightersList.Count / 2) + 1;
            HashSet<Fighter> attackingFighters= new HashSet<Fighter>();
            HashSet<Fighter> damagedFighters = new HashSet<Fighter>();
            // Act
            battle.GenerateClash(attackingFighters, damagedFighters);
            // Assert
            Assert.AreEqual<int>(atkListSize, attackingFighters.Count);
            Assert.AreEqual<int>(battle.FightersList.Count - atkListSize, damagedFighters.Count);
        }
    }
}
