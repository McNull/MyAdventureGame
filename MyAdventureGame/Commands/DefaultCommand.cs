using System;

namespace MyAdventureGame
{
    /// <summary>
    /// This is the default command that gets executed if no command has been entered on the input line.
    /// </summary>
    /// <remarks>
    /// This command only displays a helping text and does not autoregister with the InputManager and thus
    /// the command isn't visible and executable from the console.
    /// </remarks>
    public class DefaultCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.DefaultCommand"/> class.
        /// </summary>
        public DefaultCommand()
            : base("defaultcommand")
        {
            // Disable the auto register -- so this command will not be accessible from the console input.

            base.AutoRegister = false;
        }

        #region implemented abstract members of Command

        /// <summary>
        /// Execute the command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <remarks>The execute method should return the resulting output text.</remarks>
        public override string Execute(string[] args)
        {
            return "Enter help for a list of options.";
        }

        /// <summary>
        /// This method should return description text about the command, which is displayed in the console by typing
        /// "help {commandname}".
        /// </summary>
        /// <returns>The help text.</returns>
        public override string GetHelp()
        {
            // Should be impossible to execute

            throw new NotImplementedException();
        }

        #endregion
    }
}

