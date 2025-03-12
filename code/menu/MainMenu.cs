using Godot;
using System;

public partial class MainMenu : Node
{
    [Export] public Window PopupWindow;
    private AnimatedSprite2D _animation = null;
    public CanvasLayer Menu;
    public Label HighScore;

    public override void _Ready()
    {
        Test.loadGame();

        Menu = GetNode<CanvasLayer>("Menu");
        HighScore = Menu.GetNode<Label>("HighScore");

        HighScore.Text = "Highscore: " + Test.HighScore;

        _animation = GetNode<AnimatedSprite2D>("Menu/Control/AnimatedSprite2D");
        _animation.Play();
        _animation.SpeedScale = 1;

        PopupWindow = GetNode<Window>("SettingsPopup");
        PopupWindow.Visible = false;
    }

    private void OnSettingsButtonPressed()
    {
        PopupWindow.Visible = true;
    }
    private void OnSettingsPopupCloseRequested()
    {
        PopupWindow.Visible = false;
    }
    public override void _Process(double delta)
    {

    }
    private void onStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
    }
}
