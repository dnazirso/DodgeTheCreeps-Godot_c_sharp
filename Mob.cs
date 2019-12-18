using Godot;
using System;

public class Mob : RigidBody2D
{
    [Export]
    public int MinSpeed = 150; // Minimum speed range.

    [Export]
    public int MaxSpeed = 250; // Maximum speed range.

    private String[] _mobTypes = { "walk", "swim", "fly" };

    // C# doesn't implement GDScript's random methods, so we use 'System.Random' as an alternative.
    static private Random _random = new Random();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<AnimatedSprite>("AnimatedSprite").Animation = _mobTypes[_random.Next(0, _mobTypes.Length)];
        GetNode<VisibilityNotifier2D>("Visibility").Connect("screen_exited", this, nameof(OnVisibilityScreenExited));
    }

    public void OnVisibilityScreenExited()
    {
        QueueFree();
    }

    public void OnStartGame()
    {
        QueueFree();
    }
}
