using Godot;
using System.Linq;
using Direction;

public class Player : Area2D
{
    [Signal]
    public delegate void Hit();

    [Export]
    // How fast the player will move (pixels/sec).
    public int Speed = 400;

    // Size of the game window.
    private Vector2 _screenSize;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Hide();
        _screenSize = GetViewport().GetSize();

        Connect("body_entered", this, nameof(OnPlayerBodyEntered));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var velocity = new Vector2(); // The player's movement vector.

        DirectionType
        .GetAll<DirectionType>()
        .ToList()
        .FindAll(d => Input.IsActionPressed(d.UiDirection))
        .ForEach(dir => velocity = dir.Direction.Move(velocity));

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

        if (velocity.y != 0)
        {
            animatedSprite.Animation = "up";
        }

        if (velocity.x != 0)
        {
            animatedSprite.Animation = "right";
        }

        if (velocity.x != 0 || velocity.y != 0)
        {
            animatedSprite.FlipV = velocity.y > 0;
            animatedSprite.FlipH = velocity.x < 0;
        }
    }

    public void Start(Vector2 pos)
    {
        Position = pos;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    public void OnPlayerBodyEntered(PhysicsBody2D body)
    {
        Hide(); // Player disappears after being hit.
        EmitSignal("Hit");
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    }
}
