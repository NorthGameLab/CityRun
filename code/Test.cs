using Godot;
using System;

public partial class Test : Node
{
    public static int Score;
    public static int HighScore;
    public static int Money;
    public static bool fromQuest = false;

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

        File.SaveGame(data);
    }

    public static void loadGame()
    {
        Data data = File.LoadGame();
        HighScore = data.HighScore;
        Money = data.Money;
    }
}
