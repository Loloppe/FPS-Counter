using FPS_Counter.Settings.UI;
using Zenject;

namespace FPS_Counter.Installers
{
	public class MenuInstaller : Installer<MenuInstaller>
	{
		public override void InstallBindings()
		{
			Logger.Log.Debug($"Binding {nameof(SettingsController)}");
			Container.BindInterfacesAndSelfTo<SettingsController>().AsSingle();

			Logger.Log.Debug($"Binding {nameof(SettingsControllerManager)}");
			Container.BindInterfacesAndSelfTo<SettingsControllerManager>().AsSingle();
		}
	}
}