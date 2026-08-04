using System;
using UnityEngine;

// Token: 0x020000DF RID: 223
public class Tunnel : MonoBehaviour
{
	// Token: 0x0600066A RID: 1642 RVA: 0x000200BC File Offset: 0x0001E2BC
	private void Awake()
	{
		this.game = Game.Instance;
		this.tunnelLength = base.GetComponent<Collider>().bounds.size.z;
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x000200F2 File Offset: 0x0001E2F2
	private void OnTriggerEnter(Collider collider)
	{
		if ("Player".Equals(collider.tag))
		{
			this.game.Running.StartTunnel(this.tunnelLength);
		}
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0002011C File Offset: 0x0001E31C
	private void OnTriggerExit(Collider collider)
	{
		if ("Player".Equals(collider.tag))
		{
			this.game.Running.EndTunnel();
		}
	}

	// Token: 0x04000574 RID: 1396
	private Game game;

	// Token: 0x04000575 RID: 1397
	private float tunnelLength;

	// Token: 0x04000576 RID: 1398
	public AudioStateLoop audioStateLoop;
}
