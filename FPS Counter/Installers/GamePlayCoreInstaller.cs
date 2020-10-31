using FPS_Counter.Counters;
using FPS_Counter.Utilities;
using Zenject;

namespace FPS_Counter.Installers
{
	public class GamePlayCoreInstaller : Installer<GamePlayCoreInstaller>
	{
		private readonly GameplayCoreSceneSetupData? _gameplayCoreSceneSetupData;

		public GamePlayCoreInstaller([InjectOptional] GameplayCoreSceneSetupData gameplayCoreSceneSetupData)
		{
			_gameplayCoreSceneSetupData = gameplayCoreSceneSetupData;
		}

		public override void InstallBindings()
		{
			if ((!_gameplayCoreSceneSetupData?.playerSpecificSettings.noTextsAndHuds ?? false) && !Container.Resolve<PluginUtils>().IsCountersPlusPresent)
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