using System;

namespace MyAdventureGame
{
    /// <summary>
    /// Validate event arguments.
    /// </summary>
    public abstract class ValidateEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAdventureGame.DeniedEventArgs"/> class.
        /// </summary>
        public ValidateEventArgs()
            : base()
        {
            this.ValidateResult = true;
            this.DisplayValidationFailedMessage = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the validation succeeded or failed.
        /// </summary>
        /// <value><c>true</c> if validate was succesful; otherwise, <c>false</c>.</value>
        public bool ValidateResult
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a default message should be shown if the validation failed.
        /// </summary>
        /// <value><c>true</c> to display a general failed message; otherwise, <c>false</c>.</value>
        public bool DisplayValidationFailedMessage
        {
            get;
            set;
        }
    }
}

