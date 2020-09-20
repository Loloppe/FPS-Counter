using FPS_Counter.Counters;
using FPS_Counter.Utilities;
using SiraUtil.Zenject;
using Zenject;

namespace FPS_Counter.Installers
{
	[RequiresInstaller(typeof(AppInstaller))]
	public class GamePlayCoreInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			if (!Container.Resolve<GameplayCoreSceneSetupData>().playerSpecificSettings.noTextsAndHuds && !Container.Resolve<PluginUtils>().IsCountersPlusPresent)
			{
				Logger.Log.Debug($"Binding {nameof(FPSCounter)}");

				Container.BindInterfacesAndSelfTo<FpsCounter>().AsSingle().NonLazy();

				Logger.Log.Debug($"Finished binding {nameof(FPSCounter)}");
			}
			else
			{
				Logger.Log.Debug($"Either Counters+ is present or No Text and HUD enabled in PlayerSettings - Not constructing FpsCounter");
			}
		}
	}
}