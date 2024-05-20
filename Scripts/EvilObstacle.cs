using Godot;

namespace ConboiSprint;

public partial class EvilObstacle : Obstacle
{
    public override void _Ready()
    {
        base._Ready();

        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if(body is Conboi conboi) {
            _game.KillConboi();
        }
    }
}