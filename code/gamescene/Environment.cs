using Godot;
using System;

public partial class Environment : Node
{
    //TESTI
    [Export]
    public PackedScene Rock;

    public override void _Ready()
    {
        //TESTI
        GetNode<Timer>("TestRockTimer").Start();
    }

    //TESTI
    private void onRockTimeOut()
    {
        TestRock rock = Rock.Instantiate<TestRock>();
        AddChild(rock);
        Random rand = new Random();
        GetNode<Timer>("TestRockTimer").WaitTime = 0.3 * rand.Next(1, 10);
    }
}
