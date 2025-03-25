using Godot;
using System;

public partial class CaseItem : Sprite2D
{

	private Texture2D _texture = (Texture2D)ResourceLoader.Load<Texture>("res://gfx/Kolikko22.png");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Texture = _texture;
		Scale = new Vector2(2.3f, 2.3f);
		GlobalPosition = new Vector2(-150, 500);
		ZIndex = -1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(GetParent<CaseOpening>().Speed > 0)
		GlobalPosition += new Vector2(GetParent<CaseOpening>().Speed * (float)delta, 0);
	}

	private void onScreenExited()
	{
		QueueFree();
	}
}
