using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class FriendGhostHelper : MonoBehaviour
{
	// Token: 0x06000315 RID: 789 RVA: 0x0000DBD3 File Offset: 0x0000BDD3
	private void Awake()
	{
		if (!this.inited)
		{
			this.Init();
		}
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0000DBE4 File Offset: 0x0000BDE4
	private void Init()
	{
		this._backgroundAlphaDefault = this.background.alpha;
		this._frameAlphaDefault = this.frame.alpha;
		this._pointsAlphaDefault = this.points.alpha;
		this._pictureAlphaDefault = this.picture.alpha;
		this._cachedTransform = base.transform;
		this.picture.material = new Material(Shader.Find("Unlit/Transparent Colored"));
		this.inited = true;
		this.handler = this._cachedTransform.parent.GetComponent<FriendGhostHandler>();
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0000DC78 File Offset: 0x0000BE78
	public void NewGame()
	{
		if (!this.inited)
		{
			this.Init();
		}
		this._gameRunning = true;
		this._cachedTransform.localPosition = this._resetPosition;
		this.background.alpha = this._backgroundAlphaDefault;
		this.frame.alpha = this._frameAlphaDefault;
		this.points.alpha = this._pointsAlphaDefault;
		this.picture.alpha = this._pictureAlphaDefault;
		this.noFriendsLeftToGhost = false;
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0000DCF6 File Offset: 0x0000BEF6
	public void AnimateIn()
	{
		if (!this.noFriendsLeftToGhost)
		{
			base.StartCoroutine(this._AnimateIn());
		}
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0000DD0D File Offset: 0x0000BF0D
	public void AnimateOut()
	{
		if (this._gameRunning)
		{
			base.StartCoroutine(this._AnimateOut());
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0000DD24 File Offset: 0x0000BF24
	public void NoFriendsLeft()
	{
		this._cachedTransform.localPosition = this._resetPosition;
		this.background.alpha = this._backgroundAlphaDefault;
		this.frame.alpha = this._frameAlphaDefault;
		this.points.alpha = this._pointsAlphaDefault;
		this.picture.alpha = this._pictureAlphaDefault;
		this.noFriendsLeftToGhost = true;
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0000DD90 File Offset: 0x0000BF90
	public void GameOver()
	{
		this._gameRunning = false;
		this._cachedTransform.localPosition = this._resetPosition;
		this.background.alpha = this._backgroundAlphaDefault;
		this.frame.alpha = this._frameAlphaDefault;
		this.points.alpha = this._pointsAlphaDefault;
		this.picture.alpha = this._pictureAlphaDefault;
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0000DDF9 File Offset: 0x0000BFF9
	private IEnumerator _AnimateIn()
	{
		this.animatingNow = true;
		float duration = 0.5f;
		float factor2 = 0f;
		while (factor2 < 1f && this._gameRunning)
		{
			factor2 += Time.deltaTime / duration;
			factor2 = Mathf.Clamp01(factor2);
			this._cachedTransform.localPosition = Vector3.Lerp(this._resetPosition, this._activePosition, factor2);
			yield return null;
		}
		if (!this._gameRunning)
		{
			this._cachedTransform.localScale = this._resetPosition;
		}
		else
		{
			this._cachedTransform.localPosition = this._activePosition;
		}
		this.animatingNow = false;
		yield break;
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0000DE08 File Offset: 0x0000C008
	private IEnumerator _AnimateOut()
	{
		this.animatingNow = true;
		float duration = 0.5f;
		float factor2 = 0f;
		while (factor2 < 1f && this._gameRunning)
		{
			factor2 += Time.deltaTime / duration;
			factor2 = Mathf.Clamp01(factor2);
			this._cachedTransform.localPosition = Vector3.Lerp(this._activePosition, this._moveOutPosition, factor2);
			this.background.alpha = Mathf.Lerp(this._backgroundAlphaDefault, 0f, factor2);
			this.frame.alpha = Mathf.Lerp(this._frameAlphaDefault, 0f, factor2);
			this.points.alpha = Mathf.Lerp(this._pointsAlphaDefault, 0f, factor2);
			this.picture.alpha = Mathf.Lerp(this._pictureAlphaDefault, 0f, factor2);
			yield return null;
		}
		this._cachedTransform.localPosition = this._resetPosition;
		this.background.alpha = this._backgroundAlphaDefault;
		this.frame.alpha = this._frameAlphaDefault;
		this.points.alpha = this._pointsAlphaDefault;
		this.picture.alpha = this._pictureAlphaDefault;
		this.animatingNow = false;
		this.handler.FinishedAnimatingOut();
		yield break;
	}

	// Token: 0x04000248 RID: 584
	public UISlicedSprite background;

	// Token: 0x04000249 RID: 585
	public UISlicedSprite frame;

	// Token: 0x0400024A RID: 586
	public UILabel points;

	// Token: 0x0400024B RID: 587
	public UITexture picture;

	// Token: 0x0400024C RID: 588
	private Transform _cachedTransform;

	// Token: 0x0400024D RID: 589
	private Vector3 _resetPosition = new Vector3(80f, 0f, 0f);

	// Token: 0x0400024E RID: 590
	private Vector3 _activePosition = Vector3.zero;

	// Token: 0x0400024F RID: 591
	private Vector3 _moveOutPosition = new Vector3(0f, -140f, 0f);

	// Token: 0x04000250 RID: 592
	private float _backgroundAlphaDefault;

	// Token: 0x04000251 RID: 593
	private float _frameAlphaDefault;

	// Token: 0x04000252 RID: 594
	private float _pointsAlphaDefault;

	// Token: 0x04000253 RID: 595
	private float _pictureAlphaDefault;

	// Token: 0x04000254 RID: 596
	private bool inited;

	// Token: 0x04000255 RID: 597
	private bool _gameRunning;

	// Token: 0x04000256 RID: 598
	public bool noFriendsLeftToGhost;

	// Token: 0x04000257 RID: 599
	private FriendGhostHandler handler;

	// Token: 0x04000258 RID: 600
	public bool animatingNow;
}
