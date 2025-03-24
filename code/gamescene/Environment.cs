using Godot;
using System;

public partial class Environment : Node
{
    public PackedScene Road = ResourceLoader.Load<PackedScene>("res://scene/Environment/Roads/RoadArea1.tscn");
    public PackedScene Building = ResourceLoader.Load<PackedScene>("res://scene/Environment/Building.tscn");
    public PackedScene TrafficLight = ResourceLoader.Load<PackedScene>("res://scene/Environment/TrafficLight.tscn");
    public PackedScene TrafficLightGreen = ResourceLoader.Load<PackedScene>("res://scene/Environment/TrafficLightGreen.tscn");
    public PackedScene Objects = ResourceLoader.Load<PackedScene>("res://scene/Environment/Obects.tscn");
    public PackedScene Area2Objects = ResourceLoader.Load<PackedScene>("res://scene/Environment/Area2Objects.tscn");
    public float _yRoad;
    public float _yBuilding;
    public float _y;
    private Random rand = new Random();
    private bool _spawnedCrosswalk = false;
    private float _area2Y = 1100f;

    public override void _Ready()
    {

        for (int i = -2; i < 6; i++)
        {
            Road r = Road.Instantiate<Road>();
            AddChild(r);
            if (Test.CurrentArea == 1)
                r.Frame = 3;

            r.GlobalPosition += new Vector2(0, 525 * i);
        }


        if (Test.CurrentArea == 0)
        {
            if(Test.fromQuest)
            {
                for (int i = -1; i < 2; i++)
                {
                    Obects o = Objects.Instantiate<Obects>();
                    AddChild(o);
                    o.GlobalPosition += new Vector2(0, 525 * i);
                }
            }
            else
            {
                for (int i = -1; i < 3; i++)
                {
                    Obects o = Objects.Instantiate<Obects>();
                    AddChild(o);
                    o.GlobalPosition += new Vector2(0, 525 * i);
                }
            }

            _y = rand.Next(400, 400);
            for (int i = 0; i < 4; i++)
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
        }
        else if (Test.CurrentArea == 1)
        {
            if(Test.fromQuest)
            {
                for (int i = -1; i < 1; i++)
                {
                    Area2Objects o = Area2Objects.Instantiate<Area2Objects>();
                    AddChild(o);
                    o.GlobalPosition += new Vector2(0, 1100 * i);
                }
            }
            else
            {
                for (int i = -1; i < 3; i++)
                {
                    Area2Objects o = Area2Objects.Instantiate<Area2Objects>();
                    AddChild(o);
                    o.GlobalPosition += new Vector2(0, 1100 * i);
                }
            }
        }

        if (Test.fromQuest)
        {
            Road crossing = Road.Instantiate<Road>();
            AddChild(crossing);
            crossing.GlobalPosition = new Vector2(0, 510);
            if (Test.CurrentArea == 0 && Test.LastArea == 0)
                crossing.Frame = 1;
            else if (Test.CurrentArea == 1 && Test.LastArea == 0)
                crossing.Frame = 2;
            else if (Test.CurrentArea == 1 && Test.LastArea == 1)
                crossing.Frame = 4;
            else if (Test.CurrentArea == 0 && Test.LastArea == 1)
                crossing.Frame = 5;

            TrafficLight light = TrafficLightGreen.Instantiate<TrafficLight>();
            AddChild(light);
            light.GlobalPosition = crossing.GlobalPosition + new Vector2(415, -35);
        }
    }

    public override void _Process(double delta)
    {

        //spawnstuff
        _yRoad += GameScene._speed * (float)delta;
        if (_yRoad >= 525)
        {
            Road road = Road.Instantiate<Road>();
            AddChild(road);
            if (Test.CurrentArea == 1 && !_spawnedCrosswalk)
                road.Frame = 3;
            else if (Test.CurrentArea == 0 && !_spawnedCrosswalk)
                road.Frame = 0;
            else if (Test.NextArea == 0 && _spawnedCrosswalk)
                road.Frame = 0;
            else if (Test.NextArea == 1 && _spawnedCrosswalk)
                road.Frame = 3;

            _yRoad = 0;

            if (Test.CurrentArea == 0 && !_spawnedCrosswalk || Test.NextArea == 0 && _spawnedCrosswalk)
            {
                if (GameScene._distanceToNext <= -460 || GameScene._distanceToNext >= 100)
                {
                    Obects o = Objects.Instantiate<Obects>();
                    AddChild(o);
                }
            }
        }


        if (Test.CurrentArea == 1)
        {
            _area2Y  -= GameScene._speed * (float)delta;
            if (_area2Y <= 0)
            {
                if (Test.CurrentArea == 1 && !_spawnedCrosswalk || Test.NextArea == 1 && _spawnedCrosswalk)
                {
                    if (GameScene._distanceToNext <= -50 || GameScene._distanceToNext >= 700)
                    {
                        Area2Objects o = Area2Objects.Instantiate<Area2Objects>();
                        AddChild(o);
                        MoveChild(o, 0);
                    }
                }
                _area2Y = 1100;
            }
        }

        //spawncrosswalk
        if (GameScene._distanceToNext <= -60 && !_spawnedCrosswalk)
        {
            Road crossing = Road.Instantiate<Road>();
            AddChild(crossing);
            crossing.ZIndex = -2;
            if (Test.CurrentArea == 0 && Test.NextArea == 0)
                crossing.Frame = 1;
            else if (Test.CurrentArea == 0 && Test.NextArea == 1)
                crossing.Frame = 2;
            else if (Test.CurrentArea == 1 && Test.NextArea == 1)
                crossing.Frame = 4;
            else if (Test.CurrentArea == 1 && Test.NextArea == 0)
                crossing.Frame = 5;

            TrafficLight light = TrafficLight.Instantiate<TrafficLight>();
            AddChild(light);
            light.ZIndex = 3;
            _spawnedCrosswalk = true;
        }


        if (Test.CurrentArea == 0 && !_spawnedCrosswalk || Test.NextArea == 0 && _spawnedCrosswalk)
        {
            _yBuilding += GameScene._speed * (float)delta;
            if (_yBuilding >= _y && (GameScene._distanceToNext <= -300 || GameScene._distanceToNext >= 700))
            {
                Building building = Building.Instantiate<Building>();
                AddChild(building);
                MoveChild(building, 0);
                _yBuilding = 0;
                _y = rand.Next(400, 450);
            }
        }
    }
}
