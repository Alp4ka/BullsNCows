using System;

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
            string line;
            int n = 0;
            int[] slot = new int[4];
            Random rnd = new Random();


            if (tutorial)
            {
                Game.Tutorial();
            }


            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Choose the amount of digits there should be in my number(1 to 9 including): ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            while(!Int32.TryParse(Console.ReadLine(), out n))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Choose the amount of digits there should be in my number(1 to 9 including): ");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            n = (new Game(n)).N;
            Array.Resize(ref slot, n);
            Game bnc = new Game(n);
            bnc.Generate();


            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("-Lets start! Try to guess the number I choose!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new String('#', bnc.N));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            // Main cycle. 
            while (!bnc.Gameover)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Type: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                line = Console.ReadLine();
                if (line == "/answer")
                {
                    Console.WriteLine(bnc.ToString(bnc.ToGuess));
                }
                else
                {
                    try
                    {
                        slot = bnc.CorrectForm(line);
                        if (!bnc.CheckInput(slot))
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
                                AnimatedLine.WriteLine(String.Format("-Congratulations! Game over! You've won in {0} attempts!", bnc.Attempts));
                                AnimatedLine.Write("Correct answer is ");
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                AnimatedLine.WriteLine(bnc.ToString(bnc.ToGuess) + '!');
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine();
                                AnimatedLine.WriteLine("Your attempts:");
                                foreach (int[] item in bnc.Steps)
                                {
                                    Console.WriteLine(bnc.ToString(item));
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
                                AnimatedLine.WriteLine(phrases[rnd.Next() % phrases.Length]);
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
