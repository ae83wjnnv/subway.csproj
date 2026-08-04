using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class FacebookLoginButton : MonoBehaviour
{
	// Token: 0x060002D7 RID: 727 RVA: 0x0000C944 File Offset: 0x0000AB44
	private void OnClick()
	{
		Debug.Log("Facebook login clicked");
		SocialManager.instance.FacebookLogin(new Action<bool>(UIScreenController.Instance.FacebookLogIn));
	}
}
