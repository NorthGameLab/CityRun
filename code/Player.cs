using Godot;
using System;
public partial class Player : Area2D
{
	private String testi = "testi";
	private float _width;
	private int _currentLane;
	private int _currentLane2;
	private float _laneChangeSpeed = 1000f;
	private float _xToGo;
	private bool _moving = false;
	private String TESTI222222 = "asdasdasdsadd";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_currentLane = 1;
		_currentLane2 = _currentLane;
		_width = GetViewport().GetVisibleRect().Size.X / 5;
		_xToGo = _width;
		GlobalPosition = new Vector2(_width, GetViewport().GetVisibleRect().Size.Y - _width);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play();

		if (!_moving)
		{
			GlobalPosition = new Vector2(_width * _currentLane, GetViewport().GetVisibleRect().Size.Y - _width);
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

			if (Math.Abs(_xToGo - GlobalPosition.X) < 7)
			{
				_moving = false;
				_currentLane = _currentLane2;
			}
		}
	}

	private void _on_body_entered(Node2D body)
	{
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}

	public void Start()
	{
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}

}
