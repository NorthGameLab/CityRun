using Godot;
using System;

//Class for items(coin)
public abstract partial class Item : Area2D
{

    public float _width;
    public override void _Ready()
    {
        _width = GetViewportRect().Size.X / 5;
        Connect("area_entered", new Callable(this, nameof(onAreaEntered)));
        Start();
    }

    public override void _Process(double delta)
    {
        GlobalPosition += GameScene._speed * Vector2.Down * (float)delta;
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play();
    }

    public virtual void Start()
	{
		Random rand = new Random();
		int spawnPos = rand.Next(1, 4);
		GlobalPosition = new Vector2(spawnPos * _width, -_width);
	}

    public abstract void onAreaEntered(Node2D body);

	public void screenExited()
	{
		QueueFree();
	}
}
