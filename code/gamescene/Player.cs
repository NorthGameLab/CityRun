using Godot;
using System;
public partial class Player : Area2D
{
	//KAISTAN LEVEYS
	private float _width;

	//LANE JOLLA PELAAJA ON
	private int _currentLane;

	//ELI SIIS LANE JOLLA PITÄISI LIIKKUA MUTTA EI OLE VIELÄ LIIKUTTU
	private int _currentLane2;

	//KAISTAN VAIHDON NOPEUS
	private float _laneChangeSpeed = 1100f;

	//EN TIEDÄ
	private float _xToGo;

	private bool _moving = false;

	private AnimatedSprite2D _animation = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_width = GetViewport().GetVisibleRect().Size.X / 5;

		Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animation.Play();
		_animation.SpeedScale = (float)(GameScene._speed / GameScene._maxSpeed);

		//LIIKKUMISKOODI TOIMII JOTENKIN MUTTA PITÄÄ MUUTTAA EHKÄ
		if (!_moving)
		{
			GlobalPosition = new Vector2(_width * _currentLane, GetViewport().GetVisibleRect().Size.Y - _width - 40);
		}

		if (Input.IsActionJustPressed("right") && _currentLane2 < 3)
		{
			_xToGo += _width;
			_currentLane2 += 1;
			if (_currentLane != _currentLane2)
			{
				_moving = true;
			}
		}

		if (Input.IsActionJustPressed("left") && _currentLane2 > 1)
		{
			_xToGo -= _width;
			_currentLane2 -= 1;
			if (_currentLane != _currentLane2)
			{
				_moving = true;
			}
		}

		if(_moving)
		{
			if (GlobalPosition.X < _xToGo)
			{
				GlobalPosition += new Vector2((_xToGo - GlobalPosition.X + _laneChangeSpeed) * (float)delta, 0);
			}
			else if (GlobalPosition.X > _xToGo)
			{
				GlobalPosition -= new Vector2((GlobalPosition.X - _xToGo + _laneChangeSpeed) * (float)delta, 0);
			}

			if (Math.Abs(_xToGo - GlobalPosition.X) < 20)
			{
				_moving = false;
				_currentLane = _currentLane2;
			}
		}
	}


/*
	//MITÄ TAPAHTUU KUN PELAAJAAN OSUU
	private void onAreaEntered(Node2D body)
	{
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
		if (Test.Score > Test.HighScore)
		{
			Test.updateHighScore(Test.Score);
		}
		GameScene._lose = true;
		GetTree().ChangeSceneToFile("res://scene/menu/GameOver.tscn");
	}
	*/

	//START
	public void Start()
	{
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;

		_currentLane = 2;
		_xToGo = _width * _currentLane;
		_currentLane2 = _currentLane;
	}
}
