using System;

// Token: 0x02000091 RID: 145
public class MissionInfo
{
	// Token: 0x06000453 RID: 1107 RVA: 0x000130D1 File Offset: 0x000112D1
	public MissionInfo(Mission mission, MissionTemplate template, int progress, bool complete)
	{
		this.mission = mission;
		this.template = template;
		this.progress = progress;
		this.complete = complete;
	}

	// Token: 0x040003AD RID: 941
	public Mission mission;

	// Token: 0x040003AE RID: 942
	public MissionTemplate template;

	// Token: 0x040003AF RID: 943
	public int progress;

	// Token: 0x040003B0 RID: 944
	public bool complete;
}
