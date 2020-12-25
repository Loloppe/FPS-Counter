using System;
using System.Linq;
using IPA.Loader;
using SiraUtil.Tools;
using Zenject;

namespace FPS_Counter.Utilities
{
	internal class PluginUtils : IInitializable, IDisposable
	{
		private readonly SiraLog _logger;
		internal bool IsCountersPlusPresent { get; private set; }

		public PluginUtils(SiraLog logger)
		{
			_logger = logger;
		}

		public void Initialize()
		{
			RegisterPluginChangeListeners();

			_logger.Info("Checking for Counters+");
			var pluginMetaData = PluginManager.EnabledPlugins.FirstOrDefault(x => x.Id == "Counters+");
			if (pluginMetaData == null)
			{
				return;
			}

			if (pluginMetaData.Version.Major < 2)
			{
				_logger.Warning($"Version {pluginMetaData.Version} of Counters+ has been found, but is deemed incompatible with FPS Counter. NOT INTEGRATING!");
				return;
			}

			IsCountersPlusPresent = true;
			_logger.Info("Found Counters+");
		}

		public void Dispose()
		{
			UnregisterPluginChangeListeners();

			IsCountersPlusPresent = false;
		}

		private void RegisterPluginChangeListeners()
		{
			PluginManager.PluginEnabled += OnPluginEnabled;
			PluginManager.PluginDisabled += OnPluginDisabled;
		}

		private void UnregisterPluginChangeListeners()
		{
			PluginManager.PluginEnabled -= OnPluginEnabled;
			PluginManager.PluginDisabled -= OnPluginDisabled;
		}

		private void OnPluginEnabled(PluginMetadata plugin, bool needsRestart)
		{
			if (needsRestart)
			{
				return;
			}

			switch (plugin.Id)
			{
				case "Counters+" when plugin.Version.Major < 2:
					IsCountersPlusPresent = true;
					return;
			}
		}

		private void OnPluginDisabled(PluginMetadata plugin, bool needsRestart)
		{
			if (needsRestart)
			{
				return;
			}

			switch (plugin.Id)
			{
				case "Counters+" when plugin.Version.Major >= 2:
					IsCountersPlusPresent = false;
					return;
			}
		}
	}
}