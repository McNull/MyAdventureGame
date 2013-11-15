using System;

namespace MyAdventureGame
{
    /// <summary>
    /// Player enter room event arguments.
    /// </summary>
    public class PlayerEnterRoomEventArgs : PlayerCancelEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.PlayerEnterRoomEventArgs"/> class.
        /// </summary>
        /// <param name="room">Room.</param>
        /// <param name="player">Player.</param>
        public PlayerEnterRoomEventArgs(Player player, Room room)
            : base(player)
        {
            this.Room = room;
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
    }
}

