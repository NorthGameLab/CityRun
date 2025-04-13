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
    //Changes the score number in the hud
    public void changeScore(int scoreChange)
    {
        Score.Text = scoreChange.ToString();
    }

    //Changes the coins number in the hud
    public void changeCoins(int coinChange)
    {
        coinCont.GetNode<Label>("coins").Text = coinChange.ToString();
    }
}
