using Godot;
using System;

public partial class Obstacles : Node
{
    public static float _changeWaitTime = 0.0001f;
    public Timer ObstacleSpawnTimer;
    private PackedScene Walker = ResourceLoader.Load<PackedScene>("res://scene/obstacles/Walker.tscn");
    private PackedScene Stroller = ResourceLoader.Load<PackedScene>("res://scene/obstacles/Stroller.tscn");
    private PackedScene Scooter = ResourceLoader.Load<PackedScene>("res://scene/obstacles/Scooter.tscn");
    private PackedScene Cyclist = ResourceLoader.Load<PackedScene>("res://scene/obstacles/Cyclist.tscn");
    public override void _Ready()
    {
        ObstacleSpawnTimer = GetNode<Timer>("ObstacleSpawnTimer");

        Start();
    }

    public override void _Process(double delta)
    {
        ObstacleSpawnTimer.WaitTime -= _changeWaitTime;
    }

    private void onObstacleSpawnTimerTimeout()
    {
        Random rand = new Random();
        int obstacleNum = rand.Next(1, 7);
        Obstacle obs;

        switch(obstacleNum)
        {
            case 1:
                obs = Walker.Instantiate<Walker>();
                break;

            case 2:
                obs = Stroller.Instantiate<Stroller>();
                break;

            case int n when (n >= 3 && n <= 4):
                obs = Scooter.Instantiate<Scooter>();
                break;

            case int n when (n >= 5):
                obs = Cyclist.Instantiate<Cyclist>();
                break;

            default:
                obs = Walker.Instantiate<Walker>();
                break;
        }
        AddChild(obs);
    }

    private void Start()
    {
        ObstacleSpawnTimer.Start();
    }

}
