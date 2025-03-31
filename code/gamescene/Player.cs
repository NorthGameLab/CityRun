using Godot;
using System;

public partial class Player : Area2D
{
	public enum Direction
	{
		None = 0,
		Left,
		Right,
		Up,
		Down
	}
	//KAISTAN LEVEYS
	private float _width;

	//LANE JOLLA PELAAJA ON
	public static int _currentLane;

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

		_animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var currentSkinData = Test.ItemData[Test.CurrentSkinId].AsGodotDictionary();
		SpriteFrames frames = new SpriteFrames();
		int frameCount = (int)currentSkinData["frames"];
		Texture2D textureInit = (Texture2D)ResourceLoader.Load((string)currentSkinData["animPath"]);
		Image textureImage = textureInit.GetImage();
		if (frameCount > 1)
		{
			var frameRegion = new Rect2I();
			int framesX = (int)currentSkinData["framesX"];
			int framesY = (int)currentSkinData["framesY"];
			var size = textureImage.GetSize();
			var sizeX = size.X / framesX;
			var sizeY = size.Y / framesY;
			for(int y = 0; y < framesY ; y++)
			{
				for(int x = 0; x < framesX; x++)
				{
					if (x != 2 || y != 2)
					{
						frameRegion = new Rect2I(x * sizeX, y * sizeY, sizeX, sizeY);
						var frameTexture = ImageTexture.CreateFromImage(textureImage.GetRegion(frameRegion));
						frames.AddFrame("default", frameTexture);
					}
				}
			}
		}
		else
		{
			frames.AddFrame("default", textureInit);
		}
		_animation.SpriteFrames = frames;
		if(frameCount > 1)
			_animation.Scale = new Vector2(4.4f, 4.4f);
		else
			_animation.Scale = new Vector2(1f, 1f);
		_animation.GlobalPosition = new Vector2(_width * 3 - (_width / 2) - 8, 900);
        _animation.Show();
		_animation.Play();

		var shape = GetNode("CollisionShape2D");


		Start();
	}

	#region Swipe data
	private Vector2 _touchStartPosition = default(Vector2);
	private float _swipeThreshold = 30.0f;
	private Direction _swipeDirection = Direction.None;

	#endregion

   private bool _swipeDetected = false; // Tracks if a swipe has already been detected

public override void _UnhandledInput(InputEvent @event)
{
    base._UnhandledInput(@event);

    if (@event is InputEventScreenTouch touchEvent)
    {
        if (touchEvent.Pressed)
        {
            _swipeDetected = false;
            _touchStartPosition = touchEvent.Position;
        }
    }
    else if (@event is InputEventScreenDrag dragEvent && !_swipeDetected)
    {
        Vector2 touchCurrentPosition = dragEvent.Position;

        float swipeThreshold = 50.0f;

        if (_touchStartPosition.DistanceTo(touchCurrentPosition) > swipeThreshold)
        {
            DetectSwipe(_touchStartPosition, touchCurrentPosition);
            _swipeDetected = true;
        }
    }
}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		_animation.SpeedScale = (float)((GameScene._speed / GameScene._maxSpeed) * (GameScene._maxSpeed / 250));

		//LIIKKUMISKOODI TOIMII JOTENKIN MUTTA PITÄÄ MUUTTAA EHKÄ
		if (!_moving)
		{
			GlobalPosition = new Vector2(_width * _currentLane, GetViewport().GetVisibleRect().Size.Y - _width - 40);
		}

		if (_swipeDirection == Direction.Right && _currentLane2 < 3)
		{
			_xToGo += _width;
			_currentLane2 += 1;
			if (_currentLane != _currentLane2)
			{
				_moving = true;
			}
			_swipeDirection = Direction.None;
		}

		if (_swipeDirection == Direction.Left && _currentLane2 > 1)
		{
			_xToGo -= _width;
			_currentLane2 -= 1;
			if (_currentLane != _currentLane2)
			{
				_moving = true;
			}
			_swipeDirection = Direction.None;
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

	// Tarkistaa swipen onko oikea
	private void DetectSwipe(Vector2 swipeStart, Vector2 swipeEnd)
	{
		Vector2 swipeVector = swipeEnd - swipeStart;

		if(swipeVector.Length() > _swipeThreshold)
		{
			// swipe is correct
			float lengthX = Mathf.Abs(swipeVector.X);
			float lengthY = Mathf.Abs(swipeVector.Y);

			if (lengthX > lengthY)
			{

				if (swipeVector.X > 0)
				{
					_swipeDirection = Direction.Right;
					//GD.Print("liikkuu oikealle");
				}
				else
				{
					_swipeDirection = Direction.Left;
					//GD.Print("liikkuu vasemmalle");
				}
			}
			else
			{
				if (swipeVector.Y > 0)
				{
					// down
					_swipeDirection = Direction.Down;
					//GD.Print("alas");
				}
				else
				{
					// up
					_swipeDirection = Direction.Up;
					//GD.Print("ylös");
				}
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

		//_currentLane = 2;
		_xToGo = _width * _currentLane;
		_currentLane2 = _currentLane;
	}
}
