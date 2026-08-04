using System;
using UnityEngine;

// Token: 0x02000127 RID: 295
[AddComponentMenu("NGUI/Interaction/Saved Option")]
public class UISavedOption : MonoBehaviour
{
	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0002E006 File Offset: 0x0002C206
	private string key
	{
		get
		{
			if (string.IsNullOrEmpty(this.keyName))
			{
				return "NGUI State: " + base.name;
			}
			return this.keyName;
		}
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0002E02C File Offset: 0x0002C22C
	private void OnEnable()
	{
		string @string = PlayerPrefs.GetString(this.key);
		if (string.IsNullOrEmpty(@string))
		{
			return;
		}
		UICheckbox component = base.GetComponent<UICheckbox>();
		if (component != null)
		{
			component.isChecked = @string == "true";
			return;
		}
		UICheckbox[] componentsInChildren = base.GetComponentsInChildren<UICheckbox>();
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			UICheckbox uicheckbox = componentsInChildren[i];
			UIEventListener uieventListener = UIEventListener.Get(uicheckbox.gameObject);
			uieventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uieventListener.onClick, new UIEventListener.VoidDelegate(this.Save));
			uicheckbox.isChecked = uicheckbox.name == @string;
			Debug.Log(@string);
			UIEventListener uieventListener2 = UIEventListener.Get(uicheckbox.gameObject);
			uieventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new UIEventListener.VoidDelegate(this.Save));
			i++;
		}
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x0002E0FB File Offset: 0x0002C2FB
	private void OnDisable()
	{
		this.Save(null);
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x0002E104 File Offset: 0x0002C304
	private void Save(GameObject go)
	{
		UICheckbox component = base.GetComponent<UICheckbox>();
		if (component != null)
		{
			PlayerPrefs.SetString(this.key, (!component.isChecked) ? "false" : "true");
			return;
		}
		UICheckbox[] componentsInChildren = base.GetComponentsInChildren<UICheckbox>();
		int i = 0;
		int num = componentsInChildren.Length;
		while (i < num)
		{
			UICheckbox uicheckbox = componentsInChildren[i];
			if (uicheckbox.isChecked)
			{
				PlayerPrefs.SetString(this.key, uicheckbox.name);
				return;
			}
			i++;
		}
	}

	// Token: 0x04000799 RID: 1945
	public string keyName;
}
