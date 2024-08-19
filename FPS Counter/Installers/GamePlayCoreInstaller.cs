using FPS_Counter.Counters;
using FPS_Counter.Utilities;
using SiraUtil.Logging;
using Zenject;

namespace FPS_Counter.Installers
{
	internal class GamePlayCoreInstaller : Installer<GamePlayCoreInstaller>
	{
		private readonly SiraLog _logger;
		private readonly GameplayCoreSceneSetupData? _gameplayCoreSceneSetupData;

		public GamePlayCoreInstaller(SiraLog logger, [InjectOptional] GameplayCoreSceneSetupData gameplayCoreSceneSetupData)
		{
			_logger = logger;
			_gameplayCoreSceneSetupData = gameplayCoreSceneSetupData;
		}

		public override void InstallBindings()
		{
			if ((!_gameplayCoreSceneSetupData?.playerSpecificSettings.noTextsAndHuds ?? false) && !Container.Resolve<PluginUtils>().IsCountersPlusPresent)
			{
				Container.BindInterfacesAndSelfTo<FpsCounter>().AsSingle().NonLazy();
			}
			else
			{
				_logger.Debug("Either Counters+ is present or No Text and HUD enabled in PlayerSettings - Not constructing FpsCounter");
			}
		}
	}
}