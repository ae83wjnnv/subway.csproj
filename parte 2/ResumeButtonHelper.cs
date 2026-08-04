using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class ResumeButtonHelper : MonoBehaviour
{
	// Token: 0x0600054D RID: 1357 RVA: 0x00019A54 File Offset: 0x00017C54
	public void OnEnable()
	{
		this.icon.color = Color.white;
		base.StartCoroutine(this.EnableButtonWhenReady());
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00019A73 File Offset: 0x00017C73
	private IEnumerator EnableButtonWhenReady()
	{
		this.DisableButton();
		if (this.label)
		{
			this.label.text = "WAIT";
		}
		float startTime = Time.realtimeSinceStartup;
		float timeWaited = 0f;
		while (timeWaited < 1f)
		{
			timeWaited = Time.realtimeSinceStartup - startTime;
			yield return new WaitForEndOfFrame();
		}
		while (timeWaited < 5f && !SocialManager.instance.consolidatedFriendsCompleted)
		{
			timeWaited = Time.realtimeSinceStartup - startTime;
			yield return new WaitForEndOfFrame();
		}
		if (this.label)
		{
			this.label.text = "RESUME";
		}
		this.EnableButton();
		yield break;
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x00019A82 File Offset: 0x00017C82
	public void EnableButton()
	{
		if (!this.buttonEnabled)
		{
			NGUITools.AddWidgetCollider(base.gameObject);
			this.icon.color = this.initColor;
			this.buttonEnabled = true;
		}
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00019AB0 File Offset: 0x00017CB0
	public void DisableButton()
	{
		if (this.buttonEnabled)
		{
			if (base.gameObject.GetComponent<Collider>() != null)
			{
				Object.Destroy(base.gameObject.GetComponent<Collider>());
			}
			this.initColor = this.icon.color;
			this.icon.color = Color.gray;
			this.buttonEnabled = false;
		}
	}

	// Token: 0x04000471 RID: 1137
	public const float MIN_WAIT_TIME = 1f;

	// Token: 0x04000472 RID: 1138
	public const float MAX_WAIT_TIME = 5f;

	// Token: 0x04000473 RID: 1139
	public const string TEXT_WAIT = "WAIT";

	// Token: 0x04000474 RID: 1140
	private const string TEXT_RESUME = "RESUME";

	// Token: 0x04000475 RID: 1141
	public UILabel label;

	// Token: 0x04000476 RID: 1142
	private Color initColor;

	// Token: 0x04000477 RID: 1143
	public UISprite icon;

	// Token: 0x04000478 RID: 1144
	private bool buttonEnabled = true;
}
