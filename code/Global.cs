using Godot;
using System;
// global script to be used all around
public partial class Global : Node
{
	// string for language code
	public static string Language = "fi";

	// boolean for correct answer
	public static bool _isCorrect = false;

	// settings config file
	public const string SettingsFile = "user://settings.cfg";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetProcess(false);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
