using System;

namespace MyAdventureGame
{
    /// <summary>
    /// The game engine class.
    /// </summary>
    /// <remarks>
    /// It's the root of everything and the beholder of all components that are needed to run the game.
    /// All magic is performed in the MainLoop method.
    /// </remarks>
    public class Game
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Game()
        {
            // Ensure there's only one game instance. 

            if (Game.Instance != null)
            {
                throw new InvalidOperationException("There's already an instance of the Game object.");
            }
            
            Game.Instance = this;

            // Construct our sub components

            this.Input = new InputManager();
            this.Output = new OutputManager();
            this.Entities = new EntityManager();
        }

        #region Properties

        /// <summary>
        /// Gets the instance of the Game.
        /// </summary>
        /// <value>The instance.</value>
        public static Game Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the input.
        /// </summary>
        /// <value>The input.</value>
        public InputManager Input
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the output.
        /// </summary>
        /// <value>The output.</value>
        public OutputManager Output
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>The entities.</value>
        public EntityManager Entities
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates if the user is religious. Can be enabled/disabled with the godmode command.
        /// </summary>
        /// <value><c>true</c> if god mode enabled; otherwise, <c>false</c>.</value>
        public bool IsGodMode
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Start this instance.
        /// </summary>
        public void Start()
        {
            // Initialize the game engine and their components.

            this.Initialize();

            // Perform magic.

            this.MainLoop();
        }

        private bool doContinue = true;

        /// <summary>
        /// Stop this instance.
        /// </summary>
        public void Stop()
        {
            // Escape the mainloop by flagging the doContinue to false

           this.doContinue = false;
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        private void Initialize()
        {
            this.Input.Initialize();
            this.Entities.Initialize();
        }

        /// <summary>
        /// The thing that keeps it running.
        /// </summary>
        private void MainLoop()
        {
            // Flush any output that was captured during initialization.

            // this.Output.Flush();

            // Loop till sickness ... or till someone tells us to stop.
         
            while (this.doContinue)
            {
                this.Input.Update();
                // this.Output.Flush();
            }

            Game.Instance = null;
        }
    }
}

