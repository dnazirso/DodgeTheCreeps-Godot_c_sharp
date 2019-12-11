using System.Linq;
using Godot;

namespace Direction
{
    /// <summary>
    /// Static class responsible of key-direction mapping and revers
    /// </summary>
    public class DirectionType : EnumerationBase
    {
        public static readonly DirectionType Up = new DirectionType(UiDir.Up, new Up());
        public static readonly DirectionType Down = new DirectionType(UiDir.Down, new Down());
        public static readonly DirectionType Left = new DirectionType(UiDir.Left, new Left());
        public static readonly DirectionType Right = new DirectionType(UiDir.Right, new Right());

        /// <summary>
        /// constructor that prepare enumarations
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Direction"></param>
        public DirectionType(string Key, IDirection Direction) : base(Key, Direction) { }

        /// <summary>
        /// Checks if the given key is included to the enumerated ones
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ExistsWhitin(string key) => GetAll<DirectionType>()
            .ToList()
            .Exists(x => x.UiDirection.Equals(key));

        /// <summary>
        /// Checks if the given direction is included to the enumerated ones
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static bool ExistsWhitin(IDirection direction) => GetAll<DirectionType>()
            .ToList()
            .Exists(x => x.Direction.Equals(direction));

        /// <summary>
        /// Convert a Key into a Direction
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IDirection ToDirection(string key) => GetAll<DirectionType>()
            .Where(k => k.UiDirection.Equals(key))
            .FirstOrDefault().Direction;
    }
}
