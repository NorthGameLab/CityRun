using Godot;
using System;

public abstract partial class Obstacle : Area2D
{

	//Speed of obstacle
	[Export]
	public float _speed = 0f;
	//width of a lane
	public float _width;
	public AnimatedSprite2D Animation;
	// sound effects for the obstacles
	private AudioStreamPlayer2D SoundEffect;
	private AudioStreamPlayer2D SoundEffect2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_width = GetViewportRect().Size.X / 5;
		Animation = GetNode<AnimatedSprite2D>("Animation");
		SoundEffect = GetNode<AudioStreamPlayer2D>("SoundEffect");
		SoundEffect2 = GetNode<AudioStreamPlayer2D>("SoundEffect2");
		Connect("area_entered", new Callable(this, nameof(onAreaEntered)));

		Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Animation.Play();
		GlobalPosition += (GameScene._speed + _speed) * Vector2.Down * (float)delta;
	}
	// checks if player collides with obstacle
	private void onAreaEntered(Node2D body)
	{
		if (body is Player player)
		{
			if (Test.Score > Test.HighScore)
			{
				Test.updateHighScore(Test.Score);
			}
			GameScene._lose = true;
			Test.saveGame();
			GetTree().ChangeSceneToFile("res://scene/menu/GameOver.tscn");
		}
	}

	public virtual void Start()
	{
		Random rand = new Random();
		int spawnPos = rand.Next(1, 4);
		GlobalPosition = new Vector2(spawnPos * _width, -_width * 6);
	}

	// when obstacle appears on screen it palys a sound effect
	// some obstacles have a sound effect which plays only if random number is 2
	public void OnScreenEntered()
	{
		SoundEffect.Play();
		Random rand = new Random();
        int randomNum = rand.Next(1, 3);
		if(randomNum == 2)
		{
			SoundEffect2.Play();;
		}
		else {return;}
	}

	// kills the obstacle sprite
	public void screenExited()
	{
		QueueFree();
	}
}
