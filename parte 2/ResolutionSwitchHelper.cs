using System;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[AddComponentMenu("GUI/Resolution/ResolutionHelper Component &r")]
public class ResolutionSwitchHelper : MonoBehaviour
{
	// Token: 0x0600054B RID: 1355 RVA: 0x00019820 File Offset: 0x00017A20
	private void Awake()
	{
		if (!DeviceInfo.isHighres)
		{
			return;
		}
		base.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			transform.localScale = new Vector3(transform.localScale.x * 2f, transform.localScale.y * 2f, transform.localScale.z);
			transform.localPosition = new Vector3(transform.localPosition.x * 2f, transform.localPosition.y * 2f, transform.localPosition.z);
			TweenScale component = transform.gameObject.GetComponent<TweenScale>();
			if (component != null)
			{
				component.from = new Vector3(component.from.x * 2f, component.from.y * 2f, component.from.z);
				component.to = new Vector3(component.to.x * 2f, component.to.y * 2f, component.to.z);
			}
			TweenPosition component2 = transform.gameObject.GetComponent<TweenPosition>();
			if (component2 != null)
			{
				component2.from = new Vector3(component2.from.x * 2f, component2.from.y * 2f, component2.from.z);
				component2.to = new Vector3(component2.to.x * 2f, component2.to.y * 2f, component2.to.z);
			}
			if (transform.gameObject.GetComponent<TweenTransform>() != null)
			{
				Debug.LogError("TweenTransform used and not handled in resolution switcher.");
				Debug.Break();
			}
		}
	}
}
