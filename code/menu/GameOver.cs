using Godot;
using System;

public partial class GameOver : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Label Score = GetNode<Label>("Score");
		Label HighScore = GetNode<Label>("HighScore");

		Score.Text = "Score: " + Test.Score;
		HighScore.Text = "HighScore: " + Test.HighScore;
		GameScene._speed = 500f;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnRetryButtonPressed()
	{
		GameScene.resetGame();
		GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
	}

	private void OnMainMenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/menu/MainMenu.tscn");
	}
}
