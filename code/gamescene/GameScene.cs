using Godot;
using System;

public partial class GameScene : Node
{
    public Player Player;
    public Obstacles Obstacles;
    public Hud Hud;


    //VAIKUTTAA KAIKEN NOPEUTEEN
    public static float _speed = 0f;
    public static float _maxSpeed = 500f;

    public static float _acceleration = 200f;

    public static bool _lose = false;

    public static float _distance = 0f;
    public static float _distanceToNext = 200;
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
        Hud.changeCoins(Test.Money);
        Hud.changeDistance((int)_distanceToNext);

        if (_speed == _maxSpeed)
        {
            _maxSpeed += 0.1f;
            _speed = _maxSpeed;
        }
        else
        {
            _maxSpeed += 0.1f;
        }

        if (_speed < _maxSpeed)
        {
            _speed += _acceleration * (float)delta;
        }
        else
        {
            _speed = _maxSpeed;
        }

        _distance += _speed * (float)delta / 100;
        _distanceToNext -= _speed * (float)delta / 100;
        GD.Print("Speed: " + _speed);
        GD.Print("maxSpeed: " + _maxSpeed);

        if (_distanceToNext <= 0)
        {
            GetTree().ChangeSceneToFile("res://scene/menu/Quest.tscn");
            _distanceToNext = 1000;
        }
    }

    public static void resetGame()
    {
        Test.Score = 0;
        _distance = 0;
        _speed = 0;
        _maxSpeed = 500f;
        _distanceToNext = 200;
    }
}
