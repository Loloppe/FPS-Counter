using BeatSaberMarkupLanguage;
using TMPro;
using UnityEngine;

namespace FPS_Counter.Utilities
{
	internal static class TextHelper
	{
		private const float Scale = 10f;

		// Joinked from Counters+ (since its method was internal (which means inaccessible (which should probably be changed)))
		public static void CreateText(out TMP_Text tmpText, Canvas canvas, Vector3 anchoredPosition)
		{
			var rectTransform = canvas.transform as RectTransform;
			rectTransform!.sizeDelta = new Vector2(100, 50);

			tmpText = BeatSaberUI.CreateText(rectTransform, string.Empty, anchoredPosition * Scale);
			tmpText.alignment = TextAlignmentOptions.Center;
			tmpText.fontSize = 4f;
			tmpText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 2f);
			tmpText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 2f);
			tmpText.enableWordWrapping = false;
			tmpText.overflowMode = TextOverflowModes.Overflow;
		}
	}
}