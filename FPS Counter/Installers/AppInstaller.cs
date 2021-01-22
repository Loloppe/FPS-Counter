using FPS_Counter.Settings;
using FPS_Counter.Utilities;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Logging;
using SiraUtil;
using Zenject;

namespace FPS_Counter.Installers
{
	internal class AppInstaller : Installer<Logger, Configuration, AppInstaller>
	{
		private readonly Logger _logger;

		public AppInstaller(Logger logger)
		{
			_logger = logger;
		}

		public override void InstallBindings()
		{
			_logger.Debug($"Running {nameof(InstallBindings)} of {nameof(AppInstaller)}");
			Container.BindLoggerAsSiraLogger(_logger);

			_logger.Debug($"Binding {nameof(Configuration)}");
			Configuration.Instance ??= Config.GetConfigFor(Plugin.PluginName).Generated<Configuration>();
			Container.BindInstance(Configuration.Instance).AsSingle().NonLazy();

			_logger.Debug($"Binding {nameof(PluginUtils)}");
			Container.BindInterfacesAndSelfTo<PluginUtils>().AsSingle().NonLazy();

			_logger.Debug($"All bindings installed in {nameof(AppInstaller)}");
		}
	}
}