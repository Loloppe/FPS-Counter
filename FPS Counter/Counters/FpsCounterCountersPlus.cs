using System;
using CountersPlus.Counters.Custom;
using FPS_Counter.Settings;
using FPS_Counter.Utilities;
using HMUI;
using SiraUtil.Logging;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

namespace FPS_Counter.Counters
{
	public class FpsCounterCountersPlus : BasicCustomCounter, ITickable
	{
		private readonly SiraLog _logger;
		private readonly Configuration _config;
		private readonly FpsCounterUtils _fpsCounterUtils;

		private readonly Vector3 _ringSize = Vector3.one;

		private int _targetFramerate;
		private TMP_Text? _counterText;
		private float _ringFillPercent = 1;
		private ImageView? _ringImage;

		private float _timeLeft;
		private int _frameCount;
		private float _accumulatedTime;

		internal FpsCounterCountersPlus(SiraLog logger, Configuration config, FpsCounterUtils fpsCounterUtils)
		{
			_logger = logger;
			_config = config;
			_fpsCounterUtils = fpsCounterUtils;
		}

		public override void CounterInit()
		{
			try
			{
				_logger.Debug("Attempting to Initialize FPS Counter");

				_targetFramerate = (int) Math.Round(XRDevice.refreshRate);
				_logger.Debug($"Target framerate = {_targetFramerate}");

				_counterText = CanvasUtility.CreateTextFromSettings(Settings);
				_counterText.color = Color.white;
				_counterText.fontSize = 2.5f;
				_counterText.lineSpacing = -50f;

				if (!_config.ShowRing)
				{
					return;
				}

				var canvas = CanvasUtility.GetCanvasFromID(Settings.CanvasID);
				if (canvas == null)
				{
					return;
				}

				_ringImage = _fpsCounterUtils.CreateRing(canvas);
				_ringImage.rectTransform.anchoredPosition = _counterText.rectTransform.anchoredPosition;
				_ringImage.transform.localScale = _ringSize / 10;
			}
			catch (Exception ex)
			{
				_logger.Error("FPS Counter Done");
				_logger.Error(ex);
			}
		}

		public void Tick()
		{
			_fpsCounterUtils.SharedTicker(ref _accumulatedTime, ref _timeLeft, ref _frameCount, ref _targetFramerate, ref _ringFillPercent, _ringImage, _counterText);
		}

		public override void CounterDestroy()
		{
			_logger.Debug("FPS Counter got yeeted");
		}
	}
}