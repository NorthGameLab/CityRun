using Godot;
using System;

public partial class GameOver : CanvasLayer
{
	// soundeffect for collision
	private AudioStreamPlayer2D Collision;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// plays the collision sound when game over
		Collision = GetNode<AudioStreamPlayer2D>("Collision");
		Collision.Play();
		Label Score = GetNode<Label>("Score");
		Label HighScore = GetNode<Label>("HighScore");

		// shows your scores at the end
		Score.Text = "Score: " + Test.Score;
		HighScore.Text = "HighScore: " + Test.HighScore;
		GameScene._speed = 500f;
	}

	// starts game over
	private void OnRetryButtonPressed()
	{
		GameScene.resetGame();
		GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
	}
	// goes to main menu
	private void OnMainMenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/menu/MainMenu.tscn");
	}
}
