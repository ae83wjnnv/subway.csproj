using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000095 RID: 149
[RequireComponent(typeof(TrackObject))]
public class MovingCoin : MonoBehaviour
{
	// Token: 0x06000464 RID: 1124 RVA: 0x00014C74 File Offset: 0x00012E74
	public void Awake()
	{
		this.game = Game.Instance;
		if (!(this.game == null))
		{
			if (MovingCoin.characterController == null)
			{
				MovingCoin.characterController = Character.Instance.characterController;
			}
			if (base.transform.childCount == 0)
			{
				Debug.Log("No coin child");
			}
			this.coin = base.transform.GetChild(0);
			this.coin.localPosition = -Vector3.up * 200f;
			base.enabled = false;
			TrackObject component = base.GetComponent<TrackObject>();
			component.OnActivate = (TrackObject.OnActivateDelegate)Delegate.Combine(component.OnActivate, new TrackObject.OnActivateDelegate(this.OnActivate));
			component.OnDeactivate = (TrackObject.OnDeactivateDelegate)Delegate.Combine(component.OnDeactivate, new TrackObject.OnDeactivateDelegate(this.OnDeactivate));
		}
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00014D54 File Offset: 0x00012F54
	public void OnActivate()
	{
		MovingCoin.activecoins.Add(this);
		base.enabled = true;
		this.autoPilot = false;
		this.coin.localPosition = new Vector3(0f, 0f, (base.transform.position.z - MovingCoin.characterController.transform.position.z) * this.speed);
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00014DC0 File Offset: 0x00012FC0
	public void Update()
	{
		if (!(this.game == null))
		{
			if (this.autoPilot)
			{
				this.coin.position -= Vector3.forward * Time.deltaTime * this.game.currentSpeed * this.speed;
				return;
			}
			Vector3 vector = new Vector3(0f, 0f, (base.transform.position.z - MovingCoin.characterController.transform.position.z) * this.speed);
			Vector3 vector2 = base.transform.TransformPoint(vector);
			this.coin.position = vector2;
		}
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00014E7D File Offset: 0x0001307D
	public void OnDeactivate()
	{
		MovingCoin.activecoins.Remove(this);
		base.enabled = false;
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00014E94 File Offset: 0x00013094
	public void OnDrawGizmos()
	{
		if (this.coin != null)
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(this.coin.position, base.transform.position);
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(base.transform.position, 5f);
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(this.coin.position, 5f);
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00014F14 File Offset: 0x00013114
	public static void ActivateAutoPilot()
	{
		foreach (MovingCoin movingCoin in MovingCoin.activecoins)
		{
			if (movingCoin.GetComponent<Collider>().transform.position.z - MovingCoin.characterController.transform.position.z < MovingCoin.autoPilotActivationDistance)
			{
				movingCoin.autoPilot = true;
			}
		}
	}

	// Token: 0x040003C1 RID: 961
	public float speed = 1f;

	// Token: 0x040003C2 RID: 962
	private Transform coin;

	// Token: 0x040003C3 RID: 963
	private Game game;

	// Token: 0x040003C4 RID: 964
	private static CharacterController characterController;

	// Token: 0x040003C5 RID: 965
	private static List<MovingCoin> activecoins = new List<MovingCoin>();

	// Token: 0x040003C6 RID: 966
	private bool autoPilot;

	// Token: 0x040003C7 RID: 967
	public static float autoPilotActivationDistance = 200f;
}
