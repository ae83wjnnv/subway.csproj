using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class BetterList<T>
{
	// Token: 0x1700000A RID: 10
	public T this[int i]
	{
		get
		{
			return this.buffer[i];
		}
		set
		{
			this.buffer[i] = value;
		}
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000608C File Offset: 0x0000428C
	public IEnumerator<T> GetEnumerator()
	{
		if (this.buffer != null)
		{
			int num;
			for (int i = 0; i < this.size; i = num + 1)
			{
				yield return this.buffer[i];
				num = i;
			}
		}
		yield break;
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000609C File Offset: 0x0000429C
	private void AllocateMore()
	{
		T[] array = ((this.buffer == null) ? new T[32] : new T[Mathf.Max(this.buffer.Length << 1, 32)]);
		if (this.buffer != null && this.size > 0)
		{
			this.buffer.CopyTo(array, 0);
		}
		this.buffer = array;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x000060F8 File Offset: 0x000042F8
	private void Trim()
	{
		if (this.size > 0)
		{
			if (this.size < this.buffer.Length)
			{
				T[] array = new T[this.size];
				for (int i = 0; i < this.size; i++)
				{
					array[i] = this.buffer[i];
				}
				this.buffer = array;
				return;
			}
		}
		else
		{
			this.buffer = null;
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000615D File Offset: 0x0000435D
	public void Clear()
	{
		this.size = 0;
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00006166 File Offset: 0x00004366
	public void Release()
	{
		this.size = 0;
		this.buffer = null;
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00006178 File Offset: 0x00004378
	public void Add(T item)
	{
		if (this.buffer == null || this.size == this.buffer.Length)
		{
			this.AllocateMore();
		}
		T[] array = this.buffer;
		int num = this.size;
		this.size = num + 1;
		array[num] = item;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x000061C0 File Offset: 0x000043C0
	public void Remove(T item)
	{
		if (this.buffer == null)
		{
			return;
		}
		EqualityComparer<T> @default = EqualityComparer<T>.Default;
		for (int i = 0; i < this.size; i++)
		{
			if (@default.Equals(this.buffer[i], item))
			{
				this.size--;
				this.buffer[i] = default(T);
				for (int j = i; j < this.size; j++)
				{
					this.buffer[j] = this.buffer[j + 1];
				}
				return;
			}
		}
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00006254 File Offset: 0x00004454
	public void RemoveAt(int index)
	{
		if (this.buffer != null && index < this.size)
		{
			this.size--;
			this.buffer[index] = default(T);
			for (int i = index; i < this.size; i++)
			{
				this.buffer[i] = this.buffer[i + 1];
			}
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x000062C0 File Offset: 0x000044C0
	public T[] ToArray()
	{
		this.Trim();
		return this.buffer;
	}

	// Token: 0x040000D1 RID: 209
	public T[] buffer;

	// Token: 0x040000D2 RID: 210
	public int size;
}
