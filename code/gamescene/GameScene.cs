using Godot;
using System;

public partial class GameScene : Node
{
    public Player Player;
    public Obstacles Obstacles;
    public Hud Hud;


    //VAIKUTTAA KAIKEN NOPEUTEEN
    public static float _speed = 500f;

    public static bool _lose = false;

    public static float _distance = 0f;
    public static int milestones = 1;
    public override void _Ready()
    {
        Player = GetNode<Player>("Player");
        Obstacles = GetNode<Obstacles>("Obstacles");
        Hud = GetNode<Hud>("Hud");

        if (_lose)
        {
            resetGame();
            _lose = false;
        }
    }

    public override void _Process(double delta)
    {
        Test.addToScore(1);
        Hud.changeScore(Test.Score);

        _speed += 0.0001f;
        _distance += _speed * (float)delta;
        GD.Print(_distance);

        if (_distance > 10000 * milestones)
        {
            milestones += 1;
            GetTree().ChangeSceneToFile("res://scene/menu/Quest.tscn");
        }
    }

    public static void resetGame()
    {
        Test.Score = 0;
        _distance = 0;
        milestones = 1;
    }
}
