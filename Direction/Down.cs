using Godot;

namespace Direction
{
    public class Down : IDirection
    {
        public Vector2 Move(Vector2 position)
        {
            position.y += 1;
            return position;
        }
    }
}