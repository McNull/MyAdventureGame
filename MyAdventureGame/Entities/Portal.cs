using System;

namespace MyAdventureGame
{
    public class Portal : Entity
    {
        public Portal(Room room, string name = null, string description = null, string id = null)
            : base(name, description, id)
        {
            if (room == null)
                throw new ArgumentNullException("room");

            this.Room = room;
        }
       
        public Room Room
        {
            get;
            private set;
        }

        public bool UsePortal(Player player)
        {
            var eventArgs = new PlayerUsePortalEventArgs(player, this);

            this.OnPlayerUsePortal(eventArgs);

            return true;
        }

        protected virtual void OnPlayerUsePortal(PlayerUsePortalEventArgs eventArgs)
        {
            if (this.PlayerUsePortal != null)
                this.PlayerUsePortal(this, eventArgs);
        }

        public event EventHandler<PlayerUsePortalEventArgs> PlayerUsePortal;
    }
}

