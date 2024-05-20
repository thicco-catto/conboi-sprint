using Godot;

namespace ConboiSprint;

public partial class Obstacle : Area2D
{
    protected GameManager _game;

    public override void _Ready()
    {
        base._Ready();

        _game = (GameManager)GetTree().Root.GetNode("Game");
    }


    public override void _Process(double delta)
    {
        Position += Vector2.Left * _game.GameSpeed * (float)delta;

        if (GlobalPosition.X < -50)
        {
            QueueFree();
        }
    }
}
