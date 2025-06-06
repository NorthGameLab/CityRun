using Godot;
using System;
using System.Collections.Generic;

//Class that contains a lot of public static variables and methods
public partial class Test : Node
{
    public static int Score;
    public static int HighScore;
    public static int Money;
    public static int CurrentSkinId = 0;
    public static bool fromQuest = false;
	public static Godot.Collections.Dictionary ItemDataDic = File.getDictionary(File.ItemPath);
    public static Godot.Collections.Array ItemData = ItemDataDic["items"].AsGodotArray();
    public static bool[] OwnedSkins;
    public static int NextArea = 0;
    public static int LastArea = 0;
    public static int CurrentArea = 0;
    //Used to save Building positions to use them again
    public static List<Vector2> BuildingPositions = new List<Vector2>();
    //Used to save Object positions to use them again
    public static List<Vector2> ObjectPositions = new List<Vector2>();
    //Used to save Object2 positions to use them again
    public static List<Vector2> Object2Positions = new List<Vector2>();
    //Used to save Buildings animation frames to use them again
    public static List<int> BuildingFrames = new List<int>();
    public static bool[] _questionsAnswered;

    public static void updateHighScore(int score)
    {
        HighScore = score;
    }

    public static void addToScore(int score)
    {
        Score += score;
    }

    public static void addToMoney(int money)
    {
        Money += money;
    }

    //Creates save data and saves it
    public static void saveGame()
    {
        Data data = new Data();
        data.HighScore = Test.HighScore;
        data.Money = Test.Money;
        data.OwnedSkins = Test.OwnedSkins;
        File.SaveGame(data);
    }

    //Loads save data into the game
    public static void loadGame()
    {
        Data data = File.LoadGame();
        HighScore = data.HighScore;
        Money = data.Money;
        OwnedSkins = data.OwnedSkins;
    }
}