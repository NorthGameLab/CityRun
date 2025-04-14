using Godot;
using System;

public partial class SkinSelect : Control
{
	private PackedScene SkinButton = ResourceLoader.Load<PackedScene>("res://scene/menu/SkinSelect/SkinSelectButton.tscn");
	//Array of buttons that select skins
	private SkinSelectButton[] Skins;
	private ScrollContainer Scroll;
	private GridContainer Container;
	private Sprite2D selectedSquare = new Sprite2D();

	private TextureButton ExitButton;

	// Sets up skin items for the shop
	public override void _Ready()
	{
		Scroll = GetNode<ScrollContainer>("ScrollContainer");
		Container = GetNode<GridContainer>("ScrollContainer/GridContainer");

		Skins = new SkinSelectButton[Test.ItemData.Count];
		for (int i = 0; i < Skins.Length; i++)
		{
			MarginContainer margin = new MarginContainer();
			margin.AddThemeConstantOverride("margin_top", 15);
			margin.AddThemeConstantOverride("margin_left", 22);

			SkinSelectButton skin = SkinButton.Instantiate<SkinSelectButton>();
			margin.AddChild(skin);
			Container.AddChild(margin);
			skin.owned = Test.OwnedSkins[i];
			skin.id = i;
			skin.TextureNormal = getTexture(i);
			if (skin.owned)
			{
				//skin.TextureNormal = getTexture(i);
				if (Test.ItemData[i].AsGodotDictionary()["rarity"].ToString() == "common")
					skin.RarityBackground.Frame = 1;
				else if (Test.ItemData[i].AsGodotDictionary()["rarity"].ToString() == "rare")
					skin.RarityBackground.Frame = 2;
				else if (Test.ItemData[i].AsGodotDictionary()["rarity"].ToString() == "epic")
					skin.RarityBackground.Frame = 3;
			}
			else
			{
				skin.LockedBackground.Show();
				//skin.TextureNormal = getLockedTexture(i);
			}

			Scale = new Vector2(4, 4);

			if (!skin.owned)
			{
				Sprite2D x = new Sprite2D();
				skin.AddChild(x);
				Texture2D xOriginalTexture = (Texture2D)ResourceLoader.Load("res://gfx/Skinmenu/SkiniLukittuLukko.png");
				x.Texture = xOriginalTexture;
				x.Position = new Vector2(skin.TextureNormal.GetWidth() / 2 + 1, skin.TextureNormal.GetHeight() / 2);
			}

			Skins[i] = skin;
			if (Skins[i].TextureNormal.GetWidth() > 32)
			{
				Skins[i].RarityBackground.GlobalPosition += Skins[i].TextureNormal.GetSize() / 2;
				Skins[i].LockedBackground.GlobalPosition += Skins[i].TextureNormal.GetSize() / 2;
			}
		}

		ExitButton = GetNode<TextureButton>("CanvasLayer/ExitButton");
		ExitButton.Pressed += ExitButtonPressed;

		AddChild(selectedSquare);
		selectedSquare.Texture = (Texture2D)ResourceLoader.Load("res://gfx/Skinmenu/SkiniValittuIndikaattori.png");
		selectedSquare.Scale += new Vector2(0.15f, 0.15f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		selectedSquare.GlobalPosition = Skins[Test.CurrentSkinId].GlobalPosition + new Vector2(Skins[Test.CurrentSkinId].TextureNormal.GetWidth() * 2, Skins[Test.CurrentSkinId].TextureNormal.GetHeight() * 2);
	}

	//Gets the texture of an item icon with the id id
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
