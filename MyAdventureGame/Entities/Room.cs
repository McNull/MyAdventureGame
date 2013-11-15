using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAdventureGame
{
    /// <summary>
    /// The room is a container class that has connections to other rooms (via portals).
    /// </summary>
    /// <remarks>
    /// The world is build out of virtual rooms. These rooms don't have to be physical rooms as we know them but can be segmented areas within the playground.
    /// </remarks>
    public class Room : Container
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.Room"/> class.
        /// </summary>
        public Room(string name = null, string description = null)
            : base(name, description)
        {
            // Use a custom header text when displaying containing items.

            base.SubItemsDescriptionHeader = "You see the following items of possible interest: ";
        }

        #region Portals

        private Dictionary<Direction, Portal> portals = new Dictionary<Direction, Portal>();
        /// <summary>
        /// Gets the connections to other rooms.
        /// </summary>
        /// <value>The connections.</value>
        public IEnumerable<KeyValuePair<Direction, Portal>> Portals
        {
            get { return this.portals; }
        }

        /// <summary>
        /// Gets the portal for the specified direction.
        /// </summary>
        /// <returns>The portal or null if no portal for that direction exists.</returns>
        /// <param name="direction">Direction.</param>
        /// <remarks>
        ///     Note that a portal can be disabled. This method does not check that.
        /// </remarks>
        public Portal GetPortal(Direction direction)
        {
            var portal = this.Portals
                             .Where(x => x.Key == direction)
                             .Select(x => x.Value)
                             .SingleOrDefault();

            return portal;
        }

        /// <summary>
        /// Sets the portal for the specified direction.
        /// </summary>
        /// <param name="direction">Direction.</param>
        /// <param name="portal">Portal.</param>
        public void SetPortal(Direction direction, Portal portal)
        {
            if (direction == Direction.None)
                throw new ArgumentException("Must have valid direction", "direction");

            if (this.portals.ContainsKey(direction))
                throw new InvalidOperationException("There is already a portal set for the direction " + direction + ".");

            this.portals [direction] = portal;
        }

        /// <summary>
        /// Removes the portal.
        /// </summary>
        /// <returns><c>true</c>, if portal was removed, <c>false</c> otherwise.</returns>
        /// <param name="direction">Direction.</param>
        public bool RemovePortal(Direction direction)
        {
            return this.portals.Remove(direction);
        }

        #endregion


//        /// <summary>
//        /// Adds the connection (portal) between two rooms.
//        /// </summary>
//        /// <param name="direction">The direction of the portal.</param>
//        /// <param name="otherRoom">The other room to connect to.</param>
//        /// <param name="isVisible">If set to <c>true</c> the exit is visible to the user.</param>
//        public void AddConnection(Direction direction, Room otherRoom, bool isVisible = true)
//        {
//            var mirrorDirection = direction.Mirror();
//
//            if(this.portals.ContainsKey(direction))
//            {
//                string message = string.Format("The room {0} already contains a connection on the {1} side.");
//                throw new InvalidOperationException(message);
//            }
//
//            if(otherRoom.portals.ContainsKey(mirrorDirection))
//            {
//                var conflictingRoom = otherRoom.portals[mirrorDirection].Room;
//                string message = string.Format("Can't connect the room {0}.{1} to room {2}.{3}.\nThe room {2} already contains a connection on {3} to {4}.",
//                                               this.Name, direction, otherRoom.Name, mirrorDirection, conflictingRoom.Name);
//                throw new InvalidOperationException(message);
//            }
//
//            var localPortal = new Portal(otherRoom);
//            this.portals [direction] = localPortal;
//
//            var remotePortal = new Portal(this);
//            otherRoom.portals [mirrorDirection] = remotePortal;
//        }

        #region Player Enter

        /// <summary>
        /// Fires off all the events when the player enters the room.
        /// </summary>
        /// <param name="player">The player.</param>
        public bool Enter(Player player)
        {
            // Construct the event arguments

            var eventArgs = new PlayerEnterRoomEventArgs(player, this);

            this.OnPlayerEnterRoom(eventArgs);

            if (eventArgs.Cancel)
            {
                if(eventArgs.DisplayCancelMessage)
                {
                    this.Output.WriteFormat("You cannot enter the destination area.\n");
                }
            }
            else if (eventArgs.DisplaySuccesMessage)
            {
                var look = new LookCommand(this);
                look.Execute();
            }

            return !eventArgs.Cancel;
        }

        /// <summary>
        /// Raises the player enter room event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnPlayerEnterRoom(PlayerEnterRoomEventArgs eventArgs)
        {
            if (this.PlayerEnterRoom != null) this.PlayerEnterRoom(this, eventArgs);
        }

        /// <summary>
        /// Occurs when player enter room.
        /// </summary>
        public event EventHandler<PlayerEnterRoomEventArgs> PlayerEnterRoom;

        #endregion

        #region Player Exit

        /// <summary>
        /// Exit the specified player to direction.
        /// </summary>
        /// <param name="player">Player.</param>
        /// <param name="direction">Direction.</param>
        /// <remarks>
        ///     Maybe there's a big guy standing in the way. 
        ///     Maybe there's a large nail sticking the left foot of the player to the ground.
        /// </remarks>
        public Room Exit(Player player, Direction direction)
        {
            var eventArgs = new PlayerExitRoomEventArgs(player, this, direction);

            this.OnPlayerExitRoom(eventArgs);

            var displayCancelMessage = eventArgs.DisplayCancelMessage;

            if (!eventArgs.Cancel)
            {
                // Ensure cancel message

                displayCancelMessage = true;

                // Try to locate a portal for the direction.

                var portal = this.GetPortal(direction);

                if(portal != null)
                {
                    // The portal will add cancel messages
                    
                    displayCancelMessage = false;

                    if(portal.UsePortal(player))
                    {
                        if (eventArgs.DisplaySuccesMessage)
                        {
                            var msg = string.Format("You go {0}.\n\n", direction.ToString().ToLower());
                            this.Output.WriteFormat(msg);
                        }

                        var targetRoom = portal.Room;

                        if(targetRoom != null && targetRoom.Enter(player))
                        {
                            return targetRoom;
                        }
                    }
                }
            }

            if(displayCancelMessage)
            {
                this.Output.WriteFormat("You cannot go that way.\n\n");
            }
            
            return null;
        }

        /// <summary>
        /// Raises the player exit room event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnPlayerExitRoom(PlayerExitRoomEventArgs eventArgs)
        {
            if (this.PlayerExitRoom != null) 
                this.PlayerExitRoom(this, eventArgs);
        }

        /// <summary>
        /// Occurs when player exits the room.
        /// </summary>
        public event EventHandler<PlayerExitRoomEventArgs> PlayerExitRoom;
        
        #endregion
    }
}

