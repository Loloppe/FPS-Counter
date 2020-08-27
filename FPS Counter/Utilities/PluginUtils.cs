using System;
using System.Linq;
using IPA.Loader;
using Zenject;

namespace FPS_Counter.Utilities
{
	internal class PluginUtils : IInitializable, IDisposable
	{
		internal bool IsCountersPlusPresent { get; private set; }

		internal bool IsFpsCounterEnabledInCountersPlus => !IsCountersPlusPresent || CountersPlusUtils.IsEnabledInCountersPlus();

		public void Initialize()
		{
			RegisterPluginChangeListeners();

			Logger.Log.Info("Checking for Counters+");
			var pluginMetaData = PluginManager.EnabledPlugins.FirstOrDefault(x => x.Id == "Counters+");
			if (pluginMetaData == null)
			{
				return;
			}

			if (pluginMetaData.Version.Major >= 2)
			{
				Logger.Log.Warn($"Version {pluginMetaData.Version} of Counters+ has been found, but is deemed incompatible with FPS Counter. NOT INTEGRATING");
				return;
			}

			IsCountersPlusPresent = true;
			Logger.Log.Info("Found Counters+");
			CountersPlusUtils.AddCustomCounter();
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
					CountersPlusUtils.AddCustomCounter();
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
				case "Counters+" when plugin.Version.Major < 2:
					IsCountersPlusPresent = false;
					CountersPlusUtils.RemoveCustomCounter();
					return;
			}
		}
	}
}