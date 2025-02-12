using Godot;
using System;

public partial class MainMenu : Node
{
    public CanvasLayer Menu;
    public Label HighScore;

    public override void _Ready()
    {
        Menu = GetNode<CanvasLayer>("Menu");
        HighScore = Menu.GetNode<Label>("HighScore");

        HighScore.Text = "Highscore: " + Test.HighScore;

    }
    private void onStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
    }
}
