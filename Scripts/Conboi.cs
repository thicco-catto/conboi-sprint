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
	private bool _isHolding = false;
	private double _timeHeld = 0;

	private AnimatedSprite2D _sprite;

    public override void _Ready()
    {
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		_sprite.Play(ConboiAnim.RUN);
    }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		float gravity = _gravity;

		if(_isHolding) {
			if(Input.IsActionPressed("ui_accept")) {
				gravity *= _reducedGravityMult;

				_timeHeld += delta;

				if(_timeHeld >= _maxTimeHeld) {
					_isHolding = false;
				}
			} else {
				_isHolding = false;
			}
		}


		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor()) {
			_isHolding = true;
			_timeHeld = 0;
			velocity.Y = _jumpVelocity;
		}

		if (!IsOnFloor()) {
			velocity.Y += gravity * (float)delta;
		}

		Velocity = velocity;
		MoveAndSlide();

		HandleAnimations();
	}

	private void HandleAnimations() {
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

        if (_sprite.Animation != expectedAnimation) {
			_sprite.Play(expectedAnimation);
		}
	}
}
