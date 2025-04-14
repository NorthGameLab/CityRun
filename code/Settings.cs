using Godot;
using System;
	public partial class Settings : Node
	{
		// signal for changed language
		[Signal] public delegate void LanguageChangedEventHandler(string langCode);
		public SettingsData _data = null;

		public override void _Ready()
		{
			base._Ready();

			// downloads settings from a file
			_data = LoadSettings();
			ApplyData(_data);
		}

		private void ApplyData(SettingsData data)
		{
			if (data == null)
			{
				return;
			}

			// set volumes
			SetVolume("Music", data.MusicVolume);
			SetVolume("SFX", data.SfxVolume);

			// set language
			SetLanguage(data.LangCode);
		}

		public bool SaveSettings()
		{
			if (_data == null)
			{
				return false;
			}
			// set all new values to file
			ConfigFile settingsConfig = new ConfigFile();
			settingsConfig.SetValue("Localization", "LangCode", _data.LangCode);
			settingsConfig.SetValue("Audio", "MasterVolume", _data.MasterVolume);
			settingsConfig.SetValue("Audio", "MusicVolume", _data.MusicVolume);
			settingsConfig.SetValue("Audio", "SfxVolume", _data.SfxVolume);

			if (settingsConfig.Save(Global.SettingsFile) != Error.Ok)
			{
				GD.PrintErr("Failed to save settings.");
				return false;
			}

			return true;
		}

		private SettingsData LoadSettings()
		{
			SettingsData data = null;
			ConfigFile settingsConfig = new ConfigFile();
			if (settingsConfig.Load(Global.SettingsFile) == Error.Ok)
			{
				// settings downloaded successfully
				data = new SettingsData
				{
					LangCode = (string)settingsConfig.GetValue("Localization", "LangCode", "fi"),
					MasterVolume = (float)settingsConfig.GetValue("Audio", "MasterVolume", -6.0f),
					MusicVolume = (float)settingsConfig.GetValue("Audio", "MusicVolume", -6.0f),
					SfxVolume = (float)settingsConfig.GetValue("Audio", "SfxVolume", -6.0f),
				};
			}
			else
			{
				// did not find settings, create defaults
				data = SettingsData.CreateDefaults();
			}

			return data;
		}
		public bool SetVolume(string busName, float volumeDB)
		{
			if (_data == null)
			{
				return false;
			}
			// sets volume based on the bus name
			int busIndex = AudioServer.GetBusIndex(busName);
			if (busIndex < 0)
			{
				GD.PrintErr($"Bus '{busName}' not found.");
				return false;
			}

			AudioServer.SetBusVolumeDb(busIndex, volumeDB);

			switch (busName)
			{
				case "Master":
					_data.MasterVolume = volumeDB;
					break;
				case "Music":
					_data.MusicVolume = volumeDB;
					break;
				case "SFX":
					_data.SfxVolume = volumeDB;
					break;
				default:
					GD.PrintErr($"Unknown bus '{busName}'.");
					break;
			}

			return true;
		}
		// gets audio values based on bus name
		public bool GetVolume(string busName, out float volumeDB)
		{
			int busIndex = AudioServer.GetBusIndex(busName);
			if (busIndex < 0)
			{
				GD.PrintErr($"Bus '{busName}' not found.");
				volumeDB = float.NaN;
				return false;
			}

			volumeDB = AudioServer.GetBusVolumeDb(busIndex);
			return true;
		}
		public float GetSavedVolume(string busName)
		{
			return busName switch
			{
				"Master" => _data.MasterVolume,
				"Music" => _data.MusicVolume,
				"SFX" => _data.SfxVolume,
				_ => -6.0f // default value if unknown
			};
		}

		public string GetLanguage()
		{
			return TranslationServer.GetLocale();
		}

		public bool SetLanguage(string langCode)
		{
			if (_data == null)
			{
				return false;
			}

			_data.LangCode = langCode;
			TranslationServer.SetLocale(langCode);

			// emits signal for changed language
			EmitSignal(SignalName.LanguageChanged, langCode);

			_data.LangCode = langCode;

			return true;
		}
	}