using System;
using System.Linq;
using System.Text;

namespace MyAdventureGame
{
    /// <summary>
    /// This is the help command class which the user can use to get help about commands.
    /// </summary>
    /// <remarks>
    /// When no arguments are given to the help command than all the available commands are displayed.
    /// If an argument is given than the help text associated with that command is displayed.
    /// </remarks>
    public class HelpCommand : Command
    {
        #region implemented abstract members of Command

        /// <summary>
        /// Execute the command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments for the command. First argument is always the command name used.</param>
        public override void Execute(string[] args)
        {
            // Did we get an argument?
            // note: 1st argument is the name of the command (help) and is always available. So if the user
            // specified an argument the array should be longer than 1.

            if (args.Length > 1)
            {
                // Locate the associated command by the provided name

                var cmd = Game.Instance.Input.GetCommand(args[1]);

                // Did we find the argument?

                if(cmd == null)
                {
                    this.Output.WriteFormat("Unknown command '{0}'.", args[1]);
                    return;
                }

                // Ask the command to provide the help text and dump it into the output.

                var helptext = cmd.GetHelp();
                this.Output.WriteFormat(helptext);
            } 
            else
            {
                // We didn't get any argument -- so lets dump all available commands to the user
                // Build an ordered list of command names.

                var commands = Game.Instance
                                   .Input
                                   .Commands
                                   .OrderBy(x => x.Name);
                                   
                this.Output.Write("Available commands are:\n\n");

                // Loop through all the commands in the list and dump them to the screen.

                foreach (var command in commands)
                {
                    // We only display system commands if godmode is enabled.

                    if(command.IsSystem)
                    {
                        if(Game.Instance.IsGodMode) // Only visible in godmode.
                        {
                            // Output an extra ! to indicate that this is a system command

                            this.Output.WriteFormat("{0} (!)\n", command.Name);
                        }
                    }
                    else
                    {
                        this.Output.WriteLine(command.Name);
                    }
                }

                this.Output.WriteLine("\nEnter help {command name} to get command specific help.");
            }
        }
       
        /// <summary>
        /// This method should return description text about the command, which is displayed in the console by typing
        /// "help {commandname}".
        /// </summary>
        /// <returns>The help text.</returns>
        public override string GetHelp()
        {
            var array = new string[] { "You need help with help?", 
                                       "Help with help?", 
                                       "Just type help.", 
                                       "Serious?", 
                                       "I have some issues with sarcasm ...", 
                                       "You really do need help.", 
                                       "... and there I thought I was stupid.",
                                       "I'm with stupid.",
                                       "You're lost.",
                                       "You have lost me -- you want to do what?!",
                                       "You poor thing.",
                                       "Please type quit."
            };

            return array.SelectRandom();
        }

        #endregion
    }
}

