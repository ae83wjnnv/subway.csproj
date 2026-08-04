using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002E RID: 46
public abstract class CharacterState : MonoBehaviour
{
	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000215 RID: 533 RVA: 0x000093E0 File Offset: 0x000075E0
	public virtual bool PauseActiveModifiers
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000216 RID: 534 RVA: 0x000093E3 File Offset: 0x000075E3
	public virtual void HandleSwipe(SwipeDir swipeDir)
	{
	}

	// Token: 0x06000217 RID: 535 RVA: 0x000093E5 File Offset: 0x000075E5
	public virtual IEnumerator Begin()
	{
		yield break;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x000093ED File Offset: 0x000075ED
	public virtual void HandleCriticalHit()
	{
	}

	// Token: 0x06000219 RID: 537 RVA: 0x000093EF File Offset: 0x000075EF
	public virtual void HandleDoubleTap()
	{
	}
}
