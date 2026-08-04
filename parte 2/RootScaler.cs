using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
[RequireComponent(typeof(UIRoot))]
public class RootScaler : MonoBehaviour
{
	// Token: 0x06000552 RID: 1362 RVA: 0x00019B20 File Offset: 0x00017D20
	private void Awake()
	{
		this.myUIRoot = base.gameObject.GetComponent<UIRoot>();
		if (DeviceInfo.formFactor == DeviceInfo.FormFactor.iPad)
		{
			Debug.Log("iPad screen");
			int num = Mathf.RoundToInt((float)(this.myUIRoot.manualHeight * 16 / 15));
			Debug.Log("New height: " + num.ToString());
			this.myUIRoot.manualHeight = num;
		}
	}

	// Token: 0x04000479 RID: 1145
	private UIRoot myUIRoot;
}
