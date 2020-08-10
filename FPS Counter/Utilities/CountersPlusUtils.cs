using System;
using System.Collections.Generic;
using System.Reflection;
using CountersPlus.Custom;

namespace FPS_Counter.Utilities
{
	internal static class CountersPlusUtils
	{
		internal static void AddCustomCounter()
		{
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

		internal static void RemoveCustomCounter()
		{
			Logger.Log.Info("Removing Custom Counter");

			try
			{
				var loadedCustomCounters = typeof(CustomCounterCreator).GetField("LoadedCustomCounters", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null) as List<CustomCounter>;
				loadedCustomCounters?.RemoveAll(cc => cc.Name == Plugin.PluginName);
			}
			catch (Exception ex)
			{
				Logger.Log.Error("FPS Counter screwed up on removing integration from Counters+");
				Logger.Log.Error(ex);
			}
		}
	}
}