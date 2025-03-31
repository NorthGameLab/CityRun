using Godot;
using System;
using System.Linq;

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
    public float _lastDistanceObjects, _lastDistanceBuildings, _lastDistanceObjects2;
    private float _crosswalkY;
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
        /*
        if (Test.CurrentArea == 0)
        {
            if(Test.fromQuest)
            {
                foreach (Vector2 pos in Test.ObjectPositions)
                {
                    Obects o = Objects.Instantiate<Obects>();
                    AddChild(o);
                    o.Position = pos;
                }

                Obects o2 = Objects.Instantiate<Obects>();
                AddChild(o2);
                o2.Position += new Vector2(0, 0);
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


            if (!Test.fromQuest)
            {
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
            else
            {
                int id = 0;
                foreach (Vector2 pos in Test.BuildingPositions)
                {
                    Building b = Building.Instantiate<Building>();
                    AddChild(b);
                    b.Position = pos;
                    b.Frame = Test.BuildingFrames[id];

                    id++;
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
        */
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

    public override void _Process(double delta)
    {

        //spawnstuff
        _yRoad += GameScene._speed * (float)delta;
        if (_yRoad >= 520)
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

            /*
            if (Test.CurrentArea == 0 && !_spawnedCrosswalk || Test.NextArea == 0 && _spawnedCrosswalk)
            {
                if (GameScene._distanceToNext <= -460 || GameScene._distanceToNext >= 100)
                {
                    Obects o = Objects.Instantiate<Obects>();
                    AddChild(o);
                }
            }
            */
        }

        GD.Print("distance: " + GameScene._distanceToNext);
        GD.Print("lastDistance: " + _lastDistanceObjects);
        if (Test.CurrentArea == 0 && !_spawnedCrosswalk)
        {
            if (_lastDistanceObjects - GameScene._distanceToNext >= 925 && GameScene._distanceToNext > 500)
            {
                //GD.Print("spawned");
                Obects o = Objects.Instantiate<Obects>();
                AddChild(o);
                MoveChild(o, 0);
                _lastDistanceObjects = GameScene._distanceToNext;
            }

            if (_lastDistanceBuildings - GameScene._distanceToNext >= 450 && GameScene._distanceToNext > 0)
            {
                //GD.Print("spawned");
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
                //GD.Print("spawned");
                Obects o = Objects.Instantiate<Obects>();
                AddChild(o);
                MoveChild(o, 0);
                o.Position -= new Vector2(0, 400);
                _lastDistanceObjects = GameScene._distanceToNext;
            }

            if (_lastDistanceBuildings - GameScene._distanceToNext >= 450)
            {
                //GD.Print("spawned");
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
                //GD.Print(GameScene._distanceToNext);
                //GD.Print("spawned");
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
                //GD.Print("spawned");
                Area2Objects o2 = Area2Objects.Instantiate<Area2Objects>();
                AddChild(o2);
                MoveChild(o2, 0);
                o2.Position -= new Vector2(0, 525);
                _lastDistanceObjects2 = GameScene._distanceToNext;
            }

        }

        /*
        if (Test.CurrentArea == 1)
        {
            _area2Y -= GameScene._speed * (float)delta;
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
        */
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


        /*
        if (Test.CurrentArea == 0 && !_spawnedCrosswalk || Test.NextArea == 0 && _spawnedCrosswalk)
        {
            _yBuilding += GameScene._speed * (float)delta;
            if (_yBuilding >= _y && (GameScene._distanceToNext <= -400 || GameScene._distanceToNext >= 600))
            {
                Building building = Building.Instantiate<Building>();
                AddChild(building);
                MoveChild(building, 0);
                _yBuilding = 0;
                _y = rand.Next(400, 450);
            }
        }
        */
    }

    public bool getSpawnedCrosswalk()
    {
        return _spawnedCrosswalk;
    }
}
