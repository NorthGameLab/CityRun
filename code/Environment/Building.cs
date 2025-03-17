using Godot;
using System;

public partial class Building : AnimatedSprite2D
{
    // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Random rand = new Random();

        int num = rand.Next(0, 6);
        Frame = num;

        //num = rand.Next(0, 2);
		GlobalPosition = new Vector2(70, -600);
		ZIndex = -1;

        double numDouble;
        numDouble = rand.NextDouble();
        Scale += new Vector2(0.12f, 0);

        numDouble = rand.NextDouble();
        Scale += new Vector2(0, 0.26f);
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
