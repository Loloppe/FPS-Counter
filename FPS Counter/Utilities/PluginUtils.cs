﻿using System;
using System.Linq;
using IPA.Loader;
using SiraUtil.Tools;
using Zenject;

namespace FPS_Counter.Utilities
{
	internal class PluginUtils : IInitializable, IDisposable
	{
		private const string COUNTERS_PLUS_MOD_ID = "Counters+";

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
			var pluginMetaData = PluginManager.EnabledPlugins.FirstOrDefault(x => x.Id == COUNTERS_PLUS_MOD_ID);
			if (pluginMetaData == null)
			{
				return;
			}

			if (pluginMetaData.HVersion.Major < 2)
			{
				_logger.Warning($"Version {pluginMetaData.HVersion} of Counters+ has been found, but is deemed incompatible with FPS Counter. NOT INTEGRATING!");
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
				case COUNTERS_PLUS_MOD_ID when plugin.HVersion.Major < 2:
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
				case COUNTERS_PLUS_MOD_ID when plugin.HVersion.Major >= 2:
					IsCountersPlusPresent = false;
					return;
			}
		}
	}
}