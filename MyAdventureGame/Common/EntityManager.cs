using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyAdventureGame
{
    public class EntityManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.EntityManager"/> class.
        /// </summary>
        public EntityManager()
        {
        }

        private Dictionary<string,Entity> entities = new Dictionary<string, Entity>();
        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>The entities.</value>
        public IEnumerable<Entity> Items
        {
            get { return this.entities.Values; }
        }

        /// <summary>
        /// Gets the rooms.
        /// </summary>
        /// <value>The rooms.</value>
        public IEnumerable<Room> Rooms
        {
            get 
            { 
                return this.Items
                           .OfType<Room>()
                           .Cast<Room>();
            }
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
        /// Initialize this instance.
        /// </summary>
        public void Initialize()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(SingletonRoom)));
                                
            foreach (var type in types)
            {
                Activator.CreateInstance(type);
            }

            var rooms = this.Rooms.OfType<SingletonRoom>().Cast<SingletonRoom>().ToArray();

            foreach (var room in rooms)
            {
                room.Initialize();
            }

            var initialRoom = SingletonRoom.GetInstance<StartRoom>();

            this.Player = new Player(initialRoom);
            this.Player.Initialize();

            //var singletonRooms = this.Rooms.OfType<SingletonRoom<>>();


//            var x = new StartRoom();
//
//
//
//            this.World = new Container("World", "It's pretty small.");
//            
//            Room outside = this.BuildOutside();
//            Room porch = this.BuildPorch();
//            
//            outside.AddConnection(Direction.North, porch);
//            
//            this.World.Items.Add(outside);
//            this.Player = new Player(x);
//            this.Player.Initialize();
        }

        public void Register(Entity entity, string oldId = null)
        {
            if (oldId != null && !this.entities.Remove(oldId))
            {
                string msg = string.Format("Failed to locate old registered entity '{0}'", oldId);
                throw new InvalidOperationException(msg);
            }

            if (this.entities.ContainsKey(entity.Id))
            {
                string msg = string.Format("Duplicate entity id {0}.", entity.Id);
                throw new InvalidOperationException(msg);
            }

            this.entities.Add(entity.Id, entity);
        }
        
//        Room BuildOutside()
//        {
//            var outside = new Room("Outside", "A chilling wind is giving you the shivers. To the north you can see the entrance to the house.");
//            
//            var house = new Entity("house", "It's old and making soft crackling noises in the wind. There's a dim light shining through one of the upper windows.\nPerhaps there's still someone left alive?");
//            var trees = new Entity("trees");
//            var sky = new Entity("sky", "It's pitch black.");
//            var porch = new Entity("porch", "To the north you can see the porch of the old wooden house.");
//            
//            outside.Items.Add(house);
//            outside.Items.Add(trees);
//            outside.Items.Add(sky);
//            outside.Items.Add(porch);
//            
//            var box = new Box("weird box", "It's a weird looking box");
//            
//            var innerBox = new Box("innerbox", "It's abit smaller than that other box.");
//            box.Items.Add(innerBox);
//            
//            var apple = new Entity("apple", "OoOOoOooh .... shiny!!");
//            innerBox.Items.Add(apple);
//            
//            outside.Items.Add(box);
//            
//            return outside;
//        }
//        
//        Room BuildPorch()
//        {
//            var porch = new Room("Porch", @"The old wooden boards beneath your feet are stiff and loose; it's almost impossible to walk around quietly. " +
//                                 @"A large shoddy door stands in front of you with windows both sides."); 
//            
//            porch.PlayerEnterRoom += (sender, e) => 
//            {
//                Game.Instance.Output.WriteLine("Sorry ... you're denied!!!");
//                
//                e.Cancel = true;
//            };
//            
//            var weirdBox = new Box("weird-looking-box", "It's a weird looking box");
//            
//            porch.Items.Add(weirdBox);
//            
//            // var doormat = new Container("doormat", "It's a doormat
//            
//            
//            return porch;
//        }

    }
}

