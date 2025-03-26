using Godot;
using System;


	public partial class AudioControl : Control
	{
		[Export] private TextureButton _volumeUpButton = null;
		[Export] private TextureButton _volumeDownButton = null;
		[Export] private string _busName = "Master";
		[Export] private float _stepSize = 0.05f;
		private float _originalVolume = 0.0f;

		private float _currentVolumeLinear = 1.0f; // Linear range [0, 1]
		private const float MinValue = 0.01f; // Avoid -inf dB
		private const float MaxValue = 1.0f;
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
		}

		public void Initialize()
		{
			// Äänenvoimakkuuden alustus.
			if (!settings.GetVolume(_busName, out _originalVolume))
			{
				GD.PrintErr("Äänenvoimakkuuden alustus epäonnistui.");
			}

			_currentVolumeLinear = Mathf.DbToLinear(_originalVolume);
		}

		public void RestoreValues()
		{
			float linearVolume = Mathf.DbToLinear(_originalVolume);
			// Tee palautus sliderin kautta. Tämä asettaa sliderin
			// oikeaan arvoon asetusten seuraavaa avausta varten ja
			// kutsuu myös eventtiä, joka päivittää äänenvoimakkuuden.
			_currentVolumeLinear = linearVolume;
		}

		private void OnPlusButtonPressed()
		{
			_currentVolumeLinear = Mathf.Clamp(_currentVolumeLinear + _stepSize, MinValue, MaxValue);
			UpdateVolume();
			GD.Print(_currentVolumeLinear);
		}
		private void OnMinusButtonPressed()
		{
			_currentVolumeLinear = Mathf.Clamp(_currentVolumeLinear - _stepSize, MinValue, MaxValue);
			UpdateVolume();
			GD.Print(_currentVolumeLinear);
		}

		private void UpdateVolume()
		{
			float decibelVolume = Mathf.LinearToDb(_currentVolumeLinear);
			settings.SetVolume(_busName, decibelVolume);
		}
	}
