using System.Linq;
using FPS_Counter.Converters;
using FPS_Counter.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FPS_Counter.Utilities
{
	internal static class FpsCounterUtils
	{
		private const string MULTIPLIER_IMAGE_SPRITE_NAME = "Circle";
		private static Sprite? _multiplierImageSprite;

		internal static Sprite MultiplierImageSprite => _multiplierImageSprite ??= Resources.FindObjectsOfTypeAll<Sprite>().Where(x => x.name == MULTIPLIER_IMAGE_SPRITE_NAME).First();


		internal static Image CreateRing(Canvas canvas)
		{
			// Unfortunately, there is no guarantee that I have the CoreGameHUDController, since No Text and Huds
			// completely disables it from spawning. So, to be safe, we recreate this all from scratch.
			Image newImage = new GameObject("Ring Image", typeof(RectTransform), typeof(Image)).GetComponent<Image>();
			newImage.sprite = MultiplierImageSprite;
			newImage.transform.SetParent(canvas.transform, false);
			newImage.type = Image.Type.Filled;
			newImage.fillClockwise = true;
			newImage.fillOrigin = 2;
			newImage.fillMethod = Image.FillMethod.Radial360;
			return newImage;
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