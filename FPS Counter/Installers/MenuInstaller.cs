using FPS_Counter.Settings.UI;
using Zenject;

namespace FPS_Counter.Installers
{
	public class MenuInstaller : Installer<MenuInstaller>
	{
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