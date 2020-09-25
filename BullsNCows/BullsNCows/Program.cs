using System;
using System.ComponentModel;
using System.Threading;

namespace BullsNCows
{
    class Program
    {
        /// <summary>
        /// Void Main, where we call void Start.
        /// </summary>
        static void Main()
        {
            Start(true);
        }
        /// <summary>
        /// Void Start(bool tutorial) starts main cycle of the program and sets start values.
        /// </summary>
        /// <param name="tutorial"> bool tutorial = 1 makes program show you a tutorial scene. = 0 ignores the tutorial. </param>
        static void Start(bool tutorial)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string[] phrases = new string[3] { "-Oops! It's not my number. Try another!", "-He-he. You're wrong. Try next time!", "-Loser! Maybe try another number;)" };
            Game bnc = new Game();
            string line;
            int[] slot = new int[4];
            Random rnd = new Random();


            if (tutorial)
            {
                Game.Tutorial();
            }
            bnc.Generate();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("-Lets start! Try to guess the number I choose!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            while (!bnc.Gameover)
            {
                Console.Write("Type: ");
                line = Console.ReadLine();
                if (line == "/answer")
                {
                    Console.WriteLine(Game.ToString(bnc.ToGuess));
                }
                else
                {
                    try
                    {
                        slot = Game.CorrectForm(line);
                        if (!Game.CheckInput(slot))
                        {
                            Console.WriteLine();
                            throw new Exception();
                        }
                        else
                        {
                            if (bnc.Guess(slot))
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("-Congratulations! Game over! You've won in {0} attempts!", bnc.Attempts);
                                Console.Write("Correct answer is ");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine(Game.ToString(bnc.ToGuess) + '!');
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine();
                                Console.WriteLine("Your attempts:");
                                foreach (int[] item in bnc.Steps)
                                {
                                    Console.WriteLine(Game.ToString(item));
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine(Game.ShowState(bnc.CheckState(item)));
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.WriteLine();
                                }
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine();
                                Console.WriteLine(phrases[rnd.Next() % phrases.Length]);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        Console.Write("__________________________________________");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    catch
                    {
                        continue;
                    }
                }


            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Any button to restart!");
            Console.ReadKey();
            Console.Clear();
            Start(false);
        }
    }
}
