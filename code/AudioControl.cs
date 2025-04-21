using Godot;
using System;


	public partial class AudioControl : Control
	{
		// volume buttons
		[Export] private TextureButton _volumeUpButton = null;
		[Export] private TextureButton _volumeDownButton = null;
		// mute
		[Export] private TextureButton _muteButton = null;
		// restores
		[Export] private TextureButton _restoreButton = null;
		[Export] private string _busName = "Master";

		// step size for how much sound is altered with one click
		[Export] private float _stepSize = 0.20f;

		//Linear range [0, 1]
		private float _currentVolumeDB;
		private bool _isMuted = false;
		// float for saving last volume before mute
		private float _lastVolumeBeforeMute = 0.0f;
		// max and min values for sound linear
		private const float MinDecibel = -60.0f;
		private const float MaxDecibel = 0.0f; // 0 dB is full volume

		private Settings settings;

		public override void _Ready()
		{
			base._Ready();
			settings = GetParent().GetNode<Settings>("Settings");
			// check if buttons are assigned
			if (_volumeUpButton == null || _volumeDownButton == null || _muteButton == null)
			{
				GD.PrintErr("Audio not assigned");
				return;
			}
			// signals for buttons
			_volumeUpButton.Pressed += OnPlusButtonPressed;
			_volumeDownButton.Pressed += OnMinusButtonPressed;
			_restoreButton.Pressed += OnRestorePressed;

			// Get saved volume when starting game
			_currentVolumeDB = settings.GetSavedVolume(_busName);

			if (_muteButton != null)
				{
					_muteButton.Pressed += OnMuteButtonPressed;
				}
			else
			{
				GD.PrintErr("Mute button not assigned.");
			}
		}
		// adjust volume higher linearly
		private void OnPlusButtonPressed()
		{
			_currentVolumeDB = Mathf.Clamp(_currentVolumeDB + (_stepSize * 20.0f), MinDecibel, MaxDecibel);
			UpdateVolume();
			GD.Print($"Volume (dB): {_currentVolumeDB}");
		}
		private void OnMinusButtonPressed()
		{
			// lowers volume linearly
			_currentVolumeDB = Mathf.Clamp(_currentVolumeDB - (_stepSize * 20.0f), MinDecibel, MaxDecibel);
			UpdateVolume();
			GD.Print($"Volume (dB): {_currentVolumeDB}");
		}

		// Method for muting audio
		private void OnMuteButtonPressed()
		{
			// checks if audio is not muted
			if (Global._isMuted = !Global._isMuted)
			{
				// Save the last non-muted volume
				_lastVolumeBeforeMute = _currentVolumeDB;
				// fully muted
				_currentVolumeDB = MinDecibel;
			}
			else
			{
				// Restore audio to last saved
				_currentVolumeDB = _lastVolumeBeforeMute;
			}

			UpdateVolume();
		}

		// Button for restoring audio to full
		private void OnRestorePressed()
		{
			_currentVolumeDB = 1.0f;
			UpdateVolume();
		}
		// updates volume and sets it
		private void UpdateVolume()
		{
			settings.SetVolume(_busName, _currentVolumeDB);
			settings.SaveSettings();
		}
	}
