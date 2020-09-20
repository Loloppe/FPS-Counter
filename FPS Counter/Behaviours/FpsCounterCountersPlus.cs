using System;
using CountersPlus.Counters.Custom;
using FPS_Counter.Settings;
using FPS_Counter.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Zenject;

namespace FPS_Counter.Behaviours
{
	public class FpsCounterCountersPlus : BasicCustomCounter, ITickable
	{
		private readonly Vector3 _ringSize = Vector3.one;

		private int _targetFramerate;
		private TMP_Text? _counterText;
		private float _ringFillPercent = 1;
		private Image? _image;

		private float _timeLeft;
		private int _frameCount;
		private float _accumulatedTime;

		public override void CounterInit()
		{
			try
			{
				Logger.Log.Info("Attempting to Initialize FPS Counter");

				_targetFramerate = (int) XRDevice.refreshRate;
				Logger.Log.Info($"Target framerate = {_targetFramerate}");

				_counterText = CanvasUtility.CreateTextFromSettings(Settings);
				_counterText.color = Color.white;
				_counterText.fontSize = 2.5f;
				_counterText.lineSpacing = -50f;

				if (!Configuration.Instance.ShowRing)
				{
					return;
				}

				var canvas = CanvasUtility.GetCanvasFromID(Settings.CanvasID);
				if (canvas == null)
				{
					return;
				}

				_image = FpsCounterUtils.CreateRing(canvas);
				_image.rectTransform.anchoredPosition = _counterText.rectTransform.anchoredPosition;
				_image.transform.localScale = _ringSize / 10;
			}
			catch (Exception ex)
			{
				Logger.Log.Error("FPS Counter Done");
				Logger.Log.Error(ex);
			}
		}

		public void Tick()
		{
			FpsCounterUtils.SharedTicker(ref _accumulatedTime, ref _timeLeft, ref _frameCount, ref _targetFramerate, ref _ringFillPercent, _image, _counterText);
		}

		public override void CounterDestroy()
		{
			Logger.Log.Info("FPS Counter got yeeted");
		}
	}
}