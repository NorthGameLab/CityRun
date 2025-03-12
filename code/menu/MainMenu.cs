using Godot;
using System;

public partial class MainMenu : Node
{
    [Export] public Window PopupWindow;
    private Sprite2D _texture = null;
    private Sprite2D _texture2 = null;
    public CanvasLayer Menu;
    public Label HighScore;

    public override void _Ready()
    {
        Test.loadGame();

        Menu = GetNode<CanvasLayer>("Menu");
        HighScore = Menu.GetNode<Label>("HighScore");

        HighScore.Text = "Highscore: " + Test.HighScore;

        _texture = GetNode<Sprite2D>("Menu/Control/Sprite2D");

        _texture2 = new Sprite2D();
        _texture2.Texture = _texture.Texture;
        _texture2.Scale = _texture.Scale;
        _texture2.Position = _texture.Position - new Vector2(540, 0);
        GetNode<Control>("Menu/Control").AddChild(_texture2);

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
        _texture.Position += new Vector2(1, 0);
        _texture2.Position += new Vector2(1, 0);

        if (_texture.Position.X >= 540)
        {
            _texture.Position = new Vector2(-540, 0);
        }
        if (_texture2.Position.X >= 540)
        {
            _texture2.Position = new Vector2(-540, 0);
        }
    }

    private void onStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
    }
}
