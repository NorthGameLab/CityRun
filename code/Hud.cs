using Godot;
using System;

public partial class Hud : Node
{
    private Label Score;
    public override void _Ready()
    {
        Score = GetNode<Label>("Score");
    }
    public void changeScore(int scoreChange)
    {
        Score.Text = scoreChange.ToString();
    }
}
