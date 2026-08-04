using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009F RID: 159
[AddComponentMenu("NGUI/Internal/Debug")]
public class NGUIDebug : MonoBehaviour
{
	// Token: 0x0600049D RID: 1181 RVA: 0x00015FEC File Offset: 0x000141EC
	public static void Log(string text)
	{
		if (Application.isPlaying)
		{
			if (NGUIDebug.mLines.Count > 20)
			{
				NGUIDebug.mLines.RemoveAt(0);
			}
			NGUIDebug.mLines.Add(text);
			if (NGUIDebug.mInstance == null)
			{
				GameObject gameObject = new GameObject("_NGUI Debug");
				NGUIDebug.mInstance = gameObject.AddComponent<NGUIDebug>();
				Object.DontDestroyOnLoad(gameObject);
				return;
			}
		}
		else
		{
			Debug.Log(text);
		}
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00016054 File Offset: 0x00014254
	public static void DrawBounds(Bounds b)
	{
		Vector3 center = b.center;
		Vector3 vector = b.center - b.extents;
		Vector3 vector2 = b.center + b.extents;
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector2.x, vector.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector.y, center.z), new Vector3(vector.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector2.x, vector.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
		Debug.DrawLine(new Vector3(vector.x, vector2.y, center.z), new Vector3(vector2.x, vector2.y, center.z), Color.red);
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00016174 File Offset: 0x00014374
	private void OnGUI()
	{
		int i = 0;
		int count = NGUIDebug.mLines.Count;
		while (i < count)
		{
			GUILayout.Label(NGUIDebug.mLines[i], Array.Empty<GUILayoutOption>());
			i++;
		}
	}

	// Token: 0x0400040C RID: 1036
	private static List<string> mLines = new List<string>();

	// Token: 0x0400040D RID: 1037
	private static NGUIDebug mInstance = null;
}
