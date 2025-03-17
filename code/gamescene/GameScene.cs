using Godot;
using System;

public partial class GameScene : Node
{
    public Player Player;
    public Obstacles Obstacles;
    public Hud Hud;
    public Control PauseMenu;
    public ColorRect Background;



    //VAIKUTTAA KAIKEN NOPEUTEEN
    public static float _speed = 0f;
    public static float _maxSpeed = 500f;

    public static float _acceleration = 200f;

    public static bool _lose = false;

    public static float _distance = 0f;
    public static float _distanceToNext = 10000;
    private float _distanceToNextUp = 10000;
    public static int _timesQuest = 0;

    private bool _goingToQuest = false;
    private float _deceleration;
    private float _distanceToQuest = 650;

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
        PauseMenu = GetNode<Control>("PauseMenu");
        PauseMenu.Hide();
        Background = GetNode<ColorRect>("Background");
        Background.Hide();
    }

    public override void _Process(double delta)
    {
        Test.addToScore(1);
        Hud.changeScore(Test.Score);
        Hud.changeCoins(Test.Money);

        if(!_goingToQuest)
        {
            Hud.changeDistance(((int)_distanceToNext + 700) / 100);
        }

        if (!_goingToQuest)
        {
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
        }
        else
        {
            _distanceToQuest -= _speed * (float)delta;
            _speed += _deceleration * (float)delta;
        }

        //GD.Print("Speed: " + _speed);
        //GD.Print("distanceToQuest: " + _distanceToQuest);

        _distance += _speed * (float)delta;

        _distanceToNext -= _speed * (float)delta;
        // GD.Print("Speed: " + _speed);
        // GD.Print("maxSpeed: " + _maxSpeed);

        if(_goingToQuest && _speed <= 0)
        {
            _timesQuest++;
            _distanceToNext = 10000 + (_timesQuest * _distanceToNextUp);
            _goingToQuest = false;
            GetTree().ChangeSceneToFile("res://scene/menu/Quest.tscn");
        }

        if (_distanceToNext <= -700 && !_goingToQuest)
        {
            _goingToQuest = true;
            _distanceToQuest = 1000;
            float time = _distanceToQuest / _speed;
            _deceleration = -_speed / time;
        }
    }

    public static void resetGame()
    {
        Test.Score = 0;
        _distance = 0;
        _speed = 0;
        _maxSpeed = 500f;
        _distanceToNext = 5050;
    }

    private void OnPauseButtonPressed()
    {
        GetTree().Paused = true;
        PauseMenu.Show();
        Background.Show();
        Background.Color = new Color(0,0,0,0.5f);
    }
    private void OnResumePressed()
    {
        PauseMenu.Hide();
        GetTree().Paused = false;
        Background.Hide();
    }
    private void OnMainMenuPressed()
    {
        PauseMenu.Hide();
        GetTree().Paused = false;
        resetGame();
        GetTree().ChangeSceneToFile("res://scene/menu/MainMenu.tscn");
    }
    private void OnRetryPressed()
    {
        PauseMenu.Hide();
        GetTree().Paused = false;
        resetGame();
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");

    }
}
