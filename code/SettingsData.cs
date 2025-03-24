using Godot;
using System;

public partial class SettingsData : Node
{
	public static SettingsData CreateDefaults()
		{
			return new SettingsData
			{
				LangCode = "fi",
				MasterVolume = -6.0f,
				MusicVolume = -6.0f,
				SfxVolume = -6.0f
			};
		}

		public string LangCode { get; set; }
		public float MasterVolume { get; set; }
		public float MusicVolume { get; set; }
		public float SfxVolume { get; set; }
}
