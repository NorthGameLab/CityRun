using Godot;
using System;

public partial class CaseOpening : Control
{
	private PackedScene CaseItem = ResourceLoader.Load<PackedScene>("res://scene/menu/Shop/CaseOpening/CaseItem.tscn");
	private PackedScene Fireworks = ResourceLoader.Load<PackedScene>("res://scene/menu/Shop/CaseOpening/GetItemAnimation.tscn");
	private PackedScene AddCoins = ResourceLoader.Load<PackedScene>("res://scene/TestA.tscn");
	public float Speed = 2500;
	public float Acceleration = -500f;
	public float time = 0;
	private int cases = 0;

	public bool _spinning = true;
	private bool _completed = false;

	private Random Rand = new Random();
	private int _getItemId;
	private Button ExitButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_getItemId = roll();
		for (int i = 0; i < 51; i++)
		{
			CaseItem item = CaseItem.Instantiate<CaseItem>();
			AddChild(item);
			int textureId = roll();
			item.ZIndex = -3;
			item.Position -= new Vector2(120 * i, 0);
			item.Texture = getTexture(textureId);
			item.RarityBackground.Scale = new Vector2(1.2f, 1.2f);
			if (Test.ItemData[textureId].AsGodotDictionary()["rarity"].ToString() == "common")
				item.RarityBackground.Frame = 1;
			else if (Test.ItemData[textureId].AsGodotDictionary()["rarity"].ToString() == "rare")
				item.RarityBackground.Frame = 2;
			else if (Test.ItemData[textureId].AsGodotDictionary()["rarity"].ToString() == "epic")
				item.RarityBackground.Frame = 3;

			if (i == 47)
			{
				item.Texture = getTexture(_getItemId);
				if (Test.ItemData[_getItemId].AsGodotDictionary()["rarity"].ToString() == "common")
					item.RarityBackground.Frame = 1;
				else if (Test.ItemData[_getItemId].AsGodotDictionary()["rarity"].ToString() == "rare")
					item.RarityBackground.Frame = 2;
				else if (Test.ItemData[_getItemId].AsGodotDictionary()["rarity"].ToString() == "epic")
					item.RarityBackground.Frame = 3;
			}
		}

		ExitButton = GetNode<Button>("ExitButton");
		ExitButton.Pressed += exitButtonPressed;
		ExitButton.Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if(Speed > 0)
		{
			Speed += Acceleration * (float)delta;
			Acceleration -= 10f * (float)delta;
		}

		else if(Speed <= 0 && _spinning)
		{
			_spinning = false;
		}

		if(!_spinning && !_completed)
		{
			if (Test.OwnedSkins[_getItemId] == false)
			{
				Test.OwnedSkins[_getItemId] = true;
			}
			else
			{
				Test.Money += 1;
				GetParent().GetNode<Label>("coinCont/coins").Text = Test.Money.ToString();
				TestA addCoins = AddCoins.Instantiate<TestA>();
				AddChild(addCoins);
				addCoins.GetNode<Sprite2D>("Sprite2D").Show();
			}

			_completed = true;
			Test.saveGame();
			ExitButton.Show();

			GetItemAnimation fireworks = Fireworks.Instantiate<GetItemAnimation>();
			AddChild(fireworks);
			fireworks.Scale += new Vector2(1, 1);
			fireworks.Play();

			CaseItem item = CaseItem.Instantiate<CaseItem>();
			AddChild(item);
			if (Test.ItemData[_getItemId].AsGodotDictionary()["rarity"].ToString() == "common")
				item.RarityBackground.Frame = 1;
			else if (Test.ItemData[_getItemId].AsGodotDictionary()["rarity"].ToString() == "rare")
				item.RarityBackground.Frame = 2;
			else if (Test.ItemData[_getItemId].AsGodotDictionary()["rarity"].ToString() == "epic")
				item.RarityBackground.Frame = 3;

			item.Texture = getTexture(_getItemId);
			item.Scale += new Vector2(2, 2);
			item.Rect.Show();
			item.GlobalPosition = fireworks.GlobalPosition;
			GetNode<Label>("SkinGet").Show();
		}
	}

	private int roll()
	{
		int itemId = 0;
		int randomNumber;
		int randomSkinNumber;
		int[] commons = {0, 2, 3, 4, 5, 6, 7};
		int[] rares = {1, 8};
		int[] superRares = {9};

		randomNumber = Rand.Next(0, 100);
		GD.Print(randomNumber);
		switch(randomNumber)
		{
			case int n when (n >= 0 && n <= 75):
				randomSkinNumber = Rand.Next(0, commons.Length);
				itemId = commons[randomSkinNumber];
				break;

			case int n when (n >= 76 && n <= 98):
				randomSkinNumber = Rand.Next(0, rares.Length);
				itemId = rares[randomSkinNumber];
				break;

			case int n when (n >= 99 && n <= 100):
				randomSkinNumber = Rand.Next(0, superRares.Length);
				itemId = superRares[randomSkinNumber];
				break;

			default:
				GD.Print("error22");
				break;
		}

		return itemId;
	}

	private Texture2D getTexture(int id)
	{
		var skinData = Test.ItemData[id].AsGodotDictionary();
		Texture2D texture = (Texture2D)ResourceLoader.Load((string)skinData["path"]);
		return texture;
	}

	private void exitButtonPressed()
	{
		QueueFree();
	}
}
