using Godot;
using System;

public partial class Global : Node
{
	public static string Language = "fi";
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
