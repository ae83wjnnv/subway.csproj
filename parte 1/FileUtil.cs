using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Token: 0x0200005A RID: 90
internal class FileUtil
{
	// Token: 0x060002DA RID: 730 RVA: 0x0000C97C File Offset: 0x0000AB7C
	private static bool ArraysAreEqual<T>(T[] a, T[] b)
	{
		if (a == null && b == null)
		{
			return true;
		}
		if (a.Length != b.Length)
		{
			return false;
		}
		for (int i = 0; i < a.Length; i++)
		{
			if (!object.Equals(a[i], b[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0000C9CC File Offset: 0x0000ABCC
	public static Dictionary<E, int> ReadEnumIntDictionary<E>(BinaryReader reader)
	{
		int num = reader.ReadInt32();
		Dictionary<E, int> dictionary = new Dictionary<E, int>(num);
		Type typeFromHandle = typeof(E);
		for (int i = 0; i < num; i++)
		{
			string text = reader.ReadString();
			int num2 = reader.ReadInt32();
			E e = (E)((object)Enum.Parse(typeFromHandle, text, true));
			dictionary[e] = num2;
		}
		return dictionary;
	}

	// Token: 0x060002DC RID: 732 RVA: 0x0000CA2C File Offset: 0x0000AC2C
	public static void WriteEnumIntDictionary<E>(BinaryWriter writer, Dictionary<E, int> dict)
	{
		writer.Write(dict.Count);
		foreach (KeyValuePair<E, int> keyValuePair in dict)
		{
			string name = Enum.GetName(typeof(E), keyValuePair.Key);
			writer.Write(name);
			writer.Write(keyValuePair.Value);
		}
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
	public static Dictionary<E, string> ReadEnumStringDictionary<E>(BinaryReader reader)
	{
		int num = reader.ReadInt32();
		Dictionary<E, string> dictionary = new Dictionary<E, string>(num);
		Type typeFromHandle = typeof(E);
		for (int i = 0; i < num; i++)
		{
			string text = reader.ReadString();
			string text2 = reader.ReadString();
			if (Enum.IsDefined(typeFromHandle, text))
			{
				E e = (E)((object)Enum.Parse(typeFromHandle, text, true));
				dictionary[e] = text2;
			}
		}
		return dictionary;
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0000CB18 File Offset: 0x0000AD18
	public static void WriteEnumStringDictionary<E>(BinaryWriter writer, Dictionary<E, string> dict)
	{
		writer.Write(dict.Count);
		foreach (KeyValuePair<E, string> keyValuePair in dict)
		{
			string name = Enum.GetName(typeof(E), keyValuePair.Key);
			writer.Write(name);
			writer.Write(keyValuePair.Value);
		}
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0000CB9C File Offset: 0x0000AD9C
	public static byte[] Load(string path, string secret)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(secret);
		BinaryReader binaryReader = new BinaryReader(new FileStream(path, FileMode.Open));
		int num = binaryReader.ReadInt32();
		byte[] array = binaryReader.ReadBytes(num);
		int num2 = binaryReader.ReadInt32();
		byte[] array2 = binaryReader.ReadBytes(num2);
		binaryReader.Close();
		byte[] array3 = new byte[bytes.Length + array2.Length];
		Array.Copy(bytes, array3, bytes.Length);
		Array.Copy(array2, 0, array3, bytes.Length, array2.Length);
		if (!FileUtil.ArraysAreEqual<byte>(SHA1.Create().ComputeHash(array3), array))
		{
			throw new IOException("Data is corrupted");
		}
		return array2;
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0000CC34 File Offset: 0x0000AE34
	public static void Save(string path, string secret, byte[] data, int offset, int length)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(secret);
		byte[] array = new byte[bytes.Length + length];
		Array.Copy(bytes, array, bytes.Length);
		Array.Copy(data, offset, array, bytes.Length, length);
		byte[] array2 = SHA1.Create().ComputeHash(array);
		FileStream fileStream = new FileStream(path, FileMode.Create);
		BinaryWriter binaryWriter = new BinaryWriter(fileStream);
		binaryWriter.Write(array2.Length);
		binaryWriter.Write(array2);
		binaryWriter.Write(length);
		binaryWriter.Write(data, offset, length);
		fileStream.Close();
	}
}
