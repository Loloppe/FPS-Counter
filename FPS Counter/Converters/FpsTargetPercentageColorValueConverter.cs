using UnityEngine;

namespace FPS_Counter.Converters
{
	internal static class FpsTargetPercentageColorValueConverter
	{
		// Allocate beforehand, because invoking a color "constant" will create a new struct every time
		private static readonly Color Green = Color.green;
		private static readonly Color Yellow = Color.yellow;
		private static readonly Color Orange = new Color(1, 0.64f, 0);
		private static readonly Color Red = Color.red;

		public static Color Convert(float fpsTargetPercentage)
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