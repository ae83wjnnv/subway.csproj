using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
[AddComponentMenu("NGUI/Examples/Slider Colors")]
[RequireComponent(typeof(UISlider))]
[ExecuteInEditMode]
public class UISliderColors : MonoBehaviour
{
	// Token: 0x06000914 RID: 2324 RVA: 0x00030DC9 File Offset: 0x0002EFC9
	private void Start()
	{
		this.mSlider = base.GetComponent<UISlider>();
		this.Update();
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x00030DE0 File Offset: 0x0002EFE0
	private void Update()
	{
		if (this.sprite == null || this.colors.Length == 0)
		{
			return;
		}
		float num = this.mSlider.sliderValue;
		num *= (float)(this.colors.Length - 1);
		int num2 = Mathf.FloorToInt(num);
		Color color = this.colors[0];
		if (num2 >= 0)
		{
			if (num2 + 1 >= this.colors.Length)
			{
				color = ((num2 >= this.colors.Length) ? this.colors[this.colors.Length - 1] : this.colors[num2]);
			}
			else
			{
				float num3 = (num - (float)num2) / (float)(this.colors.Length - 2);
				color = Color.Lerp(this.colors[num2], this.colors[num2 + 1], num3);
			}
		}
		color.a = this.sprite.color.a;
		this.sprite.color = color;
	}

	// Token: 0x040007E9 RID: 2025
	public UISprite sprite;

	// Token: 0x040007EA RID: 2026
	public Color[] colors = new Color[]
	{
		Color.red,
		Color.yellow,
		Color.green
	};

	// Token: 0x040007EB RID: 2027
	private UISlider mSlider;
}
