using System;
using System.Collections.Generic;

namespace MyAdventureGame
{
    /// <summary>
    /// Abstract class that ensures that inherited rooms only have a single instance of them.
    /// </summary>
    public abstract class SingletonRoom : Room
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.SingletonRoom`1"/> class.
        /// </summary>
        protected SingletonRoom()
            : base()
        {
            var t = this.GetType();

            var instance = SingletonRoom.GetInstance(t);

            if (instance != null)
            {
                string msg = string.Format("The room type '{0}' already exists.", t.Name);
                throw new InvalidOperationException(msg);
            }

            SingletonRoom.instances [t] = this;
        }



        /// <summary>
        /// Do the initialization of the room here: create containing entities, connect portal/doors to other rooms.
        /// </summary>
        public abstract void Initialize();

        private static Dictionary<Type, SingletonRoom> instances = new Dictionary<Type, SingletonRoom>();
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>The instance.</returns>
        /// <param name="singletonRoomType">Singleton room type.</param>
        public static SingletonRoom GetInstance(Type singletonRoomType)
        {
            if (SingletonRoom.instances.ContainsKey(singletonRoomType))
                return SingletonRoom.instances [singletonRoomType];

            return null;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>The instance.</returns>
        /// <typeparam name="TSingletonRoom">The 1st type parameter.</typeparam>
        public static TSingletonRoom GetInstance<TSingletonRoom>()
            where TSingletonRoom : SingletonRoom
        {
            return SingletonRoom.GetInstance(typeof(TSingletonRoom)) as TSingletonRoom;
        }
    }
}

