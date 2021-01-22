using FPS_Counter.Settings.UI;
using FPS_Counter.Utilities;
using Zenject;

namespace FPS_Counter.Installers
{
	internal class MenuInstaller : Installer<MenuInstaller>
	{
		public override void InstallBindings()
		{
			if (!Container.Resolve<PluginUtils>().IsCountersPlusPresent)
			{
				Container.BindInterfacesAndSelfTo<SettingsController>().AsSingle();
				Container.BindInterfacesTo<SettingsControllerManager>().AsSingle();
			}
		}
	}
}