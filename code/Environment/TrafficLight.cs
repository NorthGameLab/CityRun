using Godot;
using System;

public partial class TrafficLight : AnimatedSprite2D
{

    public override void _Ready()
    {
        GlobalPosition = new Vector2(417f, -630f);
    }

    public override void _Process(double delta)
	{
		GlobalPosition += GameScene._speed * Vector2.Down * (float)delta;
        Play();
	}

	private void onScreenExited()
	{
		QueueFree();
	}
}
