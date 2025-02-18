using Godot;
using System;

public partial class Hud : Node
{
    private Label Score;
    private HBoxContainer coinCont;
    private Label Distance;
    public override void _Ready()
    {
        Score = GetNode<Label>("Score");
        coinCont = GetNode<HBoxContainer>("coinCont");
        Distance = GetNode<Label>("distance");
        changeCoins(Test.Money);

        Distance.Hide();
    }
    public void changeScore(int scoreChange)
    {
        Score.Text = scoreChange.ToString();
    }

    public void changeCoins(int coinChange)
    {
        coinCont.GetNode<Label>("coins").Text = coinChange.ToString();
    }

    public void changeDistance(int distance)
    {
        Distance.Text = "to next: " + distance.ToString() + "m";
    }
}
