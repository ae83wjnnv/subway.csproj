using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class GameOverOfflineHelper : MonoBehaviour
{
	// Token: 0x06000379 RID: 889 RVA: 0x0001057C File Offset: 0x0000E77C
	public void EnableButtons()
	{
		this.FacebookLoginButton.GetComponent<UIButtonColor>().defaultColor = Color.white;
		this.GameCenterLoginButton.GetComponent<UIButtonColor>().defaultColor = Color.white;
		NGUITools.AddWidgetCollider(this.FacebookLoginButton);
		NGUITools.AddWidgetCollider(this.GameCenterLoginButton);
		base.StartCoroutine(this.AnimateAlpha(this.FacebookIcon, this.GameCenterIcon, 0.2f, 1f));
	}

	// Token: 0x0600037A RID: 890 RVA: 0x000105F0 File Offset: 0x0000E7F0
	public void DisableButtons()
	{
		if (this.FacebookLoginButton.GetComponent<Collider>() != null)
		{
			Object.Destroy(this.FacebookLoginButton.GetComponent<Collider>());
		}
		if (this.GameCenterLoginButton.GetComponent<Collider>() != null)
		{
			Object.Destroy(this.GameCenterLoginButton.GetComponent<Collider>());
		}
		this.FacebookIcon.alpha = 0.5f;
		this.GameCenterIcon.alpha = 0.5f;
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00010663 File Offset: 0x0000E863
	private IEnumerator AnimateAlpha(UISprite sprite1, UISprite sprite2, float duration, float toAlpha)
	{
		float fromAlpha = sprite1.alpha;
		float factor2 = 0f;
		while (factor2 < 1f)
		{
			factor2 += Time.deltaTime / duration;
			factor2 = Mathf.Clamp01(factor2);
			sprite1.alpha = Mathf.Lerp(fromAlpha, toAlpha, factor2);
			sprite2.alpha = Mathf.Lerp(fromAlpha, toAlpha, factor2);
			yield return null;
		}
		sprite1.alpha = toAlpha;
		sprite2.alpha = toAlpha;
		yield break;
	}

	// Token: 0x040002D8 RID: 728
	public GameObject FacebookLoginButton;

	// Token: 0x040002D9 RID: 729
	public GameObject GameCenterLoginButton;

	// Token: 0x040002DA RID: 730
	public UISprite FacebookIcon;

	// Token: 0x040002DB RID: 731
	public UISprite GameCenterIcon;
}
