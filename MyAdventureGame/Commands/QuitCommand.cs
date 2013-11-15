using System;

namespace MyAdventureGame
{
    /// <summary>
    /// Quit command that .... erhm .... quits the game.
    /// </summary>
    public class QuitCommand : Command
    {
        #region implemented abstract members of Command

        /// <summary>
        /// Execute the command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments for the command. First argument is always the command name used.</param>
        public override void Execute(string[] args)
        {
            Game.Instance.Stop();
        }

        /// <summary>
        /// This method should return description text about the command, which is displayed in the console by typing
        /// "help {commandname}".
        /// </summary>
        /// <returns>The help text.</returns>
        public override string GetHelp()
        {
            return "Quits the game.";
        }

        #endregion
    }
}

