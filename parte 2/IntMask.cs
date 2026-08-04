using System;

// Token: 0x02000079 RID: 121
public struct IntMask
{
	// Token: 0x17000052 RID: 82
	public bool this[int bit]
	{
		get
		{
			return (this.mask & (1 << bit)) != 0;
		}
		set
		{
			if (value)
			{
				this.mask |= 1 << bit;
				return;
			}
			this.mask &= ~(1 << bit);
		}
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x000119CA File Offset: 0x0000FBCA
	public IntMask(int i)
	{
		this.mask = i;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x000119D3 File Offset: 0x0000FBD3
	public static implicit operator int(IntMask i)
	{
		return i.mask;
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x000119DB File Offset: 0x0000FBDB
	public static implicit operator IntMask(int i)
	{
		return new IntMask(i);
	}

	// Token: 0x0400033F RID: 831
	private int mask;
}
