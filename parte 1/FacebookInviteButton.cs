using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class FacebookInviteButton : MonoBehaviour
{
	// Token: 0x060002D5 RID: 725 RVA: 0x0000C909 File Offset: 0x0000AB09
	private void OnClick()
	{
		if (SocialManager.instance.facebookIsLoggedIn)
		{
			SocialManager.instance.RecommendAppFacebook();
			return;
		}
		SocialManager.instance.FacebookLogin(new Action<bool>(UIScreenController.Instance.FacebookLogIn));
	}
}
