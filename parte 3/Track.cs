using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class Track : MonoBehaviour
{
	// Token: 0x17000096 RID: 150
	// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001F3B0 File Offset: 0x0001D5B0
	// (set) Token: 0x0600063B RID: 1595 RVA: 0x0001F3B8 File Offset: 0x0001D5B8
	public bool IsRunningOnTutorialTrack { get; set; }

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001F3C1 File Offset: 0x0001D5C1
	public static Track Instance
	{
		get
		{
			Track track;
			if ((track = Track.instance) == null)
			{
				track = (Track.instance = Object.FindObjectOfType(typeof(Track)) as Track);
			}
			return track;
		}
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0001F3E8 File Offset: 0x0001D5E8
	public void Awake()
	{
		this.trackSpacing = (this.trackRight.position - this.trackLeft.position).magnitude / (float)(this.numberOfTracks - 1);
		this.trackChunks = new TrackChunkCollection();
		this.hoverboard = Hoverboard.Instance;
		this.tutorial = !PlayerInfo.Instance.tutorialCompleted;
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0001F451 File Offset: 0x0001D651
	public Vector3 GetPosition(float x, float z)
	{
		return Vector3.forward * z + this.trackLeft.position + x * Vector3.right;
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0001F47E File Offset: 0x0001D67E
	public float GetTrackX(int trackIndex)
	{
		return this.trackSpacing * (float)trackIndex;
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0001F489 File Offset: 0x0001D689
	public float LayJetpackChunks(float characterZ, float flyLength)
	{
		this.LayTracksUpTo(characterZ, flyLength, true);
		float num = this.trackChunkZ - characterZ;
		this.LayTrackChunk(this.jetpackLandingChunk);
		return num;
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
	public void LayEmptyChunks(float characterZ, float removeDistance)
	{
		this.RemoveChunkObstacles(characterZ + removeDistance);
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0001F4B4 File Offset: 0x0001D6B4
	public void RemoveChunkObstacles(float removeDistance)
	{
		foreach (TrackChunk trackChunk in this.activeTrackChunks)
		{
			trackChunk.DeactivateObstacles(removeDistance);
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0001F508 File Offset: 0x0001D708
	public void Initialize(float characterZ)
	{
		this.trackChunks.Initialize(characterZ);
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0001F516 File Offset: 0x0001D716
	public void LayTrackChunks(float characterZ)
	{
		this.LayTracksUpTo(characterZ, this.trackAheadDistance, false);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0001F528 File Offset: 0x0001D728
	public void LayTracksUpTo(float characterZ, float trackAheadDistance, bool isJetpack)
	{
		if (!this.trackChunks.CanDeliver())
		{
			return;
		}
		float num = characterZ + trackAheadDistance;
		float num2 = 200f;
		Debug.DrawLine(Vector3.forward * num + Vector3.left * num2, Vector3.forward * num + -Vector3.left * num2, Color.white);
		if (this.trackChunkZ < num)
		{
			this.CleanupTrackChunks(characterZ);
		}
		int num3 = 0;
		while (this.trackChunkZ < num)
		{
			this.trackChunks.MoveForward(this.trackChunkZ);
			TrackChunk trackChunk;
			if (this.firstTrackChunk && this.tutorial)
			{
				trackChunk = this.tutorialTrackChunk;
				this.firstTrackChunk = false;
				if (trackChunk.CheckPoints.Count > 0)
				{
					this.IsRunningOnTutorialTrack = true;
				}
				this.hoverboard.isAllowed = false;
			}
			else if (isJetpack)
			{
				trackChunk = this.trackChunks.GetJetPakChunk(num3);
				num3++;
			}
			else
			{
				trackChunk = this.trackChunks.GetRandomActive();
				int num4 = 0;
				while (this.activeTrackChunks.Contains(trackChunk) && num4 < 1000)
				{
					trackChunk = this.trackChunks.GetRandomActive();
					num4++;
				}
				if (num4 == 1000)
				{
					Debug.Log("active track chunks");
					Debug.Log("active: " + string.Join(", ", this.activeTrackChunks.ConvertAll<string>((TrackChunk chunk) => chunk.gameObject.name).ToArray()));
					Debug.LogError("infinite loop. not track chunks to select.");
				}
			}
			this.LayTrackChunk(trackChunk);
		}
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0001F6C9 File Offset: 0x0001D8C9
	private void LayTrackChunk(TrackChunk trackChunk)
	{
		base.StartCoroutine(this.LayTrackChunkAsync(trackChunk));
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0001F6D9 File Offset: 0x0001D8D9
	private IEnumerator LayTrackChunkAsync(TrackChunk trackChunk)
	{
		trackChunk.gameObject.transform.position = Vector3.forward * this.trackChunkZ;
		this.trackChunkZ += trackChunk.zSize;
		this.activeTrackChunks.Add(trackChunk);
		trackChunk.RestoreHiddenObstacles();
		yield return base.StartCoroutine(this.PerformRecursiveSelection(trackChunk.gameObject, true));
		int num = 0;
		Array.Sort<TrackObject>(trackChunk.objects, (TrackObject g1, TrackObject g2) => g1.transform.position.z.CompareTo(g2.transform.position.z));
		TrackObject[] objects = trackChunk.objects;
		foreach (TrackObject trackObject in objects)
		{
			if (trackObject.gameObject.active)
			{
				trackObject.Activate();
				num++;
			}
			if (num == 1)
			{
				yield return null;
				num = 0;
			}
		}
		TrackObject[] array = null;
		yield break;
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0001F6EF File Offset: 0x0001D8EF
	private IEnumerator ActivateGameObjects(List<GameObject> objects)
	{
		objects.Sort((GameObject g1, GameObject g2) => g1.transform.position.z.CompareTo(g2.transform.position.z));
		int num = 0;
		foreach (GameObject gameObject in objects)
		{
			gameObject.active = true;
			num++;
			if (num == 4)
			{
				yield return null;
				num = 0;
			}
		}
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0001F6FE File Offset: 0x0001D8FE
	private IEnumerator PerformRecursiveSelection(GameObject parent, bool sortSpawnPoints = true)
	{
		List<GameObject> list = new List<GameObject>();
		List<GameObject> list2 = new List<GameObject>();
		List<Track.SpawnPointWrapper> spawnPoints = new List<Track.SpawnPointWrapper>();
		list2.Add(parent);
		while (list2.Count > 0)
		{
			GameObject gameObject = list2[0];
			list2.RemoveAt(0);
			if (sortSpawnPoints)
			{
				SpawnPoint component = gameObject.GetComponent<SpawnPoint>();
				if (component != null)
				{
					spawnPoints.Add(new Track.SpawnPointWrapper(component));
					continue;
				}
			}
			RandomizeOffset component2 = gameObject.GetComponent<RandomizeOffset>();
			if (component2 != null)
			{
				component2.ChooseRandomOffset();
			}
			Transform transform = gameObject.transform;
			list.Add(gameObject);
			Selector component3 = gameObject.GetComponent<Selector>();
			if (component3 != null)
			{
				component3.PerformSelection(list2);
			}
			else if (gameObject.GetComponent<Group>() == null)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					GameObject gameObject2 = transform.GetChild(i).gameObject;
					list2.Add(gameObject2);
				}
			}
		}
		List<GameObject> list3 = new List<GameObject>();
		list3 = list.Where<GameObject>((GameObject x) => this.IsLowPriority(x)).ToList<GameObject>();
		list = list.Where<GameObject>((GameObject x) => !this.IsLowPriority(x)).ToList<GameObject>();
		list.Sort((GameObject g1, GameObject g2) => g1.transform.position.z.CompareTo(g2.transform.position.z));
		list3.Sort((GameObject g1, GameObject g2) => g1.transform.position.z.CompareTo(g2.transform.position.z));
		list.AddRange(list3);
		int num = 0;
		foreach (GameObject gameObject3 in list)
		{
			gameObject3.active = true;
			num++;
			if (num == 4)
			{
				yield return null;
				num = 0;
			}
		}
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		if (spawnPoints.Count <= 0)
		{
			yield break;
		}
		spawnPoints.Sort((Track.SpawnPointWrapper x, Track.SpawnPointWrapper y) => x.Z.CompareTo(y.Z));
		foreach (SpawnPoint spawnPoint in spawnPoints.ConvertAll<SpawnPoint>((Track.SpawnPointWrapper wrapper) => wrapper.SpawnPoint))
		{
			yield return base.StartCoroutine(this.PerformRecursiveSelection(spawnPoint.gameObject, false));
		}
		List<SpawnPoint>.Enumerator enumerator2 = default(List<SpawnPoint>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0001F71B File Offset: 0x0001D91B
	private bool IsLowPriority(GameObject g)
	{
		return g.layer != 16;
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0001F72C File Offset: 0x0001D92C
	public void CleanupTrackChunks(float characterZ)
	{
		float num = characterZ - this.cleanUpDistance;
		foreach (TrackChunk trackChunk in this.activeTrackChunks)
		{
			if (trackChunk.transform.position.z + trackChunk.zSize < num)
			{
				this.trackChunksForDeactivation.Add(trackChunk);
			}
		}
		foreach (TrackChunk trackChunk2 in this.trackChunksForDeactivation)
		{
			if (!trackChunk2.isTutorial)
			{
				trackChunk2.Deactivate();
			}
			this.activeTrackChunks.Remove(trackChunk2);
		}
		this.trackChunksForDeactivation.Clear();
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0001F808 File Offset: 0x0001DA08
	public void DeactivateTrackChunks()
	{
		base.StopAllCoroutines();
		foreach (TrackChunk trackChunk in this.activeTrackChunks)
		{
			trackChunk.Deactivate();
		}
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0001F860 File Offset: 0x0001DA60
	public void Restart()
	{
		foreach (TrackChunk trackChunk in this.trackChunks.TrackChunks)
		{
			Vector3 position = trackChunk.transform.position;
			position.y = -1000f;
			trackChunk.transform.position = position;
		}
		this.trackChunkZ = 0f;
		this.trackChunks.Initialize(0f);
		foreach (TrackChunk trackChunk2 in this.activeTrackChunks)
		{
			trackChunk2.Deactivate();
		}
		this.activeTrackChunks.Clear();
		this.firstTrackChunk = true;
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0001F920 File Offset: 0x0001DB20
	private void TrackPositionGizmos()
	{
		for (int i = 0; i < this.numberOfTracks; i++)
		{
			Vector3 vector = Vector3.Lerp(this.trackLeft.position, this.trackRight.position, (float)i / (float)(this.numberOfTracks - 1));
			Gizmos.DrawLine(vector, vector + Vector3.forward * 5f);
		}
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0001F97F File Offset: 0x0001DB7F
	public void OnDrawGizmos()
	{
		this.TrackPositionGizmos();
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0001F988 File Offset: 0x0001DB88
	public float GetLastCheckPoint(float characterZ)
	{
		foreach (TrackChunk trackChunk in this.activeTrackChunks)
		{
			if (this.IsRunningOnTutorialTrack && trackChunk == this.tutorialTrackChunk)
			{
				return trackChunk.GetLastCheckPoint(characterZ);
			}
		}
		Debug.Log("No checkpoints in track");
		return 0f;
	}

	// Token: 0x04000550 RID: 1360
	private const int ActiveTruePerFrame = 4;

	// Token: 0x04000551 RID: 1361
	private const int ActivatePerFrame = 1;

	// Token: 0x04000552 RID: 1362
	public Transform trackLeft;

	// Token: 0x04000553 RID: 1363
	public Transform trackRight;

	// Token: 0x04000554 RID: 1364
	public int numberOfTracks = 3;

	// Token: 0x04000555 RID: 1365
	public float cleanUpDistance = 2000f;

	// Token: 0x04000556 RID: 1366
	public float trackAheadDistance = 700f;

	// Token: 0x04000557 RID: 1367
	public Transform levelChunksParent;

	// Token: 0x04000558 RID: 1368
	public TrackChunk jetpackLandingChunk;

	// Token: 0x04000559 RID: 1369
	public bool tutorial;

	// Token: 0x0400055A RID: 1370
	public TrackChunk tutorialTrackChunk;

	// Token: 0x0400055B RID: 1371
	private bool firstTrackChunk = true;

	// Token: 0x0400055C RID: 1372
	private TrackChunkCollection trackChunks;

	// Token: 0x0400055D RID: 1373
	private float trackSpacing;

	// Token: 0x0400055E RID: 1374
	private float trackChunkZ;

	// Token: 0x0400055F RID: 1375
	private List<TrackChunk> activeTrackChunks = new List<TrackChunk>(5);

	// Token: 0x04000560 RID: 1376
	private List<TrackChunk> trackChunksForDeactivation = new List<TrackChunk>(5);

	// Token: 0x04000561 RID: 1377
	private Hoverboard hoverboard;

	// Token: 0x04000562 RID: 1378
	private static Track instance;

	// Token: 0x020001E5 RID: 485
	private struct SpawnPointWrapper
	{
		// Token: 0x06000C09 RID: 3081 RVA: 0x0003D8DB File Offset: 0x0003BADB
		public SpawnPointWrapper(SpawnPoint spawnPoint)
		{
			this.SpawnPoint = spawnPoint;
			this.Z = spawnPoint.transform.position.z;
		}

		// Token: 0x04000B58 RID: 2904
		public SpawnPoint SpawnPoint;

		// Token: 0x04000B59 RID: 2905
		public float Z;
	}
}
