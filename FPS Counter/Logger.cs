using IPALogger = IPA.Logging.Logger;

namespace FPS_Counter
{
	internal static class Logger
	{
		public static IPALogger Log { get; set; } = null!;
	}
}