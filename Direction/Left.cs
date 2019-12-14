using Godot;

namespace Direction
{
    public class Left : IDirection
    {
        public Vector2 Move(Vector2 position)
        {
            position.x -= 1;
            return position;
        }
    }
}