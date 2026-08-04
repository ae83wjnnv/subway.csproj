using System;
using UnityEngine;

// Token: 0x0200014D RID: 333
public class WhatsNew : MonoBehaviour
{
	// Token: 0x060009CE RID: 2510 RVA: 0x00036366 File Offset: 0x00034566
	private void Start()
	{
		if (this.ShouldDisplayWhatsNew())
		{
			UIScreenController.Instance.PushScreen(base.gameObject, "WhatsNew");
		}
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x00036385 File Offset: 0x00034585
	private bool ShouldDisplayWhatsNew()
	{
		if (PlayerPrefs.GetInt("theint", 0) > 5)
		{
			return false;
		}
		PlayerPrefs.SetInt("theint", PlayerPrefs.GetInt("theint", 0) + 1);
		return true;
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x000363B0 File Offset: 0x000345B0
	public static string[] getNewsForCurrentVersion()
	{
		TextAsset textAsset = Resources.Load("WhatsNew/1.0.1", typeof(TextAsset)) as TextAsset;
		if (textAsset == null)
		{
			Debug.LogWarning("NO UPDATE INFO AVALIBLE FOR VERSION: ");
			return null;
		}
		if (textAsset.text.Contains("\r"))
		{
			textAsset.text.Replace("\r", "\n");
		}
		string[] array = textAsset.text.Split(new char[] { '\n' });
		if (array.Length == 0)
		{
			return null;
		}
		return array;
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x00036432 File Offset: 0x00034632
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha9))
		{
			PlayerPrefs.DeleteAll();
			Debug.LogWarning("PlayerPrefs.DeleteAll();");
		}
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0003644C File Offset: 0x0003464C
	private void ExitGame()
	{
		Debug.Log("EXIT");
		Application.Quit();
	}

	// Token: 0x0400087A RID: 2170
	private const string PATH = "WhatsNew/";

	// Token: 0x0400087B RID: 2171
	private const string LAST_SEEN_BUNDLE_VERSION_KEY = "lastSeenBundleVersionKey";
}
