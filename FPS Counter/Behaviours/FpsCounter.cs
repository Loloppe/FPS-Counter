using FPS_Counter.Settings;
using FPS_Counter.Utilities;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace FPS_Counter.Behaviours
{
	internal class FpsCounter : MonoBehaviour
	{
		// Allocate beforehand, because invoking a color "constant" will create a new struct every time
		private static readonly Color Green = Color.green;
		private static readonly Color Yellow = Color.yellow;
		private static readonly Color Orange = new Color(1, 0.64f, 0);
		private static readonly Color Red = Color.red;

		private int _targetFramerate;
		private TMP_Text? _counter;
		private GameObject? _percent;
		private float _ringFillPercent = 1;
		private Image? _image;

		private float _timeLeft;
		private int _frameCount;
		private float _accumulatedTime;

		private void Awake()
		{
			try
			{
				Logger.Log.Debug("Attempting to Initialize FPS Counter");
				Init();
			}
			catch (Exception ex)
			{
				Logger.Log.Error("FPS Counter Done screwed up on initialization"); // -Kyle1413
				Logger.Log.Error(ex);
			}
		}

		private void Init()
		{
			_targetFramerate = (int) XRDevice.refreshRate;

			Logger.Log.Info($"Target framerate = {_targetFramerate}");

			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.WorldSpace;

			CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
			canvasScaler.scaleFactor = 10.0f;
			canvasScaler.dynamicPixelsPerUnit = 10f;

			gameObject.AddComponent<GraphicRaycaster>();
			gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
			gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);

			TextHelper.CreateText(out _counter, canvas, Vector2.zero);
			_counter.alignment = TextAlignmentOptions.Center;
			_counter.transform.localScale *= PluginUtils.IsCountersPlusPresent ? 1 : 0.12f;
			_counter.fontSize = 2.5f;
			_counter.color = Color.white;
			_counter.lineSpacing = -50f;
			_counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
			_counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
			_counter.enableWordWrapping = false;
			_counter.transform.localPosition = PluginUtils.IsCountersPlusPresent ? Vector3.zero : new Vector3(-0.1f, 3.5f, 8f);

			if (!Configuration.Instance.ShowRing)
			{
				return;
			}

			_percent = new GameObject();
			_image = _percent.AddComponent<Image>();
			_percent.transform.SetParent(_counter.transform, false);
			_percent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2f);
			_percent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2f);
			_percent.transform.localScale = new Vector3(4f, 4f, 4f);
			_percent.transform.localPosition = Vector3.zero;

			try
			{
				ScoreMultiplierUIController scoreMultiplier = Resources.FindObjectsOfTypeAll<ScoreMultiplierUIController>().First();
				var multiplierImage = BS_Utils.Utilities.ReflectionUtil.GetPrivateField<Image>(scoreMultiplier, "_multiplierProgressImage");

				if (scoreMultiplier && _image)
				{
					_image.sprite = multiplierImage.sprite;
					_image.type = Image.Type.Filled;
					_image.fillMethod = Image.FillMethod.Radial360;
					_image.fillOrigin = (int) Image.Origin360.Top;
					_image.fillClockwise = true;
				}
			}
			catch (Exception ex)
			{
				Logger.Log.Error("oops");
				Logger.Log.Error(ex);
			}
		}

		private void Update()
		{
			var localDeltaTime = Time.deltaTime;
			_accumulatedTime += Time.timeScale / localDeltaTime;
			_timeLeft -= localDeltaTime;
			++_frameCount;

			if (Configuration.Instance.ShowRing && _image)
			{
				// Animate the ring Fps indicator to it's final value with every update invocation 
				_image!.fillAmount = Mathf.Lerp(_image.fillAmount, _ringFillPercent, 2 * localDeltaTime);
			}

			if (_timeLeft > 0.0)
			{
				// The time to update hasn't come yet.
				return;
			}

			var fps = Mathf.Round(_accumulatedTime / _frameCount);
			_counter!.text = $"FPS\n{fps}";
			_ringFillPercent = fps / _targetFramerate;

			if (Configuration.Instance.UseColors)
			{
				var color = DetermineColor(_ringFillPercent);
				_counter.color = color;
				if (_image)
				{
					_image!.color = color;
				}
			}

			_timeLeft = Configuration.Instance.UpdateRate;
			_accumulatedTime = 0.0f;
			_frameCount = 0;
		}

		private static Color DetermineColor(float fpsTargetPercentage)
		{
			if (fpsTargetPercentage > 0.95f)
			{
				return Green;
			}

			if (fpsTargetPercentage > 0.7f)
			{
				return Yellow;
			}

			if (fpsTargetPercentage > 0.5f)
			{
				return Orange;
			}

			return Red;
		}
	}
}