using System;
using System.Threading;

namespace BullsNCows
{
    /// <summary>
    /// Animaor class
    /// </summary>
    public static class AnimatedLine
    {
        /// <summary>
        /// Animates line with \n in the end. Analogue of Console.WriteLine();
        /// </summary>
        /// <param name="line"> Line to animate. </param>
        /// <param name="ms"> Delay(ms) for each symbol. </param>
        public static void WriteLine(string line, int ms = 7)
        {
            foreach(char value in line)
            {
                Console.Write(value.ToString());
                Thread.Sleep(ms);
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Animates line with \n in the end. Analogue of Console.Write();
        /// </summary>
        /// <param name="line"> Line to animate. </param>
        /// <param name="ms"> Delay(ms) for each symbol. </param>
        public static void Write(string line, int ms = 7)
        {
            foreach (char value in line)
            {
                Console.Write(value.ToString());
                Thread.Sleep(ms);
            }
        }
    }
}
