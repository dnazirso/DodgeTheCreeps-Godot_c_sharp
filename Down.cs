using Godot;

namespace Direction
{
    public class Down : IDirection
    {
        public void Move(Vector2 position)
        {
            position.y -= 1;
        }
    }
}