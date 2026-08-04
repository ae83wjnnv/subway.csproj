using System;

// Token: 0x02000090 RID: 144
public struct Mission
{
	// Token: 0x06000452 RID: 1106 RVA: 0x000130C1 File Offset: 0x000112C1
	public Mission(Missions.MissionType type, int goal)
	{
		this.type = type;
		this.goal = goal;
	}

	// Token: 0x040003AB RID: 939
	public Missions.MissionType type;

	// Token: 0x040003AC RID: 940
	public int goal;
}
