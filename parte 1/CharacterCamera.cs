using System;
using UnityEngine;

// Token: 0x02000027 RID: 39
public class CharacterCamera : MonoBehaviour
{
	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060001DB RID: 475 RVA: 0x00008504 File Offset: 0x00006704
	public static CharacterCamera Instance
	{
		get
		{
			CharacterCamera characterCamera;
			if ((characterCamera = CharacterCamera.instance) == null)
			{
				characterCamera = (CharacterCamera.instance = Object.FindObjectOfType(typeof(CharacterCamera)) as CharacterCamera);
			}
			return characterCamera;
		}
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0000852C File Offset: 0x0000672C
	public void Shake()
	{
		Vector3 diff = Vector3.zero;
		float amplitude = 100f;
		base.StartCoroutine(pTween.To(0.3f, delegate(float t)
		{
			diff += Random.insideUnitSphere;
			this.shake = (1f - t) * diff * amplitude * Time.deltaTime;
		}));
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00008579 File Offset: 0x00006779
	public void LateUpdate()
	{
		base.transform.position = this.position + this.shake;
		base.transform.LookAt(this.target + this.shake);
	}

	// Token: 0x060001DE RID: 478 RVA: 0x000085B3 File Offset: 0x000067B3
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(this.position, this.target);
	}

	// Token: 0x04000131 RID: 305
	public Vector3 position;

	// Token: 0x04000132 RID: 306
	public Vector3 target;

	// Token: 0x04000133 RID: 307
	private Vector3 shake = Vector3.zero;

	// Token: 0x04000134 RID: 308
	private static CharacterCamera instance;
}
