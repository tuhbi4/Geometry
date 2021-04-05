using NUnit.Framework;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry.Tests
{
    [TestFixture()]
    public class TurnsTests
    {
        [Test()]
        public void IsTurnsCountOverTest()
        {
            Random random = new();
            int turnsAmount = random.Next(1, 100);
            Turns turns = new(turnsAmount);
            for (int currentTurn = 1; currentTurn <= turnsAmount; currentTurn++)
            {
                Assert.AreEqual(false, turns.IsTurnsCountOver());
                turns.IncrementCurrentTurnNumber();
            }
            Assert.AreEqual(true, turns.IsTurnsCountOver());
        }

        [Test()]
        public void IncrementCurrentTurnNumberTest()
        {
            Random random = new();
            int turnsAmount = random.Next(1, 100);
            Turns turns = new(turnsAmount);
            for (int currentTurn = 1; currentTurn <= turnsAmount; currentTurn++)
            {
                turns.IncrementCurrentTurnNumber();
                Assert.AreEqual(currentTurn, turns.CurrentTurnNumber);
            }            
        }
    }
}