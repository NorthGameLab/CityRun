using Godot;
using System;

public partial class Obstacles : Node
{
    public static float _speed = 1000f;
    public static float _changeWaitTime = 0.0001f;
    public Timer ObstacleSpawnTimer;
    [Export]
    private PackedScene Walker;
    [Export]
    private PackedScene Stroller;
    public override void _Ready()
    {
        ObstacleSpawnTimer = GetNode<Timer>("ObstacleSpawnTimer");
        GetNode<Timer>("ObstacleSpawnTimer").Start();

        start();
    }

    public override void _Process(double delta)
    {
        _speed += 0.1f;
        ObstacleSpawnTimer.WaitTime -= _changeWaitTime;
    }

    private void onObstacleSpawnTimerTimeout()
    {
        Random rand = new Random();
        int obstacleNum = rand.Next(1, 3);

        switch(obstacleNum)
        {
            case 1:
                Walker walk = Walker.Instantiate<Walker>();
                AddChild(walk);
                break;

            case 2:
                Stroller strol = Stroller.Instantiate<Stroller>();
                AddChild(strol);
                break;

            default:
                GD.Print("die");
                break;
        }
    }

    private void start()
    {
        ObstacleSpawnTimer.Start();
    }

}
