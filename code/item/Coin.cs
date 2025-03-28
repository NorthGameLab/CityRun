using Godot;
using System;

public partial class Coin : Item
{
    public PackedScene testA = ResourceLoader.Load<PackedScene>("res://scene/TestA.tscn");
    public TestA test = null;

    public override void onAreaEntered(Node2D body)
    {
		if (body is Player player)
		{
			Test.addToMoney(1);
      test = testA.Instantiate<TestA>();
      //test.Position = new Vector2(110, 15);
      GetParent().AddChild(test);
      test.GetNode<Sprite2D>("Sprite2D").Show();

      GameScene gameScene = GetTree().Root.GetNode<GameScene>("GameScene");
      gameScene.PlaySound(GD.Load<AudioStream>("res://Audio/CoinCollect2.mp3"));
		}
  }
}
