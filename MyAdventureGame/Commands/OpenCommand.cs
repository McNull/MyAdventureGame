using System;
using System.Linq;

namespace MyAdventureGame
{
    /// <summary>
    /// Command to open items.
    /// </summary>
    public class OpenCommand : Command
    {
        #region implemented abstract members of Command

        /// <summary>
        /// This method should return description text about the command, which is displayed in the console by typing
        /// "help {commandname}".
        /// </summary>
        /// <returns>The help text.</returns>
        public override string GetHelp()
        {
            return "Opens the specified item.";
        }

        /// <summary>
        /// Execute the command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments for the command. First argument is always the command name used.</param>
        public override void Execute(string[] args)
        {
            if (args.Length <= 1)
            {
                this.Output.Write("Open what?\n");
                return;
            }

            var selector = args.Skip(1).ToArray();
            var item = this.CurrentRoom.LocateEntity(selector);

            if (item != null)
            {
                var openableItem = item as IOpenableEntity;

                if(openableItem == null)
                {
                    this.Output.Write("That cannot be opened.\n");
                }
                else
                {
                    openableItem.Open(this.Player);
                }
            }
        }

        #endregion
    }
}

