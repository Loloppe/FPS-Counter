using FPS_Counter.Utilities;
using SiraUtil.Zenject;
using UnityEngine;
using Zenject;

namespace FPS_Counter.Installers
{
	public class GamePlayCoreInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			if (!Container.Resolve<GameplayCoreSceneSetupData>().playerSpecificSettings.noTextsAndHuds && PluginUtils.IsFpsCounterEnabledInCountersPlus)
			{
				Logger.Log.Info($"Binding {nameof(FPSCounter)}");
				var fpsCounter = new GameObject(Plugin.PluginName).AddComponent<Behaviours.FpsCounter>();
				Container.InjectSpecialInstance<Behaviours.FpsCounter>(fpsCounter);
			}
			else
			{
				Logger.Log.Info($"No Text and HUD enabled in PlayerSettings - Not constructing FpsCounter");
			}
		}
	}
}