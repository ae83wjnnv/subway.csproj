using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class TutorialEvent : MonoBehaviour
{
	// Token: 0x06000674 RID: 1652 RVA: 0x0002024C File Offset: 0x0001E44C
	public void Awake()
	{
		this.game = Game.Instance;
		this.hoverboard = Hoverboard.Instance;
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00020264 File Offset: 0x0001E464
	public void Update()
	{
		if (!(this.game == null) && !this.Initialiseret)
		{
			this.character = this.game.character;
			this.track = this.game.track;
			this.Initialiseret = true;
		}
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x000202B0 File Offset: 0x0001E4B0
	private IEnumerator ShowArrow()
	{
		this.mesh.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
		this.mesh.transform.Rotate(new Vector3(0f, 0f, 1f), this.direction);
		this.mesh.active = true;
		Vector3 pos = new Vector3(0f, 0f, 20f);
		yield return base.StartCoroutine(pTween.To(this.time, delegate(float t)
		{
			this.mesh.transform.localPosition = Vector3.Lerp(pos - this.mesh.transform.up * 5f, pos + this.mesh.transform.up * 5f, t);
			this.mesh.GetComponent<Renderer>().material.mainTextureOffset = Vector2.Lerp(Vector2.zero, new Vector2(0f, -0.02f), t);
		}));
		this.mesh.active = false;
		yield break;
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x000202C0 File Offset: 0x0001E4C0
	private void OnTriggerExit(Collider collider)
	{
		if (!this.character.stopColliding && collider.gameObject.name.Equals("Character"))
		{
			if (this.displayText)
			{
				UIScreenController.Instance.QueueMessage(this.text);
			}
			if (this.displayMesh)
			{
				base.StartCoroutine(this.ShowArrow());
			}
			if (this.allowHoverboard)
			{
				this.hoverboard.isAllowed = true;
			}
			if (this.endTutorial)
			{
				this.track.IsRunningOnTutorialTrack = false;
				PlayerInfo.Instance.tutorialCompleted = true;
				this.track.tutorial = false;
			}
		}
	}

	// Token: 0x0400057D RID: 1405
	private Game game;

	// Token: 0x0400057E RID: 1406
	public bool displayText;

	// Token: 0x0400057F RID: 1407
	public string text;

	// Token: 0x04000580 RID: 1408
	public bool displayMesh;

	// Token: 0x04000581 RID: 1409
	public GameObject mesh;

	// Token: 0x04000582 RID: 1410
	public float direction;

	// Token: 0x04000583 RID: 1411
	public float time = 1f;

	// Token: 0x04000584 RID: 1412
	public bool endTutorial;

	// Token: 0x04000585 RID: 1413
	public bool allowHoverboard;

	// Token: 0x04000586 RID: 1414
	private Hoverboard hoverboard;

	// Token: 0x04000587 RID: 1415
	private Character character;

	// Token: 0x04000588 RID: 1416
	private Track track;

	// Token: 0x04000589 RID: 1417
	private bool Initialiseret;
}
