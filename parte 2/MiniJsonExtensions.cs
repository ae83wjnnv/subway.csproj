using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000009 RID: 9
public static class MiniJsonExtensions
{
	// Token: 0x060000B3 RID: 179 RVA: 0x00003185 File Offset: 0x00001385
	public static string toJson(this Hashtable obj)
	{
		return MiniJSON.jsonEncode(obj);
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x0000318D File Offset: 0x0000138D
	public static string toJson(this Dictionary<string, string> obj)
	{
		return MiniJSON.jsonEncode(obj);
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00003195 File Offset: 0x00001395
	public static ArrayList arrayListFromJson(this string json)
	{
		return MiniJSON.jsonDecode(json) as ArrayList;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x000031A2 File Offset: 0x000013A2
	public static Hashtable hashtableFromJson(this string json)
	{
		return MiniJSON.jsonDecode(json) as Hashtable;
	}
}
