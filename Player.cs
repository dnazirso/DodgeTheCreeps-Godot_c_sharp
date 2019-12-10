using Godot;
using System;
using Direction;

public class Player : Area2D
{
    [Export]
    // How fast the player will move (pixels/sec).
    public int Speed = 400;

    // Size of the game window.
    private Vector2 _screenSize;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _screenSize = GetViewport().GetSize();
    }

    // public override void _Input(InputEvent @event)
    // {
    //     var acts = @event.GetName();
    // }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var velocity = new Vector2(); // The player's movement vector.

        var ev = Input.Singleton as InputEventAction;

        if (Input.IsActionPressed("ui_right"))
        {
            velocity.x += 1;
        }

        if (Input.IsActionPressed("ui_left"))
        {
            velocity.x -= 1;
        }

        if (Input.IsActionPressed("ui_down"))
        {
            velocity.y += 1;
        }

        if (Input.IsActionPressed("ui_up"))
        {
            velocity.y -= 1;
        }

        if (ev != null)
        {
            var dirtyp = DirectionType.ToDirection(ev.Action);
            dirtyp.Move(velocity);
        }
        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");

        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
            animatedSprite.Play();
        }
        else
        {
            animatedSprite.Stop();
        }

        Position += velocity * delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.x, 0, _screenSize.x),
            y: Mathf.Clamp(Position.y, 0, _screenSize.y)
        );
    }
}
