using Godot;
using System;

public abstract partial class Obstacle : Area2D
{

	[Export]
	public float _speed = 0f;
	public float _width;
	public AnimatedSprite2D Animation;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_width = GetViewportRect().Size.X / 5;
		Animation = GetNode<AnimatedSprite2D>("Animation");

		Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Animation.Play();
		GlobalPosition += (GameScene._speed + _speed) * Vector2.Down * (float)delta;
	}

	public virtual void Start()
	{
		Random rand = new Random();
		int spawnPos = rand.Next(1, 4);
		GlobalPosition = new Vector2(spawnPos * _width, -_width);
	}

	public void screenExited()
	{
		QueueFree();
	}
}
