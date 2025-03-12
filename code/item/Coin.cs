using Godot;
using System;

public partial class Coin : Item
{
    [Export]
    public PackedScene testA = null;
    public TestA test = null;

    public override void onAreaEntered(Node2D body)
    {
		if (body is Player player)
		{
			Test.addToMoney(1);
            test = testA.Instantiate<TestA>();
            test.Position = new Vector2(110, 15);
            GetParent().AddChild(test);
		}

        QueueFree();
    }
}
