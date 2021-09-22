using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Linq;
using System.Diagnostics;
using System.Threading;


namespace RandomZahl3
{
    class Game
    { 
        public void Start()
        {
            Title = "Mein erstes Programm"; 
            RunMainMenu();
        }

        private void RunMainMenu()
        {
            string prompt = @"
___  ___     _                       _             ______                                              
|  \/  |    (_)                     | |            | ___ \                                             
| .  . | ___ _ _ __     ___ _ __ ___| |_ ___  ___  | |_/ / __ ___   __ _ _ __ __ _ _ __ ___  _ __ ___  
| |\/| |/ _ \ | '_ \   / _ \ '__/ __| __/ _ \/ __| |  __/ '__/ _ \ / _` | '__/ _` | '_ ` _ \| '_ ` _ \ 
| |  | |  __/ | | | | |  __/ |  \__ \ ||  __/\__ \ | |  | | | (_) | (_| | | | (_| | | | | | | | | | | |
\_|  |_/\___|_|_| |_|  \___|_|  |___/\__\___||___/ \_|  |_|  \___/ \__, |_|  \__,_|_| |_| |_|_| |_| |_|
                                                                    __/ |                              
                                                                   |___/                               

Was willst du machen?
(Benutze die Pfeiltasten nach unten und oben, um durch die Optionen zu gehen und drücke Enter, um deine Auswahl zu bestätigen.)
";
            string[] options = { "Zufallszahl erraten", "Tic Tac Toe", "Snake", "Über das Programm", "Verlassen" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    RunFirstGame();
                    break;
                case 1:
                    Clear();
                    TicTacToe();
                    break;
                case 2:
                    StartSnkae();
                    break;
                case 3:
                    DisplayAboutInfo();
                    break;
                case 4:
                    ExitGame();
                    break;
            }
        }

        private void TicTacToe()
        {
            int currentPlayer = -1;
            char[] gameMarkers = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int gameStatus = 0;

            do
            {
                Clear();
                currentPlayer = GetnextPlayer(currentPlayer);

                HeadUpDisplay(currentPlayer);
                DrawGameboard(gameMarkers);

                GameEngine(gameMarkers, currentPlayer);

                gameStatus = CheckWinner (gameMarkers);

            } while (gameStatus.Equals(0));

            Clear();
            HeadUpDisplay(currentPlayer);
            DrawGameboard(gameMarkers);

            if (gameStatus.Equals(1))
            {
                if(currentPlayer == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }
                Console.WriteLine($"Spieler {currentPlayer} ist der Sieger!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Drücke eine beliebige Taste, um fortzufahren.");
                Console.ReadKey(true);
                ErneutTicTacToeSpielen();
            }

            if (gameStatus.Equals(2))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Das Spiel ist unentschieden.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Drücke eine beliebige Taste, um fortzufahren.");
                Console.ReadKey(true);
                ErneutTicTacToeSpielen();
            }
        }

        private static bool IsGameWinner(char[] gameMarkers)
        {
            if (IsGameMarkersTheSame(gameMarkers, 0, 1, 2))
            {
                return true;
            }
            if (IsGameMarkersTheSame(gameMarkers, 3, 4, 5))
            {
                return true;
            }
            if (IsGameMarkersTheSame(gameMarkers, 6, 7, 8))
            {
                return true;
            }
            if (IsGameMarkersTheSame(gameMarkers, 0, 3, 6))
            {
                return true;
            }
            if (IsGameMarkersTheSame(gameMarkers, 1, 4, 7))
            {
                return true;
            }
            if (IsGameMarkersTheSame(gameMarkers, 2, 5, 8))
            {
                return true;
            }
            if (IsGameMarkersTheSame(gameMarkers, 0, 4, 8))
            {
                return true;
            }
            if (IsGameMarkersTheSame(gameMarkers, 2, 4, 6))
            {
                return true;
            }
            return false;
        }
        private static int CheckWinner(char[] gameMarkers)
        {

            if (IsGameDraw(gameMarkers))
            {
                return 2;
            }
            

            if (IsGameWinner(gameMarkers))
            {
                return 1;
            }
            return 0;
        }

        private static bool IsGameDraw(char[] gameMarkers)
        {
            return gameMarkers[0] != '1' &&
                   gameMarkers[1] != '2' &&
                   gameMarkers[2] != '3' &&
                   gameMarkers[3] != '4' &&
                   gameMarkers[4] != '5' &&
                   gameMarkers[5] != '6' &&
                   gameMarkers[6] != '7' &&
                   gameMarkers[7] != '8' &&
                   gameMarkers[8] != '9';
        }
        private static bool IsGameMarkersTheSame(char[] testgameMarkers, int pos1, int pos2, int pos3)
        {
            return testgameMarkers[pos1].Equals(testgameMarkers[pos2]) && testgameMarkers[pos2].Equals(testgameMarkers[pos3]);
        }

        static void HeadUpDisplay(int PlayerNumber)
        {
            string TicTacToe = @"
▄▄▄█████▓ ██▓ ▄████▄     ▄▄▄█████▓ ▄▄▄       ▄████▄     ▄▄▄█████▓ ▒█████  ▓█████ 
▓  ██▒ ▓▒▓██▒▒██▀ ▀█     ▓  ██▒ ▓▒▒████▄    ▒██▀ ▀█     ▓  ██▒ ▓▒▒██▒  ██▒▓█   ▀ 
▒ ▓██░ ▒░▒██▒▒▓█    ▄    ▒ ▓██░ ▒░▒██  ▀█▄  ▒▓█    ▄    ▒ ▓██░ ▒░▒██░  ██▒▒███   
░ ▓██▓ ░ ░██░▒▓▓▄ ▄██▒   ░ ▓██▓ ░ ░██▄▄▄▄██ ▒▓▓▄ ▄██▒   ░ ▓██▓ ░ ▒██   ██░▒▓█  ▄ 
  ▒██▒ ░ ░██░▒ ▓███▀ ░     ▒██▒ ░  ▓█   ▓██▒▒ ▓███▀ ░     ▒██▒ ░ ░ ████▓▒░░▒████▒
  ▒ ░░   ░▓  ░ ░▒ ▒  ░     ▒ ░░    ▒▒   ▓▒█░░ ░▒ ▒  ░     ▒ ░░   ░ ▒░▒░▒░ ░░ ▒░ ░
    ░     ▒ ░  ░  ▒          ░      ▒   ▒▒ ░  ░  ▒          ░      ░ ▒ ▒░  ░ ░  ░
  ░       ▒ ░░             ░        ░   ▒   ░             ░      ░ ░ ░ ▒     ░   
          ░  ░ ░                        ░  ░░ ░                      ░ ░     ░  ░
             ░                              ░                                    


 ";
            Console.WriteLine(TicTacToe);
            Console.WriteLine("Wilkommen zum Tic Tac Toe Spiel.");
            WriteLine("Gib eine Zahl, die noch frei ist, ein und bestätige sie mit Enter");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Spieler 1: X");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Spieler 2: O");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            if(PlayerNumber == 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            Console.WriteLine($"Spieler {PlayerNumber} ist am Zug, suche dir ein Feld aus");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }

        static void DrawGameboard(char[] gameMarkers)
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {gameMarkers[0]}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {gameMarkers[1]}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {gameMarkers[2]}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("---+---+---");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {gameMarkers[3]}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {gameMarkers[4]}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {gameMarkers[5]}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("---+---+---");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {gameMarkers[6]}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {gameMarkers[7]}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {gameMarkers[8]}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static int GetnextPlayer(int Player)
        {
            if (Player.Equals(1))
            {
                return 2;
            }
            return 1;
        }

        private static void GameEngine(char[] gameMarkers, int currentPlayer)
        {
            bool notvalidMove = true;
            do
            {
                
                string userInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(userInput) &&
                    (userInput.Equals("1") ||
                    userInput.Equals("2") ||
                    userInput.Equals("3") ||
                    userInput.Equals("4") ||
                    userInput.Equals("5") ||
                    userInput.Equals("6") ||
                    userInput.Equals("7") ||
                    userInput.Equals("8") ||
                    userInput.Equals("9")))
                {
                    
                    int.TryParse(userInput, out var gamePlacementMarker);

                    char currentMarker = gameMarkers[gamePlacementMarker - 1];

                    if (currentMarker.Equals('X') || currentMarker.Equals('O'))
                    {
                        Console.WriteLine("Dieser Platz ist besetzt, suche einen naderen Platz.");
                    }
                    else
                    {
                        gameMarkers[gamePlacementMarker - 1] = GetPlayerMarket(currentPlayer);
                        notvalidMove = false;
                    }
                }
            } while (notvalidMove);
        }

