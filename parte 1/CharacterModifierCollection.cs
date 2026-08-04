using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class CharacterModifierCollection
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x060001FB RID: 507 RVA: 0x00008B7A File Offset: 0x00006D7A
	public CoinMagnet CoinMagnet
	{
		get
		{
			return this.coinMagnet;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x060001FC RID: 508 RVA: 0x00008B82 File Offset: 0x00006D82
	public SuperSneakers SuperSneakes
	{
		get
		{
			return this.superSneakers;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x060001FD RID: 509 RVA: 0x00008B8A File Offset: 0x00006D8A
	public Hoverboard Hoverboard
	{
		get
		{
			return this.hoverboard;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x060001FE RID: 510 RVA: 0x00008B92 File Offset: 0x00006D92
	public DoubleScoreMultiplier DoubleScoreMultiplier
	{
		get
		{
			return this.doubleScoreMultiplier;
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00008B9C File Offset: 0x00006D9C
	public CharacterModifierCollection()
	{
		this.coinMagnet = Object.FindObjectOfType(typeof(CoinMagnet)) as CoinMagnet;
		this.superSneakers = Object.FindObjectOfType(typeof(SuperSneakers)) as SuperSneakers;
		this.hoverboard = Hoverboard.Instance;
		this.doubleScoreMultiplier = Object.FindObjectOfType(typeof(DoubleScoreMultiplier)) as DoubleScoreMultiplier;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00008C1E File Offset: 0x00006E1E
	public void Add(CharacterModifier modifier)
	{
		if (!this.modifiers.Contains(modifier))
		{
			this.modifiers.Add(modifier);
			modifier.Current = modifier.Begin();
			return;
		}
		modifier.Reset();
		modifier.Current = modifier.Begin();
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00008C5C File Offset: 0x00006E5C
	public void Update()
	{
		if (this.modifiers.Count <= 0)
		{
			return;
		}
		this.deadModifiers.Clear();
		foreach (CharacterModifier characterModifier in this.modifiers)
		{
			if (!characterModifier.Paused && !characterModifier.Current.MoveNext())
			{
				this.deadModifiers.Add(characterModifier);
			}
		}
		if (this.deadModifiers.Count <= 0)
		{
			return;
		}
		foreach (CharacterModifier characterModifier2 in this.deadModifiers)
		{
			this.modifiers.Remove(characterModifier2);
		}
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00008D3C File Offset: 0x00006F3C
	public bool IsActive(CharacterModifier modifier)
	{
		return this.modifiers.Contains(modifier);
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00008D4C File Offset: 0x00006F4C
	public void StopWithNoEnding()
	{
		foreach (CharacterModifier characterModifier in this.modifiers)
		{
			characterModifier.Stop = CharacterModifier.StopSignal.STOP_NO_ENDING;
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00008DA0 File Offset: 0x00006FA0
	public void Stop()
	{
		foreach (CharacterModifier characterModifier in this.modifiers)
		{
			characterModifier.Stop = CharacterModifier.StopSignal.STOP;
		}
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00008DF4 File Offset: 0x00006FF4
	public void Pause()
	{
		foreach (CharacterModifier characterModifier in this.modifiers)
		{
			characterModifier.Pause();
		}
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00008E44 File Offset: 0x00007044
	public void PauseInJetpackMode()
	{
		foreach (CharacterModifier characterModifier in this.modifiers)
		{
			if (characterModifier.ShouldPauseInJetpack)
			{
				characterModifier.Pause();
			}
		}
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00008EA0 File Offset: 0x000070A0
	public void Resume()
	{
		foreach (CharacterModifier characterModifier in this.modifiers)
		{
			characterModifier.Resume();
		}
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00008EF0 File Offset: 0x000070F0
	public void Reset()
	{
		foreach (CharacterModifier characterModifier in this.modifiers)
		{
			characterModifier.Reset();
		}
		this.modifiers.Clear();
	}

	// Token: 0x04000145 RID: 325
	private CoinMagnet coinMagnet;

	// Token: 0x04000146 RID: 326
	private SuperSneakers superSneakers;

	// Token: 0x04000147 RID: 327
	private Hoverboard hoverboard;

	// Token: 0x04000148 RID: 328
	private DoubleScoreMultiplier doubleScoreMultiplier;

	// Token: 0x04000149 RID: 329
	private List<CharacterModifier> modifiers = new List<CharacterModifier>();

	// Token: 0x0400014A RID: 330
	private List<CharacterModifier> deadModifiers = new List<CharacterModifier>();
}
