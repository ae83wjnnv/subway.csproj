using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class FriendCrewPokeHelper : MonoBehaviour
{
	// Token: 0x06000305 RID: 773 RVA: 0x0000D64F File Offset: 0x0000B84F
	public void ActivatePoke(Friend friend)
	{
		this._friend = friend;
		NGUITools.AddWidgetCollider(base.gameObject);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x0000D664 File Offset: 0x0000B864
	public void DeactivatePoke()
	{
		Object.Destroy(base.gameObject.GetComponent<Collider>());
		NGUITools.SetActive(this.zzzIcon.gameObject, false);
		NGUITools.SetActive(this.pokeIcon.gameObject, false);
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0000D698 File Offset: 0x0000B898
	private void OnClick()
	{
		this.DeactivatePoke();
		SocialManager.instance.Poke(this._friend);
	}

	// Token: 0x0400023B RID: 571
	public UISprite zzzIcon;

	// Token: 0x0400023C RID: 572
	public UISprite pokeIcon;

	// Token: 0x0400023D RID: 573
	private Friend _friend;
}
