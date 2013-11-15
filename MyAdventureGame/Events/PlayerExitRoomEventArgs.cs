using System;

namespace MyAdventureGame
{
    /// <summary>
    /// Player exit room event arguments.
    /// </summary>
    public class PlayerExitRoomEventArgs : PlayerCancelEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.PlayerExitRoomEventArgs"/> class.
        /// </summary>
        /// <param name="room">Room.</param>
        /// <param name="player">Player.</param>
        /// <param name="direction">Direction.</param>
        public PlayerExitRoomEventArgs(Player player, Room room, Direction direction)
            : base(player)
        {
            this.Room = room;
            this.Direction = direction;
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
        /// Gets the direction.
        /// </summary>
        /// <value>The direction.</value>
        public Direction Direction
        {
            get;
            private set;
        }
    }
}

