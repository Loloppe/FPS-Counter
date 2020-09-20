using System;
using BeatSaberMarkupLanguage.Settings;
using FPS_Counter.Utilities;
using Zenject;

namespace FPS_Counter.Settings.UI
{
	internal class SettingsControllerManager : IInitializable, IDisposable
	{
		private readonly PluginUtils _pluginUtils;
		private SettingsController? _settingsHost;

		[Inject]
		public SettingsControllerManager(PluginUtils pluginUtils, SettingsController settingsHost)
		{
			_pluginUtils = pluginUtils;
			_settingsHost = settingsHost;
		}

		public void Initialize()
		{
			if (!_pluginUtils.IsCountersPlusPresent)
			{
				BSMLSettings.instance.AddSettingsMenu(Plugin.PluginName, "FPS_Counter.Settings.UI.Views.mainSettings.bsml", _settingsHost);
			}
		}

		public void Dispose()
		{
			if (_settingsHost == null)
			{
				return;
			}

			BSMLSettings.instance.RemoveSettingsMenu(_settingsHost);
			_settingsHost = null!;
		}
	}
}