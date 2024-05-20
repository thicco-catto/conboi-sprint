using Godot;

namespace ConboiSprint;

public partial class ScoreManager : Node
{
	private int _score = 0;
	public int Score {
		get {
			return _score;
		}
		private set {
			_score = value;

			_scoreLabel.Text = _score.ToString();
		}
	}

	private GameManager _game;
	private Label _scoreLabel;

	public override void _Ready()
	{
		_game = GetTree().Root.GetNode<GameManager>("Game");
		_scoreLabel = GetTree().Root.GetNode<Label>("Game/UI/Score");
		Timer scoreTimer = GetNode<Timer>("Timer");

		scoreTimer.Timeout += AddPeriodicScore;
	}

	private void AddPeriodicScore()
	{
		if(_game.State != GameState.PLAYING) { return; }

		Score += 10;
	}
}
