using System.Reflection;
using BeatSaberMarkupLanguage.Settings;
using FPS_Counter.Installers;
using FPS_Counter.Settings;
using FPS_Counter.Settings.UI;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Loader;
using IPALogger = IPA.Logging.Logger;

namespace FPS_Counter
{
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		private static string? _name;

		private SettingsController? _settingsHost;

		internal static string PluginName => _name ??= Metadata?.Name ?? Assembly.GetExecutingAssembly().GetName().Name;
		internal static PluginMetadata? Metadata;

		[Init]
		public void Init(IPALogger logger, PluginMetadata metaData, Config config)
		{
			Metadata = metaData;
			Logger.Log = logger;

			Configuration.Instance = config.Generated<Configuration>();
		}

		[OnEnable]
		public void OnEnable()
		{
			BSMLSettings.instance.AddSettingsMenu(PluginName, "FPS_Counter.Settings.UI.Views.mainsettings.bsml", _settingsHost ??= new SettingsController());

			SiraUtil.Zenject.Installer.RegisterAppInstaller<AppInstaller>();
			SiraUtil.Zenject.Installer.RegisterGameplayCoreInstaller<GamePlayCoreInstaller>();
		}

		[OnDisable]
		public void OnDisable()
		{
			SiraUtil.Zenject.Installer.UnregisterGameplayCoreInstaller<GamePlayCoreInstaller>();
			SiraUtil.Zenject.Installer.UnregisterAppInstaller<AppInstaller>();

			BSMLSettings.instance.RemoveSettingsMenu(_settingsHost);
			_settingsHost = null;
		}
	}
}