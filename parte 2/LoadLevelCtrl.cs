using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200008A RID: 138
public class LoadLevelCtrl : MonoBehaviour
{
	// Token: 0x0600043A RID: 1082 RVA: 0x00012C27 File Offset: 0x00010E27
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.transform.gameObject);
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00012C39 File Offset: 0x00010E39
	private IEnumerator Start()
	{
		yield return SceneManager.LoadSceneAsync("Merge");
		Debug.Log("Merge Level Loaded " + Time.frameCount.ToString());
		yield return SceneManager.LoadSceneAsync("LazyLoad", LoadSceneMode.Additive);
		Debug.Log("Chunks Level Loaded " + Time.frameCount.ToString());
		yield break;
	}
}
