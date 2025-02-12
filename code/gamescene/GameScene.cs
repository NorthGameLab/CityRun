using Godot;
using System;

public partial class GameScene : Node
{
    public Player Player;
    public Obstacles Obstacles;
    public Hud Hud;

    //TESTI
    [Export]
    public PackedScene Rock;

    public static float _speed = 400f;
    public override void _Ready()
    {
        Player = GetNode<Player>("Player");
        Obstacles = GetNode<Obstacles>("Obstacles");
        Hud = GetNode<Hud>("Hud");
        Test.Score = 0;

        //TESTI
        GetNode<Timer>("TestRockTimer").Start();
    }

    public override void _Process(double delta)
    {
        Test.addToScore(1);
        Hud.changeScore(Test.Score);
    }

    //TESTI
    private void onRockTimerTimeout()
    {
        TestRock rock = Rock.Instantiate<TestRock>();
        AddChild(rock);
        Random rand = new Random();
        GetNode<Timer>("TestRockTimer").WaitTime = 0.2 * rand.Next(1, 5);
    }
}
