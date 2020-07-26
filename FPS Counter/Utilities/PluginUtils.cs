using System;
using System.Linq;
using IPA.Loader;

namespace FPS_Counter.Utilities
{
	internal static class PluginUtils
	{
		private static bool _isCountersPlusPresent;

		internal static bool IsCountersPlusPresent
		{
			get => _isCountersPlusPresent;
			private set
			{
				if (value.Equals(_isCountersPlusPresent))
				{
					return;
				}

				_isCountersPlusPresent = value;
				CountersPlusStateChanged?.Invoke(typeof(PluginUtils), IsCountersPlusPresent);
			}
		}

		internal static event EventHandler<bool>? CountersPlusStateChanged;

		internal static void Setup()
		{
			RegisterPluginChangeListeners();

			IsCountersPlusPresent = PluginManager.EnabledPlugins.Any(x => x.Id == "Counters+");
		}

		internal static void Cleanup()
		{
			UnregisterPluginChangeListeners();

			IsCountersPlusPresent = false;
		}

		private static void RegisterPluginChangeListeners()
		{
			PluginManager.PluginEnabled += OnPluginEnabled;
			PluginManager.PluginDisabled += OnPluginDisabled;
		}

		private static void UnregisterPluginChangeListeners()
		{
			PluginManager.PluginEnabled -= OnPluginEnabled;
			PluginManager.PluginDisabled -= OnPluginDisabled;
		}

		private static void OnPluginEnabled(PluginMetadata plugin, bool needsRestart)
		{
			if (needsRestart)
			{
				return;
			}

			switch (plugin.Id)
			{
				case "Counters+":
					IsCountersPlusPresent = true;
					return;
			}
		}

		private static void OnPluginDisabled(PluginMetadata plugin, bool needsRestart)
		{
			if (needsRestart)
			{
				return;
			}

			switch (plugin.Id)
			{
				case "Counters+":
					IsCountersPlusPresent = false;
					return;
			}
		}
	}
}