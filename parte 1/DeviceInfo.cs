using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
public static class DeviceInfo
{
	// Token: 0x040001E2 RID: 482
	public static string deviceModel = SystemInfo.deviceModel;

	// Token: 0x040001E3 RID: 483
	public static readonly DeviceInfo.FormFactor formFactor;

	// Token: 0x040001E4 RID: 484
	public static readonly bool isHighres;

	// Token: 0x02000184 RID: 388
	public enum FormFactor
	{
		// Token: 0x04000953 RID: 2387
		iPhone,
		// Token: 0x04000954 RID: 2388
		iPad
	}

	// Token: 0x02000185 RID: 389
	public enum PerformanceLevel
	{
		// Token: 0x04000956 RID: 2390
		Low,
		// Token: 0x04000957 RID: 2391
		Medium,
		// Token: 0x04000958 RID: 2392
		High
	}
}
