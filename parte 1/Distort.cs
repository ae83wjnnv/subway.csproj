using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class Distort : MonoBehaviour
{
	// Token: 0x060002A9 RID: 681 RVA: 0x0000BBEC File Offset: 0x00009DEC
	private void Awake()
	{
		this.character = Character.Instance;
		this.distortionStart = this.distortion;
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000BC05 File Offset: 0x00009E05
	private void Start()
	{
		this.StartDestortion();
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000BC0D File Offset: 0x00009E0D
	public void Reset()
	{
		this.distortion = this.distortionStart;
		this.StartDestortion();
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000BC24 File Offset: 0x00009E24
	private void StartDestortion()
	{
		base.StopAllCoroutines();
		this.distortionTarget = this.distortion;
		this.lastZ = this.character.z;
		this.nextPartZ = this.character.z + this.partLength;
		base.StartCoroutine(this.RandomDirs());
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000BC79 File Offset: 0x00009E79
	private IEnumerator RandomDirs()
	{
		for (;;)
		{
			yield return new WaitForSeconds(1f);
			if (this.character.z > this.nextPartZ)
			{
				this.distortionTarget = new Vector3(Random.Range(this.xMin, this.xMax), Random.Range(this.yMin, this.yMax), 0f);
				this.nextPartZ = this.character.z + this.partLength;
			}
		}
		yield break;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000BC88 File Offset: 0x00009E88
	private void Update()
	{
		float num = this.character.z - this.lastZ;
		this.lastZ = this.character.z;
		this.distortion = Vector3.SmoothDamp(this.distortion, this.distortionTarget, ref this.distortionVelocity, this.partLength, float.MaxValue, num);
		Vector3 vector = this.distortion / 100f;
		Material[] array = this.materials;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetVector("_Distort", vector);
		}
	}

	// Token: 0x040001E5 RID: 485
	private float xMin = -0.2f;

	// Token: 0x040001E6 RID: 486
	private float xMax = 0.2f;

	// Token: 0x040001E7 RID: 487
	private float yMin = -0.1f;

	// Token: 0x040001E8 RID: 488
	private float yMax;

	// Token: 0x040001E9 RID: 489
	public float partLength = 700f;

	// Token: 0x040001EA RID: 490
	public Material[] materials;

	// Token: 0x040001EB RID: 491
	public Vector3 distortion = Vector3.zero;

	// Token: 0x040001EC RID: 492
	private Vector3 distortionTarget = Vector3.zero;

	// Token: 0x040001ED RID: 493
	private Vector3 distortionVelocity = Vector3.zero;

	// Token: 0x040001EE RID: 494
	private float lastZ;

	// Token: 0x040001EF RID: 495
	private float nextPartZ;

	// Token: 0x040001F0 RID: 496
	private float environmentT;

	// Token: 0x040001F1 RID: 497
	private Character character;

	// Token: 0x040001F2 RID: 498
	private Vector3 distortionStart = new Vector3(-0.05f, -0.03f, 0f);

	// Token: 0x040001F3 RID: 499
	public Distort.EnviromentSettings day;

	// Token: 0x040001F4 RID: 500
	public Distort.EnviromentSettings night;

	// Token: 0x02000186 RID: 390
	[Serializable]
	public class EnviromentSettings
	{
		// Token: 0x04000959 RID: 2393
		public Color backgroundColor;

		// Token: 0x0400095A RID: 2394
		public Color materialColor = new Color(1f, 1f, 1f, 1f);
	}
}
