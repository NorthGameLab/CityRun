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

		//Linear range [0, 1]
		private float _currentVolumeDB;
		// max and min values for sound linear
		private const float MinDecibel = -60.0f;
		private const float MaxDecibel = 0.0f; // 0 dB is full volume

		private Settings settings;

		public override void _Ready()
		{
			base._Ready();
			settings = GetParent().GetNode<Settings>("Settings");
			if (_volumeUpButton == null || _volumeDownButton == null)
			{
				GD.PrintErr("Audio not assigned");
				return;
			}
			_volumeUpButton.Pressed += OnPlusButtonPressed;
			_volumeDownButton.Pressed += OnMinusButtonPressed;

			// 🔥 GET the *saved* volume, NOT live bus volume
			_currentVolumeDB = settings.GetSavedVolume(_busName);
		}
		// updates colume to higher
		private void OnPlusButtonPressed()
		{
			_currentVolumeDB = Mathf.Clamp(_currentVolumeDB + (_stepSize * 20.0f), MinDecibel, MaxDecibel);
			UpdateVolume();
			GD.Print($"Volume (dB): {_currentVolumeDB}");
		}
		// updates volume to lower
		private void OnMinusButtonPressed()
		{
			_currentVolumeDB = Mathf.Clamp(_currentVolumeDB - (_stepSize * 20.0f), MinDecibel, MaxDecibel);
			UpdateVolume();
			GD.Print($"Volume (dB): {_currentVolumeDB}");
		}
		// updates volume and sets it
		private void UpdateVolume()
		{
			settings.SetVolume(_busName, _currentVolumeDB);
			settings.SaveSettings();
		}
	}
