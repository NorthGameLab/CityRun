using Godot;
using System;

public partial class TestRock : Sprite2D
{
	public int _pos;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Random rand = new Random();
		_pos = rand.Next(1, 100);
		GlobalPosition = new Vector2(GetViewport().GetVisibleRect().Size.X / 100 * _pos, -50);
		ZIndex = -1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition += GameScene._speed * Vector2.Down * (float)delta;
	}
}
