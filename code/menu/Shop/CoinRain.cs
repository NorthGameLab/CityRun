using Godot;
using System;

public partial class CoinRain : Node2D
{
	[Export]
	private PackedScene Coin;
	private Timer CoinTimer = new Timer();
	private Random rand = new Random();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddChild(CoinTimer);
		CoinTimer.WaitTime = 0.15;
		CoinTimer.Timeout += timeout;
		CoinTimer.Start();
	}

	private void timeout()
	{
		int num2 = rand.Next(4, 6);

		for (int i = 0; i < num2; i++)
		{
			int num = rand.Next(0, 550);
			int num3 = rand.Next(-100, 0);
			CoinRainCoin coin = Coin.Instantiate<CoinRainCoin>();
			AddChild(coin);
			coin.GlobalPosition += new Vector2(num, num3);
		}
	}
}
