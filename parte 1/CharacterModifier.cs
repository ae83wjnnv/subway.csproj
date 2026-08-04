using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002A RID: 42
public abstract class CharacterModifier : MonoBehaviour
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008B31 File Offset: 0x00006D31
	// (set) Token: 0x060001F2 RID: 498 RVA: 0x00008B39 File Offset: 0x00006D39
	public CharacterModifier.StopSignal Stop
	{
		get
		{
			return this.stop;
		}
		set
		{
			this.stop = value;
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060001F3 RID: 499 RVA: 0x00008B42 File Offset: 0x00006D42
	public virtual bool ShouldPauseInJetpack
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008B45 File Offset: 0x00006D45
	// (set) Token: 0x060001F5 RID: 501 RVA: 0x00008B4D File Offset: 0x00006D4D
	public IEnumerator Current
	{
		get
		{
			return this.current;
		}
		set
		{
			this.current = value;
		}
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x00008B56 File Offset: 0x00006D56
	public virtual IEnumerator Begin()
	{
		yield break;
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x00008B5E File Offset: 0x00006D5E
	public virtual void Pause()
	{
		this.Paused = true;
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x00008B67 File Offset: 0x00006D67
	public virtual void Resume()
	{
		this.Paused = false;
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00008B70 File Offset: 0x00006D70
	public virtual void Reset()
	{
	}

	// Token: 0x04000142 RID: 322
	public bool Paused;

	// Token: 0x04000143 RID: 323
	protected CharacterModifier.StopSignal stop;

	// Token: 0x04000144 RID: 324
	private IEnumerator current;

	// Token: 0x02000178 RID: 376
	public enum StopSignal
	{
		// Token: 0x0400092C RID: 2348
		DONT_STOP,
		// Token: 0x0400092D RID: 2349
		STOP,
		// Token: 0x0400092E RID: 2350
		STOP_NO_ENDING
	}
}
