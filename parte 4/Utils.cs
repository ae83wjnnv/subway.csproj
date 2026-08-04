using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014A RID: 330
public static class Utils
{
	// Token: 0x060009C1 RID: 2497 RVA: 0x00036174 File Offset: 0x00034374
	public static T FindObject<T>(this MonoBehaviour obj) where T : class
	{
		T t = Object.FindObjectOfType(typeof(T)) as T;
		if (t == null)
		{
			Debug.LogWarning(string.Format("Game object '{0}' could not find object of type {1}.", obj.gameObject.name, typeof(T).Name));
		}
		return t;
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x000361CB File Offset: 0x000343CB
	public static T FindComponentInParents<T>(this MonoBehaviour obj) where T : Component
	{
		return Utils.FindComponentInThisOrParents<T>(obj.transform.parent);
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x000361E0 File Offset: 0x000343E0
	public static T FindComponentInThisOrParents<T>(Transform t) where T : Component
	{
		Transform transform = t;
		while (transform != null)
		{
			T component = t.GetComponent<T>();
			if (component != null)
			{
				return component;
			}
			transform = transform.parent;
		}
		return default(T);
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x00036221 File Offset: 0x00034421
	public static string GetLongName(Transform transform)
	{
		if (transform == null)
		{
			return string.Empty;
		}
		return Utils.GetLongName(transform.parent) + "/" + transform.name;
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0003624D File Offset: 0x0003444D
	public static string GetLongNameList(Component[] components)
	{
		return string.Join(", ", new List<Component>(components).ConvertAll<string>((Component c) => Utils.GetLongName(c.transform)).ToArray());
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x00036288 File Offset: 0x00034488
	public static void Bar(string text, float ratio, int offset, Color color)
	{
		float num = 10f;
		float num2 = 20f;
		GUI.color = color;
		GUI.Button(new Rect(num, (float)Screen.height - num2 - num - (float)offset * num2, ((float)Screen.width - 2f * num) * ratio, num2), text);
	}
}
