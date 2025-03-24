using Godot;
using System;

public partial class CaseOpening : Control
{
	private PackedScene CaseItem = ResourceLoader.Load<PackedScene>("res://scene/menu/Shop/CaseOpening/CaseItem.tscn");
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
		_getItemId = Rand.Next(0, Test.ItemData.Count);
		for (int i = 0; i < 51; i++)
		{
			CaseItem item = CaseItem.Instantiate<CaseItem>();
			AddChild(item);
			int textureId = Rand.Next(0, Test.ItemData.Count);
			item.Position -= new Vector2(120 * i, 0);
			item.Texture = getTexture(textureId);

			if (i == 47)
			{
				item.Texture = getTexture(_getItemId);
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
			Test.OwnedSkins[_getItemId] = true;
			_completed = true;
			Test.saveGame();
			ExitButton.Show();
		}
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
