using Godot;
using System;

public partial class Shop : Control
{
	TextureButton BuyButton;
	PackedScene CaseOpening = ResourceLoader.Load<PackedScene>("res://scene/menu/Shop/CaseOpening/CaseOpening.tscn");
	CaseOpening Opening;
	private Window InfoWindow;
	public static AnimatedSprite2D ChestOpen;

	public Label CoinsLabel;

	private TextureButton ExitButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InfoWindow = GetNode<Window>("Window");
		InfoWindow.Hide();
		BuyButton = GetNode<TextureButton>("BuyButton");
		BuyButton.Pressed += onButtonPressed;

		ExitButton = GetNode<TextureButton>("ExitButton");
		ExitButton.Pressed += ExitButtonPressed;

		CoinsLabel = GetNode<Label>("coinCont/coins");
		CoinsLabel.Text = Test.Money.ToString();

		ChestOpen = GetNode<AnimatedSprite2D>("BuyButton/ChestOpen");
		ChestOpen.AnimationLooped += chestOpened;
		ChestOpen.Frame = 0;
	}

	private void onButtonPressed()
	{
		if (Test.Money >= 10)
		{
			Test.Money -= 10;
			CoinsLabel.Text = Test.Money.ToString();
			ChestOpen.Play();
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

	private void OnExitPressed()
	{
		InfoWindow.Hide();
	}

	private void chestOpened()
	{
		ChestOpen.Stop();
		ChestOpen.Frame = 8;
		Opening = CaseOpening.Instantiate<CaseOpening>();
		AddChild(Opening);
	}
}
