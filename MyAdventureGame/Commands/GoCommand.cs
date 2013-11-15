using System;
using System.Linq;

namespace MyAdventureGame
{
    /// <summary>
    /// The command that lets the player actually 'go' somewhere. 
    /// </summary>
    public class GoCommand : Command
    {
        #region implemented abstract members of Command

        /// <summary>
        /// This method should return description text about the command, which is displayed in the console by typing
        /// "help {commandname}".
        /// </summary>
        /// <returns>The help text.</returns>
        public override string GetHelp()
        {
            return "Travel your toon to a certain location.\nUsage: go {direction}\nSee the command 'exits' for a list of valid directions for the current location."; 
        }

        // Build a list of available directions from the Direction enum.

        private static Direction[] directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToArray();

        /// <summary>
        /// Execute the command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments for the command. First argument is always the command name used.</param>
        public override void Execute(string[] args)
        {
            // Do we actually have a destination?

            if (args.Length <= 1)
            {
                this.Output.WriteLine("Got nowhere to go.");
                return;
            }

            // Locate the first location that starts with the provided argument

            var direction = GoCommand.directions.FirstOrDefault(x => 
            {
                if(x == Direction.None)
                {
                    return false;
                }

                var str = x.ToString();

                if(!str.StartsWith(args [1], StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }

                return true;
            });

            if (direction == Direction.None)
            {
                // Try to locate an exit (portal) that matches by name.

                direction = this.CurrentRoom.Portals.Where(x => x.Value.IsVisible && x.Value.Name.StartsWith(args[1], StringComparison.InvariantCultureIgnoreCase))
                                                    .Select(x => x.Key)
                                                    .SingleOrDefault();
            }

            // Tell the player object to move to the specified direction

            this.Player.Move(direction);
        }

        #endregion
    }
}

