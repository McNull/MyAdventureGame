using System;

namespace MyAdventureGame
{
    public interface IOpenableEntity
    {
        bool IsOpen
        {
            get;
        }

        void Open(Player player);
        void Close(Player player);
    }
}

