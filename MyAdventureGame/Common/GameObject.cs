using System;

namespace MyAdventureGame
{
    /// <summary>
    /// Abstract class that provides some shortcuts to the most used items.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Use this property to write output to the screen.
        /// </summary>
        /// <value>The output.</value>
        protected OutputManager Output
        {
            get { return Game.Instance.Output; }
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>The entities.</value>
        protected EntityManager Entities
        {
            get { return Game.Instance.Entities; }
        }

        /// <summary>
        /// Use this property to access player stats.
        /// </summary>
        /// <value>The player.</value>
        protected Player Player
        {
            get { return this.Entities.Player; }
        }
       
        /// <summary>
        /// Shorthand to get the current room the player is located.
        /// </summary>
        /// <value>The current room.</value>
        protected Room CurrentRoom
        {
            get { return this.Player.CurrentRoom; }
        }

        /// <summary>
        /// Introduces a pause.
        /// </summary>
        /// <param name="timeToWait">Time to wait.</param>
        /// <param name="flushOutput">If set to <c>true</c> the console output buffer will be flushed to the screen.</param>
        public void Delay(int timeToWait)
        //public void Delay(int timeToWait, bool flushOutput = true)
        {
//            if (flushOutput)
//                this.Output.Flush(appendLine: false, trimEnd: false);
//
            var cmd = new DelayCommand(timeToWait);
            cmd.Execute();
        }
    }
}

