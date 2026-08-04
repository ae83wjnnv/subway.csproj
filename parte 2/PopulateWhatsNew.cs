using System;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class PopulateWhatsNew : MonoBehaviour
{
	// Token: 0x06000534 RID: 1332 RVA: 0x00018FFC File Offset: 0x000171FC
	private void Start()
	{
		string[] newsForCurrentVersion = WhatsNew.getNewsForCurrentVersion();
		if (newsForCurrentVersion == null)
		{
			Debug.LogWarning("Unable to get WhatsNew data. Returning to MainMenu");
			UIScreenController.Instance.ShowMainMenu();
			return;
		}
		if (this.labelList.Length < newsForCurrentVersion.Length)
		{
			Debug.LogWarning("Whats news list for current update contains more than 5 updates. Cannot display all");
		}
		for (int i = 0; i < this.labelList.Length; i++)
		{
			if (i < newsForCurrentVersion.Length)
			{
				this.labelList[i].text = newsForCurrentVersion[i];
			}
			else
			{
				this.labelList[i].transform.parent.gameObject.SetActiveRecursively(false);
			}
		}
	}

	// Token: 0x0400044F RID: 1103
	public UILabel[] labelList;
}
