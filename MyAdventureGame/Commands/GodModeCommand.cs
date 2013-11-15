using System;

namespace MyAdventureGame
{
    /// <summary>
    /// Command that enables some more commands in the console.
    /// </summary>
    public class GodModeCommand : Command
    {
        #region implemented abstract members of Command

        /// <summary>
        /// This method should return description text about the command, which is displayed in the console by typing
        /// "help {commandname}".
        /// </summary>
        /// <returns>The help text.</returns>
        public override string GetHelp()
        {
            return "Toggles developers godmode. Developers generally suck at text adventures; they need this!";
        }

        /// <summary>
        /// Execute the command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments for the command. First argument is always the command name used.</param>
        public override void Execute(string[] args)
        {
            // By using the ! sign before a boolean value (in this case the GodMode property) we invert the value.
            // So true becomes false and false becomes true.

            Game.Instance.IsGodMode = !Game.Instance.IsGodMode;

            // Output a line indicating if godmode is enabled or disabled.

            this.Output.WriteFormat("God mode {0}.\n", (Game.Instance.IsGodMode ? "enabled." : "disabled.")); 
        }

        #endregion
    }
}

