using Godot;
using System;

public abstract partial class Road : AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Frame = 0;
		GlobalPosition = new Vector2(0, -600);
		ZIndex = -3;
		//Scale = new Vector2(1, 1);
	}

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
