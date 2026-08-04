using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000096 RID: 150
[RequireComponent(typeof(TrackObject))]
public class MovingTrain : MonoBehaviour
{
	// Token: 0x0600046C RID: 1132 RVA: 0x00014FC4 File Offset: 0x000131C4
	public void Awake()
	{
		this.game = Game.Instance;
		if (!(this.game == null))
		{
			if (this.game.awakeDone)
			{
				this.Init();
			}
			if (base.transform.childCount == 0)
			{
				Debug.Log("No train child");
			}
			this.train = base.transform.GetChild(0);
			this.train.localPosition = -Vector3.up * 200f;
			this.trainCollider = base.GetComponent<BoxCollider>();
			Vector3 size = this.trainCollider.size;
			this.trainCollider.size = new Vector3(size.x, size.y, size.z / (1f + this.speed));
			this.trainCollider.center = new Vector3(0f, 15.2f, (30f * this.trainCount + 1f) / (1f + this.speed));
			base.enabled = false;
			TrackObject component = base.GetComponent<TrackObject>();
			component.OnActivate = (TrackObject.OnActivateDelegate)Delegate.Combine(component.OnActivate, new TrackObject.OnActivateDelegate(this.OnActivate));
			component.OnDeactivate = (TrackObject.OnDeactivateDelegate)Delegate.Combine(component.OnDeactivate, new TrackObject.OnDeactivateDelegate(this.OnDeactivate));
			this.trainPassSource = base.gameObject.AddComponent<AudioSource>();
			this.trainPassSource.minDistance = 20f;
			this.trainPassSource.maxDistance = 50f;
			this.trainPassSource.playOnAwake = false;
			this.trainPassSource.loop = true;
			this.trainPassSource.clip = this.trianPassClip;
		}
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00015172 File Offset: 0x00013372
	private void Init()
	{
		if (!this.isInitialized)
		{
			MovingTrain.characterController = this.game.character.characterController;
			this.isInitialized = true;
		}
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00015198 File Offset: 0x00013398
	public void OnActivate()
	{
		MovingTrain.activeTrains.Add(this);
		base.enabled = true;
		this.autoPilot = false;
		this.train.localPosition = new Vector3(0f, 0f, (base.transform.position.z - MovingTrain.characterController.transform.position.z) * this.speed);
		this.startSound = true;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0001520C File Offset: 0x0001340C
	public void Update()
	{
		this.Init();
		if (this.startSound)
		{
			this.trainPassSource.pitch = Random.Range(0.8f, 1.1f);
			this.trainPassSource.volume = Random.Range(0.1f, 0.6f);
			this.trainPassSource.timeSamples = Random.Range(0, this.trainPassSource.timeSamples);
			this.trainPassSource.Play();
			this.startSound = false;
		}
		if (this.autoPilot)
		{
			this.train.position -= Vector3.forward * Time.deltaTime * this.game.currentSpeed * this.speed;
			return;
		}
		Vector3 vector = new Vector3(0f, 0f, (base.transform.position.z - MovingTrain.characterController.transform.position.z) * this.speed);
		Vector3 vector2 = base.transform.TransformPoint(vector);
		this.train.position = vector2;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x00015328 File Offset: 0x00013528
	public void OnDeactivate()
	{
		this.trainPassSource.Stop();
		MovingTrain.activeTrains.Remove(this);
		base.enabled = false;
		this.train.transform.localPosition = -100f * Vector3.up;
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00015368 File Offset: 0x00013568
	public void OnDrawGizmos()
	{
		if (this.train != null)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(this.train.position, base.transform.position);
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(base.transform.position, 5f);
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(this.train.position, 5f);
		}
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x000153E8 File Offset: 0x000135E8
	public static void ActivateAutoPilot()
	{
		foreach (MovingTrain movingTrain in MovingTrain.activeTrains)
		{
			if (movingTrain.GetComponent<Collider>().bounds.min.z - MovingTrain.characterController.transform.position.z < MovingTrain.autoPilotActivationDistance)
			{
				movingTrain.autoPilot = true;
			}
		}
	}

	// Token: 0x040003C8 RID: 968
	public float speed = 1f;

	// Token: 0x040003C9 RID: 969
	private Transform train;

	// Token: 0x040003CA RID: 970
	private BoxCollider trainCollider;

	// Token: 0x040003CB RID: 971
	public float trainCount = 3f;

	// Token: 0x040003CC RID: 972
	private Game game;

	// Token: 0x040003CD RID: 973
	private static CharacterController characterController;

	// Token: 0x040003CE RID: 974
	private static List<MovingTrain> activeTrains = new List<MovingTrain>();

	// Token: 0x040003CF RID: 975
	private bool autoPilot;

	// Token: 0x040003D0 RID: 976
	public static float autoPilotActivationDistance = 200f;

	// Token: 0x040003D1 RID: 977
	public AudioClip trianPassClip;

	// Token: 0x040003D2 RID: 978
	private AudioSource trainPassSource;

	// Token: 0x040003D3 RID: 979
	private bool isInitialized;

	// Token: 0x040003D4 RID: 980
	private bool startSound;
}
