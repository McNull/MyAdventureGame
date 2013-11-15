using System;

namespace MyAdventureGame
{
    /// <summary>
    /// The abstract command class. Use this class to (auto) register console commands in the InputManager.
    /// </summary>
    /// <remarks>
    /// Classes that inherit from the Command class are automatically registered in the commands collection of
    /// the InputManager. 
    /// Important: If implementation classes don't specify a name, the class name should end in "Command".
    /// </remarks>
    public abstract class Command : GameObject
    {
        /// <summary>
        /// Constructor of the Command class.
        /// </summary>
        /// <param name="name">Optional command name to use.</param>
        /// <remarks>
        /// The name of the class is used as command name if no name is specified.
        /// </remarks>
        public Command(string name = null)
        {
            // Check if a name value has been provided

            if (string.IsNullOrWhiteSpace(name))
            {
                // Grab the name of the class

                name = this.GetType().Name;

                // Make sure it ends with "Command"

                if(!name.EndsWith("Command"))
                {
                    string message = string.Format("Invalid Command class name {0}.", this.GetType().Name);
                    throw new InvalidOperationException(message);
                }

                // Scrape the name without the ending "Command"

                name = name.Substring(0, name.Length - "Command".Length);
            }

            // Always make sure the name is lowercase.

            this.Name = name.ToLower();

            // Enable the auto register (with the InputManager) by default

            this.AutoRegister = true;
        }

        /// <summary>
        /// This method should return description text about the command, which is displayed in the console by typing "help {commandname}".
        /// </summary>
        /// <returns>The help text.</returns>
        public abstract string GetHelp();

        /// <summary>
        /// Execute this instance.
        /// </summary>
        public void Execute() 
        {
            this.Execute(new string[] { this.Name } );
        }

        /// <summary>
        /// Execute the command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments for the command. First argument is always the command name used.</param>
        public abstract void Execute(string[] args);

        /// <summary>
        /// Gets or sets a value indicating whether this class should be auto registered with the InputManager.
        /// </summary>
        /// <value><c>true</c> to auto register; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// Default value is true.
        /// </remarks>
        public bool AutoRegister
        {
            get; protected set;
        }

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates if the command is a system command (and not visible for the normal user).
        /// </summary>
        /// <value><c>true</c> if this instance is a system command; otherwise, <c>false</c>.</value>
        public bool IsSystem
        {
            get;
            set;
        }
    }

    /*
    public class LambdaCommand : CommandBase
    {
        public LambdaCommand(Func<string> expression)
            : this((x) => expression())
        {
        }

        public LambdaCommand(Func<string[], string> expression)
        {
            this.Expression = expression;
        }

        private Func<string[], string> Expression
        {
            get; set;
        }

        #region implemented abstract members of Command

        public override string Execute(string[] args)
        {
            return this.Expression(args);
        }

        #endregion
    }*/
}

