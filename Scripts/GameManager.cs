using Godot;

namespace ConboiSprint;

public enum GameState
{
    PLAYING,
    DYING,
    DEAD
}

public partial class GameManager : Node2D
{
    public GameState State { get; private set; } = GameState.PLAYING;
    public float GameSpeed { get; private set; } = 900f;

    [Export]
    private PackedScene _deathExplosion;
    private Conboi _conboi;
    private Control _deathMenu;

    public override void _Ready()
    {
        base._Ready();

        _conboi = GetNode<Conboi>("Conboi");
        _deathMenu = GetNode<Control>("DeathMenu");
    }

    public void KillConboi()
    {
        State = GameState.DYING;
        GameSpeed = 0;

        _conboi.Visible = false;

        AnimatedSprite2D explosion = _deathExplosion.Instantiate<AnimatedSprite2D>();
        AddChild(explosion);
        explosion.GlobalPosition = _conboi.GlobalPosition;

        explosion.Play("default");
        explosion.AnimationFinished += () =>
        {
            explosion.QueueFree();
            FinishGame();
        };
    }

    public void FinishGame()
    {
        State = GameState.DEAD;
        _deathMenu.Visible = true;
    }
}