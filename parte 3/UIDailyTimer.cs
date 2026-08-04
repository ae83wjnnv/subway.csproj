using System;
using UnityEngine;

// Token: 0x020000FF RID: 255
public class UIDailyTimer : MonoBehaviour
{
	// Token: 0x0600073B RID: 1851 RVA: 0x00024087 File Offset: 0x00022287
	private void Start()
	{
		this._timerLabel = base.GetComponent<UILabel>();
		DailyWord.Instance.ForceSync();
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x000240A0 File Offset: 0x000222A0
	private void Update()
	{
		TimeSpan timeSpan = PlayerInfo.Instance.dailyWordExpireTime - DateTime.UtcNow;
		if (timeSpan.Ticks > 0L)
		{
			this._timerLabel.text = string.Format("Time: {0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			return;
		}
		this._timerLabel.text = "Time: 00:00:00";
	}

	// Token: 0x04000646 RID: 1606
	private UILabel _timerLabel;
}
