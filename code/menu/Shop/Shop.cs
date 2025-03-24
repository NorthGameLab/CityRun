using Godot;
using System;

public partial class Shop : Control
{
	Button CaseButton;
	PackedScene CaseOpening = ResourceLoader.Load<PackedScene>("res://scene/menu/Shop/CaseOpening/CaseOpening.tscn");
	CaseOpening Opening;

	private Button ExitButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CaseButton = GetNode<Button>("CaseButton");
		CaseButton.Pressed += onButtonPressed;

		ExitButton = GetNode<Button>("ExitButton");
		ExitButton.Pressed += ExitButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void onButtonPressed()
	{
		if (Test.Money >= 10)
		{
			Test.Money -= 10;
			Opening = CaseOpening.Instantiate<CaseOpening>();
			AddChild(Opening);
		}
	}

	private void ExitButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/menu/MainMenu.tscn");
	}
}
