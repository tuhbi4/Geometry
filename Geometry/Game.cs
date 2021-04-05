using System;
using System.Collections.Generic;
using static Geometry.Input;

namespace Geometry
{
    public class Game
    {
        public List<Player> Players { get; set; } = new() { };
        public Board CurrentBoard { get; private set; }
        public Turns CurrentTurns { get; private set; }
        public Point CurrentPoint { get; private set; } = new() { };
        public string Winner { get; set; }
        public static bool GameOver { get; set; }
        public string GameMode { get; set; }

        private readonly List<string> gameModesList = new()
        {
            "Player Vs Player",
            "Player Vs Computer"
        };

        public void StartGame()
        {
            StartGame(RequestGameMode(), false, false, false);
        }
        public void StartGame(int gameMode, bool allDefault)
        {
            if (allDefault)
            {
                StartGame(gameMode, true, true, true);
            }
            else
            {
                StartGame(gameMode, false, false, false);
            }
        }
        public void StartGame(int gameMode, bool defaultPlayersNames, bool defaultBoardSize, bool defaultTurnsCount)
        {
            GameMode = gameModesList[gameMode - 1];
            string nameFirstPlayer;
            string fillerFirstPlayer;
            string nameSecondPlayer;
            string fillerSecondPlayer;
            int cols;
            int rows;
            int turns;
            if (defaultPlayersNames)
            {
                nameFirstPlayer = ("Player 1");
                fillerFirstPlayer = ("####");
                if (GameMode.Equals(gameModesList[0]))
                {
                    nameSecondPlayer = ("Player 2");
                    fillerSecondPlayer = ("****");
                }
                else
                {
                    nameSecondPlayer = ("Computer");
                    fillerSecondPlayer = ("%%%%");
                }
            }
            else
            {
                IdentifyPlayers(out nameFirstPlayer, out fillerFirstPlayer, out nameSecondPlayer, out fillerSecondPlayer);
            }
            if (defaultBoardSize)
            {
                cols = 20;
                rows = 30;
            }
            else
            {
                RequestBoardSize(out cols, out rows);
            }
            if (defaultTurnsCount)
            {
                turns = 20;
            }
            else
            {
                turns = RequestNumberOfTurns();
            }
            StartGame(gameMode, nameFirstPlayer, fillerFirstPlayer, nameSecondPlayer, fillerSecondPlayer, cols, rows, turns);
        }
        public void StartGame(int gameMode, string nameFirstPlayer, string fillerFirstPlayer, string nameSecondPlayer, string fillerSecondPlayer, int cols, int rows, int turns)
        {
            GameMode = gameModesList[gameMode - 1];
            Players.Add(new(nameFirstPlayer, fillerFirstPlayer, 1));
            foreach (var mode in gameModesList)
            {
                switch (GameMode.Equals(mode))
                {
                    case true when GameMode.Equals(gameModesList[0]):
                        Players.Add(new(nameSecondPlayer, fillerSecondPlayer, 1));
                        break;
                    case true when GameMode.Equals(gameModesList[1]):
                        Players.Add(new("Computer", fillerSecondPlayer, 1));
                        break;
                    default:
                        //throw new ArgumentException("Invalid argument value");
                        break;
                }
            }
            CurrentBoard = new(rows, cols, "    ");
            CurrentTurns = new(turns);
            Board.DrawBoard();
            NextTurn();
            WhoWins();
        }
        public void IdentifyPlayers(out string nameFirstPlayer, out string fillerFirstPlayer, out string nameSecondPlayer, out string fillerSecondPlayer)
        {
            nameFirstPlayer = RequestName("Player 1");
            fillerFirstPlayer = RequestFiller("####", Players);
            nameSecondPlayer = RequestName("Player 2");
            fillerSecondPlayer = RequestFiller("****", Players);
            if (GameMode.Equals(gameModesList[1]))
            {
                Console.WriteLine("Your opponent is Computer!");
                nameSecondPlayer = ("Computer");
                fillerSecondPlayer = Players[0].Filler.Equals("%%%%") ? ("####") : ("%%%%");
            }
        }
        public void NextTurn()
        {
            while (!GameOver)
            {
                CurrentTurns.IncrementCurrentTurnNumber();
                if (CurrentTurns.CurrentTurnNumber == 1)
                {
                    Console.WriteLine($"\nGame started! Current turn: {CurrentTurns.CurrentTurnNumber}.");
                }
                else
                {
                    Console.WriteLine($"\nCurrent turn: {CurrentTurns.CurrentTurnNumber}. Turns are left: {CurrentTurns.TurnsCount - CurrentTurns.CurrentTurnNumber}.");
                }
                foreach (Player player in Players)
                {
                    Console.WriteLine($"\n{player.Name}, your turn!");
                    if (!player.GameOver)
                    {
                        Dices CurrentDices = new(player);
                        CurrentBoard.DrawField(player, CurrentPoint, CurrentDices.FirstDice, CurrentDices.SecondDice);
                        Board.DrawBoard();
                    }
                }
                IsGameOver();
            }
        }
        public void IsGameOver()
        {
            foreach (Player player in Players)
            {
                if (!player.GameOver)
                {
                    break;
                }
                GameOver = true;
                SetWinner();
            }
            if (CurrentBoard.EmptyCellsCount < 1)
            {
                Console.WriteLine("There are no more free cells on the field...");
                GameOver = true;
                SetWinner();
            }
            if (CurrentTurns.CurrentTurnNumber == CurrentTurns.TurnsCount)
            {
                Console.WriteLine("Players, your moves are over...");
                GameOver = true;
                SetWinner();
            }
        }
        public void SetWinner()
        {
            Winner = Players[0].FilledCells > Players[1].FilledCells ? Players[0].Name : Players[1].Name;
        }
        public void WhoWins()
        {
            Console.WriteLine($"\n*****GAME OVER!*****\nCongratulations {Winner}, you wins!");
        }
    }
}
