using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace FPS_Counter.Settings
{
	internal class Configuration
	{
		public virtual float UpdateRate { get; set; } = 0.5f;
		public virtual bool ShowRing { get; set; } = true;
		public virtual bool UseColors { get; set; } = true;
		public virtual float PosX { get; set; } = 0.0f;
		public virtual float PosY { get; set; } = 3.5f;
		public virtual float PosZ { get; set; } = 8.0f;
	}
}