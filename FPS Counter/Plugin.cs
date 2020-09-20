using System.Reflection;
using FPS_Counter.Installers;
using IPA;
using IPA.Loader;
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
		public void Init(IPALogger logger, PluginMetadata metaData)
		{
			_metadata = metaData;
			Logger.Log = logger;
		}

		[OnEnable]
		public void OnEnable()
		{
			SiraUtil.Zenject.Installer.RegisterAppInstaller<AppInstaller>();
			SiraUtil.Zenject.Installer.RegisterGameplayCoreInstaller<GamePlayCoreInstaller>();
		}

		[OnDisable]
		public void OnDisable()
		{
			SiraUtil.Zenject.Installer.UnregisterGameplayCoreInstaller<GamePlayCoreInstaller>();
			SiraUtil.Zenject.Installer.UnregisterAppInstaller<AppInstaller>();
		}
	}
}