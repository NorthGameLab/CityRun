using Godot;
using System;

public partial class SkinSelect : Control
{
	private PackedScene SkinButton = ResourceLoader.Load<PackedScene>("res://scene/menu/SkinSelect/SkinSelectButton.tscn");
	private SkinSelectButton[] Skins;
	private string[] paths;
	private ScrollContainer Scroll;
	private GridContainer Container;
	private Sprite2D selectedSquare = new Sprite2D();

	private Button ExitButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Scroll = GetNode<ScrollContainer>("ScrollContainer");
		Container = GetNode<GridContainer>("ScrollContainer/GridContainer");

		paths = Test.getItemPaths();
		Skins = new SkinSelectButton[Test.ItemData.Count];
		for (int i = 0; i < Skins.Length; i++)
		{
			SkinSelectButton skin = SkinButton.Instantiate<SkinSelectButton>();
			Container.AddChild(skin);
			skin.owned = Test.OwnedSkins[i];
			skin.id = i;
			skin.TextureNormal = getTexture(i);

			if (skin.owned)
				if (Test.ItemData[i].AsGodotDictionary()["rarity"].ToString() == "common")
					skin.RarityBackground.Frame = 1;
				else if (Test.ItemData[i].AsGodotDictionary()["rarity"].ToString() == "rare")
					skin.RarityBackground.Frame = 2;
				else if (Test.ItemData[i].AsGodotDictionary()["rarity"].ToString() == "epic")
					skin.RarityBackground.Frame = 3;

			Scale = new Vector2(4, 4);

			if (!skin.owned)
			{
				Sprite2D x = new Sprite2D();
				skin.AddChild(x);
				Texture2D xOriginalTexture = (Texture2D)ResourceLoader.Load("res://gfx/X.png");
				x.Texture = xOriginalTexture;
				x.Position = new Vector2(32, 32);
			}

			Skins[i] = skin;
		}

		ExitButton = GetNode<Button>("CanvasLayer/ExitButton");
		ExitButton.Pressed += ExitButtonPressed;

		AddChild(selectedSquare);
		selectedSquare.Texture = (Texture2D)ResourceLoader.Load("res://gfx/Skinmenu/SkiniValittuIndikaattori.png");
		selectedSquare.Scale += new Vector2(0.15f, 0.15f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		selectedSquare.GlobalPosition = Skins[Test.CurrentSkinId].GlobalPosition + new Vector2(128, 128);
	}

	private Texture2D getTexture(int id)
	{
		var skinData = Test.ItemData[id].AsGodotDictionary();
		Texture2D textureInit = (Texture2D)ResourceLoader.Load((string)skinData["path"]);

		return textureInit;
	}

	private void ExitButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/menu/MainMenu.tscn");
	}
}
