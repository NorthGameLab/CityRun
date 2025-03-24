using Godot;
using System;

public abstract partial class Obstacle : Area2D
{

	[Export]
	public float _speed = 0f;
	public float _width;
	public AnimatedSprite2D Animation;
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
		GlobalPosition = new Vector2(spawnPos * _width, -_width);
	}
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


	public void screenExited()
	{
		QueueFree();
	}
}
