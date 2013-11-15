using System;
using System.Threading;

namespace MyAdventureGame
{
    /// <summary>
    /// Pauses the output/input for a certain moment.
    /// </summary>
    public class DelayCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.DelayCommand"/> class.
        /// </summary>
        public DelayCommand()
            : base()
        {
            // This is an internal system command -- no need to bug the user with it.
            
            this.IsSystem = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.DelayCommand"/> class.
        /// </summary>
        /// <param name="timeToWait">The time to delay in milliseconds. Minimum value is 50.</param>
        /// <remarks>
        /// This constructor is only called from code -- not from the console.
        /// </remarks>
        public DelayCommand(int timeToWait)
            : this()
        {
            if (timeToWait < 50)
            {
                throw new ArgumentOutOfRangeException("timeToWait");
            }

            this.TimeToWait = timeToWait;
        }

        /// <summary>
        /// Gets or sets the time to wait.
        /// </summary>
        /// <value>The time to wait.</value>
        /// <remarks>
        /// This value is set by the constructor and can be overruled by the console arguments.
        /// </remarks>
        protected int TimeToWait 
        {
            get;
            set;
        }

        #region implemented abstract members of Command

        /// <summary>
        /// This method should return description text about the command, which is displayed in the console by typing
        /// "help {commandname}".
        /// </summary>
        /// <returns>The help text.</returns>
        public override string GetHelp()
        {
            return "Delays the output/input for a certain amount of time.\n" +
                   "The user can abort the pause by pressing a key.\n" +
                    "Usage: delay {milliseconds}";
        }
       
        /// <summary>
        /// Execute the command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments for the command. First argument is always the command name used.</param>
        public override void Execute(string[] args)
        {
            // Figure out how much total time we're going to wait.

            var timeToWait = this.TimeToWait; // Grab initial value from our property.

            if (args.Length > 1) // If there are arguments ...
            {
                // ... try to parse the second one (first one is the command name)

                if(!Int32.TryParse(args[1], out timeToWait))
                {
                    this.Output.WriteFormat("The argument '{0}' is not a number.\n", args[1]);
                    return;
                }
            }

            this.Output.Delay(timeToWait);
        }

        #endregion
    }
}

