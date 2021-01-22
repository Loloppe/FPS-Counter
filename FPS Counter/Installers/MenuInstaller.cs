using FPS_Counter.Settings.UI;
using FPS_Counter.Utilities;
using SiraUtil.Tools;
using Zenject;

namespace FPS_Counter.Installers
{
	internal class MenuInstaller : Installer<MenuInstaller>
	{
		private readonly SiraLog _logger;

		public MenuInstaller(SiraLog logger)
		{
			_logger = logger;
		}

		public override void InstallBindings()
		{
			if (!Container.Resolve<PluginUtils>().IsCountersPlusPresent)
			{
				_logger.Debug($"Binding {nameof(SettingsController)}");
				Container.BindInterfacesAndSelfTo<SettingsController>().AsSingle();

				_logger.Debug($"Binding {nameof(SettingsControllerManager)}");
				Container.BindInterfacesAndSelfTo<SettingsControllerManager>().AsSingle();
			}
		}
	}
}