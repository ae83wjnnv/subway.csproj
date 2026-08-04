using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class TrackChunkCollection
{
	// Token: 0x17000098 RID: 152
	// (get) Token: 0x0600065D RID: 1629 RVA: 0x0001FDAD File Offset: 0x0001DFAD
	public TrackChunk[] TrackChunks
	{
		get
		{
			return TrackChunkCollection.trackChunks.ToArray();
		}
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0001FDBC File Offset: 0x0001DFBC
	public static void AddToChunks(TrackChunk newTrackChunk)
	{
		int count = TrackChunkCollection.trackChunks.Count;
		if (count == 0)
		{
			TrackChunkCollection.trackChunks.Add(newTrackChunk);
			return;
		}
		int num = 0;
		while (TrackChunkCollection.trackChunks[num].zMinimum < newTrackChunk.zMinimum)
		{
			num++;
			if (num == count)
			{
				break;
			}
		}
		TrackChunkCollection.trackChunks.Insert(num, newTrackChunk);
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0001FE14 File Offset: 0x0001E014
	public void Initialize(float z)
	{
		this.activeTrackChunks.Clear();
		this.lastAddedIndex = -1;
		for (int i = 0; i < TrackChunkCollection.trackChunks.Count; i++)
		{
			TrackChunk trackChunk = TrackChunkCollection.trackChunks[i];
			if (trackChunk.zMinimum <= z && z < trackChunk.zMaximum)
			{
				this.activeTrackChunks.Add(trackChunk);
				this.lastAddedIndex = i;
			}
		}
		this.Recalculate();
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0001FE80 File Offset: 0x0001E080
	public void MoveForward(float z)
	{
		int num = 0;
		for (int i = this.lastAddedIndex + 1; i < TrackChunkCollection.trackChunks.Count; i++)
		{
			TrackChunk trackChunk2 = TrackChunkCollection.trackChunks[i];
			if (trackChunk2.zMinimum > z)
			{
				break;
			}
			this.activeTrackChunks.Add(trackChunk2);
			num++;
			this.lastAddedIndex = i;
		}
		int num2 = this.activeTrackChunks.RemoveAll((TrackChunk trackChunk) => trackChunk.zMaximum < z);
		if (num > 0 || num2 > 0)
		{
			this.Recalculate();
		}
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0001FF14 File Offset: 0x0001E114
	private void Recalculate()
	{
		this.randomSpace.Clear();
		for (int i = 0; i < this.activeTrackChunks.Count; i++)
		{
			TrackChunk trackChunk = this.activeTrackChunks[i];
			for (int j = 0; j < trackChunk.probability; j++)
			{
				this.randomSpace.Add(i);
			}
		}
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0001FF6C File Offset: 0x0001E16C
	public bool CanDeliver()
	{
		return this.randomSpace.Count > 0;
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0001FF7C File Offset: 0x0001E17C
	public TrackChunk GetRandomActive()
	{
		int num = Random.Range(0, this.randomSpace.Count);
		int num2 = this.randomSpace[num];
		return this.activeTrackChunks[num2];
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0001FFB4 File Offset: 0x0001E1B4
	public TrackChunk GetJetPakChunk(int index)
	{
		TrackChunk trackChunk = TrackChunkCollection.trackChunks[TrackChunkCollection.trackChunks.Count - 1 - index];
		if (trackChunk.zMaximum > 0f || trackChunk.zMinimum < 1000000f)
		{
			Debug.Log(string.Concat(new string[]
			{
				"Illegal TrackChunk used in jetpack mode. Index=",
				index.ToString(),
				" Name : ",
				trackChunk.name,
				" zmax : ",
				trackChunk.zMaximum.ToString(),
				" zmin : ",
				trackChunk.zMinimum.ToString()
			}));
			Debug.Break();
		}
		return trackChunk;
	}

	// Token: 0x0400056E RID: 1390
	public static List<TrackChunk> trackChunks = new List<TrackChunk>();

	// Token: 0x0400056F RID: 1391
	private List<TrackChunk> activeTrackChunks = new List<TrackChunk>();

	// Token: 0x04000570 RID: 1392
	private int lastAddedIndex = -1;

	// Token: 0x04000571 RID: 1393
	private List<int> randomSpace = new List<int>();
}
