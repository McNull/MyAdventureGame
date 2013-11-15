using System;

namespace MyAdventureGame
{
    public class Box : Container, IOpenableEntity
    {
        public Box(string name, string description, bool isOpen = false)
            : base(name, description)
        {
            this.IsOpen = isOpen;
        
            this.SubItemsEmptyDescription = "It's empty.";
        }

        protected override void OnRenderDescription(RenderDescriptionEventArgs eventArgs)
        {
            if (this.IsOpen)
                base.OnRenderDescription(eventArgs);
            else
                eventArgs.AdditionalText = "It's closed.\n";
        }

        #region IOpenableEntity implementation

        public bool IsOpen
        {
            get;
            private set;
        }

        public void Open(Player player)
        {
            if (this.IsOpen)
                this.Output.Write("It's already open.\n");
            else 
            {
                var eventArgs = new PlayerOpenEntityEventArgs<Box>(this, isOpenEvent: true, player: player);
                this.OnPlayerOpenEntity(eventArgs);

                if(!eventArgs.Cancel)
                {
                    if(eventArgs.DisplaySuccesMessage)
                    {
                        this.Output.Write("Opened.\n");
//                        this.Output.WriteLine();
//                        base.RenderSubItemsDecription();
                    }

                    this.IsOpen = true;
                }
                else
                {
                    if(eventArgs.DisplayCancelMessage)
                    {
                        this.Output.Write("It failed to open.");
                    }
                }
            }
        }

        public void Close(Player player)
        {
            if (!this.IsOpen)
                this.Output.Write("It's already closed.\n");
            else 
            {
                var eventArgs = new PlayerOpenEntityEventArgs<Box>(this, isOpenEvent: false, player: player);
                this.OnPlayerCloseEntity(eventArgs);
                
                if(!eventArgs.Cancel)
                {
                    if(eventArgs.DisplaySuccesMessage)
                    {
                        this.Output.Write("Closed.\n");
                    }
                    
                    this.IsOpen = false;
                }
                else
                {
                    if(eventArgs.DisplayCancelMessage)
                    {
                        this.Output.Write("It failed to close.");
                    }
                }
            }
        }

        #endregion

        protected virtual void OnPlayerOpenEntity(PlayerOpenEntityEventArgs<Box> eventArgs)
        {
            if (this.PlayerOpenEntity != null)
                this.PlayerOpenEntity(this, eventArgs);
        }

        public event EventHandler<PlayerOpenEntityEventArgs<Box>> PlayerOpenEntity; 

        protected virtual void OnPlayerCloseEntity(PlayerOpenEntityEventArgs<Box> eventArgs)
        {
            if (this.PlayerCloseEntity != null)
                this.PlayerCloseEntity(this, eventArgs);
        }
        
        public event EventHandler<PlayerOpenEntityEventArgs<Box>> PlayerCloseEntity;
    }
}

