using System;
using System.Diagnostics;

namespace MyAdventureGame
{
    /// <summary>
    /// This is the base entity class.
    /// </summary>
    [DebuggerDisplay("Name = {Name}")]
    public class Entity : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.Entity"/> class.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <param name="description">the description of the entity.</param>
        /// <remarks>
        /// This is the constructor of the <see cref="Entity"/> class.
        /// It has two arguments (name & description) and both have a default value which is
        /// indicated by the equal (=) sign. In this case the value null is used if no other 
        /// value is specified.
        /// 
        /// This means that the constructor can be called without specifying arguments: Entity().
        /// Or with only a name: Entity("MyName").
        /// Or with both a name and description: Entity("MyName", "MyDescription").
        /// Or with only a description, by explicitly specifiying the argument name: 
        /// Entity(description: "MyDescription")
        /// </remarks>
        public Entity(string name = null, string description = null, string id = null)
        {
            // Check if no name is specified

            if (name == null)
            {
                // No name given -- let's use the name of the class (eg: Door, Box, ...)
                // Gettings the name of the class can be done at runtime by getting the Type
                // of an instance. Every object in .NET has the method GetType(), which returns
                // just that: the Type of that instance.
                // The Type class has many properties and methods. The one we're interrested in
                // is the Name property, which gives us the name of the class.
                // Note that this is the name of the inherited class.

                name = this.GetType().Name;
            }

//            // Check if no description is specified
//
//            if(description == null)
//            {
//                // No description specified -- we'll make up some crappy default.
//
//                description = "I don't see anything special about it.";
//            }

            if (name.Contains("\"") || name.Contains("."))
            {
                throw new ArgumentException("Entity name contains invalid characters", "name");
            }

            this.Name = name;
            this.Description = description;

            // Ensure we've got a valid id

            this.Id = id ?? Guid.NewGuid().ToString();

            // Entity is visible by default

            this.IsVisible = true;
        }

        /// <summary>
        /// Gets or sets the name of the entity. 
        /// </summary>
        /// <value>The name.</value>
        /// <remarks>
        /// This value is displayed when the player looks around and is used to interacts with the entity.
        /// </remarks>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description of the entity.
        /// </summary>
        /// <value>The description.</value>
        /// <remarks>
        /// This gives the player a description when the player exams the entity. 
        /// </remarks>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates if this entity is a visible entity.
        /// </summary>
        /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
        public bool IsVisible
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is takeable.
        /// </summary>
        /// <value><c>true</c> if this instance is takeable; otherwise, <c>false</c>.</value>
        public bool IsTakeable
        {
            get;
            set;
        }

        private string id;
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id
        {
            get
            {
                return this.id;
            }
            set 
            { 
                if(value == null) throw new ArgumentNullException();
                
                if(this.id != value)
                {
                    var oldId = this.id;
                    this.id = value;
                    Game.Instance.Entities.Register(this, oldId);
                }
            }
        }

        #region Description

        /// <summary>
        /// Renders the description to the output.
        /// </summary>
        public void DisplayDescription()
        {
            var eventArgs = new RenderDescriptionEventArgs()
            {
                Description = this.Description
            };

            this.OnRenderDescription(eventArgs);

            eventArgs.Trim(); // Trims all values and ensures that Description is always filled.

            this.Output.TypeWrite(eventArgs.Description + "\n");

            if(eventArgs.AdditionalText != null)
                this.Output.Write("\n" + eventArgs.AdditionalText + "\n");

            if (eventArgs.Hints != null)
                this.Output.Write("\n" + eventArgs.Hints + "\n");
        }

        /// <summary>
        /// Raises the render description event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnRenderDescription(RenderDescriptionEventArgs eventArgs)
        {
            if (this.RenderDescription != null)
                this.RenderDescription(this, eventArgs);
        }

        /// <summary>
        /// Occurs when render description.
        /// </summary>
        public event EventHandler<RenderDescriptionEventArgs> RenderDescription;

        #endregion

        #region Pickup

        public void Pickup(Player player)
        {

        }

        #endregion
    }
}

