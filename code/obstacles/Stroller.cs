using Godot;
using System;

public partial class Stroller : Obstacle
{
	public override void Start()
	{
		Random rand = new Random();
		int spawnPos = rand.Next(1, 3);
		GlobalPosition = new Vector2(spawnPos * _width, -_width);
	}
}
