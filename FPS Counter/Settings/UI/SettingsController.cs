using BeatSaberMarkupLanguage.Attributes;

namespace FPS_Counter.Settings.UI
{
	internal class SettingsController
	{
		private readonly Configuration _configuration;

		public SettingsController(Configuration configuration)
		{
			_configuration = configuration;
		}

		[UIValue("update-rate")]
		public float FpsUpdateRate
		{
			get => _configuration.UpdateRate;
			set => _configuration.UpdateRate = value;
		}

		[UIValue("show-ring")]
		public bool ShowFpsRing
		{
			get => _configuration.ShowRing;
			set => _configuration.ShowRing = value;
		}

		[UIValue("use-colors")]
		public bool FpsUseColors
		{
			get => _configuration.UseColors;
			set => _configuration.UseColors = value;
		}
	}
}