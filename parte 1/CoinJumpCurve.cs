using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class CoinJumpCurve : MonoBehaviour
{
	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000239 RID: 569 RVA: 0x00009B8E File Offset: 0x00007D8E
	private float JumpHeight
	{
		get
		{
			if (this.superSneakers)
			{
				return this.character.jumpHeightSuperSneakers;
			}
			return this.character.jumpHeightNormal;
		}
	}

	// Token: 0x0600023A RID: 570 RVA: 0x00009BB0 File Offset: 0x00007DB0
	public void Awake()
	{
		this.game = Game.Instance;
		this.running = Running.Instance;
		this.character = Character.Instance;
		if (CoinJumpCurve.coinPool == null)
		{
			CoinJumpCurve.coinPool = CoinPool.Instance;
		}
		TrackObject component = base.GetComponent<TrackObject>();
		component.OnActivate = (TrackObject.OnActivateDelegate)Delegate.Combine(component.OnActivate, new TrackObject.OnActivateDelegate(this.OnActivate));
		component.OnDeactivate = (TrackObject.OnDeactivateDelegate)Delegate.Combine(component.OnDeactivate, new TrackObject.OnDeactivateDelegate(this.OnDeactivate));
		this.group = Utils.FindComponentInThisOrParents<Group>(base.transform);
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00009C50 File Offset: 0x00007E50
	public void OnActivate()
	{
		if (this.activation == 1)
		{
			Debug.Log("CoinJumpCurve has been activate twice. " + Utils.GetLongName(base.transform));
			Debug.Break();
		}
		this.activation++;
		float num = this.character.JumpLength(this.game.currentLevelSpeed, this.JumpHeight);
		for (float num2 = this.beginRatio * num; num2 < this.endRatio * num; num2 += this.coinSpacing)
		{
			Transform coin = CoinJumpCurve.coinPool.GetCoin();
			coin.parent = base.transform;
			coin.position = this.CalcJumpCurve(num2 / num);
			coin.GetComponent<TrackObject>().Activate();
			this.coins.Add(coin);
		}
		if (this.group != null)
		{
			this.group.UpdateChildren();
		}
	}

	// Token: 0x0600023C RID: 572 RVA: 0x00009D28 File Offset: 0x00007F28
	public void OnDeactivate()
	{
		foreach (Transform transform in this.coins)
		{
			transform.GetComponent<TrackObject>().OnDeactivate();
		}
		this.activation--;
		CoinJumpCurve.coinPool.Put(this.coins);
		this.coins.Clear();
	}

	// Token: 0x0600023D RID: 573 RVA: 0x00009DAC File Offset: 0x00007FAC
	private float NormalizedJumpCurve(float z)
	{
		return 4f * z * (1f - z);
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00009DBD File Offset: 0x00007FBD
	private float InvertedSpeed(float z)
	{
		return this.NormalizedJumpCurve(z) / Mathf.Sqrt(1f + Mathf.Pow(-8f * z + 4f, 2f));
	}

	// Token: 0x0600023F RID: 575 RVA: 0x00009DE9 File Offset: 0x00007FE9
	private Vector3 CalcJumpCurve(float ratio)
	{
		return this.CalcJumpCurve(ratio, this.game.currentLevelSpeed);
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00009E00 File Offset: 0x00008000
	private Vector3 CalcJumpCurve(float ratio, float speed)
	{
		return base.transform.position + base.transform.forward * this.character.JumpLength(speed, this.JumpHeight) * (ratio - this.curveOffset) + base.transform.up * this.NormalizedJumpCurve(ratio) * this.JumpHeight;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00009E74 File Offset: 0x00008074
	public void OnDrawGizmos()
	{
		if (this.game == null)
		{
			this.game = Game.Instance;
		}
		if (this.character == null)
		{
			this.character = Character.Instance;
		}
		if (this.running == null)
		{
			this.running = Running.Instance;
		}
		this.DrawCurve(this.game.speed.min, Color.grey);
		this.DrawCurve(this.game.speed.max, Color.grey);
		this.DrawCurve(this.speed, Color.yellow);
	}

	// Token: 0x06000242 RID: 578 RVA: 0x00009F14 File Offset: 0x00008114
	private void DrawCurve(float speed, Color color)
	{
		Gizmos.color = color;
		Vector3 vector = this.CalcJumpCurve(this.beginRatio, speed);
		for (int i = 0; i < this.previewSteps; i++)
		{
			Vector3 vector2 = this.CalcJumpCurve((this.endRatio - this.beginRatio) * (float)i / (float)(this.previewSteps - 1) + this.beginRatio, speed);
			Gizmos.DrawLine(vector, vector2);
			vector = vector2;
		}
	}

	// Token: 0x04000174 RID: 372
	public float speed = 100f;

	// Token: 0x04000175 RID: 373
	public float curveOffset;

	// Token: 0x04000176 RID: 374
	public float coinSpacing = 15f;

	// Token: 0x04000177 RID: 375
	public float beginRatio;

	// Token: 0x04000178 RID: 376
	public float endRatio = 1f;

	// Token: 0x04000179 RID: 377
	public bool superSneakers;

	// Token: 0x0400017A RID: 378
	private Game game;

	// Token: 0x0400017B RID: 379
	private Character character;

	// Token: 0x0400017C RID: 380
	private Running running;

	// Token: 0x0400017D RID: 381
	private static CoinPool coinPool;

	// Token: 0x0400017E RID: 382
	private int previewSteps = 10;

	// Token: 0x0400017F RID: 383
	private List<Transform> coins = new List<Transform>();

	// Token: 0x04000180 RID: 384
	private Group group;

	// Token: 0x04000181 RID: 385
	private bool Initialiseret;

	// Token: 0x04000182 RID: 386
	private int activation;
}
