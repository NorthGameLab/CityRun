using Godot;
using System;
using System.Linq;

//Class for creating environment
public partial class Environment : Node
{
    public PackedScene Road = ResourceLoader.Load<PackedScene>("res://scene/Environment/Roads/RoadArea1.tscn");
    public PackedScene Building = ResourceLoader.Load<PackedScene>("res://scene/Environment/Building.tscn");
    public PackedScene TrafficLight = ResourceLoader.Load<PackedScene>("res://scene/Environment/TrafficLight.tscn");
    public PackedScene TrafficLightGreen = ResourceLoader.Load<PackedScene>("res://scene/Environment/TrafficLightGreen.tscn");
    public PackedScene Objects = ResourceLoader.Load<PackedScene>("res://scene/Environment/Obects.tscn");
    public PackedScene Area2Objects = ResourceLoader.Load<PackedScene>("res://scene/Environment/Area2Objects.tscn");
    //when reaches certain value a road spawns
    public float _yRoad;
    //Has crosswalk been spawned
    private bool _spawnedCrosswalk = false;
    public float _lastDistanceObjects, _lastDistanceBuildings, _lastDistanceObjects2;

    //Creates initial environment
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

        if (Test.fromQuest)
        {
            foreach (Vector2 pos in Test.ObjectPositions)
            {
                Obects o = Objects.Instantiate<Obects>();
                AddChild(o);
                o.Position = pos;
            }

            int id = 0;
            foreach (Vector2 pos in Test.BuildingPositions)
            {
                Building b = Building.Instantiate<Building>();
                AddChild(b);
                b.Position = pos;
                b.Frame = Test.BuildingFrames[id];

                id++;
            }

            foreach (Vector2 pos in Test.Object2Positions)
            {
                Area2Objects o2 = Area2Objects.Instantiate<Area2Objects>();
                AddChild(o2);
                o2.Position = pos;
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                Obects o = Objects.Instantiate<Obects>();
                AddChild(o);
                o.GlobalPosition += new Vector2(0, 925 * i);
            }

            for (int i = 0; i < 4; i++)
            {
                Building b = Building.Instantiate<Building>();
                AddChild(b);
                b.GlobalPosition += new Vector2(0, 450 * i);
            }
        }

        if (Test.fromQuest)
        {
            Road crossing = Road.Instantiate<Road>();
            AddChild(crossing);
            crossing.GlobalPosition = new Vector2(0, 528);
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
            light.GlobalPosition = new Vector2(417f, crossing.GlobalPosition.Y - 30);
        }


        _lastDistanceObjects = GameScene._distanceToNext;
        _lastDistanceBuildings = GameScene._distanceToNext;
        _lastDistanceObjects2 = GameScene._distanceToNext;

    }

    //Creates environment as game goes on
    public override void _Process(double delta)
    {

        //Creates road
        _yRoad += GameScene._speed * (float)delta;
        if (_yRoad >= 500)
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

        }

        //Creates objects
        if (Test.CurrentArea == 0 && !_spawnedCrosswalk)
        {
            if (_lastDistanceObjects - GameScene._distanceToNext >= 925 && GameScene._distanceToNext > 500)
            {
                Obects o = Objects.Instantiate<Obects>();
                AddChild(o);
                MoveChild(o, 0);
                _lastDistanceObjects = GameScene._distanceToNext;
            }

            if (_lastDistanceBuildings - GameScene._distanceToNext >= 450 && GameScene._distanceToNext > 0)
            {
                Building b = Building.Instantiate<Building>();
                AddChild(b);
                MoveChild(b, 0);
                _lastDistanceBuildings = GameScene._distanceToNext;
            }
        }
        else if (Test.NextArea == 0 && _spawnedCrosswalk)
        {

            if (_lastDistanceObjects - GameScene._distanceToNext >= 925)
            {
                Obects o = Objects.Instantiate<Obects>();
                AddChild(o);
                MoveChild(o, 0);
                o.Position -= new Vector2(0, 400);
                _lastDistanceObjects = GameScene._distanceToNext;
            }

            if (_lastDistanceBuildings - GameScene._distanceToNext >= 450)
            {
                Building b = Building.Instantiate<Building>();
                AddChild(b);
                MoveChild(b, 0);
                b.Position -= new Vector2(0, 500);
                _lastDistanceBuildings = GameScene._distanceToNext;
            }

        }
        else if (Test.CurrentArea == 1 && !_spawnedCrosswalk)
        {
            if (_lastDistanceObjects2 - GameScene._distanceToNext >= 1100 && GameScene._distanceToNext > 500)
            {
                Area2Objects o2 = Area2Objects.Instantiate<Area2Objects>();
                AddChild(o2);
                MoveChild(o2, 0);
                _lastDistanceObjects2 = GameScene._distanceToNext;
            }
        }
        else if (Test.NextArea == 1 && _spawnedCrosswalk)
        {

            if (_lastDistanceObjects2 - GameScene._distanceToNext >= 1100)
            {
                Area2Objects o2 = Area2Objects.Instantiate<Area2Objects>();
                AddChild(o2);
                MoveChild(o2, 0);
                o2.Position -= new Vector2(0, 525);
                _lastDistanceObjects2 = GameScene._distanceToNext;
            }

        }

        //Spawns a crosswalk
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
    }

    public bool getSpawnedCrosswalk()
    {
        return _spawnedCrosswalk;
    }
}
