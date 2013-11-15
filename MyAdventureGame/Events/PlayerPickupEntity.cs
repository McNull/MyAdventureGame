using System;

namespace MyAdventureGame
{
    public class PlayerPickupEntity : PlayerCancelEventArgs
    {
        public PlayerPickupEntity(Player player, Entity item, bool displayCancelMessage = true, bool displaySuccesMessage = true, bool cancel = false)
            : base(player, displayCancelMessage, displaySuccesMessage, cancel)
        {
            this.Entity = item;
        }

        Entity Entity
        {
            get;
            set;
        }
    }
}

