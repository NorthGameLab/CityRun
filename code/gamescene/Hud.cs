using Godot;
using System;

public partial class Hud : Node
{
    private Label Score;
    private HBoxContainer coinCont;
    public override void _Ready()
    {
        Score = GetNode<Label>("Score");
        coinCont = GetNode<HBoxContainer>("coinCont");
        changeCoins(Test.Money);
    }
    public void changeScore(int scoreChange)
    {
        Score.Text = scoreChange.ToString();
    }

    public void changeCoins(int coinChange)
    {
        coinCont.GetNode<Label>("coins").Text = coinChange.ToString();
    }
}
