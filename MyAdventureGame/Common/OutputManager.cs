using System;
using System.Text;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace MyAdventureGame
{
    /// <summary>
    /// The output manager is used to collect all text output and display it once per gameloop on the screen.
    /// </summary>
    public class OutputManager
    {
        /// <summary>
        /// The randomizer we'll use for .... random things.
        /// </summary>
        private Random rnd = new Random();

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="pause">Pause.</param>
        public void WriteLine(int pause = 0)
        {
            Console.WriteLine();
            this.Delay(pause);
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="pause">Pause.</param>
        public void WriteLine(string text, int pause = 0)
        {
            Console.WriteLine(text);
            this.Delay(pause);
        }

        /// <summary>
        /// Write the specified text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="pause">Pause.</param>
        public void Write(string text, int pause = 0)
        {
            Console.Write(text);
            this.Delay(pause);
        }

        /// <summary>
        /// Format writes the provided text.
        /// </summary>
        /// <param name="pause">Pause.</param>
        /// <param name="text">Text.</param>
        /// <param name="args">Arguments.</param>
        public void WriteFormat(int pause, string text, params object[] args)
        {
            this.WriteFormat(text, args);
            this.Delay(pause);
        }

        /// <summary>
        /// Format writes the text.
        /// </summary>
        /// <param name="format">Format.</param>
        /// <param name="args">Arguments.</param>
        public void WriteFormat(string format, params object[] args)
        {
            format = string.Format(format, args);
            Console.Write(format);
        }

        /// <summary>
        /// Pauses the output for a certain time.
        /// </summary>
        /// <param name="timeToWait">Time to wait.</param>
        public void Delay(int timeToWait)
        {
            // Now for the waiting game.
            // We let the current thread do small sleeps (power naps) in a loop and
            // stop the loop if all the power nap time is greater than the total wait time.
            // We also allow the user to skip the pause, which we do by checking
            // the boolean value Console.KeyAvailable. If this value is true then there is 
            // a keystroke in the input buffer.
            
            while (timeToWait > 0 && !Console.KeyAvailable)
            {
                Thread.Sleep(50);
                timeToWait -= 50;
            }
            
            // If our pause got interrupted by a keystroke we probably want to grab it,
            // otherwise the next prompt line has some invisible characters.
            
            while (Console.KeyAvailable)
            {
                // We read the key (but don't do anything with the return value)
                // The intercept argument tells the console that it should not be displayed.
                
                Console.ReadKey(intercept: true);
            }
        }

        /// <summary>
        /// Writes the provided text character by character.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="speed">The time in ms to wait between the characters.</param>
        /// <param name="random">The random speed variation.</param>
        /// <param name="pause">Pause the output/input after writing the line of text.</param> 
        public void TypeWrite(string text, int speed = 50, int random = 30, int pause = 0, bool makeTypos = true)
        {
            // Disable typos if the text contains '\b' and we're not typing at full speed.

            makeTypos = makeTypos && speed <= 50 && text.Contains("\b") == false;

            // Ensure random value is alwaus smaller than speed.
           
            random = Math.Min(speed, random);

            // Convert the string to a list so we can adjust it on the fly.

            var charList = text.ToList();

            for (int i = 0; i < charList.Count; i++)
            {
                if(Console.KeyAvailable)
                {
                    var remainder = new string(charList.ToArray()).Substring(i);
                    Console.Write(remainder);
                    break; // This will abort the for loop
                }

                // Every 40 characters there's a change a typo occurs.

                if(makeTypos && rnd.Next() % 40 == 0)
                    this.MakeTypo(charList, i);

                Console.Write(charList[i]);

                int timeToWait = this.rnd.Next(-1 * random, random) + speed;

                Thread.Sleep(timeToWait);
            }

            if (pause > 0)
            {
                this.Delay(pause);
            }

            // If our pause got interrupted by a keystroke we probably want to grab it,
            // otherwise the next prompt line has some invisible characters.
            
            while (Console.KeyAvailable)
            {
                // We read the key (but don't do anything with the return value)
                // The intercept argument tells the console that it should not be displayed.
                
                Console.ReadKey(intercept: true);
            }
        }

        private string qwertyTypoKeys = "qwertyuiop[asdfghjkl;zxcvbnm,";

        private void MakeTypo(List<char> text, int i)
        {
            var c = text [i];

            // We only do typos on letters

            if (Char.IsLetter(c))
            {
                // Locate the first location in the text that is not a letter.
                // This way we can insert a typo, compleet the rest of the word and then
                // return (with backspaces) to the virtual mistake to correct it. 
                // This looks pretty realistic.

                var n = text.FindIndex(i, x => !Char.IsLetter(x));
                
                if(n == -1) // End of line
                    n = text.Count;
                
                var l = n - i; // Calc count
                
                var sb = new StringBuilder(l * 2); // Reserve buffer
                
                sb.Append('\b', l); //  Append the correcting backspaces
                
                for(int x = 0; x < l; x++)
                {
                    sb.Append(text[i+x]); // Append the correct text
                }
                
                text.InsertRange(n, sb.ToString()); // Insert the correcting text at the location (n)

                // Use the lookup array to locate a character that is close to the original on the keyboard.

                char typo = this.qwertyTypoKeys.SkipWhile(x => x != Char.ToLower(c)).Take(2).Last();

                if(Char.IsUpper(c))
                    typo = Char.ToUpper(typo);

                text[i] = typo;
            }

        }
    }
}

