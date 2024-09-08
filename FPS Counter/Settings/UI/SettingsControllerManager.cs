using System;
using BeatSaberMarkupLanguage.Settings;
using FPS_Counter.Utilities;
using IPA.Loader;
using SiraUtil.Zenject;
using Zenject;

namespace FPS_Counter.Settings.UI
{
	internal class SettingsControllerManager : IInitializable, IDisposable
	{
		private readonly PluginMetadata _pluginMetadata;
		private readonly PluginUtils _pluginUtils;
		private SettingsController? _settingsHost;

		[Inject]
		public SettingsControllerManager(UBinder<Plugin, PluginMetadata> pluginMetadata, PluginUtils pluginUtils, SettingsController settingsHost)
		{
			_pluginMetadata = pluginMetadata.Value;
			_pluginUtils = pluginUtils;
			_settingsHost = settingsHost;
		}

		public void Initialize()
		{
			if (!_pluginUtils.IsCountersPlusPresent)
			{
				BSMLSettings.Instance.AddSettingsMenu(_pluginMetadata.Name, "FPS_Counter.Settings.UI.Views.mainSettings.bsml", _settingsHost);
			}
		}

		public void Dispose()
		{
			if (_settingsHost == null)
			{
				return;
			}

			BSMLSettings.Instance.RemoveSettingsMenu(_settingsHost);
			_settingsHost = null!;
		}
	}
}