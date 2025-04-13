using Godot;
using System;

public partial class MainMenu : Node
{
    // soundeffects
    public enum EffectType
    {
        none = 0,
        Select,
        Back,
    }
    // settings window
    [Export] public Window PopupWindow;
    // Buttons for sound and audio effects for select and back
    [Export] private AudioStreamPlayer2D _selectButton = null;
    [Export] private AudioStreamPlayer2D _backButton = null;
    [Export] private TextureButton _MusicPlus = null;
    [Export] private TextureButton _musicMinus = null;
    [Export] private TextureButton _sfxPlus = null;
    [Export] private TextureButton _sfxMinus = null;

    public CanvasLayer Menu;
    public Label HighScore;
    private Settings settings;

    private TextureButton TestMenuButton;
    private TextureButton TestSkinSelectButton;


    public override void _Ready()
    {
        MusicManager.Instance.PlayMenuMusic();
        settings = GetNode<Settings>("Settings");
        Test.loadGame();
        Menu = GetNode<CanvasLayer>("Menu");
        HighScore = Menu.GetNode<Label>("HighScore");

        HighScore.Text = "HighScore: " + Test.HighScore;

        // hides settings in start
        PopupWindow = GetNode<Window>("SettingsPopup");
        PopupWindow.Visible = false;

        TestMenuButton = GetNode<TextureButton>("Menu/TestShopButton");
        TestMenuButton.Pressed += TestMenuButtonPressed;

        TestSkinSelectButton = GetNode<TextureButton>("Menu/TestSkinSelectButton");
        TestSkinSelectButton.Pressed += TestSkinSelectButtonPressed;
    }
    // shows settings popup
    private void OnSettingsButtonPressed()
    {
        PopupWindow.Visible = true;
        PlayAudioEffect(EffectType.Select);
    }
    // exit settings
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
    // changes language to finnish
    private void OnFinnishFlagPressed()
    {
        if(settings == null)
        {
            GD.PrintErr("settings not found");
        }

        settings.SetLanguage("fi");
        PlayAudioEffect(EffectType.Select);
    }
    // changes language to english
    private void OnEnglishFlagPressed()
    {
        if(settings == null)
        {
            GD.PrintErr("settings not found");
        }
        settings.SetLanguage("en");
        PlayAudioEffect(EffectType.Select);
    }

    // start game
    private void onStartButtonPressed()
    {
        GameScene.resetGame();
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
    // exits the game
    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
    // method for playing the audio effects
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