using Godot;
using System;

public partial class SkinSelectButton : TextureButton
{
	public bool owned;
	public AnimatedSprite2D RarityBackground;
	public int id;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RarityBackground = GetNode<AnimatedSprite2D>("RarityBackground");
		if (!owned)
			RarityBackground.Frame = 0;

		Pressed += onPressed;
	}

	private void onPressed()
	{
		if (owned)
			Test.CurrentSkinId = id;
	}
}
