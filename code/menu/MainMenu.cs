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
    [Export] private TextureButton _MusicPlus = null;
    [Export] private TextureButton _musicMinus = null;
    [Export] private TextureButton _sfxPlus = null;
    [Export] private TextureButton _sfxMinus = null;

    private Sprite2D _texture = null;
    private Sprite2D _texture2 = null;
    public CanvasLayer Menu;
    public Label HighScore;
    private Settings settings;

    private TextureButton TestMenuButton;
    private TextureButton TestSkinSelectButton;


    public override void _Ready()
    {
        settings = GetNode<Settings>("Settings");
        Test.loadGame();
        Menu = GetNode<CanvasLayer>("Menu");
        HighScore = Menu.GetNode<Label>("HighScore");

        HighScore.Text = "HighScore: " + Test.HighScore;

        /*
        _texture2 = new Sprite2D();
        _texture2.Texture = _texture.Texture;
        _texture2.Scale = _texture.Scale;
        _texture2.Position = _texture.Position - new Vector2(540, 0);
        GetNode<Control>("Menu/Control").AddChild(_texture2);
        */
        PopupWindow = GetNode<Window>("SettingsPopup");
        PopupWindow.Visible = false;

        TestMenuButton = GetNode<TextureButton>("Menu/TestShopButton");
        TestMenuButton.Pressed += TestMenuButtonPressed;

        TestSkinSelectButton = GetNode<TextureButton>("Menu/TestSkinSelectButton");
        TestSkinSelectButton.Pressed += TestSkinSelectButtonPressed;
    }

    private void OnSettingsButtonPressed()
    {
        PopupWindow.Visible = true;
        PlayAudioEffect(EffectType.Select);
    }
    private void OnExitButtonPressed()
    {
        if(settings == null)
        {
            GD.PrintErr("settings not found");
        }
        settings.SaveSettings();
        PlayAudioEffect(EffectType.Back);
        PopupWindow.Visible = false;
    }

    private void OnFinnishFlagPressed()
    {
        if(settings == null)
        {
            GD.PrintErr("settings not found");
        }

        settings.SetLanguage("fi");
        PlayAudioEffect(EffectType.Select);
    }
    private void OnEnglishFlagPressed()
    {
        if(settings == null)
        {
            GD.PrintErr("settings not found");
        }
        settings.SetLanguage("en");
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

    private void OnQuitButtonPressed()
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

