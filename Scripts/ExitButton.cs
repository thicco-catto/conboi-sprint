using Godot;

namespace ConboiSprint;

public partial class ExitButton : Button
{
	public override void _Ready()
	{
		Pressed += () => GetTree().Quit();
	}
}
