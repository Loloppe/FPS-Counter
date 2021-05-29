using System.Reflection;
using FPS_Counter.Installers;
using FPS_Counter.Settings;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Loader;
using IPA.Logging;
using SiraUtil.Zenject;

namespace FPS_Counter
{
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		private static PluginMetadata? _metadata;
		private static string? _name;

		internal static string PluginName => _name ??= _metadata?.Name ?? Assembly.GetExecutingAssembly().GetName().Name;

		[Init]
		public void Init(Logger logger, Config config, PluginMetadata metaData, Zenjector zenject)
		{
			_metadata = metaData;

			zenject.OnApp<AppInstaller>().WithParameters(logger, config.Generated<Configuration>());
			zenject.OnMenu<MenuInstaller>();
			zenject.OnGame<GamePlayCoreInstaller>();
		}

		[OnEnable, OnDisable]
		public void OnStateChanged()
		{
			// SiraUtil handles this for me, but just adding an empty body method to prevent warnings in the logs ^^
		}
	}
}