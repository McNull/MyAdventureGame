using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace MyAdventureGame
{
    /// <summary>
    /// The container entity class.
    /// </summary>
    /// <remarks>
    /// The container class is a class that "contains" other entities. So this can be a box with items in it,
    /// or the inventory of the player or even the room which the player stands in.
    /// </remarks>
    public class Container : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.Container"/> class.
        /// </summary>
        /// <param name="name">The name of the container.</param>
        /// <param name="description">The description of the container.</param>
        /// <remarks>
        /// The constructor of the container has two optional arguments: name and description.
        /// Both are passed down to the constructor of the Entity class. 
        /// See the Entity class for more information about these arguments.
        /// </remarks>
        public Container(string name = null, string description = null)
            : base(name, description)
        {
            // Create the list property that will contain all the sub entities.

            this.Items = new List<Entity>();

            // Set the default description header, which is displayed when the player
            // examines a container with items in it.

            this.SubItemsDescriptionHeader = "It contains the following items: ";
        }

        /// <summary>
        /// Gets the list of items that are contained within this container.
        /// </summary>
        /// <value>The items.</value>
        /// <remarks>
        /// </remarks>
        /// 
        public List<Entity> Items
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the sub items description header.
        /// </summary>
        /// <value>The sub items description header.</value>
        /// <remarks>
        ///     This header text is only displayed to the user when the container does actually contain items.
        ///     The default is "It contains the following items: ".
        /// </remarks>
        protected string SubItemsDescriptionHeader
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the sub items empty description.
        /// </summary>
        /// <value>The sub items empty description.</value>
        /// <remarks>
        ///     This is the text to display when the container does not contain any items (it's empty).
        ///     If this value is null or "" than nothing is displayed (which is the default).
        /// </remarks>
        protected string SubItemsEmptyDescription
        {
            get; set;
        }

        /// <summary>
        /// Raises the render description event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected override void OnRenderDescription(RenderDescriptionEventArgs eventArgs)
        {
            var sb = new StringBuilder();

            if (this.Items.Count > 0)
            {
                sb.AppendLine(this.SubItemsDescriptionHeader);
                sb.AppendLine();
                
                foreach (var item in this.Items)
                {
                    sb.AppendFormat("{0}, ", item.Name);
                }
                
                sb.Length -= 2; // Trim the last ", "
            } 
            else if (!string.IsNullOrWhiteSpace(this.SubItemsEmptyDescription))
            {
                sb.AppendLine(this.SubItemsEmptyDescription);
            }

            eventArgs.AdditionalText = sb.ToString();

            base.OnRenderDescription(eventArgs);
        }

        /// <summary>
        /// Locates the entity based on the provided selector.
        /// </summary>
        /// <returns>The entity.</returns>
        /// <param name="selector">An array of strings that indicate the nested entity.</param>
        public Entity LocateEntity(params string[] selector)
        {
            Entity item = this;

            for (int i = 0; i < selector.Length; i++)
            {
                var container = item as Container;
                
                if (container == null)
                {
                    this.Output.WriteFormat("The item '{0}' is not a container.\n", item.Name);
                    return null;
                }
                
                var itemName = selector[i].ToLower();
                item = container.Items.SingleOrDefault(x => x.Name == itemName);
                
                if (item == null)
                {
                    item = container.Items.SingleOrDefault(x => x.Name.StartsWith(itemName));

                    if(item == null)
                    {
                        this.Output.WriteFormat("Unable to locate the item '{0}'.\n", itemName);
                        return null;
                    }

                    if(Game.Instance.IsGodMode)
                        this.Output.WriteFormat("Assuming item '{0}'.\n\n", item.Name);
                }
            }

            return item;
        }
    }
}