        private static char GetPlayerMarket(int Player)
        {
            if(Player % 2 == 0)
            {
                return 'O';
            }
            return 'X';
        }

        private void ExitGame()
        {
            Environment.Exit(0);
        }

        private void DisplayAboutInfo()
        {
            Clear();
            CursorVisible = false;
            TextAnimationen.WriteAnimation("Dieses Programm wurde von mir, Nathan Göhl, entwickelt.");
            WriteLine("");
            TextAnimationen.WriteAnimation("Dies ist meine erste Begegnung mit Visual Studio und meine erstes Programm mit C#");
            WriteLine("");
            TextAnimationen.WriteAnimation("Ich hoffe, dass es dir genau so Spass macht, das Spiel zu spielen, wie es mir Spass gemacht hat, es zu programmieren.");
            WriteLine("");
            Thread.Sleep(1000);
            CursorVisible = true;
            WriteLine("Drücke eine beliebige Taste, um zum Menu zurück zu kehren.");
            ReadKey(true);
            RunMainMenu();
        }

        private void ErneutSpielen()
        {
            string prompt = "Willst du weiter spielen?";
            string[] options = { "Ja", "Nein" };
            Menu erneutspielenMenu = new Menu(prompt, options);
            int selectedIndex = erneutspielenMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    RunFirstGame();
                    break;
                case 1:
                    RunMainMenu();
                    break;
            }
        }

