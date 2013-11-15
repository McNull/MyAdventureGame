using System;
using System.Linq;

namespace MyAdventureGame
{
    /// <summary>
    /// Command that allows the user to look around or examine an object.
    /// </summary>
    public class LookCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.LookCommand"/> class.
        /// </summary>
        public LookCommand()
            : base()
        {
            // Ensures we have a default constructor.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.LookCommand"/> class.
        /// </summary>
        /// <param name="lookAt">Look at.</param>
        public LookCommand(Entity lookAt)
            : this()
        {
            this.LookAt = lookAt;
        }

        /// <summary>
        /// Gets the look at.
        /// </summary>
        /// <value>The look at.</value>
        protected Entity LookAt
        {
            get;
            private set;
        }

        #region implemented abstract members of Command

        /// <summary>
        /// This method should return description text about the command, which is displayed in the console by typing
        /// "help {commandname}".
        /// </summary>
        /// <returns>The help text.</returns>
        public override string GetHelp()
        {
            return "Look or examine an item or surroundings.\n" +
                   "Usage: look [container-name] [item-name]\n";
        }

        /// <summary>
        /// Execute the command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments for the command. First argument is always the command name used.</param>
        public override void Execute(string[] args)
        {
            // If no arguments are given we'll use the current room 

            Entity item = this.LookAt ?? this.CurrentRoom;

            // Locate the item based on the arguments given

            if (args.Length > 1)
            {
                var selector = args.Skip(1).ToArray();
                item = this.CurrentRoom.LocateEntity(selector);
            }

            if (item != null)
            {
                item.DisplayDescription();
            }
        }

        #endregion
    }
}

