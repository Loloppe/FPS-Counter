using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CountersPlus.Custom;

namespace FPS_Counter.Utilities
{
	internal static class CountersPlusUtils
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static bool IsEnabledInCountersPlus() =>
			CountersPlus.Config.ConfigLoader.LoadCustomCounters()?.FirstOrDefault(x => x.DisplayName == Constants.CountersPlusSectionName)?.Enabled ?? false;

		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void AddCustomCounter()
		{
			var loadedCustomCounters = LoadedCustomCounters();
			if (loadedCustomCounters?.Any(cc => cc.Name == Plugin.PluginName) ?? true)
			{
				Logger.Log.Info("FPS Counter was already registered in Counters+. Skipping...");
				return;
			}

			Logger.Log.Info("Creating Custom Counter");
			CustomCounter counter = new CustomCounter
			{
				SectionName = Constants.CountersPlusSectionName,
				Name = Plugin.PluginName,
				BSIPAMod = Plugin.Metadata,
				Counter = Plugin.PluginName,
				Icon_ResourceName = "FPS_Counter.Resources.icon.png"
			};

			CustomCounterCreator.Create(counter);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void RemoveCustomCounter()
		{
			Logger.Log.Info("Removing Custom Counter");

			try
			{
				var loadedCustomCounters = LoadedCustomCounters();
				loadedCustomCounters?.RemoveAll(cc => cc.Name == Plugin.PluginName);
			}
			catch (Exception ex)
			{
				Logger.Log.Error("FPS Counter screwed up on removing integration from Counters+");
				Logger.Log.Error(ex);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static List<CustomCounter>? LoadedCustomCounters() =>
			typeof(CustomCounterCreator).GetField("LoadedCustomCounters", BindingFlags.Static | BindingFlags.NonPublic)?.GetValue(null) as List<CustomCounter>;
	}
}