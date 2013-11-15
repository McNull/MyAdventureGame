using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;

namespace MyAdventureGame
{
    /// <summary>
    /// The input manager handles input from the user and provides the available commands.
    /// </summary>
    /// <remarks>
    /// When the input manager is initializing it will search the current assembly (that is 
    /// this console application) for all classes that inherit from the class Command and
    /// register them in the game, so they are available from the console input for the user.
    /// 
    /// The game mainloop will call the Prompt method each cycle. This method shall read 
    /// the input from the user and translate it to actual command objects and arguments. 
    /// Both the command and arguments are packaged into a InputResult and provided back
    /// to the mainloop, which executes and displays the result to the user.
    /// </remarks>
    public class InputManager
    {
        private Dictionary<string, Command> commands = new Dictionary<string, Command>();
        /// <summary>
        /// Gets list of registered commands.
        /// </summary>
        /// <value>The commands.</value>
        /// <remarks>
        /// The private dictionary object is down casted implicitely by returning an IEnumarable 
        /// interface. This is done so noone except the InputManager itself can add or remove commands
        /// available to the user.
        /// </remarks>
        public IEnumerable<Command> Commands
        {
            get { return this.commands.Values; }
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        /// <remarks>
        /// This method is called once during application start via the Game instance.
        /// </remarks>
        public void Initialize()
        {
            // Locate all classes within this assembly, that are not abstract and inherit from 
            // the class Command.

            var types = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .Where(x => !x.IsAbstract)
                                .Where(x => typeof(Command).IsAssignableFrom(x));

            // Now loop through all the located types.

            foreach (var type in types)
            {
                // Use the Activator (from the .NET Reflection namespace) to 
                // create an instance of the class.

                var instance = Activator.CreateInstance(type) as Command;

                // If the activation was succesful and the instance has marked himself
                // as auto registering ...

                if(instance != null && instance.AutoRegister)
                {
                    // Check if we've already registered a command with the same name.

                    var existingCommand = this.GetCommand(instance.Name);

                    if(existingCommand != null)
                    {
                        string message = string.Format("A command with a duplicate key tried to register.\nExisting type: {0}.\nNew type: {1}.", 
                                                       existingCommand.GetType().Name, type.Name);
                        
                        throw new InvalidOperationException(message);
                    }

                    // ... add the command to the list.

                    this.commands.Add(instance.Name, instance);
                }
            }
        }

        /// <summary>
        /// Locates a command by the specified name.
        /// </summary>
        /// <returns>null if the command could not be found, otherwise an instance of the command will be returned.</returns>
        /// <param name="name">The name of the command.</param>
        public Command GetCommand(string name)
        {
            // Does the dictionary contain the key?

            if (this.commands.ContainsKey(name))
            {
                // If so ... return that instance.

                return this.commands[name];
            }

            // Not found ... return null.

            return null;
        }

        /// <summary>
        /// Displays and handles the input prompt for the user.
        /// </summary>
        /// <remarks>
        /// This is called once per game loop.
        /// </remarks>
        public void Update()
        {
           
            // Ensure we've got something to return to the caller.

            Command cmd = new MessageCommand("Enter 'help' for a list of available commands.");

            // Display our luvly prompt to the user.

            var prompt = string.Format("\n{0}: ", Game.Instance.Entities.Player.CurrentRoom.Name);

            Console.Write(prompt);

            // Read input from the user.

            var inputLine = Console.ReadLine();
            Console.WriteLine();

            // Try to parse (tokenize) the line entered by the user.

            List<string> tokens = this.Tokenize(inputLine);

            // If we've got anything in return ...

            if (tokens != null && tokens.Count > 0)
            {
                // The first token is always the command name

                var commandName = tokens.First();

                // Try to locate the command.

                if(this.commands.ContainsKey(commandName))
                {
                    cmd = this.commands[commandName];
                }
                else
                {
                    cmd = this.commands.Where(x => x.Key.StartsWith(commandName))
                                         .Select(x => x.Value)
                                         .FirstOrDefault();

                    if(cmd == null)
                    {
                        // Command does not exist

                        var msg = string.Format("Unknown command '{0}'.", commandName);
                        cmd = new MessageCommand(msg);
                    }
                }
            }

            // Execute the command.

            cmd.Execute(tokens.ToArray());
        }
       

        private Regex inputRegularExpression = new Regex(@"[\w-]+|""[\w\s-]*""");
        /// <summary>
        /// Tokenize the specified inputLine.
        /// </summary>
        /// <param name="inputLine">Input line.</param>
        private List<string> Tokenize(string inputLine)
        {
            List<string> tokens = new List<string>();

            if (!string.IsNullOrWhiteSpace(inputLine))
            {
                tokens = this.inputRegularExpression
                             .Matches(inputLine)
                             .Cast<Match>()
                             .Select(m => m.Value.Trim('\"'))
                             .ToList();
            }

            return tokens;
        }
    }
}

