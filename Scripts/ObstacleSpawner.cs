using Godot;
using Godot.Collections;

namespace ConboiSprint;

public partial class ObstacleSpawner : Node
{
	[Export]
	private Array<PackedScene> ObstacleGroups = new();
	private RandomNumberGenerator _rng = new();
	private Marker2D _spawnPos;

    public override void _Ready()
    {
        _rng.Randomize();
		_spawnPos = GetNode<Marker2D>("SpawnPos");
    }

	public void SpawnRandomObstacle() {
		int randomIndex = _rng.RandiRange(0, ObstacleGroups.Count-1);

		PackedScene group = ObstacleGroups[randomIndex];
		Node2D instantiatedGroup = group.Instantiate<Node2D>();
		Node parent = GetParent();
		parent.AddChild(instantiatedGroup);
		instantiatedGroup.Position = _spawnPos.Position;

		foreach(var child in instantiatedGroup.GetChildren()) {
			if(child is Node2D obstacle) {
				obstacle.Reparent(parent);
			}
		}

		instantiatedGroup.QueueFree();
	}
}
