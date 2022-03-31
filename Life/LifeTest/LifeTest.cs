using Display;
using Life;
using System;
using System.Collections.Generic;
using Xunit;

namespace LifeTest
{
    public class LifeTest
    {
        private Life.Life Life;

        [Fact]
        public void TestVon()
        {
            Grid grid = new Grid(16, 16);
            Life = new Life.Life(16, 16, false, 0.5f, 50, 5, false, @"C:\Users\Pro\Seeds1\glider.seed", grid, "vonNeumann", 1, false, new List<int>() { 2, 3 }, new List<int>() { 3 }, 4, false);
            Life.Initialise();
            Life.Evolve();
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 0).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 1).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 1).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 2).State);
        }

        [Fact]
        public void TestMooreOrder()
        {
            Grid grid = new Grid(16, 16);
            Life = new Life.Life(16, 16, false, 0.5f, 50, 5, false, @"C:\Users\Pro\Seeds1\glider.seed", grid, "moore", 2, false, new List<int>() { 2, 3 }, new List<int>() { 3 }, 4, false);
            Life.Initialise();
            Life.Evolve();
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 3).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 3).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 3).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 3).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 3).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 4).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 4).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 4).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 4).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 4).State);

        }

        [Fact]
        public void TestMooreOrderPeriodic()
        {
            Grid grid = new Grid(16, 16);
            Life = new Life.Life(16, 16, true, 0.5f, 50, 5, false, @"C:\Users\Pro\Seeds1\glider.seed", grid, "moore", 2, false, new List<int>() { 2, 3 }, new List<int>() { 3 }, 4, false);
            Life.Initialise();
            Life.Evolve();
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 3).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 3).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 3).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 3).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 3).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 4).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 4).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 4).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 4).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 4).State);

            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 15).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 15).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 14).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 14).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 0 && c.Y == 0).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 0 && c.Y == 1).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 0 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 0 && c.Y == 3).State);
        }

        [Fact]
        public void TestMooreOrderSB()
        {
            Grid grid = new Grid(16, 16);
            Life = new Life.Life(16, 16, true, 0.5f, 50, 5, false, @"C:\Users\Pro\Seeds1\glider.seed", grid, "moore", 2, false, new List<int>() { 2, 3 }, new List<int>() { 3 }, 4, false);
            Life.Initialise();
            Life.Evolve();
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 0).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 1).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 3).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 3).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 3).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 3).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 3).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 4).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 4).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 13 && c.Y == 4).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 12 && c.Y == 4).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 11 && c.Y == 4).State);

            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 15).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 15).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 15 && c.Y == 14).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 14 && c.Y == 14).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 0 && c.Y == 0).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 0 && c.Y == 1).State);
            Assert.Equal(CellState.Full, Array.Find(Life.LifeCells, c => c.X == 0 && c.Y == 2).State);
            Assert.Equal(CellState.Blank, Array.Find(Life.LifeCells, c => c.X == 0 && c.Y == 3).State);
        }
    }
}
