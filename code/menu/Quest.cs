using Godot;
// using Microsoft.VisualBasic;
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
    public TextEdit AnsInfo;

    private int questionNum;

    [Export] Window InfoWindow;

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

        _animation.Play();
        _animation.SpeedScale = 1;

        InfoWindow.Hide();

        TextureButton option1 = GetNode<TextureButton>("CanvasLayer/VBoxContainer/Option1");
        TextureButton option2 = GetNode<TextureButton>("CanvasLayer/VBoxContainer/Option2");
        Label text1 = option1.GetNode<Label>("Label");
        Label text2 = option2.GetNode<Label>("Label");
        Label question = GetNode<Label>("CanvasLayer/Label");

        Random rand = new Random();
        questionNum = 0;

        Godot.Collections.Dictionary data = File.getQuestions();
        questionNum = rand.Next(0, data["questions"].AsGodotArray().Count);



        question.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["question"].AsString();

        int flip = rand.Next(0, 2);
        if (flip == 0)
        {
            text1.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["wrongAnswer"].AsString();
            text2.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["correctAnswer"].AsString();
            oneIsCorrect = false;
        }
        else
        {
            text1.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["correctAnswer"].AsString();
            text2.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["wrongAnswer"].AsString();
            oneIsCorrect = true;
        }
    }

    public override void _Process(double delta)
    {

    }

    private void ShowInfo()
    {

        GD.Print("Infowindow called");
        InfoWindow.Visible = true;

        Godot.Collections.Dictionary data = File.getQuestions();
        AnsInfo = GetNode<TextEdit>("CanvasLayer/info/TextEdit");

        AnsInfo.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["info"].AsString();
    }

    private void OnInfoCloseRequested()
    {
        GD.Print("close called");
        InfoWindow.Visible = false	;
        test.Position += new Vector2(-150, 600);
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");

    }

    private void button1Pressed()
    {

        if (oneIsCorrect)
        {
            test = testA.Instantiate<TestA>();
            test.Text = "OIKEIN";
            GetParent().AddChild(test);

            scoreAdd = testA.Instantiate<TestA>();
            scorePlusSet();
            GetParent().AddChild(scoreAdd);

            test.Position += new Vector2(-150, 600);
            GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
        }
        else
        {
            test = testA.Instantiate<TestA>();
            test.Text = "VÄÄRIN";
            GetParent().AddChild(test);
            ShowInfo();
        }
    }

    private void button2Pressed()
    {
        if (oneIsCorrect)
        {
            test = testA.Instantiate<TestA>();
            test.Text = "VÄÄRIN";
            GetParent().AddChild(test);
            ShowInfo();
        }
        else
        {
            test = testA.Instantiate<TestA>();
            test.Text = "OIKEIN";
            GetParent().AddChild(test);

            scoreAdd = testA.Instantiate<TestA>();
            scorePlusSet();
            GetParent().AddChild(scoreAdd);

            test.Position += new Vector2(-150, 600);
            GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
        }
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
