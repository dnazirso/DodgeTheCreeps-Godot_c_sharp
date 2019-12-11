using Godot;

namespace Direction
{
    public class Up : IDirection
    {
        public void Move(Vector2 position)
        {
            position.y += 1;
        }
    }
}