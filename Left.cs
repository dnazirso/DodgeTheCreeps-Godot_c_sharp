using Godot;

namespace Direction
{
    public class Left : IDirection
    {
        public void Move(Vector2 position)
        {
            position.x += 1;
        }
    }
}