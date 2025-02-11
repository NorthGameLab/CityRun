using Godot;
using System;

public partial class MainMenu : Node
{
    public CanvasLayer Menu;

    public override void _Ready()
    {
        Menu = GetNode<CanvasLayer>("Menu");
    }
    private void onStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://scene/gamescene/GameScene.tscn");
    }
}
