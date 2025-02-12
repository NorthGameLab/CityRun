using Godot;
using System;

public partial class Quest : Node
{
    public bool oneIsCorrect = false;
    public override void _Ready()
    {
        Random rand = new Random();
        int randomNum = rand.Next(1, 3);
        switch(randomNum)
        {
            case 1:
                oneIsCorrect = true;
                break;

            case 2:
                oneIsCorrect = false;
                break;

            default:
                GD.Print("broken");
                break;
        }
    }

    public override void _Process(double delta)
    {

    }

    private void button1Pressed()
    {
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
    }

    private void button2Pressed()
    {
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
    }
}
