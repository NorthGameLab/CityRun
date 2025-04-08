using Godot;
using System;

public partial class GameScene : Node
{
    // hud
    public Hud Hud;
    // pause menu
    public Control PauseMenu;
    // pause menu background
    public ColorRect Background;
    // soundeffect for coin
    private AudioStreamPlayer2D _coinSound;
    // soundeffect for correct answer
    private AudioStreamPlayer2D _correct;



    //game speed
    public static float _speed = 0f;
    //starting max game speed
    public static float _maxSpeed = 500f;
    //max max game speed
    public static float _maxMaxSpeed = 1000f;
    //acceleration of game speed
    public static float _acceleration = 200f;
    //Is the game lost
    public static bool _lose = false;
    //distance traveled
    public static float _distance = 0f;
    //initial distance to next question
    public static float _distanceToNextInit = 1000;
    //distance to next question
    public static float _distanceToNext = _distanceToNextInit;
    //how much distance to next question increases with each question
    private float _distanceToNextUp = 1000;
    //how many questions have happened
    public static int _timesQuest = 0;
    //is next question coming triggered
    private bool _goingToQuest = false;
    //deceleration of game speed
    private float _deceleration;
    //distance to question when going to question
    private float _distanceToQuest = 650;
    //increase to max speed
    private float _maxSpeedIncrease = 6f;
    private static Environment environment;

    public override void _Ready()
    {
        _coinSound = GetNode<AudioStreamPlayer2D>("Collect");
        _correct = GetNode<AudioStreamPlayer2D>("Correct");
        environment = GetNode<Environment>("Environment");
        Hud = GetNode<Hud>("Hud");

        //resets game if player lost
        if (_lose)
        {
            resetGame();
            _lose = false;
        }
        PauseMenu = GetNode<Control>("PauseMenu");
        PauseMenu.Hide();
        Background = GetNode<ColorRect>("Background");
        Background.Hide();

        //changes area
        if (_timesQuest != 0 && _timesQuest % 2 == 0 && Test.NextArea == 0)
        {
            Test.NextArea = 1;
        }
        else if (Test.NextArea == 1)
        {
            Test.NextArea = 0;
        }

        // plays the audio if answered correctly to a question

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


        //if not going to question speed increases and if going to question speed decreases
        if (!_goingToQuest)
        {
            if (_maxSpeed < _maxMaxSpeed)
            {
                if (_speed == _maxSpeed)
                {
                    _maxSpeed += _maxSpeedIncrease * (float)delta;
                    _speed = _maxSpeed;
                }
                else
                {
                    _maxSpeed += _maxSpeedIncrease * (float)delta;;
                }
                GD.Print(_maxSpeed);
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

        _distance += _speed * (float)delta;
        _distanceToNext -= _speed * (float)delta;

        if(_goingToQuest && _speed <= 0)
        {
            _timesQuest++;
            _distanceToNext = 3000 + (_timesQuest * _distanceToNextUp);
            _goingToQuest = false;

            Test.BuildingPositions.Clear();
            Test.BuildingFrames.Clear();
            Test.ObjectPositions.Clear();
            Test.Object2Positions.Clear();

            //Stores environment objects to spawn them again
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

    //resets variables to original values
    public static void resetGame()
    {
        Test.Score = 0;
        Player._currentLane = 2;
        _distance = 0;
        _speed = 0;
        _maxSpeed = 500f;
        _distanceToNext = _distanceToNextInit;
        _timesQuest = 0;
        Test.fromQuest = false;
        Test.NextArea = 0;
        Test.CurrentArea = 0;
        Test.LastArea = 0;
        Test._questionsAnswered = null;
    }

    // open pause menu
    private void OnPauseButtonPressed()
    {
        GetTree().Paused = true;
        PauseMenu.Show();
        Background.Show();
        Background.Color = new Color(0,0,0,0.5f);
    }
    // game continues
    private void OnResumePressed()
    {
        PauseMenu.Hide();
        GetTree().Paused = false;
        Background.Hide();
    }
    // return to main menu
    private void OnMainMenuPressed()
    {
        PauseMenu.Hide();
        GetTree().Paused = false;
        resetGame();
        Test.saveGame();
        GetTree().ChangeSceneToFile("res://scene/menu/MainMenu.tscn");
    }
    // starts game over
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
    // plays sound for a picked coin
    public void PlaySound(AudioStream sound)
    {
        _coinSound.Stream = sound;
        _coinSound.Play();
    }
    // method for playin the correct sound
    public void PlayCorrectSound()
    {
        _correct.Play();
    }
}
