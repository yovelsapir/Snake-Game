using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCompleteProject
{
    public static class Controller
    {
        // Snake Position
        private static int xPos = 5;
        private static int yPos = 5;

        //if GameStart == true) GAME OVER
        private static bool GameStart = false;

        // Map Size
        private const int xSize = 20;
        private const int ySize = 10;

        //Start Position
        private const int xStart = 5;
        private const int yStart = 5;

        //Food Start Spawn
        private static int[] RespawnX = new int[] { 5, 5, 1, 2, 2 };
        private static int[] RespawnY = new int[] { 9, 9, 2, 5, 5 };

        //Game Map Array
        private static char[,] gameArray = new char[ySize, xSize];

        // Snake Save Position X,Y
        private static int[] ixPoition = new int[xSize * ySize];
        private static int[] iyPoition = new int[xSize * ySize];

        //Remove Last Snake Char
        public static int limitCount = 0;
        //Snake Lentgh
        public static int snakeLentgh = 1;

        //Game Speed (Threat.Sleep)
        public static int gameSpeed = 150;

        //Wall Char
        private const char WALL = '*';
        //Snake Char
        private const char Snake = '#';
        //Food Char
        private const char Food = '@';

        public static void StartGame()
        {
            snakeLentgh = 1;
            limitCount = 0;
            GameStart = true;
            for(int i = 0; i < ixPoition.Length; i++)
            {
                ixPoition[i] = 0;
                iyPoition[i] = 0;
            }
            xPos = xStart;
            yPos = yStart;
            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    gameArray[i, j] = ' ';
                }
            }
            Random rand = new Random();
            int r = rand.Next(RespawnX.Length);
            gameArray[RespawnX[r], RespawnY[r]] = Food;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
        }

        public static void ShowPanel()
        {
            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    Console.Write(gameArray[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Score: {0}", snakeLentgh);
        }

        public static void GameBounds()
        {
            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    gameArray[i, 0] = WALL;
                    gameArray[i, xSize - 1] = WALL;
                    gameArray[0, j] = WALL;
                    gameArray[ySize - 1, j] = WALL;
                }
            }
        }

        public static void SnakeMove(ref ConsoleKey command)
        {
            ixPoition[limitCount] = xPos;
            iyPoition[limitCount] = yPos;

            if (limitCount >= snakeLentgh)
            {
                limitCount = 0;
            }
            limitCount++;
            switch (command)
            {
                case ConsoleKey.DownArrow:
                    {
                        yPos++;
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        yPos--;
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        xPos++;
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        xPos--;
                        break;
                    }
            }

            if (gameArray[yPos, xPos] == Snake)
            {
                GameStart = false;
                limitCount = 0;
                return;
            }
            else if (gameArray[yPos, xPos] == Food)
            {
                CreateFood();
                snakeLentgh++;
            }

            gameArray[yPos, xPos] = Snake;
            if (iyPoition[limitCount] != 0)
            {
                gameArray[iyPoition[limitCount], ixPoition[limitCount]] = ' ';
            }

            GameStart = OutOfRange();

            if (Console.KeyAvailable)
            {
                if (command == ConsoleKey.RightArrow)
                {
                    command = Console.ReadKey().Key;
                    if (command == ConsoleKey.LeftArrow)
                    {
                        command = ConsoleKey.RightArrow;
                    }
                }
                else if (command == ConsoleKey.LeftArrow)
                {
                    command = Console.ReadKey().Key;
                    if (command == ConsoleKey.RightArrow)
                    {
                        command = ConsoleKey.LeftArrow;
                    }
                }
                else if (command == ConsoleKey.UpArrow)
                {
                    command = Console.ReadKey().Key;
                    if (command == ConsoleKey.DownArrow)
                    {
                        command = ConsoleKey.UpArrow;
                    }
                }
                else if (command == ConsoleKey.DownArrow)
                {
                    command = Console.ReadKey().Key;
                    if (command == ConsoleKey.UpArrow)
                    {
                        command = ConsoleKey.DownArrow;
                    }
                }
            }
            
        }

        public static void CreateFood()
        {
            Random rand = new Random();
            bool flag = false;
            int X = 0, Y = 0;
            while(!flag)
            {
                X = rand.Next(xSize-2)+1;
                Y = rand.Next(ySize-2)+1;
                if (gameArray[Y, X] == Snake)
                {
                    continue;
                }
                gameArray[Y, X] = Food;
                flag = true;
            }
        }

        public static bool OutOfRange()
        {
            if(yPos == ySize-1 || xPos == xSize-1 || xPos == 0 || yPos == 0)
            {
                return false;
            }
            return true;
        }

        public static bool gameStart(){
            return GameStart;
        }

        public static int GameSpeed()
        {
            return gameSpeed;
        }

        public static void SetGameSpeed(int speed)
        {
            gameSpeed = speed;
        }
    }
}
