using Godot;
using System;

public partial class Shop : Control
{
	// button for buying a case
	TextureButton BuyButton;
	PackedScene CaseOpening = ResourceLoader.Load<PackedScene>("res://scene/menu/Shop/CaseOpening/CaseOpening.tscn");
	CaseOpening Opening;
	// window for all the rarity odds
	private Window InfoWindow;
	public static AnimatedSprite2D ChestOpen;

	// coin count
	public Label CoinsLabel;

	// button for exiting the shop
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
	// checks the money count and opens a case if coin count is over 10
	private void onButtonPressed()
	{
		if (Test.Money >= 20)
		{
			Test.Money -= 20;
			CoinsLabel.Text = Test.Money.ToString();
			ChestOpen.Play();
		}
	}
	// exits the shop
	private void ExitButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/menu/MainMenu.tscn");
	}
	// shows rarity info
	private void OnInfoPressed()
	{
		InfoWindow.Show();
	}
	// hides the info window
	private void OnExitPressed()
	{
		InfoWindow.Hide();
	}

	//Called when chestopen animation loops to open the chest
	private void chestOpened()
	{
		ChestOpen.Stop();
		ChestOpen.Frame = 8;
		Opening = CaseOpening.Instantiate<CaseOpening>();
		AddChild(Opening);
	}
}
