using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAdventureGame
{
    /// <summary>
    /// The command that dispays all possible exits to the user.
    /// </summary>
    public class ExitsCommand : Command
    {
        #region implemented abstract members of Command

        /// <summary>
        /// This method should return description text about the command, which is displayed in the console by typing
        /// "help {commandname}".
        /// </summary>
        /// <returns>The help text.</returns>
        public override string GetHelp()
        {
            return "Displays all the possible exits of the current location.";
        }

        /// <summary>
        /// Execute the command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments for the command. First argument is always the command name used.</param>
        public override void Execute(string[] args)
        {
            // Build a list of all the portals in the current room that are enabled.

            var validExits = this.CurrentRoom.Portals
                                             .Where(x => x.Value.IsVisible)
                                             .ToList();

            // Do we actually have exits?

            if (validExits.Count == 0)
            {
                this.Output.Write("No exits found.\n");
                return;
            }

            // Write the exits to the screen by using the ForEach method of List

            this.Output.WriteLine("The following exits are available:");
            validExits.ForEach(x => this.Output.WriteFormat("{0} => {1}\n", x.Key, x.Value.Room.Name));
        }

        #endregion
    }
}

