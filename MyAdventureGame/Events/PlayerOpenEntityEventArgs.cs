using System;

namespace MyAdventureGame
{
    public class PlayerOpenEntityEventArgs<TEntity> : PlayerCancelEventArgs
        where TEntity : Entity, IOpenableEntity
    {
        public PlayerOpenEntityEventArgs(TEntity entity, bool isOpenEvent, Player player = null, bool displayCancelMessage = true, bool displaySuccesMessage = true, bool cancel = false)
            : base(player: player, displayCancelMessage: displayCancelMessage, displaySuccesMessage: displaySuccesMessage, cancel: cancel)
        {
            this.Entity = entity;
            this.IsOpenEvent = isOpenEvent;
        }

        public TEntity Entity
        {
            get;
            private set;
        }

        public bool IsOpenEvent
        {
            get;
            private set;
        }
    }
}

