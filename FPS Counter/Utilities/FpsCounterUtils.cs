using System.Linq;
using FPS_Counter.Converters;
using FPS_Counter.Settings;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FPS_Counter.Utilities
{
	internal static class FpsCounterUtils
	{
		private const string NO_GLOW_MATERIAL_NAME = "UINoGlow";
		private const string MULTIPLIER_IMAGE_SPRITE_NAME = "Circle";
		private static Material? _noGlowMaterial;
		private static Sprite? _multiplierImageSprite;

		internal static Material NoGlowMaterial => _noGlowMaterial ??= new Material(Resources.FindObjectsOfTypeAll<Material>().Where(m => m.name == NO_GLOW_MATERIAL_NAME).First());
		internal static Sprite MultiplierImageSprite => _multiplierImageSprite ??= Resources.FindObjectsOfTypeAll<Sprite>().Where(x => x.name == MULTIPLIER_IMAGE_SPRITE_NAME).First();


		// Pretty much yoinked from https://github.com/Caeden117/CountersPlus/blob/master/Counters%2B/Counters/ProgressCounter.cs#L93-L110
		// Please don't smite me
		internal static ImageView CreateRing(Canvas canvas)
		{
			var ringGo = new GameObject("Ring Image", typeof(RectTransform));
			ringGo.transform.SetParent(canvas.transform, false);
			var ringImage = ringGo.AddComponent<ImageView>();
			ringImage.enabled = false;
			ringImage.material = NoGlowMaterial;
			ringImage.sprite = MultiplierImageSprite;
			ringImage.type = Image.Type.Filled;
			ringImage.fillClockwise = true;
			ringImage.fillOrigin = 2;
			ringImage.fillMethod = Image.FillMethod.Radial360;
			// ReSharper disable once Unity.InefficientPropertyAccess
			ringImage.enabled = true;
			return ringImage;
		}

		internal static void SharedTicker(ref float accumulatedTime, ref float timeLeft, ref int frameCount, ref int targetFramerate, ref float ringFillPercent, Image? percentageRing, TMP_Text? text)
		{
			var localDeltaTime = Time.deltaTime;
			accumulatedTime += Time.timeScale / localDeltaTime;
			timeLeft -= localDeltaTime;
			++frameCount;

			if (Configuration.Instance!.ShowRing && percentageRing)
			{
				// Animate the ring Fps indicator to it's final value with every update invocation
				percentageRing!.fillAmount = Mathf.Lerp(percentageRing.fillAmount, ringFillPercent, 2 * localDeltaTime);
			}

			if (timeLeft > 0.0)
			{
				// The time to update hasn't come yet.
				return;
			}

			var fps = Mathf.Round(accumulatedTime / frameCount);
			text!.text = $"FPS\n{fps}";
			ringFillPercent = fps / targetFramerate;

			if (Configuration.Instance.UseColors)
			{
				var color = FpsTargetPercentageColorValueConverter.Convert(ringFillPercent);
				text.color = color;
				if (percentageRing)
				{
					percentageRing!.color = color;
				}
			}

			timeLeft = Configuration.Instance.UpdateRate;
			accumulatedTime = 0.0f;
			frameCount = 0;
		}
	}
}