using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeCompleteProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Again:
            {

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();

                Console.WriteLine("Score: {0}", Controller.snakeLentgh);
                Console.WriteLine("Press Key: Down/Up/Left/Right");

                Controller.StartGame();
                Controller.GameBounds();
                Controller.SetGameSpeed(50);

                Console.CursorVisible = false;

                ConsoleKey command = Console.ReadKey().Key;
                do
                {
                    Console.CursorTop = 0;
                    Console.CursorLeft = 0;
                    Controller.ShowPanel();
                    Controller.SnakeMove(ref command);
     
                    System.Threading.Thread.Sleep(Controller.GameSpeed());
                }
                while (Controller.gameStart());

                Console.WriteLine("Game Over!");
            }

            goto Again;
        
        }
    }
}
