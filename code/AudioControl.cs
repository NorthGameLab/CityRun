using Godot;
using System;


	public partial class AudioControl : Control
	{
		// volume buttons
		[Export] private TextureButton _volumeUpButton = null;
		[Export] private TextureButton _volumeDownButton = null;
		[Export] private string _busName = "Master";

		// step size for how much sound is altered with one click
		[Export] private float _stepSize = 0.05f;

		// starting volume
		private float _originalVolume = 1.0f;
		//Linear range [0, 1]
		private float _currentVolumeLinear;
		// max and min values for sound linear
		private const float MinValue = 0.01f;
		private const float MaxValue = 1.0f;
		private Settings settings;

		public override void _Ready()
		{
			base._Ready();
			// checks if the audio is assigned, prints error if not
			settings = GetParent().GetNode<Settings>("Settings");
			if (_volumeUpButton == null || _volumeDownButton == null)
			{
				GD.PrintErr("Audio not assigned");
				return;
			}
			// signals for buttons
			_volumeUpButton.Pressed += OnPlusButtonPressed;
			_volumeDownButton.Pressed += OnMinusButtonPressed;
			// alters sound to user previous settings
			if(settings.GetVolume(_busName, out float volumeDB) == true)
			{
				_currentVolumeLinear = volumeDB;
			}
		}
		// updates colume to higher
		private void OnPlusButtonPressed()
		{
			_currentVolumeLinear = Mathf.Clamp(_currentVolumeLinear + _stepSize, MinValue, MaxValue);
			UpdateVolume();
			GD.Print(_currentVolumeLinear);
		}
		// updates volume to lower
		private void OnMinusButtonPressed()
		{
			_currentVolumeLinear = Mathf.Clamp(_currentVolumeLinear - _stepSize, MinValue, MaxValue);
			UpdateVolume();
			GD.Print(_currentVolumeLinear);
		}
		// updates volume and sets it
		private void UpdateVolume()
		{
			float decibelVolume = Mathf.LinearToDb(_currentVolumeLinear);
			settings.SetVolume(_busName, decibelVolume);

		}
	}
