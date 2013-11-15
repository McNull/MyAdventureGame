using System;

namespace MyAdventureGame
{
    /// <summary>
    /// Player exit room validate event arguments.
    /// </summary>
    public class PlayerExitRoomValidateEventArgs : ValidateEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.PlayerExitRoomValidateEventArgs"/> class.
        /// </summary>
        /// <param name="room">Room.</param>
        /// <param name="player">Player.</param>
        /// <param name="direction">Direction.</param>
        public PlayerExitRoomValidateEventArgs(Room room, Player player, Direction direction)
        {
            this.Room = room;
            this.Player = player;
            this.Direction = direction;
            this.DisplayValidationFailedMessage = true;
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

        /// <summary>
        /// Gets the directorion.
        /// </summary>
        /// <value>The directorion.</value>
        public Direction Direction
        {
            get;
            private set;
        }
    }
}

