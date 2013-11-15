using System;
using System.Text;

namespace MyAdventureGame
{
    public class RenderDescriptionEventArgs : EventArgs
    {
        public string Description
        {
            get;
            set;
        }

        public string AdditionalText
        {
            get;
            set;
        }

        public string Hints
        {
            get;
            set;
        }

        public void Trim()
        {
            this.Description = string.IsNullOrWhiteSpace(this.Description) ? "I don't see anything special about it." : this.Description.Trim();
            this.AdditionalText = string.IsNullOrWhiteSpace(this.AdditionalText) ? null : this.AdditionalText.Trim();
            this.Hints = string.IsNullOrWhiteSpace(this.Hints) ? null : this.Hints;
        }
    }
}

