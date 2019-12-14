using Godot;

namespace Direction
{
    public interface IDirection
    {
        Vector2 Move(Vector2 position);
    }
}