using Godot;
using System;

public partial class Obstacles : Node
{
    public Timer ObstacleSpawnTimer;
    //Initial time for obstacle spawn timer
    private float _time = 5f;

    // paths to all obstacle scnes
    private PackedScene Walker = ResourceLoader.Load<PackedScene>("res://scene/obstacles/Walker.tscn");
    private PackedScene Stroller = ResourceLoader.Load<PackedScene>("res://scene/obstacles/Stroller.tscn");
    private PackedScene Scooter = ResourceLoader.Load<PackedScene>("res://scene/obstacles/Scooter.tscn");
    private PackedScene Cyclist = ResourceLoader.Load<PackedScene>("res://scene/obstacles/Cyclist.tscn");
    private PackedScene Kids = ResourceLoader.Load<PackedScene>("res://scene/obstacles/Puisto/Kids.tscn");
    private PackedScene Phonewalker = ResourceLoader.Load<PackedScene>("res://scene/obstacles/Puisto/Phonewalker.tscn");
    private PackedScene Dogwalker = ResourceLoader.Load<PackedScene>("res://scene/obstacles/Puisto/Dogwalker.tscn");
    public override void _Ready()
    {
        // timer for obstacle spawn frequency
        ObstacleSpawnTimer = GetNode<Timer>("ObstacleSpawnTimer");

        Start();
    }

    public override void _Process(double delta)
    {
        ObstacleSpawnTimer.WaitTime = _time - 3 * (GameScene._speed / 1000);
        //GD.Print(ObstacleSpawnTimer.WaitTime);
        if (GameScene.GetEnvironment().getSpawnedCrosswalk())
        {
            ObstacleSpawnTimer.Stop();
        }
    }

    // when timer runs out, an obstacle spawns
    private void onObstacleSpawnTimerTimeout()
    {
        // checks area for different obstacles
        if (Test.CurrentArea == 0)
        {
            Random rand = new Random();
            int obstacleNum = rand.Next(1, 9);
            Obstacle obs;

            // cases for spawning obstacles.
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

                case 5:
                    obs = Walker.Instantiate<Walker>();
                    break;

                case 6:
                    obs = Scooter.Instantiate<Scooter>();
                    break;

                case 7:
                    obs = Scooter.Instantiate<Scooter>();
                    break;

                case 8:
                    obs = Walker.Instantiate<Walker>();
                    break;

                default:
                    obs = Walker.Instantiate<Walker>();
                    break;
            }
            AddChild(obs);
        }
        else if (Test.CurrentArea == 1)
        {
            Random rand = new Random();
            int obstacleNum = rand.Next(1, 5);
            Obstacle obs;

            switch(obstacleNum)
            {
                case 1:
                    obs = Kids.Instantiate<Kids>();
                    break;

                case 2:
                    obs = Phonewalker.Instantiate<Phonewalker>();
                    break;

                case 3:
                    obs = Dogwalker.Instantiate<Dogwalker>();
                    break;

                case 4:
                    obs = Cyclist.Instantiate<Cyclist>();
                    break;

                default:
                    obs = Walker.Instantiate<Walker>();
                    break;
            }
            AddChild(obs);
        }
    }

    // start timer
    private void Start()
    {
        ObstacleSpawnTimer.Start();
    }

}
