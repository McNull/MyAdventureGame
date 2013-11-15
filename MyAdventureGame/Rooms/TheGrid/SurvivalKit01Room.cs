using System;
using System.Text;

namespace MyAdventureGame
{
    public class SurvivalKit01Room : SingletonRoom
    {
        public SurvivalKit01Room()
        {
            // Emergency kit
            // 8 days supply of water
            // a toy crossbow
            // season 2 of star trek original series on a high density flash drive
            // if there's an apacolypse: good luck
            // rise and shine sleepy head, half the town is probably dead
            // ho! wait ... look around you! ... there's hypothetical broken glass all around you!

            this.Name = "Unknown location";
            this.Description = "There's light coming from all directions. On the ground you see a box with a big yellow label on it.";
        }

        public override void Initialize()
        {
            var emergencyKit = new Box("box with big yellow label on it", 
                                   "The label says: 'EMERGENCY KIT #01'");

            this.Items.Add(emergencyKit);

            emergencyKit.PlayerOpenEntity += (sender, e) => 
            {
                this.Output.TypeWrite("You open the box.\n\n");
                this.Output.Write("Hint: Look at the box to see its contents.\n");

                e.DisplaySuccesMessage = false;
            };

            emergencyKit.RenderDescription += (sender, e) => 
            {
                if(emergencyKit.IsOpen)
                {
                    e.Hints = "Hint: You can interact with items in containers by specifying the container name in the prefix.\n" +
                              "For example 'l box note' will look at the 'note' within the 'box'.";
                }
            };

            var flipFlops = new Entity("flipflops", "It's a pair of cheap yellow flipflops with a smiley pattern all over it.");
            emergencyKit.Items.Add(flipFlops);

            var note = new Entity("note", "It says: 'for emergencies only'.");
            emergencyKit.Items.Add(note);

            this.RenderDescription += (sender, e) => 
            {
                var sb = new StringBuilder();
                
                sb.Append("HINT: You don't need to type the full name of an item to interact with it.\n");
                sb.AppendFormat("Instead of typing 'look \"{0}\"', you can also look at the box by typing 'look {1}'.", emergencyKit.Name, emergencyKit.Name [0]);
                
                e.Hints = sb.ToString();
            };
        }
    }
}

