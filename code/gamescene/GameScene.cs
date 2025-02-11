using Godot;
using System;

public partial class GameScene : Node
{
    public Player Player;
    public Obstacles Obstacles;
    public Hud Hud;
    public int _score = 0;

    public static float _speed = 400f;
    public override void _Ready()
    {
        Player = GetNode<Player>("Player");
        Obstacles = GetNode<Obstacles>("Obstacles");
        Hud = GetNode<Hud>("Hud");
    }

    public override void _Process(double delta)
    {
        _score++;
        Hud.changeScore(_score);
    }
}
