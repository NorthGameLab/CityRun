using Godot;
using System;

public partial class Shop : Control
{
	Button CaseButton;
	PackedScene CaseOpening = ResourceLoader.Load<PackedScene>("res://scene/menu/Shop/CaseOpening/CaseOpening.tscn");
	CaseOpening Opening;
	private Window InfoWindow;

	public Label CoinsLabel;

	private Button ExitButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InfoWindow = GetNode<Window>("Window");
		InfoWindow.Hide();
		CaseButton = GetNode<Button>("CaseButton");
		CaseButton.Pressed += onButtonPressed;

		ExitButton = GetNode<Button>("ExitButton");
		ExitButton.Pressed += ExitButtonPressed;

		CoinsLabel = GetNode<Label>("coinCont/coins");
		CoinsLabel.Text = Test.Money.ToString();
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
			CoinsLabel.Text = Test.Money.ToString();
			Opening = CaseOpening.Instantiate<CaseOpening>();
			AddChild(Opening);
		}
	}

	private void ExitButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/menu/MainMenu.tscn");
	}
	private void OnInfoPressed()
	{
		InfoWindow.Show();
	}
}
