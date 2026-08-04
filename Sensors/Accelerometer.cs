using System;

namespace Sensors
{
	// Token: 0x02000159 RID: 345
	public class Accelerometer
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x0003701F File Offset: 0x0003521F
		public static bool init()
		{
			return true;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00037022 File Offset: 0x00035222
		public static void raw(ref IntPtr data)
		{
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00037024 File Offset: 0x00035224
		public static void raw(out AccelData accelData)
		{
			accelData = default(AccelData);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0003702D File Offset: 0x0003522D
		public static void terminate()
		{
		}

		// Token: 0x0400089D RID: 2205
		private static bool hasInitialized;
	}
}
