using Godot;
using System;

public partial class Area2Objects : Node2D
{

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition += GameScene._speed * Vector2.Down * (float)delta;
	}

	private void onScreenExited()
	{
		QueueFree();
	}
}
