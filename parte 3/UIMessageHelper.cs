using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class UIMessageHelper : MonoBehaviour
{
	// Token: 0x0600083E RID: 2110 RVA: 0x0002AF76 File Offset: 0x00029176
	private void Awake()
	{
		this._label = base.GetComponent<UILabel>();
		this._label.alpha = 0f;
		base.gameObject.active = false;
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0002AFA0 File Offset: 0x000291A0
	public void ShowMessage(string message)
	{
		base.gameObject.active = true;
		this._label.text = message;
		this._label.color = this.shownColor;
		base.StartCoroutine("FadeOut");
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x0002AFD7 File Offset: 0x000291D7
	private IEnumerator FadeOut()
	{
		yield return new WaitForSeconds(2f);
		float duration = 0.2f;
		float fadeTime = 0f;
		Vector3 scaleFrom = this._label.transform.localScale;
		Vector3 scaleTo = new Vector3(scaleFrom.x, 0f, scaleFrom.z);
		while (fadeTime < 1f)
		{
			fadeTime += Time.deltaTime / duration;
			this._label.transform.localScale = Vector3.Lerp(scaleFrom, scaleTo, fadeTime);
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		this._label.text = string.Empty;
		this._label.transform.localScale = scaleFrom;
		base.gameObject.active = false;
		UIScreenController.Instance.ReadyForNextMessage();
		yield break;
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x0002AFE6 File Offset: 0x000291E6
	public void SetTemporaryHidden(bool hidden)
	{
		if (this._label != null)
		{
			this._label.enabled = !hidden;
		}
	}

	// Token: 0x04000731 RID: 1841
	public Color shownColor = Color.white;

	// Token: 0x04000732 RID: 1842
	private UILabel _label;
}
