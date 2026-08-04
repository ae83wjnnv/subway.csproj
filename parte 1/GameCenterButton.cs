using System;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class GameCenterButton : MonoBehaviour
{
	// Token: 0x06000377 RID: 887 RVA: 0x0001053F File Offset: 0x0000E73F
	private void OnClick()
	{
		Debug.Log("Game Center button clicked");
		if (!Social.localUser.authenticated)
		{
			DeviceUtility.showNativePopup("Game Center Disabled", "Sign in with the Game Center application to enable", "Ok");
			return;
		}
		Social.ShowLeaderboardUI();
	}
}
