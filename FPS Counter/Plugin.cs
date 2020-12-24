using System.Reflection;
using FPS_Counter.Installers;
using IPA;
using IPA.Loader;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;

namespace FPS_Counter
{
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		private static PluginMetadata? _metadata;
		private static string? _name;

		internal static string PluginName => _name ??= _metadata?.Name ?? Assembly.GetExecutingAssembly().GetName().Name;

		[Init]
		public void Init(IPALogger logger, PluginMetadata metaData, Zenjector zenject)
		{
			_metadata = metaData;
			Logger.Log = logger;

			zenject.OnApp<AppInstaller>();
			zenject.OnMenu<Installers.MenuInstaller>();
			zenject.OnGame<GamePlayCoreInstaller>();
		}

		[OnEnable, OnDisable]
		public void OnStateChanged()
		{
			// SiraUtil handles this for me, but just adding an empty body method to prevent warnings in the logs ^^
		}
	}
}