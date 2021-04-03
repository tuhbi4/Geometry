using System;
using static Geometry.Input;

namespace Geometry
{
    public class Board
    {
        public static int Cols { get; private set; }
        public static int Rows { get; private set; }
        public int EmptyCellsCount { get; private set; }
        public static string Filler { get; private set; }
        protected static string[,] BoardMatrix { get; private set; }
        private Point CurrentPoint { get; set; }
        public Board(int rows, int cols, string filler)
        {
            Cols = cols;
            Rows = rows;
            Filler = filler;
            EmptyCellsCount = cols * rows;
            BoardMatrix = new string[rows + 4, cols + 4];
            FillBoardMatrix(rows, cols, filler);
        }
        public Board(int rows, int cols) : this(rows, cols, "    ") { }
        public Board() : this(30, 20) { }
        private void FillBoardMatrix(int rows, int cols, string filler)
        {
            int width = cols + 4;
            int height = rows + 4;
            for (int rowIndex = 0; rowIndex < height; rowIndex++)
            {
                for (int colIndex = 0; colIndex < width; colIndex++)
                {
                    BoardMatrix[rowIndex, colIndex] = filler;
                    if (colIndex == 0 || colIndex == width - 1)
                    {
                        if (rowIndex <= 10) { BoardMatrix[rowIndex, colIndex] = $"|{rowIndex - 1}-|"; }
                        else { BoardMatrix[rowIndex, colIndex] = $"|{rowIndex - 1}|"; }
                    }
                    if (rowIndex == 0 || rowIndex == height - 1)
                    {
                        if (colIndex <= 10) { BoardMatrix[rowIndex, colIndex] = $"|{colIndex - 1}-|"; }
                        else { BoardMatrix[rowIndex, colIndex] = $"|{colIndex - 1}|"; }
                    }
                    if (colIndex == 1 || colIndex == width - 2)
                    {
                        BoardMatrix[rowIndex, colIndex] = "|--|";
                    }
                    if (rowIndex == 1 || rowIndex == height - 2)
                    {
                        BoardMatrix[rowIndex, colIndex] = "|--|";
                    }
                    if ((rowIndex < 2 || rowIndex > height - 3) && (colIndex < 2 || colIndex > width - 3))
                    {
                        BoardMatrix[rowIndex, colIndex] = "|++|";
                    }
                }
            }
        }
        public static void DrawBoard()
        {
            for (int rowIndex = 0; rowIndex < (BoardMatrix.GetUpperBound(0) + 1); rowIndex++)
            {
                for (int colIndex = 0; colIndex < BoardMatrix.GetUpperBound(1) + 1; colIndex++)
                {
                    Console.Write(BoardMatrix[rowIndex, colIndex]);
                }
                Console.WriteLine();
            }
        }
        public void DrawField(Player player, Point CurrentPoint, int firstDice, int secondDice)
        {
            GetСoordinates(player, firstDice, secondDice);
            if (IsPlacementFieldOnBooardPossible(firstDice, secondDice))
            {
                for (var rowIndex = 0; rowIndex < (BoardMatrix.GetUpperBound(0) + 1); rowIndex++)
                {
                    for (var colIndex = 0; colIndex < BoardMatrix.GetUpperBound(1) + 1; colIndex++)
                    {
                        if (rowIndex >= CurrentPoint.Y + 1
                         && rowIndex < CurrentPoint.Y + 1 + firstDice
                          && colIndex >= CurrentPoint.X + 1
                           && colIndex < CurrentPoint.X + 1 + secondDice)
                        {
                            BoardMatrix[rowIndex, colIndex] = player.Filler;
                        }
                        Console.Write(BoardMatrix[rowIndex, colIndex]);
                    }
                    Console.WriteLine();
                }
                var filledCells = firstDice * secondDice;
                EmptyCellsCount -= filledCells;
                player.FilledCells += filledCells;
                Console.WriteLine($"{player.Name}, you have already filled {player.FilledCells} cells.");
            }
        }
        private void GetСoordinates(Player player, int firstDice, int secondDice)
        {
            if (player.Name.Equals("Computer"))
            {
                CurrentPoint = new(1, Cols, Rows);
                if (!IsFieldFitToBoard(CurrentPoint.X, CurrentPoint.Y, firstDice, secondDice) || IsFieldOverlapAnotherField(CurrentPoint.X, CurrentPoint.Y, firstDice, secondDice))
                {
                    GetСoordinates(player, firstDice, secondDice);
                }
                Console.WriteLine($"Computer selects a point...\nComputer drew from [{CurrentPoint.X},{CurrentPoint.Y}].");
            }
            else
            {
                Console.WriteLine("Please enter the coordinates of the drawing start point:");
                CurrentPoint = RequestСoordinates(Cols, Rows);
                if (!IsFieldFitToBoard(CurrentPoint.X, CurrentPoint.Y, firstDice, secondDice))
                {
                    Console.WriteLine("Cannot be drawn from this point - the field will not fit!");
                    GetСoordinates(player, firstDice, secondDice);
                }
                else if (IsFieldOverlapAnotherField(CurrentPoint.X, CurrentPoint.Y, firstDice, secondDice))
                {
                    Console.WriteLine("Cannot be drawn there, cells is already occupied!");
                    GetСoordinates(player, firstDice, secondDice);
                }
            }
        }
        public static bool IsPlacementFieldOnBooardPossible(int firstDice, int secondDice)
        {
            for (var rowIndex = 2; rowIndex < (BoardMatrix.GetUpperBound(0) - 2); rowIndex++)
            {
                for (var colIndex = 2; colIndex < BoardMatrix.GetUpperBound(1) - 2; colIndex++)
                {
                    if (BoardMatrix[rowIndex, colIndex].Equals(Filler)
                     && IsFieldFitToBoard(colIndex - 1, rowIndex - 1, firstDice, secondDice)
                     && !IsFieldOverlapAnotherField(colIndex - 1, rowIndex - 1, firstDice, secondDice))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private static bool IsFieldFitToBoard(int startCoordinateX, int startCoordinateY, int firstDice, int secondDice)
        {
            return startCoordinateY + firstDice - 1 <= Rows
             && startCoordinateX + secondDice - 1 <= Cols;
        }
        private static bool IsFieldOverlapAnotherField(int startCoordinateX, int startCoordinateY, int firstDice, int secondDice)
        {
            for (var rowIndex = startCoordinateY + 1; rowIndex < startCoordinateY + 1 + firstDice; rowIndex++)
            {
                for (var colIndex = startCoordinateX + 1; colIndex < startCoordinateX + 1 + secondDice; colIndex++)
                {
                    if (!BoardMatrix[rowIndex, colIndex].Equals(Filler))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}