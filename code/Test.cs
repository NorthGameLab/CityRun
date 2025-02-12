using Godot;
using System;

public partial class Test : Node
{
    public static int Score;
    public static int HighScore;

    public static void updateHighScore(int score)
    {
        HighScore = score;
    }

    public static void addToScore(int score)
    {
        Score += score;
    }
}
