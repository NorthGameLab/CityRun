using Godot;
using System;

public partial class GameScene : Node
{
    public Hud Hud;
    public Control PauseMenu;
    public ColorRect Background;
    private AudioStreamPlayer2D _sound;
    private AudioStreamPlayer2D _correct;



    //VAIKUTTAA KAIKEN NOPEUTEEN
    public static float _speed = 0f;
    public static float _maxSpeed = 500f;
    public static float _maxMaxSpeed = 1000f;

    public static float _acceleration = 200f;

    public static bool _lose = false;

    public static float _distance = 0f;
    public static float _distanceToNext = 3000;
    private float _distanceToNextUp = 3000;
    public static int _timesQuest = 0;

    private bool _goingToQuest = false;
    private float _deceleration;
    private float _distanceToQuest = 650;
    private static Environment environment;

    public override void _Ready()
    {
        _sound = GetNode<AudioStreamPlayer2D>("Collect");
        _correct = GetNode<AudioStreamPlayer2D>("Correct");
        environment = GetNode<Environment>("Environment");
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


        if (_timesQuest != 0 && _timesQuest % 2 == 0 && Test.NextArea == 0)
        {
            Test.NextArea = 1;
        }
        else if (Test.NextArea == 1)
        {
            Test.NextArea = 0;
        }

        if (Global._isCorrect == true)
        {
            PlayCorrectSound();
            Global._isCorrect = false;
        }
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
            if (_maxSpeed < _maxMaxSpeed)
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
            _distanceToNext = 3000 + (_timesQuest * _distanceToNextUp);
            _goingToQuest = false;

            Test.BuildingPositions.Clear();
            Test.BuildingFrames.Clear();
            Test.ObjectPositions.Clear();
            Test.Object2Positions.Clear();

            foreach (Node node in GetNode<Node>("Environment").GetChildren())
            {
                if (node is Building b)
                {
                    Test.BuildingPositions.Add(b.Position);
                    Test.BuildingFrames.Add(b.Frame);
                }
                else if (node is Obects o)
                {
                    Test.ObjectPositions.Add(o.Position);
                }
                else if (node is Area2Objects o2)
                {
                    Test.Object2Positions.Add(o2.Position);
                }
            }


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
        Player._currentLane = 2;
        _distance = 0;
        _speed = 0;
        _maxSpeed = 500f;
        _distanceToNext = 5050;
        _timesQuest = 0;
        Test.fromQuest = false;
        Test.NextArea = 0;
        Test.CurrentArea = 0;
        Test.LastArea = 0;
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
        Test.saveGame();
        GetTree().ChangeSceneToFile("res://scene/menu/MainMenu.tscn");
    }
    private void OnRetryPressed()
    {
        PauseMenu.Hide();
        GetTree().Paused = false;
        resetGame();
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");

    }
    public static Environment GetEnvironment()
    {
        return environment;
    }

    public void PlaySound(AudioStream sound)
    {
        _sound.Stream = sound;
        _sound.Play();
    }
    public void PlayCorrectSound()
    {
        _correct.Play();
    }
}
