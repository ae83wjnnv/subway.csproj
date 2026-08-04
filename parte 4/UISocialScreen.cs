using System;
using UnityEngine;

// Token: 0x02000133 RID: 307
public class UISocialScreen : MonoBehaviour
{
	// Token: 0x06000917 RID: 2327 RVA: 0x00030F04 File Offset: 0x0002F104
	public void ReloadFriends()
	{
		if (this._highScoreHandler != null)
		{
			this._highScoreHandler.LoadHighScore();
			return;
		}
		if (!(this._crewHandler != null))
		{
			return;
		}
		this._crewHandler.InitCrew();
		if (SocialManager.instance.facebookIsLoggedIn)
		{
			if (this._FacebookLoginButton != null)
			{
				NGUITools.SetActive(this._FacebookLoginButton, false);
				Object.Destroy(this._FacebookLoginButton);
				return;
			}
		}
		else if (this._FacebookLoginButton == null)
		{
			this._FacebookLoginButton = NGUITools.AddChild(base.gameObject, this.FacebookLoginPrefab);
		}
	}

	// Token: 0x040007EC RID: 2028
	public FriendHandlerHighScore _highScoreHandler;

	// Token: 0x040007ED RID: 2029
	public FriendHandlerCrew _crewHandler;

	// Token: 0x040007EE RID: 2030
	public GameObject FacebookLoginPrefab;

	// Token: 0x040007EF RID: 2031
	private GameObject _FacebookLoginButton;
}
