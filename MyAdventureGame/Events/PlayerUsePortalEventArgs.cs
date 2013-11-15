using System;

namespace MyAdventureGame
{
    public class PlayerUsePortalEventArgs : PlayerCancelEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.PlayerUsePortalEventArgs"/> class.
        /// </summary>
        /// <param name="player">Player.</param>
        /// <param name="portal">Portal.</param>
        public PlayerUsePortalEventArgs(Player player, Portal portal)
            : base(player, displaySuccesMessage: false)
        {
            this.Portal = portal;
        }

        /// <summary>
        /// Gets the portal.
        /// </summary>
        /// <value>The portal.</value>
        public Portal Portal
        {
            get;
            private set;
        }
    }
}

