using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class ByteReader
{
	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006864 File Offset: 0x00004A64
	public bool canRead
	{
		get
		{
			return this.mBuffer != null && this.mOffset < this.mBuffer.Length;
		}
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00006880 File Offset: 0x00004A80
	public ByteReader(byte[] bytes)
	{
		this.mBuffer = bytes;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000688F File Offset: 0x00004A8F
	public ByteReader(TextAsset asset)
	{
		this.mBuffer = asset.bytes;
	}

	// Token: 0x060001AB RID: 427 RVA: 0x000068A3 File Offset: 0x00004AA3
	private static string ReadLine(byte[] buffer, int start, int count)
	{
		return Encoding.UTF8.GetString(buffer, start, count);
	}

	// Token: 0x060001AC RID: 428 RVA: 0x000068B4 File Offset: 0x00004AB4
	public string ReadLine()
	{
		int num = this.mBuffer.Length;
		while (this.mOffset < num && this.mBuffer[this.mOffset] < 32)
		{
			this.mOffset++;
		}
		int i = this.mOffset;
		if (i < num)
		{
			while (i < num)
			{
				int num2 = (int)this.mBuffer[i++];
				if (num2 == 10 || num2 == 13)
				{
					IL_0061:
					string text = ByteReader.ReadLine(this.mBuffer, this.mOffset, i - this.mOffset - 1);
					this.mOffset = i;
					return text;
				}
			}
			i++;
			goto IL_0061;
		}
		this.mOffset = num;
		return null;
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00006950 File Offset: 0x00004B50
	public Dictionary<string, string> ReadDictionary()
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		char[] array = new char[] { '=' };
		while (this.canRead)
		{
			string text = this.ReadLine();
			if (text == null)
			{
				break;
			}
			string[] array2 = text.Split(array, 2, StringSplitOptions.RemoveEmptyEntries);
			if (array2.Length == 2)
			{
				string text2 = array2[0].Trim();
				string text3 = array2[1].Trim();
				dictionary[text2] = text3;
			}
		}
		return dictionary;
	}

	// Token: 0x040000E2 RID: 226
	private byte[] mBuffer;

	// Token: 0x040000E3 RID: 227
	private int mOffset;
}
