using System;

namespace MyAdventureGame
{
    /// <summary>
    /// Indicates a direction.
    /// </summary>
    public enum Direction
    {
        None,
        North,
        East,
        South,
        West,
        Up,
        Down
    }

    public static class DirectionExtensions
    {
        public static Direction Mirror(this Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;

                case Direction.South:
                    return Direction.North;

                case Direction.East:
                    return Direction.West;

                case Direction.West:
                    return Direction.East;

                case Direction.Up:
                    return Direction.Down;

                case Direction.Down:
                    return Direction.Up;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}

