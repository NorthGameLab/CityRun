using Godot;
using System;

public partial class SkinSelectButton : TextureButton
{
	public bool owned;
	public int id;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += onPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void onPressed()
	{
		if (owned)
		Test.CurrentSkinId = id;
	}
}
