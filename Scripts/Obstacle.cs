using Godot;

namespace ConboiSprint;

public partial class Obstacle : Area2D
{
	private float speed = 900f;

	public override void _Process(double delta)
	{
		Position += Vector2.Left * speed * (float)delta;

		if(GlobalPosition.X < -50) {
			QueueFree();
		}
	}
}
