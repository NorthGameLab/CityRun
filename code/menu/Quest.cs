using Godot;
using System;

public partial class Quest : Node
{
    public bool oneIsCorrect = false;

    [Export]
    public PackedScene testA = null;
    public TestA test = null;
    public TestA scoreAdd = null;
    public int _scoreAdded = 1000;
    private AnimatedSprite2D _animation = null;
    private AnimatedSprite2D _backGroundAnimation = null;
    public override void _Ready()
    {
        // Random rand = new Random();
        // int randomNum = rand.Next(1, 3);
        // switch(randomNum)
        // {
        //     case 1:
        //         oneIsCorrect = true;
        //         break;

        //     case 2:
        //         oneIsCorrect = false;
        //         break;

        //     default:
        //         GD.Print("broken");
        //         break;
        // }
        _animation = GetNode<AnimatedSprite2D>("CanvasLayer/Control/QuestionPicAnimated");
        _backGroundAnimation = GetNode<AnimatedSprite2D>("CanvasLayer/Control2/AnimatedSprite2D");

        _animation.Play();
        _backGroundAnimation.Play();

        _animation.SpeedScale = 1;
        _backGroundAnimation.SpeedScale = 2;
    }

    public override void _Process(double delta)
    {
    }

    private void button1Pressed()
    {
        // if (oneIsCorrect)
        // {
        //     test = testA.Instantiate<TestA>();
        //     test.Text = "OIKEIN";
        //     GetParent().AddChild(test);

        //     scoreAdd = testA.Instantiate<TestA>();
        //     scorePlusSet();
        //     GetParent().AddChild(scoreAdd);
        // }
        // else
        // {
            test = testA.Instantiate<TestA>();
            test.Text = "VÄÄRIN";
            GetParent().AddChild(test);
        // }
        test.Position += new Vector2(-150, 600);
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
    }

    private void button2Pressed()
    {
        // if (oneIsCorrect)
        // {
        //     test = testA.Instantiate<TestA>();
        //     test.Text = "VÄÄRIN";
        //     GetParent().AddChild(test);
        // }
        // else
        // {
            test = testA.Instantiate<TestA>();
            test.Text = "OIKEIN";
            GetParent().AddChild(test);

            scoreAdd = testA.Instantiate<TestA>();
            scorePlusSet();
            GetParent().AddChild(scoreAdd);
        // }
        test.Position += new Vector2(-150, 600);
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
    }

    public void scorePlusSet()
    {
        scoreAdd.Text = "+" + _scoreAdded.ToString();
        Test.addToScore(_scoreAdded);
        scoreAdd.speed = -scoreAdd.speed;
        scoreAdd.duration = 1.0f;
        scoreAdd.GlobalPosition = new Vector2(200, 60);
    }
}
