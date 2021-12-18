using FPS_Counter.Installers;
using FPS_Counter.Settings;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Logging;
using SiraUtil.Zenject;

namespace FPS_Counter
{
	[Plugin(RuntimeOptions.DynamicInit), NoEnableDisable]
	public class Plugin
	{
		[Init]
		public void Init(Logger logger, Config config, Zenjector zenject)
		{
			zenject.UseLogger(logger);
			zenject.UseMetadataBinder<Plugin>();

			zenject.Install<AppInstaller>(Location.App, config.Generated<Configuration>());
			zenject.Install<MenuInstaller>(Location.Menu);
			zenject.Install<GamePlayCoreInstaller>(Location.Player);
		}
	}
}