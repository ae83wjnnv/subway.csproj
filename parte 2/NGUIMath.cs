using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public static class NGUIMath
{
	// Token: 0x060004A2 RID: 1186 RVA: 0x000161C7 File Offset: 0x000143C7
	public static float WrapAngle(float angle)
	{
		while (angle > 180f)
		{
			angle -= 360f;
		}
		while (angle < -180f)
		{
			angle += 360f;
		}
		return angle;
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x000161F0 File Offset: 0x000143F0
	public static int HexToDecimal(char ch)
	{
		switch (ch)
		{
		case '0':
			return 0;
		case '1':
			return 1;
		case '2':
			return 2;
		case '3':
			return 3;
		case '4':
			return 4;
		case '5':
			return 5;
		case '6':
			return 6;
		case '7':
			return 7;
		case '8':
			return 8;
		case '9':
			return 9;
		case ':':
		case ';':
		case '<':
		case '=':
		case '>':
		case '?':
		case '@':
			return 15;
		case 'A':
			break;
		case 'B':
			return 11;
		case 'C':
			return 12;
		case 'D':
			return 13;
		case 'E':
			return 14;
		case 'F':
			return 15;
		default:
			switch (ch)
			{
			case 'a':
				break;
			case 'b':
				return 11;
			case 'c':
				return 12;
			case 'd':
				return 13;
			case 'e':
				return 14;
			case 'f':
				return 15;
			default:
				return 15;
			}
			break;
		}
		return 10;
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x000162B0 File Offset: 0x000144B0
	public static int ColorToInt(Color c)
	{
		return 0 | (Mathf.RoundToInt(c.r * 255f) << 24) | (Mathf.RoundToInt(c.g * 255f) << 16) | (Mathf.RoundToInt(c.b * 255f) << 8) | Mathf.RoundToInt(c.a * 255f);
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x00016310 File Offset: 0x00014510
	public static Color IntToColor(int val)
	{
		float num = 0.003921569f;
		Color black = Color.black;
		black.r = num * (float)((val >> 24) & 255);
		black.g = num * (float)((val >> 16) & 255);
		black.b = num * (float)((val >> 8) & 255);
		black.a = num * (float)(val & 255);
		return black;
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x00016378 File Offset: 0x00014578
	public static string IntToBinary(int val, int bits)
	{
		string text = string.Empty;
		int i = bits;
		while (i > 0)
		{
			if (i == 8 || i == 16 || i == 24)
			{
				text += " ";
			}
			text += (((val & (1 << --i)) == 0) ? '0' : '1').ToString();
		}
		return text;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x000163D1 File Offset: 0x000145D1
	public static Color HexToColor(uint val)
	{
		return NGUIMath.IntToColor((int)val);
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x000163DC File Offset: 0x000145DC
	public static Rect ConvertToTexCoords(Rect rect, int width, int height)
	{
		Rect rect2 = rect;
		if ((float)width != 0f && (float)height != 0f)
		{
			rect2.xMin = rect.xMin / (float)width;
			rect2.xMax = rect.xMax / (float)width;
			rect2.yMin = 1f - rect.yMax / (float)height;
			rect2.yMax = 1f - rect.yMin / (float)height;
		}
		return rect2;
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00016450 File Offset: 0x00014650
	public static Rect ConvertToPixels(Rect rect, int width, int height, bool round)
	{
		Rect rect2 = rect;
		if (round)
		{
			rect2.xMin = (float)Mathf.RoundToInt(rect.xMin * (float)width);
			rect2.xMax = (float)Mathf.RoundToInt(rect.xMax * (float)width);
			rect2.yMin = (float)Mathf.RoundToInt((1f - rect.yMax) * (float)height);
			rect2.yMax = (float)Mathf.RoundToInt((1f - rect.yMin) * (float)height);
		}
		else
		{
			rect2.xMin = rect.xMin * (float)width;
			rect2.xMax = rect.xMax * (float)width;
			rect2.yMin = (1f - rect.yMax) * (float)height;
			rect2.yMax = (1f - rect.yMin) * (float)height;
		}
		return rect2;
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00016520 File Offset: 0x00014720
	public static Rect MakePixelPerfect(Rect rect)
	{
		rect.xMin = (float)Mathf.RoundToInt(rect.xMin);
		rect.yMin = (float)Mathf.RoundToInt(rect.yMin);
		rect.xMax = (float)Mathf.RoundToInt(rect.xMax);
		rect.yMax = (float)Mathf.RoundToInt(rect.yMax);
		return rect;
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00016580 File Offset: 0x00014780
	public static Rect MakePixelPerfect(Rect rect, int width, int height)
	{
		rect = NGUIMath.ConvertToPixels(rect, width, height, true);
		rect.xMin = (float)Mathf.RoundToInt(rect.xMin);
		rect.yMin = (float)Mathf.RoundToInt(rect.yMin);
		rect.xMax = (float)Mathf.RoundToInt(rect.xMax);
		rect.yMax = (float)Mathf.RoundToInt(rect.yMax);
		return NGUIMath.ConvertToTexCoords(rect, width, height);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x000165F0 File Offset: 0x000147F0
	public static Vector3 ApplyHalfPixelOffset(Vector3 pos)
	{
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.WindowsPlayer || platform == RuntimePlatform.WebGLPlayer || platform == RuntimePlatform.WindowsEditor)
		{
			pos.x -= 0.5f;
			pos.y += 0.5f;
		}
		return pos;
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00016634 File Offset: 0x00014834
	public static Vector3 ApplyHalfPixelOffset(Vector3 pos, Vector3 scale)
	{
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.WindowsPlayer || platform == RuntimePlatform.WebGLPlayer || platform == RuntimePlatform.WindowsEditor)
		{
			if (Mathf.RoundToInt(scale.x) == Mathf.RoundToInt(scale.x * 0.5f) * 2)
			{
				pos.x -= 0.5f;
			}
			if (Mathf.RoundToInt(scale.y) == Mathf.RoundToInt(scale.y * 0.5f) * 2)
			{
				pos.y += 0.5f;
			}
		}
		return pos;
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x000166B8 File Offset: 0x000148B8
	public static Vector2 ConstrainRect(Vector2 minRect, Vector2 maxRect, Vector2 minArea, Vector2 maxArea)
	{
		Vector2 zero = Vector2.zero;
		float num = maxRect.x - minRect.x;
		float num2 = maxRect.y - minRect.y;
		float num3 = maxArea.x - minArea.x;
		float num4 = maxArea.y - minArea.y;
		if (num > num3)
		{
			float num5 = num - num3;
			minArea.x -= num5;
			maxArea.x += num5;
		}
		if (num2 > num4)
		{
			float num6 = num2 - num4;
			minArea.y -= num6;
			maxArea.y += num6;
		}
		if (minRect.x < minArea.x)
		{
			zero.x += minArea.x - minRect.x;
		}
		if (maxRect.x > maxArea.x)
		{
			zero.x -= maxRect.x - maxArea.x;
		}
		if (minRect.y < minArea.y)
		{
			zero.y += minArea.y - minRect.y;
		}
		if (maxRect.y > maxArea.y)
		{
			zero.y -= maxRect.y - maxArea.y;
		}
		return zero;
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x000167E8 File Offset: 0x000149E8
	public static Bounds CalculateAbsoluteWidgetBounds(Transform trans)
	{
		UIWidget[] componentsInChildren = trans.GetComponentsInChildren<UIWidget>();
		Bounds bounds = new Bounds(trans.transform.position, Vector3.zero);
		bool flag = true;
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			UIWidget uiwidget = componentsInChildren[i];
			Vector2 vector = uiwidget.relativeSize;
			Vector2 pivotOffset = uiwidget.pivotOffset;
			float num2 = (pivotOffset.x + 0.5f) * vector.x;
			float num3 = (pivotOffset.y - 0.5f) * vector.y;
			vector *= 0.5f;
			Transform cachedTransform = uiwidget.cachedTransform;
			Vector3 vector2 = cachedTransform.TransformPoint(new Vector3(num2 - vector.x, num3 - vector.y, 0f));
			if (flag)
			{
				flag = false;
				bounds = new Bounds(vector2, Vector3.zero);
			}
			else
			{
				bounds.Encapsulate(vector2);
			}
			bounds.Encapsulate(cachedTransform.TransformPoint(new Vector3(num2 - vector.x, num3 + vector.y, 0f)));
			bounds.Encapsulate(cachedTransform.TransformPoint(new Vector3(num2 + vector.x, num3 - vector.y, 0f)));
			bounds.Encapsulate(cachedTransform.TransformPoint(new Vector3(num2 + vector.x, num3 + vector.y, 0f)));
			i++;
		}
		return bounds;
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00016948 File Offset: 0x00014B48
	public static Bounds CalculateRelativeWidgetBounds(Transform root, Transform child)
	{
		UIWidget[] componentsInChildren = child.GetComponentsInChildren<UIWidget>();
		Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
		bool flag = true;
		Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			UIWidget uiwidget = componentsInChildren[i];
			Vector2 vector = uiwidget.relativeSize;
			Vector2 pivotOffset = uiwidget.pivotOffset;
			Transform cachedTransform = uiwidget.cachedTransform;
			float num2 = (pivotOffset.x + 0.5f) * vector.x;
			float num3 = (pivotOffset.y - 0.5f) * vector.y;
			vector *= 0.5f;
			Vector3 vector2 = new Vector3(num2 - vector.x, num3 - vector.y, 0f);
			vector2 = cachedTransform.TransformPoint(vector2);
			vector2 = worldToLocalMatrix.MultiplyPoint3x4(vector2);
			if (flag)
			{
				flag = false;
				bounds = new Bounds(vector2, Vector3.zero);
			}
			else
			{
				bounds.Encapsulate(vector2);
			}
			vector2 = new Vector3(num2 - vector.x, num3 + vector.y, 0f);
			vector2 = cachedTransform.TransformPoint(vector2);
			vector2 = worldToLocalMatrix.MultiplyPoint3x4(vector2);
			bounds.Encapsulate(vector2);
			vector2 = new Vector3(num2 + vector.x, num3 - vector.y, 0f);
			vector2 = cachedTransform.TransformPoint(vector2);
			vector2 = worldToLocalMatrix.MultiplyPoint3x4(vector2);
			bounds.Encapsulate(vector2);
			vector2 = new Vector3(num2 + vector.x, num3 + vector.y, 0f);
			vector2 = cachedTransform.TransformPoint(vector2);
			vector2 = worldToLocalMatrix.MultiplyPoint3x4(vector2);
			bounds.Encapsulate(vector2);
			i++;
		}
		return bounds;
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00016AF4 File Offset: 0x00014CF4
	public static Bounds CalculateRelativeInnerBounds(Transform root, UISlicedSprite sprite)
	{
		Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
		Vector2 vector = sprite.relativeSize;
		Vector2 pivotOffset = sprite.pivotOffset;
		Transform cachedTransform = sprite.cachedTransform;
		float num = (pivotOffset.x + 0.5f) * vector.x;
		float num2 = (pivotOffset.y - 0.5f) * vector.y;
		vector *= 0.5f;
		float x = cachedTransform.localScale.x;
		float y = cachedTransform.localScale.y;
		Vector4 border = sprite.border;
		if (x != 0f)
		{
			border.x /= x;
			border.z /= x;
		}
		if (y != 0f)
		{
			border.y /= y;
			border.w /= y;
		}
		float num3 = num - vector.x + border.x;
		float num4 = num + vector.x - border.z;
		float num5 = num2 - vector.y + border.y;
		float num6 = num2 + vector.y - border.w;
		Vector3 vector2 = new Vector3(num3, num5, 0f);
		vector2 = cachedTransform.TransformPoint(vector2);
		vector2 = worldToLocalMatrix.MultiplyPoint3x4(vector2);
		Bounds bounds = new Bounds(vector2, Vector3.zero);
		vector2 = new Vector3(num3, num6, 0f);
		vector2 = cachedTransform.TransformPoint(vector2);
		vector2 = worldToLocalMatrix.MultiplyPoint3x4(vector2);
		bounds.Encapsulate(vector2);
		vector2 = new Vector3(num4, num6, 0f);
		vector2 = cachedTransform.TransformPoint(vector2);
		vector2 = worldToLocalMatrix.MultiplyPoint3x4(vector2);
		bounds.Encapsulate(vector2);
		vector2 = new Vector3(num4, num5, 0f);
		vector2 = cachedTransform.TransformPoint(vector2);
		vector2 = worldToLocalMatrix.MultiplyPoint3x4(vector2);
		bounds.Encapsulate(vector2);
		return bounds;
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00016CBC File Offset: 0x00014EBC
	public static Bounds CalculateRelativeInnerBounds(Transform root, UISprite sprite)
	{
		if (sprite is UISlicedSprite)
		{
			return NGUIMath.CalculateRelativeInnerBounds(root, sprite as UISlicedSprite);
		}
		return NGUIMath.CalculateRelativeWidgetBounds(root, sprite.cachedTransform);
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00016CDF File Offset: 0x00014EDF
	public static Bounds CalculateRelativeWidgetBounds(Transform trans)
	{
		return NGUIMath.CalculateRelativeWidgetBounds(trans, trans);
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00016CE8 File Offset: 0x00014EE8
	public static Vector3 SpringDampen(ref Vector3 velocity, float strength, float deltaTime)
	{
		float num = 1f - strength * 0.001f;
		int num2 = Mathf.RoundToInt(deltaTime * 1000f);
		Vector3 vector = Vector3.zero;
		for (int i = 0; i < num2; i++)
		{
			vector += velocity * 0.06f;
			velocity *= num;
		}
		return vector;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00016D4C File Offset: 0x00014F4C
	public static Vector2 SpringDampen(ref Vector2 velocity, float strength, float deltaTime)
	{
		float num = 1f - strength * 0.001f;
		int num2 = Mathf.RoundToInt(deltaTime * 1000f);
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < num2; i++)
		{
			vector += velocity * 0.06f;
			velocity *= num;
		}
		return vector;
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x00016DB0 File Offset: 0x00014FB0
	public static float SpringLerp(float strength, float deltaTime)
	{
		int num = Mathf.RoundToInt(deltaTime * 1000f);
		deltaTime = 0.001f * strength;
		float num2 = 0f;
		for (int i = 0; i < num; i++)
		{
			num2 = Mathf.Lerp(num2, 1f, deltaTime);
		}
		return num2;
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00016DF4 File Offset: 0x00014FF4
	public static float SpringLerp(float from, float to, float strength, float deltaTime)
	{
		int num = Mathf.RoundToInt(deltaTime * 1000f);
		deltaTime = 0.001f * strength;
		for (int i = 0; i < num; i++)
		{
			from = Mathf.Lerp(from, to, deltaTime);
		}
		return from;
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00016E2E File Offset: 0x0001502E
	public static Vector2 SpringLerp(Vector2 from, Vector2 to, float strength, float deltaTime)
	{
		return Vector2.Lerp(from, to, NGUIMath.SpringLerp(strength, deltaTime));
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x00016E3E File Offset: 0x0001503E
	public static Vector3 SpringLerp(Vector3 from, Vector3 to, float strength, float deltaTime)
	{
		return Vector3.Lerp(from, to, NGUIMath.SpringLerp(strength, deltaTime));
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00016E4E File Offset: 0x0001504E
	public static Quaternion SpringLerp(Quaternion from, Quaternion to, float strength, float deltaTime)
	{
		return Quaternion.Slerp(from, to, NGUIMath.SpringLerp(strength, deltaTime));
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00016E60 File Offset: 0x00015060
	public static float RotateTowards(float from, float to, float maxAngle)
	{
		float num = NGUIMath.WrapAngle(to - from);
		if (Mathf.Abs(num) > maxAngle)
		{
			num = maxAngle * Mathf.Sign(num);
		}
		return from + num;
	}
}
