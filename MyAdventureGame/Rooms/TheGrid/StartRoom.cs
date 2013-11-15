using System;

namespace MyAdventureGame
{
    public class StartRoom : SingletonRoom
    {
        public StartRoom()
        {
            // Every room needs a name and a description.

            this.Name = "Unknown location";
            this.Description = "You woke up here and you do not have any idea where you are. " +
                               "There's light scattering from every angle. Multiple lightsources around you are making it quite hard to see anything clearly.\n" +
                               "If you squeeze your eyes you can see something blurry to the north.";
        }

        public override void Initialize()
        {
            var survivalKitRoom = SingletonRoom.GetInstance<SurvivalKit01Room>();
            var blurrySomething = new Portal(survivalKitRoom, "blurry something", "It's blurry. Go north to get a closer look.");

            this.Items.Add(blurrySomething);
            this.SetPortal(Direction.North, blurrySomething);
        }

        private bool introductionShown = false;

        protected override void OnPlayerEnterRoom(PlayerEnterRoomEventArgs eventArgs)
        {
            // This method is called whenever the player enters the room/area. We only want this introduction to be played once so we track it
            // with the boolean member variable. After playing the introduction we set the boolean value to true so it never gets replayed.

            if (!this.introductionShown)
            {
                this.PlayIntroduction();

                this.introductionShown = true;

                // Notify our base class not to execute the default succes message.

                eventArgs.DisplaySuccesMessage = false;

                // Give the new player some guidelines

                this.Output.WriteFormat("Enter 'help' for a list of available commands.\n");
            }

            base.OnPlayerEnterRoom(eventArgs);
        }

        private void PlayIntroduction()
        {
            this.Output.TypeWrite("The numbness", speed: 200, pause: 1000);
            this.Output.TypeWrite(" ... is slowly starting to tingle. ", speed: 200, random: 100, pause: 1000);
            this.Output.TypeWrite("\n\n'Where ... am ... I?'\n", pause: 1000);
            this.Output.TypeWrite("\nIt's dark.\n", pause: 500);
            this.Output.TypeWrite("\n'How long have I been here?'\n", pause: 1000);
            this.Output.TypeWrite("\nThe hard floor is stealing your body warmth away, making your muscles cramp.\n", pause: 500);
            this.Output.TypeWrite("\n'Where is here?'", pause: 500);
            this.Output.TypeWrite(" - 'Where am I?'", pause: 500);
            this.Output.TypeWrite(" - 'How did I get here?'\n", pause: 1000);
            this.Output.TypeWrite("\nA sound", pause: 1000);
            this.Output.TypeWrite(" - machinery echoing from some distance.", pause: 1000);
            this.Output.TypeWrite(" The ground trembles lightly.\n");

            this.Output.TypeWrite("\n'Something has been turned on!\n", pause: 1500);

            this.Output.TypeWrite("\nThe sound is picking up pitch, like the machine is setting something in motion. ", pause: 1000);
            this.Output.TypeWrite("Another sound joins in, much louder than the previous one:\n", pause: 1000);

            this.Output.Write("\nKA", pause: 1000);
            this.Output.Write("-CHUB!", pause: 1000);
            this.Output.Write(" KA", pause: 1000);
            this.Output.Write("-CHUB!", pause: 1000);

            this.Output.TypeWrite("\n\n ... and it's getting louder.\n\n", pause: 1000);

            this.Output.Write("KA", pause: 1000);
            this.Output.Write("-CHUB!", pause: 1000);
            this.Output.Write(" KA", pause: 800);
            this.Output.Write("-CHUB!", pause: 800);
            this.Output.Write(" KA", pause: 800);
            this.Output.Write("-CHUB!", pause: 800);
            this.Output.Write(" KA", pause: 600);
            this.Output.Write("-CHUB!", pause: 600);
            this.Output.Write(" KA", pause: 600);
            this.Output.Write("-CHUB!", pause: 600);
            this.Output.Write(" KA", pause: 400);
            this.Output.Write("-CHUB!", pause: 400);
            this.Output.Write(" KA", pause: 400);
            this.Output.Write("-CHUB!", pause: 400);
            this.Output.Write(" KA", pause: 400);
            this.Output.Write("-CHUB!", pause: 400);
            this.Output.Write(" KA", pause: 200);
            this.Output.Write("-CHUB!", pause: 200);
            this.Output.Write(" KA", pause: 200);
            this.Output.Write("-CHUB!", pause: 200);
            this.Output.Write(" KA", pause: 200);
            this.Output.Write("-CHUB!", pause: 200);
            this.Output.Write(" KA", pause: 200);
            this.Output.Write("-CHUB!", pause: 200);
            this.Output.Write(" KA", pause: 100);
            this.Output.Write("-CHUB!", pause: 100);

            this.Output.TypeWrite("\n\nUntil ");
            this.Output.TypeWrite("...", speed: 500, random: 0, pause: 1000);

            this.Output.Write("\n\nBOOM! LIGHT!\n", pause: 1000);

            this.Output.TypeWrite("\nThere's so much light that the whole air feels electric and is making a buzzing sound.\n");
            this.Output.TypeWrite("\nYou're standing.\n", pause: 1000);
            this.Output.TypeWrite("\nBare feet.", pause: 1000);
            this.Output.TypeWrite(" The floor is cold.", pause: 1000);
            this.Output.TypeWrite("\b\b\b\b\bvery cold.", pause: 1000);
            this.Output.TypeWrite("\b\b\b\b\b\b\b\b\b\bextremely cold.", pause: 1000);

            this.Output.TypeWrite(" You're not sure if all this time, you were standing already, but you can't recall getting up on your feet either.\n", pause: 1000);
            this.Output.TypeWrite("Everything is white. The combination of the sound and light makes you almost tumble over.\n\n", pause: 1000);
            this.Output.TypeWrite("'Where the fuck am I?!'\n\n", speed: 200, random: 100);
        }
    }
}

