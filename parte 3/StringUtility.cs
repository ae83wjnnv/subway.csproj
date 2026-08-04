using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class StringUtility
{
	// Token: 0x06000623 RID: 1571 RVA: 0x0001ED5C File Offset: 0x0001CF5C
	public static bool Match(string str, string pattern, StringUtility.MatchType matchType, bool ignoreCase)
	{
		switch (matchType)
		{
		case StringUtility.MatchType.Is:
			return str.Equals(pattern, (!ignoreCase) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
		case StringUtility.MatchType.EndsWith:
			return str.EndsWith(pattern, (!ignoreCase) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
		case StringUtility.MatchType.BeginsWith:
			return str.StartsWith(pattern, (!ignoreCase) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
		case StringUtility.MatchType.Contains:
			if (ignoreCase)
			{
				str = str.ToLower();
				pattern = pattern.ToLower();
			}
			return str.Contains(pattern);
		case StringUtility.MatchType.RegEx:
		{
			RegexOptions regexOptions = ((!ignoreCase) ? RegexOptions.Singleline : (RegexOptions.IgnoreCase | RegexOptions.Singleline));
			return new Regex(pattern, regexOptions).IsMatch(str);
		}
		case StringUtility.MatchType.Pattern:
			return new Regex("^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(str);
		default:
			return false;
		}
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0001EE30 File Offset: 0x0001D030
	public static string ArrayToString<MyType>(MyType[] array)
	{
		bool flag = true;
		StringBuilder stringBuilder = new StringBuilder();
		foreach (MyType myType in array)
		{
			if (flag)
			{
				flag = false;
			}
			else
			{
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(myType.ToString());
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0001EE88 File Offset: 0x0001D088
	public static void ValidateEnumValueString(Type enumType, string enumValueString, bool mayBeNullOrEmpty, string varName)
	{
		if (string.IsNullOrEmpty(enumValueString))
		{
			if (mayBeNullOrEmpty)
			{
				return;
			}
		}
		else
		{
			try
			{
				Enum.Parse(enumType, enumValueString, true);
				return;
			}
			catch (Exception)
			{
			}
		}
		Debug.LogError(varName + " must be one of: " + StringUtility.ArrayToString<string>(Enum.GetNames(enumType)));
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0001EEDC File Offset: 0x0001D0DC
	public static int NonEscapedIndexOf(string text, int startIndex, char ch)
	{
		int num = text.IndexOf(ch, startIndex);
		if (num == 0)
		{
			return num;
		}
		if (num > 0 && text[num - 1] != '\\')
		{
			return num;
		}
		return -1;
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0001EF0C File Offset: 0x0001D10C
	public static int GetNextKeyValuePair(string text, int startIndex, out string key, out string value)
	{
		int num = StringUtility.NonEscapedIndexOf(text, startIndex, '[');
		if (num >= 0)
		{
			int num2 = StringUtility.NonEscapedIndexOf(text, num + 1, ']');
			if (num2 >= 0)
			{
				key = text.Substring(num + 1, num2 - num - 1).Trim();
				int num3 = StringUtility.NonEscapedIndexOf(text, num2 + 1, '[');
				if (num3 < 0)
				{
					num3 = text.Length;
				}
				value = text.Substring(num2 + 1, num3 - num2 - 1).Trim();
				return num3;
			}
		}
		key = null;
		value = null;
		return -1;
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0001EF84 File Offset: 0x0001D184
	public static Dictionary<string, string> ParseProperties(string text)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		int num = 0;
		string text2;
		string text3;
		while ((num = StringUtility.GetNextKeyValuePair(text, num, out text2, out text3)) >= 0)
		{
			dictionary[text2] = text3;
			if (num == text.Length)
			{
				break;
			}
		}
		return dictionary;
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0001EFC0 File Offset: 0x0001D1C0
	public static string FormatProperties(Dictionary<string, string> props)
	{
		StringBuilder stringBuilder = new StringBuilder();
		char[] array = new char[] { '[', ']' };
		foreach (KeyValuePair<string, string> keyValuePair in props)
		{
			if (keyValuePair.Key.IndexOfAny(array) >= 0)
			{
				Debug.LogError("Property data should not contain '[' and ']' characters");
			}
			stringBuilder.Append('[').Append(keyValuePair.Key).Append(']')
				.Append(keyValuePair.Value);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x020001E1 RID: 481
	public enum MatchType
	{
		// Token: 0x04000B47 RID: 2887
		Is,
		// Token: 0x04000B48 RID: 2888
		EndsWith,
		// Token: 0x04000B49 RID: 2889
		BeginsWith,
		// Token: 0x04000B4A RID: 2890
		Contains,
		// Token: 0x04000B4B RID: 2891
		RegEx,
		// Token: 0x04000B4C RID: 2892
		Pattern
	}
}
