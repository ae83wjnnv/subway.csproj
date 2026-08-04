using System;

// Token: 0x0200014B RID: 331
public class Variable<T> where T : IComparable
{
	// Token: 0x1700011C RID: 284
	// (get) Token: 0x060009C7 RID: 2503 RVA: 0x000362D4 File Offset: 0x000344D4
	// (set) Token: 0x060009C8 RID: 2504 RVA: 0x000362DC File Offset: 0x000344DC
	public T Value
	{
		get
		{
			return this.value;
		}
		set
		{
			bool flag = value.CompareTo(this.value) != 0;
			this.value = value;
			if (flag)
			{
				this.FireOnChange();
			}
		}
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x00036308 File Offset: 0x00034508
	public Variable(T initialValue)
	{
		this.value = initialValue;
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x00036317 File Offset: 0x00034517
	public void FireOnChange()
	{
		if (this.OnChange != null)
		{
			this.OnChange(this.value);
		}
	}

	// Token: 0x04000877 RID: 2167
	private T value;

	// Token: 0x04000878 RID: 2168
	public Variable<T>.OnChangeDelegate OnChange;

	// Token: 0x02000228 RID: 552
	// (Invoke) Token: 0x06000CA2 RID: 3234
	public delegate void OnChangeDelegate(T value);
}
