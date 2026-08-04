using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public static class NGUITools
{
	// Token: 0x060004BC RID: 1212 RVA: 0x00016E8B File Offset: 0x0001508B
	public static AudioSource PlaySound(AudioClip clip)
	{
		return NGUITools.PlaySound(clip, 1f, 1f);
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00016E9D File Offset: 0x0001509D
	public static AudioSource PlaySound(AudioClip clip, float volume)
	{
		return NGUITools.PlaySound(clip, volume, 1f);
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00016EAC File Offset: 0x000150AC
	public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
	{
		if (clip != null)
		{
			if (NGUITools.mListener == null)
			{
				NGUITools.mListener = Object.FindObjectOfType(typeof(AudioListener)) as AudioListener;
				if (NGUITools.mListener == null)
				{
					Camera camera = Camera.main;
					if (camera == null)
					{
						camera = Object.FindObjectOfType(typeof(Camera)) as Camera;
					}
					if (camera != null)
					{
						NGUITools.mListener = camera.gameObject.AddComponent<AudioListener>();
					}
				}
			}
			if (NGUITools.mListener != null)
			{
				AudioSource audioSource = NGUITools.mListener.GetComponent<AudioSource>();
				if (audioSource == null)
				{
					audioSource = NGUITools.mListener.gameObject.AddComponent<AudioSource>();
				}
				audioSource.pitch = pitch;
				audioSource.PlayOneShot(clip, volume);
				return audioSource;
			}
		}
		return null;
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00016F78 File Offset: 0x00015178
	public static WWW OpenURL(string url)
	{
		WWW www = null;
		try
		{
			www = new WWW(url);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
		}
		return www;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00016FB0 File Offset: 0x000151B0
	public static int RandomRange(int min, int max)
	{
		if (min == max)
		{
			return min;
		}
		return Random.Range(min, max + 1);
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00016FC4 File Offset: 0x000151C4
	public static string GetHierarchy(GameObject obj)
	{
		string text = obj.name;
		while (obj.transform.parent != null)
		{
			obj = obj.transform.parent.gameObject;
			text = obj.name + "/" + text;
		}
		return "\"" + text + "\"";
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00017024 File Offset: 0x00015224
	public static Color ParseColor(string text, int offset)
	{
		int num = (NGUIMath.HexToDecimal(text[offset]) << 4) | NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = (NGUIMath.HexToDecimal(text[offset + 2]) << 4) | NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = (NGUIMath.HexToDecimal(text[offset + 4]) << 4) | NGUIMath.HexToDecimal(text[offset + 5]);
		float num4 = 0.003921569f;
		return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x000170A8 File Offset: 0x000152A8
	public static string EncodeColor(Color c)
	{
		return (16777215 & (NGUIMath.ColorToInt(c) >> 8)).ToString("X6");
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x000170D0 File Offset: 0x000152D0
	public static int ParseSymbol(string text, int index, List<Color> colors)
	{
		int length = text.Length;
		if (index + 2 < length)
		{
			if (text[index + 1] == '-')
			{
				if (text[index + 2] == ']')
				{
					if (colors != null && colors.Count > 1)
					{
						colors.RemoveAt(colors.Count - 1);
					}
					return 3;
				}
			}
			else if (index + 7 < length && text[index + 7] == ']')
			{
				if (colors != null)
				{
					Color color = NGUITools.ParseColor(text, index + 1);
					color.a = colors[colors.Count - 1].a;
					colors.Add(color);
				}
				return 8;
			}
		}
		return 0;
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x00017164 File Offset: 0x00015364
	public static string StripSymbols(string text)
	{
		if (text != null)
		{
			text = text.Replace("\\n", "\n");
			int i = 0;
			int num = text.Length;
			while (i < num)
			{
				if (text[i] == '[')
				{
					int num2 = NGUITools.ParseSymbol(text, i, null);
					if (num2 > 0)
					{
						text = text.Remove(i, num2);
						num = text.Length;
						continue;
					}
				}
				i++;
			}
		}
		return text;
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x000171C5 File Offset: 0x000153C5
	public static T[] FindActive<T>() where T : Component
	{
		return Object.FindSceneObjectsOfType(typeof(T)) as T[];
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x000171DC File Offset: 0x000153DC
	public static Camera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		Camera[] array = NGUITools.FindActive<Camera>();
		int i = 0;
		int num2 = array.Length;
		while (i < num2)
		{
			Camera camera = array[i];
			if ((camera.cullingMask & num) != 0)
			{
				return camera;
			}
			i++;
		}
		return null;
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0001721C File Offset: 0x0001541C
	public static BoxCollider AddWidgetCollider(GameObject go)
	{
		if (go != null)
		{
			Collider component = go.GetComponent<Collider>();
			BoxCollider boxCollider = component as BoxCollider;
			if (boxCollider == null)
			{
				if (component != null)
				{
					if (Application.isPlaying)
					{
						Object.Destroy(component);
					}
					else
					{
						Object.DestroyImmediate(component);
					}
				}
				boxCollider = go.AddComponent<BoxCollider>();
			}
			int num = NGUITools.CalculateNextDepth(go);
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(go.transform);
			boxCollider.isTrigger = true;
			boxCollider.center = bounds.center + Vector3.back * ((float)num * 0.25f);
			boxCollider.size = new Vector3(bounds.size.x, bounds.size.y, 0f);
			return boxCollider;
		}
		return null;
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x000172DC File Offset: 0x000154DC
	[Obsolete("Use UIAtlas.replacement instead")]
	public static void ReplaceAtlas(UIAtlas before, UIAtlas after)
	{
		UISprite[] array = NGUITools.FindActive<UISprite>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UISprite uisprite = array[i];
			if (uisprite.atlas == before)
			{
				uisprite.atlas = after;
			}
			i++;
		}
		UILabel[] array2 = NGUITools.FindActive<UILabel>();
		int j = 0;
		int num2 = array2.Length;
		while (j < num2)
		{
			UILabel uilabel = array2[j];
			if (uilabel.font != null && uilabel.font.atlas == before)
			{
				uilabel.font.atlas = after;
			}
			j++;
		}
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0001736C File Offset: 0x0001556C
	[Obsolete("Use UIFont.replacement instead")]
	public static void ReplaceFont(UIFont before, UIFont after)
	{
		UILabel[] array = NGUITools.FindActive<UILabel>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UILabel uilabel = array[i];
			if (uilabel.font == before)
			{
				uilabel.font = after;
			}
			i++;
		}
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x000173A8 File Offset: 0x000155A8
	public static string GetName<T>() where T : Component
	{
		string text = typeof(T).ToString();
		if (text.StartsWith("UI"))
		{
			text = text.Substring(2);
		}
		else if (text.StartsWith("UnityEngine."))
		{
			text = text.Substring(12);
		}
		return text;
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x000173F4 File Offset: 0x000155F4
	public static GameObject AddChild(GameObject parent)
	{
		GameObject gameObject = new GameObject();
		if (parent != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = parent.layer;
		}
		return gameObject;
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x00017450 File Offset: 0x00015650
	public static GameObject AddChild(GameObject parent, GameObject prefab)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(prefab);
		if (gameObject != null && parent != null)
		{
			Transform transform = gameObject.transform;
			transform.parent = parent.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gameObject.layer = parent.layer;
		}
		return gameObject;
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x000174B8 File Offset: 0x000156B8
	public static int CalculateNextDepth(GameObject go)
	{
		int num = -1;
		UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
		int i = 0;
		int num2 = componentsInChildren.Length;
		while (i < num2)
		{
			num = Mathf.Max(num, componentsInChildren[i].depth);
			i++;
		}
		return num + 1;
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x000174F0 File Offset: 0x000156F0
	public static T AddChild<T>(GameObject parent) where T : Component
	{
		GameObject gameObject = NGUITools.AddChild(parent);
		gameObject.name = NGUITools.GetName<T>();
		return gameObject.AddComponent<T>();
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x00017508 File Offset: 0x00015708
	public static T AddWidget<T>(GameObject go) where T : UIWidget
	{
		int num = NGUITools.CalculateNextDepth(go);
		T t = NGUITools.AddChild<T>(go);
		t.depth = num;
		Transform transform = t.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = new Vector3(100f, 100f, 1f);
		t.gameObject.layer = go.layer;
		return t;
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x00017580 File Offset: 0x00015780
	public static UISprite AddSprite(GameObject go, UIAtlas atlas, string spriteName)
	{
		UIAtlas.Sprite sprite = ((!(atlas != null)) ? null : atlas.GetSprite(spriteName));
		UISlicedSprite uislicedSprite = ((sprite != null && !(sprite.inner == sprite.outer)) ? NGUITools.AddWidget<UISlicedSprite>(go) : NGUITools.AddWidget<UISprite>(go));
		uislicedSprite.atlas = atlas;
		uislicedSprite.spriteName = spriteName;
		return uislicedSprite;
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x000175D4 File Offset: 0x000157D4
	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null)
		{
			return default(T);
		}
		object obj = go.GetComponent<T>();
		if (obj == null)
		{
			Transform transform = go.transform.parent;
			while (transform != null && obj == null)
			{
				obj = transform.gameObject.GetComponent<T>();
				transform = transform.parent;
			}
		}
		return (T)((object)obj);
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0001763B File Offset: 0x0001583B
	public static void Destroy(Object obj)
	{
		if (obj != null)
		{
			if (Application.isPlaying)
			{
				Object.Destroy(obj);
				return;
			}
			Object.DestroyImmediate(obj);
		}
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0001765A File Offset: 0x0001585A
	public static void DestroyImmediate(Object obj)
	{
		if (obj != null)
		{
			if (Application.isEditor)
			{
				Object.DestroyImmediate(obj);
				return;
			}
			Object.Destroy(obj);
		}
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0001767C File Offset: 0x0001587C
	public static void Broadcast(string funcName)
	{
		GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, SendMessageOptions.DontRequireReceiver);
			i++;
		}
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x000176B8 File Offset: 0x000158B8
	public static void Broadcast(string funcName, object param)
	{
		GameObject[] array = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			array[i].SendMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
			i++;
		}
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x000176F5 File Offset: 0x000158F5
	public static bool IsChild(Transform parent, Transform child)
	{
		if (parent == null || child == null)
		{
			return false;
		}
		while (child != null)
		{
			if (child == parent)
			{
				return true;
			}
			child = child.parent;
		}
		return false;
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x00017728 File Offset: 0x00015928
	private static void Activate(Transform t)
	{
		t.gameObject.active = true;
		int i = 0;
		int childCount = t.childCount;
		while (i < childCount)
		{
			NGUITools.Activate(t.GetChild(i));
			i++;
		}
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x00017760 File Offset: 0x00015960
	private static void Deactivate(Transform t)
	{
		int i = 0;
		int childCount = t.childCount;
		while (i < childCount)
		{
			NGUITools.Deactivate(t.GetChild(i));
			i++;
		}
		t.gameObject.active = false;
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00017798 File Offset: 0x00015998
	public static void SetActive(GameObject go, bool state)
	{
		if (state)
		{
			NGUITools.Activate(go.transform);
			return;
		}
		NGUITools.Deactivate(go.transform);
	}

	// Token: 0x0400040E RID: 1038
	private static AudioListener mListener;
}
