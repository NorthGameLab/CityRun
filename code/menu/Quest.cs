using Godot;
// using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Quest : Node
{
    public bool oneIsCorrect = false;

    [Export]
    public PackedScene testA = null;
    public TestA test = null;
    public TestA scoreAdd = null;
    public int _scoreAdded = 1000;
    public TextEdit AnsInfo;
    string language = Global.Language;

    private int questionNum;
    private static string QuestPath = "res://data/QuestData.json";
    private AudioStreamPlayer2D CorrectSound;
	private AudioStreamPlayer2D WrongSound;
    private Random rand = new Random();

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

        Test.fromQuest = true;
        Test.LastArea = Test.CurrentArea;
        Test.CurrentArea = Test.NextArea;
        Test.NextArea = Test.LastArea;

        CorrectSound = GetNode<AudioStreamPlayer2D>("Correct");
		WrongSound = GetNode<AudioStreamPlayer2D>("Wrong");

        InfoWindow.Position = new Vector2I(70,430);
        InfoWindow.Hide();

        TextureButton option1 = GetNode<TextureButton>("CanvasLayer/VBoxContainer/Option1");
        TextureButton option2 = GetNode<TextureButton>("CanvasLayer/VBoxContainer/Option2");
        Label text1 = option1.GetNode<Label>("Label");
        Label text2 = option2.GetNode<Label>("Label");
        Label question = GetNode<Label>("CanvasLayer/Label");

        questionNum = 0;

        Godot.Collections.Dictionary data = File.getDictionary(QuestPath);

        if(Test._questionsAnswered == null)
        Test._questionsAnswered = new bool[data["questions"].AsGodotArray().Count];

        questionNum = rollQuestionNum();
        // questionNum = 4;
        var questionData = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary();

        question.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["question"].AsGodotDictionary()[language].AsString();

        int frameCount = Int32.Parse(data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["frames"].AsString());
        Texture2D texture = (Texture2D)ResourceLoader.Load(data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["path"].AsString());
        Image image = texture.GetImage();
        Vector2 size = image.GetSize();
        SpriteFrames frames = new SpriteFrames();
        for (int i = 0; i < frameCount; i++)
        {
            var frameRegion = new Rect2I();
            if (frameCount > 1)
            {
                frameRegion = new Rect2I(0, i * 1600, 2240, 1600);
            }
            else
            {
                frameRegion = new Rect2I(0, 0, (int)size.X, (int)size.Y);
            }
            var frameTexture = ImageTexture.CreateFromImage(image.GetRegion(frameRegion));

            frames.AddFrame("default", frameTexture);
        }

        GetNode<AnimatedSprite2D>("CanvasLayer/Control/QuestionPicAnimated").SpriteFrames = frames;
        GetNode<AnimatedSprite2D>("CanvasLayer/Control/QuestionPicAnimated").Show();
        GetNode<AnimatedSprite2D>("CanvasLayer/Control/QuestionPicAnimated").Play();


        int flip = rand.Next(0, 2);
        if (flip == 0)
        {
            text1.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["wrongAnswer"].AsGodotDictionary()[language].AsString();
            text2.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["correctAnswer"].AsGodotDictionary()[language].AsString();
            oneIsCorrect = false;
        }
        else
        {
            text1.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["correctAnswer"].AsGodotDictionary()[language].AsString();
            text2.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["wrongAnswer"].AsGodotDictionary()[language].AsString();
            oneIsCorrect = true;
        }
    }

    public override void _Process(double delta)
    {

    }

    // Info popup, jos vastaa väärin
    private void ShowInfo()
    {

        GD.Print("Infowindow called");
        InfoWindow.Visible = true;

        Godot.Collections.Dictionary data = File.getDictionary(QuestPath);
        AnsInfo = GetNode<TextEdit>("CanvasLayer/info/TextEdit");

        AnsInfo.Text = data["questions"].AsGodotArray()[questionNum].AsGodotDictionary()["info"].AsGodotDictionary()[language].AsString();
    }

    private void OnInfoExitPressed()
    {
        GD.Print("close called");
        InfoWindow.QueueFree();
        test = testA.Instantiate<TestA>();
        test.Text = "VÄÄRIN";
        GetParent().AddChild(test);
        test.Position += new Vector2(120, 600);
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

            test.Position += new Vector2(120, 600);
            Global._isCorrect = true;
            GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
        }
        else
        {
            WrongSound.Play();
            ShowInfo();
        }
    }

    private void button2Pressed()
    {
        if (oneIsCorrect)
        {
            WrongSound.Play();
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

            test.Position += new Vector2(120, 600);
            Global._isCorrect = true;
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

    private int rollQuestionNum()
    {
        List<int> ids = new List<int>();
        if(isAllAnswered())
        {
            for (int i = 0; i < Test._questionsAnswered.Length; i++)
            {
                Test._questionsAnswered[i] = false;
            }
        }
        for (int i = 0; i < Test._questionsAnswered.Length; i ++)
        {
            if (Test._questionsAnswered[i] == false)
            {
                ids.Add(i);
            }
        }

        int index = rand.Next(0, ids.Count);
        int id = ids[index];
        Test._questionsAnswered[id] = true;

        for (int i = 0; i <  Test._questionsAnswered.Length; i++)
        {
            GD.Print( Test._questionsAnswered[i]);
        }

        return id;
    }

    private bool isAllAnswered()
    {
        for (int i = 0; i < Test._questionsAnswered.Length; i++)
        {
            if (Test._questionsAnswered[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}
