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
		private TMP_Text _counter;
		private GameObject _percent;
		private int _targetFramerate;
		private Image _image;

		private float _numFrames;
		private float _lastFrameTime;
		private float _nextCounterUpdate = Time.time + Configuration.Instance.UpdateRate;
		private float _ringFillPercent = 1;

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
			_counter.transform.localScale *= Plugin.IsCountersPlusPresent ? 1 : 0.12f;
			_counter.fontSize = 2.5f;
			_counter.color = Color.white;
			_counter.lineSpacing = -50f;
			_counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
			_counter.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
			_counter.enableWordWrapping = false;
			_counter.transform.localPosition = Plugin.IsCountersPlusPresent ? Vector3.zero : new Vector3(-0.1f, 3.5f, 8f);

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
			if (Time.time > _nextCounterUpdate)
			{
				float fps = Mathf.Round(_numFrames / (Time.time - _lastFrameTime));
				_counter.text = $"FPS\n{fps}";
				_ringFillPercent = fps / _targetFramerate;

				if (Configuration.Instance.UseColors)
				{
					Color color;

					if (_ringFillPercent > 0.95f)
					{
						color = Color.green;
					}
					else if (_ringFillPercent > 0.7f)
					{
						color = Color.yellow;
					}
					else if (_ringFillPercent > 0.5f)
					{
						color = new Color(1, 0.64f, 0);
					}
					else
					{
						color = Color.red;
					}

					_image?.SetColor(color);
					_counter.color = color;
				}

				_lastFrameTime = _nextCounterUpdate;
				_nextCounterUpdate += Configuration.Instance.UpdateRate;
				_numFrames = 0;
			}

			if (Configuration.Instance.ShowRing)
			{
				if (_image)
				{
					_image.fillAmount = Mathf.Lerp(_image.fillAmount, _ringFillPercent, 2 * Time.deltaTime);
				}
			}

			_numFrames++;
		}
	}

	internal static class ExtensionMethods
	{
		public static void SetColor(this Image image, Color color)
		{
			if (image)
			{
				image.color = color;
			}
		}
	}
}