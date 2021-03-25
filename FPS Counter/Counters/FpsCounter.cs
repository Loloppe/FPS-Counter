using System;
using BeatSaberMarkupLanguage;
using FPS_Counter.Settings;
using FPS_Counter.Utilities;
using HMUI;
using SiraUtil.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

namespace FPS_Counter.Counters
{
	internal class FpsCounter : IInitializable, ITickable, IDisposable
	{
		private readonly SiraLog _logger;
		private readonly Configuration _config;
		private readonly FpsCounterUtils _fpsCounterUtils;

		private int _targetFramerate;
		private TMP_Text? _counter;
		private float _ringFillPercent = 1;
		private ImageView? _image;

		private float _timeLeft;
		private int _frameCount;
		private float _accumulatedTime;

		internal FpsCounter(SiraLog logger, Configuration config, FpsCounterUtils fpsCounterUtils)
		{
			_logger = logger;
			_config = config;
			_fpsCounterUtils = fpsCounterUtils;
		}

		public void Initialize()
		{
			try
			{
				_logger.Debug("Attempting to Initialize FPS Counter");

				_targetFramerate = (int) XRDevice.refreshRate;
				_logger.Debug($"Target framerate = {_targetFramerate}");

				var gameObject = new GameObject("FPS Counter");

				var canvas = gameObject.AddComponent<Canvas>();
				canvas.renderMode = RenderMode.WorldSpace;
				gameObject.transform.localScale = Vector3.one / 10;
				gameObject.transform.position = new Vector3(0, 3.5f, 8f);
				gameObject.transform.rotation = Quaternion.identity;
				gameObject.AddComponent<CurvedCanvasSettings>().SetRadius(0f);

				var canvasTransform = canvas.transform as RectTransform;

				_counter = BeatSaberUI.CreateText(canvasTransform, string.Empty, Vector3.zero);
				_counter.alignment = TextAlignmentOptions.Center;
				_counter.fontSize = 2.5f;
				_counter.color = Color.white;
				_counter.lineSpacing = -50f;
				_counter.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2f);
				_counter.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2f);
				_counter.enableWordWrapping = false;
				_counter.overflowMode = TextOverflowModes.Overflow;

				if (!_config.ShowRing)
				{
					return;
				}

				_image = _fpsCounterUtils.CreateRing(canvas);
				var imageTransform = _image.rectTransform;
				imageTransform.anchoredPosition = _counter.rectTransform.anchoredPosition;
				imageTransform.localScale *= 0.1f;
			}
			catch (Exception ex)
			{
				_logger.Error("FPS Counter Done");
				_logger.Error(ex);
			}
		}

		public void Tick()
		{
			_fpsCounterUtils.SharedTicker(ref _accumulatedTime, ref _timeLeft, ref _frameCount, ref _targetFramerate, ref _ringFillPercent, _image, _counter);
		}

		public void Dispose()
		{
			_logger.Debug("FPS Counter got yeeted");
		}
	}
}