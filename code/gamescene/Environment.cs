using Godot;
using System;
using System.Security.AccessControl;

public partial class Environment : Node
{
    public PackedScene Road = ResourceLoader.Load<PackedScene>("res://scene/Environment/Road.tscn");
    public PackedScene Building = ResourceLoader.Load<PackedScene>("res://scene/Environment/Building.tscn");
    public PackedScene TrafficLight = ResourceLoader.Load<PackedScene>("res://scene/Environment/TrafficLight.tscn");
    public PackedScene TrafficLightGreen = ResourceLoader.Load<PackedScene>("res://scene/Environment/TrafficLightGreen.tscn");
    public PackedScene Objects = ResourceLoader.Load<PackedScene>("res://scene/Environment/Obects.tscn");
    public float _yRoad;
    public float _yBuilding;
    public float _y;
    private Random rand = new Random();
    private bool _spawnedCrosswalk = false;

    public override void _Ready()
    {

        for (int i = -2; i < 6; i++)
        {
            Road r = Road.Instantiate<Road>();
            AddChild(r);
            r.GlobalPosition += new Vector2(0, 525 * i);
        }


        for (int i = -1; i < 2; i++)
        {
            Obects o = Objects.Instantiate<Obects>();
            AddChild(o);
            o.GlobalPosition += new Vector2(0, 525 * i);
        }

        _y = rand.Next(350, 400);
        for (int i = 0; i < 10; i++)
        {

            if (Test.fromQuest)
            {
                if (_y * i <= 1250 || _y * i >= 1600 && !Test.fromQuest)
                {
                Building b = Building.Instantiate<Building>();
                AddChild(b);
                b.GlobalPosition += new Vector2(0, _y * i);
                }
            }
            else
            {
                Building b = Building.Instantiate<Building>();
                AddChild(b);
                b.GlobalPosition += new Vector2(0, _y * i);
            }

        }

        if (Test.fromQuest)
        {
            Road crossing = Road.Instantiate<Road>();
            AddChild(crossing);
            crossing.GlobalPosition = new Vector2(0, 510);
            crossing.Frame = 1;

            TrafficLight light = TrafficLightGreen.Instantiate<TrafficLight>();
            AddChild(light);
            light.GlobalPosition = crossing.GlobalPosition + new Vector2(415, -35);
        }
    }

    public override void _Process(double delta)
    {
        _yRoad += GameScene._speed * (float)delta;

        if (_yRoad >= 525)
        {
            Road road = Road.Instantiate<Road>();
            AddChild(road);
            _yRoad = 0;

            if (GameScene._distanceToNext <= -460 || GameScene._distanceToNext >= 100)
            {
                Obects o = Objects.Instantiate<Obects>();
                AddChild(o);
            }
        }

        if (GameScene._distanceToNext <= -60 && !_spawnedCrosswalk)
        {
            Road road = Road.Instantiate<Road>();
            AddChild(road);
            road.ZIndex = -2;
            road.Frame = 1;
            TrafficLight light = TrafficLight.Instantiate<TrafficLight>();
            AddChild(light);
            light.ZIndex = 3;
            _spawnedCrosswalk = true;
        }

        _yBuilding += GameScene._speed * (float)delta;
        if (_yBuilding >= _y && (GameScene._distanceToNext <= -500 || GameScene._distanceToNext >= 500))
        {
            Building building = Building.Instantiate<Building>();
            AddChild(building);
            MoveChild(building, 0);
            _yBuilding = 0;
            _y = rand.Next(350, 400);
        }
    }
}
