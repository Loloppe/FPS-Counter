using BeatSaberMarkupLanguage.Attributes;

namespace FPS_Counter.Settings.UI
{
	internal class SettingsController
	{
		[UIValue("update-rate")]
		public float FpsUpdateRate
		{
			get => Configuration.Instance.UpdateRate;
			set => Configuration.Instance.UpdateRate = value;
		}

		[UIValue("show-ring")]
		public bool ShowFpsRing
		{
			get => Configuration.Instance.ShowRing;
			set => Configuration.Instance.ShowRing = value;
		}

		[UIValue("use-colors")]
		public bool FpsUseColors
		{
			get => Configuration.Instance.UseColors;
			set => Configuration.Instance.UseColors = value;
		}
	}
}