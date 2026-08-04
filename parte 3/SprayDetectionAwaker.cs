using System;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class SprayDetectionAwaker : MonoBehaviour
{
	// Token: 0x06000617 RID: 1559 RVA: 0x0001E97E File Offset: 0x0001CB7E
	private void Awake()
	{
		UIScreenController instance = UIScreenController.Instance;
		instance.OnChangedScreen = (Action<bool>)Delegate.Combine(instance.OnChangedScreen, new Action<bool>(this.runnerAnimPlayer.PlayOrMutePaintSound));
	}

	// Token: 0x0400051D RID: 1309
	public RunnerAnimPlayer runnerAnimPlayer;
}
