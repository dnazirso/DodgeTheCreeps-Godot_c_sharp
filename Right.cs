using Godot;

namespace Direction
{
    public class Right : IDirection
    {
        public void Move(Vector2 position)
        {
            position.x -= 1;
        }
    }
}