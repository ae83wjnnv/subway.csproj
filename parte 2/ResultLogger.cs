using System;
using System.Collections;
using System.Text;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class ResultLogger : Object
{
	// Token: 0x060000B7 RID: 183 RVA: 0x000031B0 File Offset: 0x000013B0
	public static void logObject(object result)
	{
		if (result.GetType() == typeof(ArrayList))
		{
			ResultLogger.logArraylist((ArrayList)result);
			return;
		}
		if (result.GetType() == typeof(Hashtable))
		{
			ResultLogger.logHashtable((Hashtable)result);
			return;
		}
		Debug.Log("result is not a hashtable or arraylist");
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00003210 File Offset: 0x00001410
	public static void logArraylist(ArrayList result)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (object obj in result)
		{
			Hashtable hashtable = (Hashtable)obj;
			ResultLogger.addHashtableToString(stringBuilder, hashtable);
			stringBuilder.Append("\n--------------------\n");
		}
		Debug.Log(stringBuilder.ToString());
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00003284 File Offset: 0x00001484
	public static void logHashtable(Hashtable result)
	{
		StringBuilder stringBuilder = new StringBuilder();
		ResultLogger.addHashtableToString(stringBuilder, result);
		Debug.Log(stringBuilder.ToString());
	}

	// Token: 0x060000BA RID: 186 RVA: 0x0000329C File Offset: 0x0000149C
	public static void addHashtableToString(StringBuilder builder, Hashtable item)
	{
		foreach (object obj in item)
		{
			DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
			if (dictionaryEntry.Value is Hashtable)
			{
				builder.AppendFormat("{0}: ", dictionaryEntry.Key);
				ResultLogger.addHashtableToString(builder, (Hashtable)dictionaryEntry.Value);
			}
			else if (dictionaryEntry.Value is ArrayList)
			{
				builder.AppendFormat("{0}: ", dictionaryEntry.Key);
				ResultLogger.addArraylistToString(builder, (ArrayList)dictionaryEntry.Value);
			}
			else
			{
				builder.AppendFormat("{0}: {1}\n", dictionaryEntry.Key, dictionaryEntry.Value);
			}
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00003374 File Offset: 0x00001574
	public static void addArraylistToString(StringBuilder builder, ArrayList result)
	{
		foreach (object obj in result)
		{
			if (obj is Hashtable)
			{
				ResultLogger.addHashtableToString(builder, (Hashtable)obj);
			}
			else if (obj is ArrayList)
			{
				ResultLogger.addArraylistToString(builder, (ArrayList)obj);
			}
			builder.Append("\n--------------------\n");
		}
		Debug.Log(builder.ToString());
	}
}
