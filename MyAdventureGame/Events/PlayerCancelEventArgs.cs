using System;
using System.ComponentModel;

namespace MyAdventureGame
{
    /// <summary>
    /// Player cancel event arguments.
    /// </summary>
    public class PlayerCancelEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.PlayerCancelEventArgs"/> class.
        /// </summary>
        /// <param name="player">Player.</param>
        /// <param name="displayCancelMessage">If set to <c>true</c> display cancel message.</param>
        /// <param name="displaySuccesMessage">If set to <c>true</c> display succes message.</param>
        /// <param name="cancel">If set to <c>true</c> cancel.</param>
        public PlayerCancelEventArgs(Player player = null, bool displayCancelMessage = true, bool displaySuccesMessage = true, bool cancel = false)
            : base(cancel)
        {
            this.Player = player ?? Game.Instance.Entities.Player;
            this.DisplayCancelMessage = displayCancelMessage;
            this.DisplaySuccesMessage = displaySuccesMessage;
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>The player.</value>
        public Player Player
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to display a message this event is cancelled.
        /// </summary>
        /// <value><c>true</c> to display a general message; otherwise, <c>false</c>.</value>
        /// <remarks>
        ///     Set this to false if you've already written why the event was cancelled.
        /// </remarks>
        public bool DisplayCancelMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to display a message when the event was not cancelled.
        /// </summary>
        /// <value><c>true</c> to display succes message; otherwise, <c>false</c>.</value>
        public bool DisplaySuccesMessage
        {
            get;
            set;
        }
    }
}

