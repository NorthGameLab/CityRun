using Godot;
using System;

public partial class MusicManager : Node
{
    [Export] public AudioStreamPlayer MenuMusic; // assigned menu music
    [Export] public AudioStreamPlayer GameMusic; // assigned game music

    public static MusicManager Instance;

    public override void _Ready()
    {
        Instance = this; // Ensure we have a singleton instance

        /*
        if (MenuMusic != null && !MenuMusic.Playing)
        {
            PlayMenuMusic(); // Start with menu music
        }
        */
    }

    public void PlayMenuMusic()
    {
        GD.Print("Playing menu music...");
        if (GameMusic != null && GameMusic.Playing)
            GameMusic.Stop();

        if (MenuMusic != null && !MenuMusic.Playing)
            MenuMusic.Play();
    }

    public void PlayGameMusic()
    {
        if (MenuMusic != null && MenuMusic.Playing)
            MenuMusic.Stop();

        if (GameMusic != null && !GameMusic.Playing)
            GameMusic.Play();
    }
}
