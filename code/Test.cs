using Godot;
using System;

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

    public static void saveGame()
    {
        Data data = new Data();
        data.HighScore = Test.HighScore;
        data.Money = Test.Money;
        data.OwnedSkins = Test.OwnedSkins;
        File.SaveGame(data);
    }

    public static void loadGame()
    {
        Data data = File.LoadGame();
        HighScore = data.HighScore;
        Money = data.Money;
        OwnedSkins = data.OwnedSkins;
    }

    public static string[] getItemPaths()
    {
		string[] paths = new string[ItemData.Count];
		for (int i = 0; i < ItemData.Count; i++)
		{
			paths[i] = ItemData[i].AsGodotDictionary()["path"].AsString();
		}
        return paths;
    }
}