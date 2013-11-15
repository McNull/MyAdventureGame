using System;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MyAdventureGame
{
    /// <summary>
    /// Our player entity -- the one that will die -- alot!
    /// </summary>
    public class Player : Entity
    {
        public Player(Room initialRoom)
        {
            this.CurrentRoom = initialRoom;
        }

        public void Initialize()
        {
            this.Inventory = new Container("Inventory");

            if (!this.CurrentRoom.Enter(player: this))
            {
                throw new InvalidOperationException("Player failed to enter initial room.");
            }
        }


        /// <summary>
        /// Gets or sets the current room.
        /// </summary>
        /// <value>The current room.</value>
        public new Room CurrentRoom
        {
            get;
            private set;
        }

        public Container Inventory
        {
            get;
            private set;
        }
       
        /// <summary>
        /// Move to the specified direction.
        /// </summary>
        /// <param name="direction">Direction.</param>
        public void Move(Direction direction)
        {
            var result = this.CurrentRoom.Exit(this, direction);

            if (result != null)
                this.CurrentRoom = result;
        }

        /// <summary>
        /// Pickup the specified entity.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public void Pickup(Entity entity)
        {

        }
    }


}

