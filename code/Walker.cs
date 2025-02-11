using Godot;
using System;

public partial class Walker : RigidBody2D
{
	[Export]
	private float _speed = 50f;
	private float _width;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_width = GetViewportRect().Size.X / 5;

		start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition += Obstacles._speed * Vector2.Down * (float)delta;
	}

	private void start()
	{
		Random rand = new Random();
		GlobalPosition = new Vector2(rand.Next(1, 4) * _width, -_width);
	}

	private void screenExited()
	{
		QueueFree();
	}
}
