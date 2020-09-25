﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BullsNCows
{
    /// <summary>
    /// Class Game stores game variables and base game functions.
    /// </summary>
    class Game
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Game()
        {
            this.to_guess = new int[4] { -1, -1, -1, -1 };
            this.gameover = false;
            this.attempts = 0;
            this.steps = new List<int[]>();
            this.n = 0;
        }
        private int[] to_guess;
        private bool gameover;
        private uint attempts;
        private List<int[]> steps;
        private int n;
        /// <summary>
        /// Generates random n-digit number(May starts with a 0-digit).
        /// </summary>
        /// <returns> returns integer array with length = n. </returns>
        public int[] Generate()
        {
            List<int> possible_nums = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Random rand = new Random();
            int[] nums = new int[4];
            for (int i = 0; i < 4; ++i)
            {
                int r = rand.Next() % possible_nums.Count;
                nums[i] = possible_nums[r];
                possible_nums.RemoveAt(r);
            }
            this.to_guess = nums;
            return nums;
        }

        /// <summary>
        /// Proves if the input is correct.
        /// </summary>
        /// <param name="input"> Integer array with length = n. </param>
        /// <returns> True if input is correct, otherwise - false. </returns>
        public static bool CheckInput(int[] input)
        {
            if (input.Length == 4)
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (input[i] > 9 || input[i] < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Digits range = [0, 9]!");
                        Console.ForegroundColor = ConsoleColor.White;
                        return false;
                    }
                }
                for (int i = 0; i < 3; ++i)
                {
                    for (int j = i + 1; j < 4; ++j)
                    {
                        if (input[i] == input[j])
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("U should not duplicate digits!");
                            Console.ForegroundColor = ConsoleColor.White;
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Line lenght should be 4 symbols!");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
        }

        /// <summary>
        /// Converts input string to integer array with length = n.
        /// </summary>
        /// <param name="input"> Input string. </param>
        /// <returns> Integer array with length = n. </returns>
        public static int[] CorrectForm(string input)
        {
            int[] result = new int[4] { 0, 0, 0, 0 };
            if (input.Length != 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Line lenght should be 4 symbols!");
                Console.ForegroundColor = ConsoleColor.White;
                throw new Exception("Line lenght should be 4 symbols!");
            }
            for (int i = 0; i < 4; ++i)
            {
                if (!Int32.TryParse(input[i].ToString(), out result[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Are you sure it is digit?");
                    Console.ForegroundColor = ConsoleColor.White;
                    throw new Exception("Are you sure it is digit?");
                }
            }
            return result;

        }

        /// <summary>
        /// Converts integer array with length = n to string.
        /// </summary>
        /// <param name="a"> Integer array with length = n. </param>
        /// <returns> String shows human view of integer array with length = n. </returns>
        public static string ToString(int[] a)
        {
            string result = "";
            for (int i = 0; i < 4; ++i)
            {
                result += a[i].ToString();
            }
            return result;
        }
        /// <summary>
        /// Shows how precisely user chooses the number.
        /// </summary>
        /// <param name="variant"> Integer array with length = n. </param>
        /// <returns> Integer array with length = n with '2' on the places that means Bull and '1' that means Cow. </returns>
        public int[] CheckState(int[] variant)
        {
            int[] result = new int[4] { 0, 0, 0, 0 };
            for (int i = 0; i < 4; ++i)
            {
                if (this.to_guess.Contains(variant[i]))
                {
                    result[i] = 1;
                }
                if (this.to_guess[i] == variant[i])
                {
                    result[i] = 2;
                }
            }
            return result;
        }

        /// <summary>
        /// Human view of CheckState.
        /// </summary>
        /// <param name="to_check"> Integer array with length = n. </param>
        /// <returns> String 'Bulls: num \n Cows: num'. </returns>
        public static string ShowState(int[] to_check)
        {
            int B, K = 0;
            B = to_check.Count(value => value == 2);
            K = to_check.Count(value => value == 1);
            return String.Format("Bulls: {0} \nCows: {1}", B, K);
        }
        /// <summary>
        /// Я устал.
        /// </summary>
        /// <param name="attempt"> </param>
        /// <returns> Писать. </returns>
        public bool Guess(int[] attempt)
        {
            this.attempts++;
            int[] current_state = CheckState(attempt);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ShowState(current_state));
            Console.ForegroundColor = ConsoleColor.White;
            this.steps.Add(attempt);
            if (current_state.Sum() == 8)
            {
                this.gameover = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Господи, хватит.
        /// </summary>
        public static void Tutorial()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("*press enter to see the next step*");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("I choose a secret number (4 digits and there are no duplicate digits) and ask you to guess what the number is. When you make a guess, I provide a hint with the following info:");
            Console.ReadLine();
            Console.WriteLine("    -The number of 'bulls', which are digits in the guess that are in the correct position.");
            Console.ReadLine();
            Console.WriteLine("    -The number of 'cows', which are digits in the guess that are in your secret number but are located in the wrong position. Specifically, the non-bull digits in the guess that could be rearranged such that they become bulls.");
            Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Example: ");
            Console.WriteLine("My number is 3459.");
            Console.WriteLine("You type 3490.");
            Console.ReadLine();
            Console.WriteLine("I give you a hint: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Bulls: 2");
            Console.WriteLine("Cows: 1");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("It means 3, 4 are 'Bulls' and 9 is a 'Cow'.");
            Console.ReadLine();
            Console.WriteLine("Good luck!");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Если ты лох, то напиши /answer вместо своего предположения");
            Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }


        /// <summary>
        /// АААААААААААААААА
        /// </summary>
        public uint Attempts
        {
            get
            {
                return this.attempts;
            }
            set
            {
                this.attempts = value;
            }
        }

        /// <summary>
        /// ААААААААААААААААААААААААААААААААААААА
        /// </summary>
        public int[] ToGuess
        {
            get
            {
                return this.to_guess;
            }
            set
            {
                try
                {
                    if (value.Length == 4 && value is int[])
                    {
                        for (int i = 0; i < 4; ++i)
                        {
                            this.to_guess[i] = value[i];
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Sad");
                }
            }
        }

        /// <summary>
        /// ааааааааааааааааааааааааааааааааааааа
        /// </summary>
        public bool Gameover
        {
            get
            {
                return this.gameover;
            }
            set
            {
                this.gameover = value;
            }
        }


        /// <summary>
        /// *crying*
        /// </summary>
        public List<int[]> Steps
        {
            get
            {
                return this.steps;
            }
        }
    }
}
