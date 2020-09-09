using FPS_Counter.Utilities;
using Zenject;

namespace FPS_Counter.Installers
{
	public class AppInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Logger.Log.Debug($"Running {nameof(InstallBindings)} of {nameof(AppInstaller)}");

			Logger.Log.Debug($"Binding {nameof(PluginUtils)}");
			Container.BindInterfacesAndSelfTo<PluginUtils>().AsSingle().NonLazy();
			Logger.Log.Debug($"All bindings installed in {nameof(AppInstaller)}");
		}
	}
}