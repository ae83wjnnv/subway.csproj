using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// Token: 0x02000008 RID: 8
public class MiniJSON
{
	// Token: 0x0600009C RID: 156 RVA: 0x00002754 File Offset: 0x00000954
	public static object jsonDecode(string json)
	{
		MiniJSON.lastDecode = json;
		if (json == null)
		{
			return null;
		}
		char[] array = json.ToCharArray();
		int num = 0;
		bool flag = true;
		object obj = MiniJSON.parseValue(array, ref num, ref flag);
		if (flag)
		{
			MiniJSON.lastErrorIndex = -1;
			return obj;
		}
		MiniJSON.lastErrorIndex = num;
		return obj;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00002790 File Offset: 0x00000990
	public static string jsonEncode(object json)
	{
		StringBuilder stringBuilder = new StringBuilder(2000);
		if (MiniJSON.serializeValue(json, stringBuilder))
		{
			return stringBuilder.ToString();
		}
		return null;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x000027B9 File Offset: 0x000009B9
	public static bool lastDecodeSuccessful()
	{
		return MiniJSON.lastErrorIndex == -1;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x000027C3 File Offset: 0x000009C3
	public static int getLastErrorIndex()
	{
		return MiniJSON.lastErrorIndex;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x000027CC File Offset: 0x000009CC
	public static string getLastErrorSnippet()
	{
		if (MiniJSON.lastErrorIndex == -1)
		{
			return string.Empty;
		}
		int num = MiniJSON.lastErrorIndex - 5;
		int num2 = MiniJSON.lastErrorIndex + 15;
		if (num < 0)
		{
			num = 0;
		}
		if (num2 >= MiniJSON.lastDecode.Length)
		{
			num2 = MiniJSON.lastDecode.Length - 1;
		}
		return MiniJSON.lastDecode.Substring(num, num2 - num + 1);
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00002828 File Offset: 0x00000A28
	protected static Hashtable parseObject(char[] json, ref int index)
	{
		Hashtable hashtable = new Hashtable();
		MiniJSON.nextToken(json, ref index);
		bool flag = false;
		while (!flag)
		{
			int num = MiniJSON.lookAhead(json, index);
			if (num == 0)
			{
				return null;
			}
			if (num == 2)
			{
				MiniJSON.nextToken(json, ref index);
				return hashtable;
			}
			if (num != 6)
			{
				string text = MiniJSON.parseString(json, ref index);
				if (text == null)
				{
					return null;
				}
				if (MiniJSON.nextToken(json, ref index) != 5)
				{
					return null;
				}
				bool flag2 = true;
				object obj = MiniJSON.parseValue(json, ref index, ref flag2);
				if (!flag2)
				{
					return null;
				}
				hashtable[text] = obj;
			}
			else
			{
				MiniJSON.nextToken(json, ref index);
			}
		}
		return hashtable;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x000028B0 File Offset: 0x00000AB0
	protected static ArrayList parseArray(char[] json, ref int index)
	{
		ArrayList arrayList = new ArrayList();
		MiniJSON.nextToken(json, ref index);
		bool flag = false;
		while (!flag)
		{
			int num = MiniJSON.lookAhead(json, index);
			if (num == 0)
			{
				return null;
			}
			if (num == 4)
			{
				MiniJSON.nextToken(json, ref index);
				break;
			}
			if (num != 6)
			{
				bool flag2 = true;
				object obj = MiniJSON.parseValue(json, ref index, ref flag2);
				if (!flag2)
				{
					return null;
				}
				arrayList.Add(obj);
			}
			else
			{
				MiniJSON.nextToken(json, ref index);
			}
		}
		return arrayList;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000291C File Offset: 0x00000B1C
	protected static object parseValue(char[] json, ref int index, ref bool success)
	{
		switch (MiniJSON.lookAhead(json, index))
		{
		case 1:
			return MiniJSON.parseObject(json, ref index);
		case 3:
			return MiniJSON.parseArray(json, ref index);
		case 7:
			return MiniJSON.parseString(json, ref index);
		case 8:
			return MiniJSON.parseNumber(json, ref index);
		case 9:
			MiniJSON.nextToken(json, ref index);
			return bool.Parse("TRUE");
		case 10:
			MiniJSON.nextToken(json, ref index);
			return bool.Parse("FALSE");
		case 11:
			MiniJSON.nextToken(json, ref index);
			return null;
		}
		success = false;
		return null;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000029CC File Offset: 0x00000BCC
	protected static string parseString(char[] json, ref int index)
	{
		string text = string.Empty;
		MiniJSON.eatWhitespace(json, ref index);
		int num = index;
		index = num + 1;
		char c = json[num];
		bool flag = false;
		while (!flag && index != json.Length)
		{
			num = index;
			index = num + 1;
			c = json[num];
			if (c == '"')
			{
				flag = true;
				break;
			}
			if (c != '\\')
			{
				text += c.ToString();
			}
			else
			{
				if (index == json.Length)
				{
					break;
				}
				num = index;
				index = num + 1;
				char c2 = json[num];
				if (c2 <= '\\')
				{
					if (c2 != '"')
					{
						if (c2 != '/')
						{
							if (c2 == '\\')
							{
								text += "\\";
							}
						}
						else
						{
							text += "/";
						}
					}
					else
					{
						text += "\"";
					}
				}
				else if (c2 <= 'f')
				{
					if (c2 != 'b')
					{
						if (c2 == 'f')
						{
							text += "\f";
						}
					}
					else
					{
						text += "\b";
					}
				}
				else if (c2 != 'n')
				{
					switch (c2)
					{
					case 'r':
						text += "\r";
						break;
					case 't':
						text += "\t";
						break;
					case 'u':
					{
						if (json.Length - index < 4)
						{
							goto IL_0186;
						}
						char[] array = new char[4];
						Array.Copy(json, index, array, 0, 4);
						text = text + "&#x" + new string(array) + ";";
						index += 4;
						break;
					}
					}
				}
				else
				{
					text += "\n";
				}
			}
		}
		IL_0186:
		if (!flag)
		{
			return null;
		}
		return text;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00002B68 File Offset: 0x00000D68
	protected static double parseNumber(char[] json, ref int index)
	{
		MiniJSON.eatWhitespace(json, ref index);
		int lastIndexOfNumber = MiniJSON.getLastIndexOfNumber(json, index);
		int num = lastIndexOfNumber - index + 1;
		char[] array = new char[num];
		Array.Copy(json, index, array, 0, num);
		index = lastIndexOfNumber + 1;
		return double.Parse(new string(array));
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00002BB0 File Offset: 0x00000DB0
	protected static int getLastIndexOfNumber(char[] json, int index)
	{
		int num = index;
		while (num < json.Length && "0123456789+-.eE".IndexOf(json[num]) != -1)
		{
			num++;
		}
		return num - 1;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00002BDE File Offset: 0x00000DDE
	protected static void eatWhitespace(char[] json, ref int index)
	{
		while (index < json.Length && " \t\n\r".IndexOf(json[index]) != -1)
		{
			index++;
		}
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00002C00 File Offset: 0x00000E00
	protected static int lookAhead(char[] json, int index)
	{
		int num = index;
		return MiniJSON.nextToken(json, ref num);
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00002C18 File Offset: 0x00000E18
	protected static int nextToken(char[] json, ref int index)
	{
		MiniJSON.eatWhitespace(json, ref index);
		if (index == json.Length)
		{
			return 0;
		}
		char c = json[index];
		index++;
		if (c <= '[')
		{
			switch (c)
			{
			case '"':
				return 7;
			case '#':
			case '$':
			case '%':
			case '&':
			case '\'':
			case '(':
			case ')':
			case '*':
			case '+':
			case '.':
			case '/':
				break;
			case ',':
				return 6;
			case '-':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				return 8;
			case ':':
				return 5;
			default:
				if (c == '[')
				{
					return 3;
				}
				break;
			}
		}
		else
		{
			if (c == ']')
			{
				return 4;
			}
			if (c == '{')
			{
				return 1;
			}
			if (c == '}')
			{
				return 2;
			}
		}
		index--;
		int num = json.Length - index;
		if (num >= 5 && json[index] == 'f' && json[index + 1] == 'a' && json[index + 2] == 'l' && json[index + 3] == 's' && json[index + 4] == 'e')
		{
			index += 5;
			return 10;
		}
		if (num >= 4 && json[index] == 't' && json[index + 1] == 'r' && json[index + 2] == 'u' && json[index + 3] == 'e')
		{
			index += 4;
			return 9;
		}
		if (num >= 4 && json[index] == 'n' && json[index + 1] == 'u' && json[index + 2] == 'l' && json[index + 3] == 'l')
		{
			index += 4;
			return 11;
		}
		return 0;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00002D8B File Offset: 0x00000F8B
	protected static bool serializeObjectOrArray(object objectOrArray, StringBuilder builder)
	{
		if (objectOrArray is Hashtable)
		{
			return MiniJSON.serializeObject((Hashtable)objectOrArray, builder);
		}
		return objectOrArray is ArrayList && MiniJSON.serializeArray((ArrayList)objectOrArray, builder);
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00002DB8 File Offset: 0x00000FB8
	protected static bool serializeObject(Hashtable anObject, StringBuilder builder)
	{
		builder.Append("{");
		IDictionaryEnumerator enumerator = anObject.GetEnumerator();
		bool flag = true;
		while (enumerator.MoveNext())
		{
			string text = enumerator.Key.ToString();
			object value = enumerator.Value;
			if (!flag)
			{
				builder.Append(", ");
			}
			MiniJSON.serializeString(text, builder);
			builder.Append(":");
			if (!MiniJSON.serializeValue(value, builder))
			{
				return false;
			}
			flag = false;
		}
		builder.Append("}");
		return true;
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00002E34 File Offset: 0x00001034
	protected static bool serializeDictionary(Dictionary<string, string> dict, StringBuilder builder)
	{
		builder.Append("{");
		bool flag = true;
		foreach (KeyValuePair<string, string> keyValuePair in dict)
		{
			if (!flag)
			{
				builder.Append(", ");
			}
			MiniJSON.serializeString(keyValuePair.Key, builder);
			builder.Append(":");
			MiniJSON.serializeString(keyValuePair.Value, builder);
			flag = false;
		}
		builder.Append("}");
		return true;
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00002ED0 File Offset: 0x000010D0
	protected static bool serializeArray(ArrayList anArray, StringBuilder builder)
	{
		builder.Append("[");
		bool flag = true;
		for (int i = 0; i < anArray.Count; i++)
		{
			object obj = anArray[i];
			if (!flag)
			{
				builder.Append(", ");
			}
			if (!MiniJSON.serializeValue(obj, builder))
			{
				return false;
			}
			flag = false;
		}
		builder.Append("]");
		return true;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00002F2C File Offset: 0x0000112C
	protected static bool serializeValue(object value, StringBuilder builder)
	{
		if (value == null)
		{
			builder.Append("null");
		}
		else if (value.GetType().IsArray)
		{
			MiniJSON.serializeArray(new ArrayList((ICollection)value), builder);
		}
		else if (value is string)
		{
			MiniJSON.serializeString((string)value, builder);
		}
		else if (value is char)
		{
			MiniJSON.serializeString(Convert.ToString((char)value), builder);
		}
		else if (value is Hashtable)
		{
			MiniJSON.serializeObject((Hashtable)value, builder);
		}
		else if (value is Dictionary<string, string>)
		{
			MiniJSON.serializeDictionary((Dictionary<string, string>)value, builder);
		}
		else if (value is ArrayList)
		{
			MiniJSON.serializeArray((ArrayList)value, builder);
		}
		else if (value is bool && (bool)value)
		{
			builder.Append("true");
		}
		else if (value is bool && !(bool)value)
		{
			builder.Append("false");
		}
		else
		{
			if (!value.GetType().IsPrimitive)
			{
				return false;
			}
			MiniJSON.serializeNumber(Convert.ToDouble(value), builder);
		}
		return true;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00003048 File Offset: 0x00001248
	protected static void serializeString(string aString, StringBuilder builder)
	{
		builder.Append("\"");
		char[] array = aString.ToCharArray();
		int i = 0;
		while (i < array.Length)
		{
			char c = array[i];
			switch (c)
			{
			case '\b':
				builder.Append("\\b");
				break;
			case '\t':
				builder.Append("\\t");
				break;
			case '\n':
				builder.Append("\\n");
				break;
			case '\v':
				goto IL_00B2;
			case '\f':
				builder.Append("\\f");
				break;
			case '\r':
				builder.Append("\\r");
				break;
			default:
				if (c != '"')
				{
					if (c != '\\')
					{
						goto IL_00B2;
					}
					builder.Append("\\\\");
				}
				else
				{
					builder.Append("\\\"");
				}
				break;
			}
			IL_00EE:
			i++;
			continue;
			IL_00B2:
			int num = Convert.ToInt32(c);
			if (num >= 32 && num <= 126)
			{
				builder.Append(c);
				goto IL_00EE;
			}
			builder.Append("\\u" + Convert.ToString(num, 16).PadLeft(4, '0'));
			goto IL_00EE;
		}
		builder.Append("\"");
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000315C File Offset: 0x0000135C
	protected static void serializeNumber(double number, StringBuilder builder)
	{
		builder.Append(Convert.ToString(number));
	}

	// Token: 0x0400003E RID: 62
	private const int TOKEN_NONE = 0;

	// Token: 0x0400003F RID: 63
	private const int TOKEN_CURLY_OPEN = 1;

	// Token: 0x04000040 RID: 64
	private const int TOKEN_CURLY_CLOSE = 2;

	// Token: 0x04000041 RID: 65
	private const int TOKEN_SQUARED_OPEN = 3;

	// Token: 0x04000042 RID: 66
	private const int TOKEN_SQUARED_CLOSE = 4;

	// Token: 0x04000043 RID: 67
	private const int TOKEN_COLON = 5;

	// Token: 0x04000044 RID: 68
	private const int TOKEN_COMMA = 6;

	// Token: 0x04000045 RID: 69
	private const int TOKEN_STRING = 7;

	// Token: 0x04000046 RID: 70
	private const int TOKEN_NUMBER = 8;

	// Token: 0x04000047 RID: 71
	private const int TOKEN_TRUE = 9;

	// Token: 0x04000048 RID: 72
	private const int TOKEN_FALSE = 10;

	// Token: 0x04000049 RID: 73
	private const int TOKEN_NULL = 11;

	// Token: 0x0400004A RID: 74
	private const int BUILDER_CAPACITY = 2000;

	// Token: 0x0400004B RID: 75
	protected static int lastErrorIndex = -1;

	// Token: 0x0400004C RID: 76
	protected static string lastDecode = string.Empty;
}
