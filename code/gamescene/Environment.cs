using Godot;
using System;

public partial class Environment : Node
{
    public PackedScene Road = ResourceLoader.Load<PackedScene>("res://scene/Environment/Road.tscn");
    public PackedScene Building = ResourceLoader.Load<PackedScene>("res://scene/Environment/Building.tscn");
    public float _yRoad;
    public float _yBuilding;
    public float _y;
    private Random rand = new Random();

    public override void _Ready()
    {

        for (int i = -2; i < 6; i++)
        {
            Road r = Road.Instantiate<Road>();
            AddChild(r);
            r.GlobalPosition += new Vector2(0, 525 * i);
        }

        _y = rand.Next(350, 400);
        for (int i = 0; i < 10; i++)
        {
            Building b = Building.Instantiate<Building>();
            AddChild(b);
            b.GlobalPosition += new Vector2(0, _y * i);
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

            if (GameScene._distanceToNext <= 0 && GameScene._distanceToNext >= -500)
            {
                road.Frame = 1;
            }
        }

        _yBuilding += GameScene._speed * (float)delta;
        if (_yBuilding >= _y && (GameScene._distanceToNext >= 200 || GameScene._distanceToNext <= -150))
        {
            Building building = Building.Instantiate<Building>();
            AddChild(building);
            MoveChild(building, 0);
            _yBuilding = 0;
            _y = rand.Next(350, 400);
        }
    }
}
