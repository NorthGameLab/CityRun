using Godot;
using System;

public partial class MusicManager : Node
{
     // assigned menu music
    [Export] public AudioStreamPlayer MenuMusic;
    // assigned game music
    [Export] public AudioStreamPlayer GameMusic;

    // singleton
    public static MusicManager Instance;

    public override void _Ready()
    {
        Instance = this;
    }
    // plays menu music in all menus without cutting the audio
    public void PlayMenuMusic()
    {
        GD.Print("Playing menu music...");
        if (GameMusic != null && GameMusic.Playing)
            GameMusic.Stop();

        if (MenuMusic != null && !MenuMusic.Playing)
            MenuMusic.Play();
    }
    // plays game music in GameScene
    public void PlayGameMusic()
    {
        if (MenuMusic != null && MenuMusic.Playing)
            MenuMusic.Stop();

        if (GameMusic != null && !GameMusic.Playing)
            GameMusic.Play();
    }
}
