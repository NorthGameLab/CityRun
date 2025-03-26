using Godot;
using System;

public partial class Items : Node
{
    public Timer timer;
    private PackedScene coins = ResourceLoader.Load<PackedScene>("res://scene/item/Coin.tscn");
    private Coin coin = null;
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.Start();
    }

    private void onTimerTimeout()
    {
        if (!GameScene.GetEnvironment().getSpawnedCrosswalk())
        {
            coin = coins.Instantiate<Coin>();
            AddChild(coin);
        }
    }
}
