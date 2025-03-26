using Godot;
using System;

public partial class MainMenu : Node
{
    public enum EffectType
    {
        none = 0,
        Select,
        Back,
    }
    [Export] public Window PopupWindow;
    [Export] private AudioStreamPlayer2D _selectButton = null;
    [Export] private AudioStreamPlayer2D _backButton = null;
    private Sprite2D _texture = null;
    private Sprite2D _texture2 = null;
    public CanvasLayer Menu;
    public Label HighScore;

    private Button TestMenuButton;
    private Button TestSkinSelectButton;


    public override void _Ready()
    {
        Test.loadGame();

        Menu = GetNode<CanvasLayer>("Menu");
        HighScore = Menu.GetNode<Label>("HighScore");

        HighScore.Text = "Highscore: " + Test.HighScore;

        _texture = GetNode<Sprite2D>("Menu/Control/Sprite2D");

        /*
        _texture2 = new Sprite2D();
        _texture2.Texture = _texture.Texture;
        _texture2.Scale = _texture.Scale;
        _texture2.Position = _texture.Position - new Vector2(540, 0);
        GetNode<Control>("Menu/Control").AddChild(_texture2);
        */
        PopupWindow = GetNode<Window>("SettingsPopup");
        PopupWindow.Visible = false;

        TestMenuButton = GetNode<Button>("Menu/TestShopButton");
        TestMenuButton.Pressed += TestMenuButtonPressed;

        TestSkinSelectButton = GetNode<Button>("Menu/TestSkinSelectButton");
        TestSkinSelectButton.Pressed += TestSkinSelectButtonPressed;
    }

    private void OnSettingsButtonPressed()
    {
        PopupWindow.Visible = true;
        PlayAudioEffect(EffectType.Select);
    }
    private void OnExitButtonPressed()
    {
        PlayAudioEffect(EffectType.Back);
        PopupWindow.Visible = false;
    }

    private void OnFinnishFlagPressed()
    {
        Global.Language = "fi";
        PlayAudioEffect(EffectType.Select);
    }
    private void OnEnglishFlagPressed()
    {
        Global.Language = "en";
        PlayAudioEffect(EffectType.Select);
    }

    public override void _Process(double delta)
    {
        /*
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
        */
    }

    private void onStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
    }

    private void TestMenuButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/menu/Shop/Shop.tscn");
    }

    private void TestSkinSelectButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/menu/SkinSelect/SkinSelect.tscn");
    }

    private void onExitGamePressed()
    {
        GetTree().Quit();
    }
    public void PlayAudioEffect(EffectType effectType)
    {
        switch (effectType)
        {
        case EffectType.Select:
        if(_selectButton != null)
        {
            _selectButton.Play();
        }
        break;
        case EffectType.Back:
        if(_backButton != null)
        {
            _backButton.Play();
        }
        break;

        default:
        break;
        }

    }
}

