using FPS_Counter.Settings;
using FPS_Counter.Utilities;
using IPA.Logging;
using SiraUtil;
using Zenject;

namespace FPS_Counter.Installers
{
	internal class AppInstaller : Installer<Logger, Configuration, AppInstaller>
	{
		private readonly Logger _logger;
		private readonly Configuration _configuration;

		public AppInstaller(Logger logger, Configuration configuration)
		{
			_logger = logger;
			_configuration = configuration;
		}

		public override void InstallBindings()
		{
			Container.BindLoggerAsSiraLogger(_logger);

			Container.BindInstance(_configuration).AsSingle().NonLazy();

			Container.BindInterfacesAndSelfTo<PluginUtils>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<FpsCounterUtils>().AsSingle();
		}
	}
}