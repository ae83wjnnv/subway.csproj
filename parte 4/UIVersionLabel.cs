using System;
using UnityEngine;

// Token: 0x02000141 RID: 321
[RequireComponent(typeof(UILabel))]
public class UIVersionLabel : MonoBehaviour
{
	// Token: 0x0600096C RID: 2412 RVA: 0x000338BC File Offset: 0x00031ABC
	private void Start()
	{
		base.GetComponent<UILabel>().text = "v" + DeviceUtility.GetBundleVersion();
	}
}
