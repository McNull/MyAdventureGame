using System;

namespace MyAdventureGame
{
    class MainClass
    {
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            // Lets create a game ...

            var game = new Game();

            // ... and start it

            game.Start();
        }
    }
}
