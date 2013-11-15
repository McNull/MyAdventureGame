using System;
using System.Text;

namespace MyAdventureGame
{
    /// <summary>
    /// Message command that can be used to display text messages to the user.
    /// </summary>
    public class MessageCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.MessageCommand"/> class.
        /// </summary>
        public MessageCommand()
            : base("message")
        {
            // This is an internal system command -- no need to bug the user with it.

            this.IsSystem = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.MessageCommand"/> class.
        /// </summary>
        /// <param name="text">The text to display.</param>
        public MessageCommand(string text)
            : this()
        {
            this.text = text;   
        }

        private string text;

        #region implemented abstract members of Command

        /// <summary>
        /// This method should return description text about the command, which is displayed in the console by typing
        /// "help {commandname}".
        /// </summary>
        /// <returns>The help text.</returns>
        public override string GetHelp()
        {
            return "Displays a message.\n" + 
                   @"Usage: message ""{text to display}""";
        }

        /// <summary>
        /// Execute the command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments for the command. First argument is always the command name used.</param>
        public override void Execute(string[] args)
        {
            if (this.text != null)
            {
                this.Output.WriteLine(this.text);
            } 
            else
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 1; i < args.Length; i++)
                {
                    sb.Append(args [i]);
                    sb.Append(" ");
                }

                sb.Length -= 1;

                this.Output.WriteLine(sb.ToString());
            }
        }

        #endregion
    }
}

