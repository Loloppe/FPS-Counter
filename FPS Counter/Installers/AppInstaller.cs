using FPS_Counter.Settings;
using FPS_Counter.Utilities;
using IPA.Config;
using IPA.Config.Stores;
using Zenject;

namespace FPS_Counter.Installers
{
	public class AppInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Logger.Log.Debug($"Running {nameof(InstallBindings)} of {nameof(AppInstaller)}");

			Logger.Log.Debug($"Binding {nameof(Configuration)}");
			Configuration.Instance ??= Config.GetConfigFor(Plugin.PluginName).Generated<Configuration>();
			Container.BindInstance(Configuration.Instance).AsSingle().NonLazy();

			Logger.Log.Debug($"Binding {nameof(PluginUtils)}");
			Container.BindInterfacesAndSelfTo<PluginUtils>().AsSingle().NonLazy();

			Logger.Log.Debug($"All bindings installed in {nameof(AppInstaller)}");
		}
	}
}