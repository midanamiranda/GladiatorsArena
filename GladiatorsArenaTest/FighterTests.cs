using Microsoft.VisualStudio.TestTools.UnitTesting;
using GladiatorsArena;
using System.IO;
using System;
using System.Collections.Generic;

namespace GladiatorsArenaTest
{
    [TestClass]
    public class FighterTests
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
        public void CreateFighterWithHealthSet()
        {
            // Arrange
            int expected = Battle.MaxFighterHealth;
            // Act
            using Fighter fighter = new Fighter();
            // Assert
            Assert.AreEqual<int>(expected, fighter.Health);
        }

        [TestMethod]
        public void CreateFighterWithNameAndAutomaticCodeAndHealth()
        {
            // Arrange
            string expected = $"My name is Hercules, my health is still on {Battle.MaxFighterHealth} and my code is 1";
            // Act
            using Fighter hercules = new Fighter("Hercules");
            string actual = $"My name is {hercules.Name}, my health is still on {hercules.Health} and my code is {hercules.Code}";
            // Assert
            Assert.AreEqual<string>(expected, actual, "Created with wrong args");
        }

        [TestMethod]
        public void CreateMultipleFightersWithRightCode()
        {
            // Arrange
            string expectedHercules = "Hercules, and my code is 1";
            string expectedConan = "Conan, and my code is 2";
            string expectedJetLee = "Jet Lee, and my code is 3";
            // Act
            using Fighter hercules = new Fighter("Hercules");
            using Fighter conan = new Fighter("Conan");
            using Fighter jetLee = new Fighter("Jet Lee");

            string actualHercules = $"{hercules.Name}, and my code is {hercules.Code}";
            string actualConan = $"{conan.Name}, and my code is {conan.Code}";
            string actualJetLee = $"{jetLee.Name}, and my code is {jetLee.Code}";
            // Assert
            Assert.AreEqual<string>(expectedHercules, actualHercules);
            Assert.AreEqual<string>(expectedConan, actualConan);
            Assert.AreEqual<string>(expectedJetLee, actualJetLee);
        }

        [TestMethod]
        public void FighterLosesIndicatedAmountOfHealth()
        {
            // Arrange
            using Fighter fighter = new Fighter();
            
            int damage = 8;
            int expected = Battle.MaxFighterHealth - damage;
            // Act
            fighter.Health = damage;
            int actual = fighter.Health;
            // Assert
            Assert.AreEqual<int>(expected, actual);
        }

        [TestMethod]
        public void NotifyFighterGetsHealthUpdate()
        {
            // Arrange
            using Fighter hercules = new Fighter("Hercules");
            hercules.HealthUpdate += hercules.HealthUpdateListener;
            int damage = 5;

            using StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string expected = $"Hercules was damaged and lost {damage} health but still has a health of {Battle.MaxFighterHealth - damage}.{Environment.NewLine}";
            // Act
            hercules.Health = damage;

            string actual = stringWriter.ToString();
            // Assert
            Assert.AreEqual<string>(expected, actual);
        }

        [TestMethod]
        public void DamageLogKeepsTrackAcrossMultipleFightersDamage()
        {
            // Arrange
            using Fighter hercules = new Fighter("Hercules");
            using Fighter conan = new Fighter("Conan");
            using Fighter jetLee = new Fighter("Jet Lee");

            hercules.HealthUpdate += hercules.HealthUpdateListener;
            conan.HealthUpdate += conan.HealthUpdateListener;
            jetLee.HealthUpdate += jetLee.HealthUpdateListener;

            int damage = 5;

            string expected = $"Hercules was damaged and lost 5 health but still has a health of {Battle.MaxFighterHealth - 5}.{Environment.NewLine}"
                                  + $"Conan was damaged and lost 6 health but still has a health of {Battle.MaxFighterHealth - 6}.{Environment.NewLine}"
                                  + $"Jet Lee was damaged and lost 7 health but still has a health of {Battle.MaxFighterHealth - 7}.{Environment.NewLine}";

            using StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Act
            hercules.Health = damage;
            damage++;
            conan.Health = damage;
            damage++;
            jetLee.Health = damage;

            string actual = stringWriter.ToString();
            // Assert
            Assert.AreEqual<string>(expected, actual);
        }
    }
}
