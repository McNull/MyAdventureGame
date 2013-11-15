using System;

namespace MyAdventureGame
{
    /// <summary>
    /// Player enter room validate event arguments.
    /// </summary>
    public class PlayerEnterRoomValidateEventArgs : ValidateEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.PlayerEnterRoomValidateEventArgs"/> class.
        /// </summary>
        /// <param name="room">Room.</param>
        /// <param name="player">Player.</param>
        public PlayerEnterRoomValidateEventArgs(Room room, Player player)
        {
            this.Room = room;
            this.Player = player;
        }

        /// <summary>
        /// Gets the room.
        /// </summary>
        /// <value>The room.</value>
        public Room Room
        {
            get;
            private set;
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
    }
}

