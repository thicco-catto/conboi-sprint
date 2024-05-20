using Godot;

namespace ConboiSprint;

public class ConboiAnim
{
    public static StringName RUN = new("Run");
    public static StringName JUMP = new("Jump");
    public static StringName FALL = new("Fall");
}

public partial class Conboi : CharacterBody2D
{
    private float _jumpVelocity = -600.0f;
    private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private float _reducedGravityMult = 0.14f;
    private double _maxTimeHeld = 0.5d;
    private double _maxJumpBuffer = 0.1d;
    private bool _isHolding = false;
    private double _timeHeld = 0;
    private double _currentJumpBuffer = 0;

    private GameManager _game;
    private AnimatedSprite2D _sprite;

    public override void _Ready()
    {
        _game = GetTree().Root.GetNode<GameManager>("Game");
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        _sprite.Play(ConboiAnim.RUN);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_game.State != GameState.PLAYING)
        {
            return;
        }

        Vector2 velocity = Velocity;

        bool onFloor = IsOnFloor();
        bool isPressed = Input.IsActionJustPressed("ui_accept");
        bool isHeld = Input.IsActionPressed("ui_accept");
        float gravity = _gravity;

		// Handle held jump
        if (_isHolding)
        {
            if (isHeld)
            {
                gravity *= _reducedGravityMult;

                _timeHeld += delta;

                if (_timeHeld >= _maxTimeHeld)
                {
                    _isHolding = false;
                }
            }
            else
            {
                _isHolding = false;
            }
        }

		// Handle gravity
        if (!onFloor)
        {
            velocity.Y += gravity * (float)delta;
            _currentJumpBuffer -= delta;
        }

		// Handle jump
        bool canJump = onFloor && (isPressed || _currentJumpBuffer > 0);
        if (canJump)
        {
            _isHolding = true;
            _timeHeld = 0;
            velocity.Y = _jumpVelocity;
            _currentJumpBuffer = 0;
        }

		// Handle jump buffering
        if (isPressed && !onFloor)
        {
            _currentJumpBuffer = _maxJumpBuffer;
        }

        Velocity = velocity;
        MoveAndSlide();

        HandleAnimations();
    }

    private void HandleAnimations()
    {
        bool onFloor = IsOnFloor();
        bool falling = Velocity.Y > 0;

        StringName expectedAnimation;
        if (onFloor)
        {
            expectedAnimation = ConboiAnim.RUN;
        }
        else if (falling)
        {
            expectedAnimation = ConboiAnim.FALL;
        }
        else
        {
            expectedAnimation = ConboiAnim.JUMP;
        }

        if (_sprite.Animation != expectedAnimation)
        {
            _sprite.Play(expectedAnimation);
        }
    }
}
