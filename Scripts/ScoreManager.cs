using Godot;

namespace ConboiSprint;

public partial class ScoreManager : Node
{
	private ulong _score = 0;
	public ulong Score {
		get {
			return _score;
		}
		private set {
			_score = value;

			_scoreLabel.Text = _score.ToString();
		}
	}

	private PersistentData _data;
	private GameManager _game;
	private Label _scoreLabel;

	[Signal]
	public delegate void SetHighScoreEventHandler(bool isNewHigh);

	public override void _Ready()
	{
		_data = GetNode<PersistentData>("/root/PersistentData");
		_game = GetTree().Root.GetNode<GameManager>("Game");
		_scoreLabel = GetTree().Root.GetNode<Label>("Game/UI/Score");
		Timer scoreTimer = GetNode<Timer>("Timer");

		scoreTimer.Timeout += AddPeriodicScore;
		_game.GameFinished += GameFinished;
	}

	private void AddPeriodicScore()
	{
		if(_game.State != GameState.PLAYING) { return; }

		Score += 10;
	}

	private void GameFinished()
	{
		bool isNewHigh = _data.TrySetHighScore(Score);

		EmitSignal(SignalName.SetHighScore, isNewHigh);
	}
}