        private void ErneutTicTacToeSpielen()
        {
            string prompt = "Wollt Ihr  nochmals spielen?";
            string[] options = { "Ja", "Nein" };
            Menu erneutspielenMenu = new Menu(prompt, options);
            int selectedIndex = erneutspielenMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    TicTacToe();
                    break;
                case 1:
                    RunMainMenu();
                    break;
            }
        }

        private void RunFirstGame()
        {
            Clear();

            int Grösse = 100;
            double Versuche = 0;

            string prompt = @"
▒███████▒ █    ██   █████▒▄▄▄       ██▓     ██▓      ██████    ▒███████▒ ▄▄▄       ██░ ██  ██▓    
▒ ▒ ▒ ▄▀░ ██  ▓██▒▓██   ▒▒████▄    ▓██▒    ▓██▒    ▒██    ▒    ▒ ▒ ▒ ▄▀░▒████▄    ▓██░ ██▒▓██▒    
░ ▒ ▄▀▒░ ▓██  ▒██░▒████ ░▒██  ▀█▄  ▒██░    ▒██░    ░ ▓██▄      ░ ▒ ▄▀▒░ ▒██  ▀█▄  ▒██▀▀██░▒██░    
  ▄▀▒   ░▓▓█  ░██░░▓█▒  ░░██▄▄▄▄██ ▒██░    ▒██░      ▒   ██▒     ▄▀▒   ░░██▄▄▄▄██ ░▓█ ░██ ▒██░    
▒███████▒▒▒█████▓ ░▒█░    ▓█   ▓██▒░██████▒░██████▒▒██████▒▒   ▒███████▒ ▓█   ▓██▒░▓█▒░██▓░██████▒
░▒▒ ▓░▒░▒░▒▓▒ ▒ ▒  ▒ ░    ▒▒   ▓▒█░░ ▒░▓  ░░ ▒░▓  ░▒ ▒▓▒ ▒ ░   ░▒▒ ▓░▒░▒ ▒▒   ▓▒█░ ▒ ░░▒░▒░ ▒░▓  ░
░░▒ ▒ ░ ▒░░▒░ ░ ░  ░       ▒   ▒▒ ░░ ░ ▒  ░░ ░ ▒  ░░ ░▒  ░ ░   ░░▒ ▒ ░ ▒  ▒   ▒▒ ░ ▒ ░▒░ ░░ ░ ▒  ░
░ ░ ░ ░ ░ ░░░ ░ ░  ░ ░     ░   ▒     ░ ░     ░ ░   ░  ░  ░     ░ ░ ░ ░ ░  ░   ▒    ░  ░░ ░  ░ ░   
  ░ ░       ░                  ░  ░    ░  ░    ░  ░      ░       ░ ░          ░  ░ ░  ░  ░    ░  ░
░                                                              ░                                  

Wie viele mögliche Zahlen soll es geben?";
            string[] options = { "10", "20", "50", "100", "200" ,"500", "1'000","2'000" ,"5'000" ,"10'000" ,"100'000" , "1'000'000" };
            Menu erneutspielenMenu = new Menu(prompt, options);
            int selectedIndex = erneutspielenMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Grösse = 10;
                    break;
                case 1:
                    Grösse = 20;
                    break;
                case 2:
                    Grösse = 50;
                    break;
                case 3:
                    Grösse = 100;
                    break;
                case 4:
                    Grösse = 200;
                    break;
                case 5:
                    Grösse = 500;
                    break;
                case 6:
                    Grösse = 1000;
                    break;
                case 7:
                    Grösse = 2000;
                    break;
                case 8:
                    Grösse = 5000;
                    break;
                case 9:
                    Grösse = 10000;
                    break;
                case 10:
                    Grösse = 100000;
                    break;
                case 11:
                    Grösse = 1000000;
                    break;
            }

                    int Zahl = new Random().Next(1, Grösse);
            Console.Clear();
            Console.WriteLine("Eine Zahl wurde generiert.");
            Console.WriteLine("Versuche, diese nun zu erraten.");
            Console.WriteLine("Viel Spass.");
            Console.WriteLine("Gib nun eine Zahl zwischen 1 und " + Grösse + " ein.");
            int Eingabe = 0;

            while (Eingabe != Zahl)
            {
                try
                {

                    Eingabe = Convert.ToInt32(Console.ReadLine());
                    if (Eingabe > Grösse || Eingabe < 0)
                    {
                        throw new Exception();
                    }

                    if (Zahl == Eingabe)
                    {

                        Console.Clear();
                        Versuche = Versuche + 1;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Du hast die richtige Zahl erraten.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Du hast ");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write(Versuche);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(" Versuche gebraucht, um die Zahl zu erraten. ");
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteLine("Drücke eine beliebige Taste, um fortzufahren.");
                        ReadKey(true);
                        ErneutSpielen();

                    }
                    else
                    {

                        if (Zahl > Eingabe)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("Deine Zahl ist zu klein, versuche es nochmals.");
                            Console.ForegroundColor = ConsoleColor.White;
                            Versuche = Versuche + 1;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Deine Zahl ist zu gross, versuche es nochmals.");
                            Console.ForegroundColor = ConsoleColor.White;
                            Versuche = Versuche + 1;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Ungültige Eingabe");
                }
            }
        }


        int Höhe = 20;
        int Weite = 30;

        int[] X = new int[50];
        int[] Y = new int[50];

        int fruitX;
        int fruitY;

        int Applecount = 3;

        int Startprotection = 0;

        int parts = 3;

        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        char key = 'W';

        Random rnd = new Random();

        private void Snake()
        {
            parts = 3;
            key = 'd';
            X[0] = 5;
            Y[0] = 5;

            if(parts == 3)
            {
                fruitX = 15;
                fruitY = 5;
            }
            else
            {
                fruitX = rnd.Next(2, (Weite - 2));
                fruitY = rnd.Next(2, (Höhe - 2));
            }
            CursorVisible = false;
            

            while (true)
            {
                WriteBoard();
                Input();
                Logic();
            }
        }

        public void WriteBoard()
        {
            Clear();
            for (int i = 1; i < (Weite + 2); i++)
            {
                SetCursorPosition(i, 1);
                Write("-");
            }
            for (int i = 1; i < (Weite + 2); i++)
            {
                SetCursorPosition(i, (Höhe + 2));
                Write("-");
            }
            for (int i = 1; i < (Höhe + 1); i++)
            {
                SetCursorPosition(1, i);
                Write("|");
            }
            for (int i = 1; i < (Höhe + 1); i++)
            {
                SetCursorPosition((Weite + 2), i);
                Write("|");
            }
        }

        public void Input()
        {
            if (KeyAvailable)
            {
                keyInfo = ReadKey(true);
                key = keyInfo.KeyChar;
            }
        }

        public void Logic()
        {
            if (X[0] == fruitX)
            {
                if (Y[0] == fruitY)
                {
                    parts++;
                    fruitX = rnd.Next(2, (Weite - 2));
                    fruitY = rnd.Next(2, (Höhe - 2));
                    Applecount = Applecount + 1;
                    if (Applecount == 49)
                    {
                        WinnerSnake();
                    }
                }
            }
            for (int i = parts; i > 1; i--)
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
            }
            switch (key)
            {
                case 'w':
                    Y[0]--;
                    Startprotection = Startprotection + 1;
                    break;
                case 's':
                    Y[0]++;
                    Startprotection = Startprotection + 1;
                    break;
                case 'd':
                    X[0]++;
                    Startprotection = Startprotection + 1;
                    break;
                case 'a':
                    X[0]--;
                    Startprotection = Startprotection + 1;
                    break;
            }
            for (int i = 0; i <= (parts - 1); i++)
            {
                ForegroundColor = ConsoleColor.Green;
                WritePoint(X[i], Y[i]);

                if (i == 0)
                {
                    ForegroundColor = ConsoleColor.Blue;
                    WritePoint(X[i], Y[i]);

                    if (X[i] == (Weite + 2))
                    {
                        EndProgramm();
                    }
                    if (Y[i] == (Höhe + 1))
                    {
                        EndProgramm();
                    }
                    if (X[i] == 1)
                    {
                        EndProgramm();
                    }
                    if (Y[i] == 1)
                    {
                        EndProgramm();
                    }

                }
                else
                {
                    ForegroundColor = ConsoleColor.Green;
                    WritePoint(X[i], Y[i]);
                    if (Startprotection > 4)
                    {
                        if (X[i] == X[0])
                        {
                            if (Y[i] == Y[0])
                            {
                                EndProgramm();
                            }
                        }
                    }
                }

                ForegroundColor = ConsoleColor.Red;
                WritePoint(fruitX, fruitY);
                ForegroundColor = ConsoleColor.White;
            }
            Thread.Sleep(100);
        }

        private void EndProgramm()
        {
            ForegroundColor = ConsoleColor.White;
            string prompt = "Du hast verloren. Willst du weiter spielen?";
            string[] options = { "Ja", "Nein" };
            Menu erneutspielenMenu = new Menu(prompt, options);
            int selectedIndex = erneutspielenMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Snake();
                    break;
                case 1:
                    RunMainMenu();
                    break;
            }
        }

        private void WinnerSnake()
        {
            ForegroundColor = ConsoleColor.White;
            string prompt = "Du hast Gewonnen. Willst du weiter spielen?";
            string[] options = { "Ja", "Nein" };
            Menu erneutspielenMenu = new Menu(prompt, options);
            int selectedIndex = erneutspielenMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Snake();
                    break;
                case 1:
                    RunMainMenu();
                    break;
            }
        }

        public void WritePoint(int x, int y)
        {
            SetCursorPosition(x, y);
            Write("#");
        }

        private void StartSnkae()
        {
            Clear();
            string TitelSnake = @"
  ██████  ███▄    █  ▄▄▄       ██ ▄█▀▓█████ 
▒██    ▒  ██ ▀█   █ ▒████▄     ██▄█▒ ▓█   ▀ 
░ ▓██▄   ▓██  ▀█ ██▒▒██  ▀█▄  ▓███▄░ ▒███   
  ▒   ██▒▓██▒  ▐▌██▒░██▄▄▄▄██ ▓██ █▄ ▒▓█  ▄ 
▒██████▒▒▒██░   ▓██░ ▓█   ▓██▒▒██▒ █▄░▒████▒
▒ ▒▓▒ ▒ ░░ ▒░   ▒ ▒  ▒▒   ▓▒█░▒ ▒▒ ▓▒░░ ▒░ ░
░ ░▒  ░ ░░ ░░   ░ ▒░  ▒   ▒▒ ░░ ░▒ ▒░ ░ ░  ░
░  ░  ░     ░   ░ ░   ░   ▒   ░ ░░ ░    ░   
      ░           ░       ░  ░░  ░      ░  ░
                                            
                                            ";
            WriteLine(TitelSnake);
            WriteLine("Benutze W, A, S und D, um die Schlange zu steuern.");
            WriteLine("Sammle die roten Äpfel ein. Wenn du eine gewisse Länge ereicht hast gewinnst du das Spiel.");
            WriteLine("Drücke eine beliebige Taste, um das Spiel zu starten.");
            ReadKey(true);
            Snake();
        }
    }
}