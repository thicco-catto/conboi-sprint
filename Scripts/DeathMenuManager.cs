using Godot;

namespace ConboiSprint;

public partial class DeathMenuManager : Control
{
	private GameManager _game;
	private PersistentData _data;
	private ScoreManager _scoreManager;
	private Label _highScoreLabel;
	private Label _scoreLabel;
	private Label _newHighScoreLabel;

	public override void _Ready()
	{
		_game = GetNode<GameManager>("/root/Game");
		_data = GetNode<PersistentData>("/root/PersistentData");
		_scoreManager = GetNode<ScoreManager>("/root/Game/ScoreManager");

		_highScoreLabel = GetNode<Label>("Scores/HighScore");
		_scoreLabel = GetNode<Label>("Scores/Score");
		_newHighScoreLabel = GetNode<Label>("NewHighScore");

		_scoreManager.SetHighScore += OnSetHighScore;
	}

	private void OnSetHighScore(bool isNewHigh)
	{
		_highScoreLabel.Text = $"HighScore: {_data.HighScore}";
		_scoreLabel.Text = $"Score: {_scoreManager.Score}";

		_newHighScoreLabel.Visible = isNewHigh;
	}
}
