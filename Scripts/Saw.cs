using Godot;

namespace ConboiSprint;

public partial class Saw : EvilObstacle
{
    private float _rotationSpeed = 60f;

    public override void _Ready()
    {
        base._Ready();

        RandomNumberGenerator rng = new();
        rng.Randomize();

        RotationDegrees = rng.RandiRange(1, 360);
        _rotationSpeed = rng.RandfRange(300, 400);
        if (rng.Randi() % 2 == 0)
        {
            _rotationSpeed *= -1;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        RotationDegrees += _rotationSpeed * (float)delta;
    }
}