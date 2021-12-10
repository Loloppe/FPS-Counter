using FPS_Counter.Settings;
using FPS_Counter.Utilities;
using Zenject;

namespace FPS_Counter.Installers
{
	internal class AppInstaller : Installer<Configuration, AppInstaller>
	{
		private readonly Configuration _configuration;

		public AppInstaller(Configuration configuration)
		{
			_configuration = configuration;
		}

		public override void InstallBindings()
		{
			Container.BindInstance(_configuration).AsSingle().NonLazy();

			Container.BindInterfacesAndSelfTo<PluginUtils>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<FpsCounterUtils>().AsSingle();
		}
	}
}