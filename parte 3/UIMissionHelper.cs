using System;
using UnityEngine;

// Token: 0x0200011D RID: 285
public class UIMissionHelper : MonoBehaviour
{
	// Token: 0x06000843 RID: 2115 RVA: 0x0002B018 File Offset: 0x00029218
	private void Update()
	{
		if (this.hasDestroyedGameObjects)
		{
			return;
		}
		if (!Missions.Instance.HasMoreMissions())
		{
			this.MissionLabel2.text = "No missions available";
			Object.Destroy(this.MissionCheckBox1.gameObject);
			Object.Destroy(this.MissionCheckBox3.gameObject);
			foreach (object obj in this.MissionCheckBox2.transform)
			{
				Transform transform = (Transform)obj;
				if (transform.gameObject != this.MissionLabel2.gameObject)
				{
					Object.Destroy(transform.gameObject);
				}
			}
			Object.Destroy(this.MissionCheckBox2);
			this.hasDestroyedGameObjects = true;
			return;
		}
		this._currentMissions = Missions.Instance.GetMissionInfo();
		if (this.hasCached)
		{
			bool flag = true;
			for (int i = 0; i < 3; i++)
			{
				if (this._cachedMissionProgressions[i] != this._currentMissions[i].progress)
				{
					flag = false;
				}
			}
			if (this._cachedMissionSet != Missions.Instance.currentMissionSet)
			{
				flag = false;
			}
			if (flag)
			{
				return;
			}
		}
		this.MissionLabel2.text = string.Format(this._currentMissions[1].template.description, this._currentMissions[1].mission.goal, this._currentMissions[1].mission.goal - this._currentMissions[1].progress);
		this.MissionLabel3.text = string.Format(this._currentMissions[2].template.description, this._currentMissions[2].mission.goal, this._currentMissions[2].mission.goal - this._currentMissions[2].progress);
		this.MissionCheckBox1.isChecked = this._currentMissions[0].complete;
		this.MissionCheckBox2.isChecked = this._currentMissions[1].complete;
		this.MissionCheckBox3.isChecked = this._currentMissions[2].complete;
		this.hasCached = true;
		this.LabelAndNumberUpdate(0, this.MissionLabel1, this.MissionNumber1);
		this.LabelAndNumberUpdate(1, this.MissionLabel2, this.MissionNumber2);
		this.LabelAndNumberUpdate(2, this.MissionLabel3, this.MissionNumber3);
		this._cachedMissionProgressions[0] = this._currentMissions[0].progress;
		this._cachedMissionProgressions[1] = this._currentMissions[1].progress;
		this._cachedMissionProgressions[2] = this._currentMissions[2].progress;
		this._cachedMissionSet = Missions.Instance.currentMissionSet;
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0002B2E0 File Offset: 0x000294E0
	private void LabelAndNumberUpdate(int missionArrayNr, UILabel sendMissionLabel, UILabel sendMissionNumber)
	{
		if (this._currentMissions[missionArrayNr].complete)
		{
			string text = ((this._currentMissions[missionArrayNr].mission.goal != 1) ? this._currentMissions[missionArrayNr].template.ultraShortDescription : this._currentMissions[missionArrayNr].template.ultraShortDescriptionSingle);
			sendMissionLabel.text = string.Format(text, this._currentMissions[missionArrayNr].mission.goal);
			sendMissionNumber.text = string.Empty;
			return;
		}
		string text2 = ((this._currentMissions[missionArrayNr].mission.goal != 1) ? this._currentMissions[missionArrayNr].template.description : this._currentMissions[missionArrayNr].template.descriptionSingle);
		if (this._currentMissions[missionArrayNr].mission.type == Missions.MissionType.TimeDeath)
		{
			if (Game.Instance.isPaused)
			{
				sendMissionLabel.text = string.Format(text2, this._currentMissions[missionArrayNr].mission.goal, (int)Game.Instance.GetDuration());
				this.hasCached = false;
			}
			else
			{
				sendMissionLabel.text = string.Format(text2, this._currentMissions[missionArrayNr].mission.goal, 0);
			}
		}
		else
		{
			sendMissionLabel.text = string.Format(text2, this._currentMissions[missionArrayNr].mission.goal, this._currentMissions[missionArrayNr].mission.goal - this._currentMissions[missionArrayNr].progress);
		}
		sendMissionNumber.text = (missionArrayNr + 1).ToString() + string.Empty;
	}

	// Token: 0x04000733 RID: 1843
	public UICheckbox MissionCheckBox1;

	// Token: 0x04000734 RID: 1844
	public UICheckbox MissionCheckBox2;

	// Token: 0x04000735 RID: 1845
	public UICheckbox MissionCheckBox3;

	// Token: 0x04000736 RID: 1846
	public UILabel MissionLabel1;

	// Token: 0x04000737 RID: 1847
	public UILabel MissionLabel2;

	// Token: 0x04000738 RID: 1848
	public UILabel MissionLabel3;

	// Token: 0x04000739 RID: 1849
	public UILabel MissionNumber1;

	// Token: 0x0400073A RID: 1850
	public UILabel MissionNumber2;

	// Token: 0x0400073B RID: 1851
	public UILabel MissionNumber3;

	// Token: 0x0400073C RID: 1852
	private int[] _cachedMissionProgressions = new int[3];

	// Token: 0x0400073D RID: 1853
	private int _cachedMissionSet;

	// Token: 0x0400073E RID: 1854
	private bool hasCached;

	// Token: 0x0400073F RID: 1855
	private bool hasDestroyedGameObjects;

	// Token: 0x04000740 RID: 1856
	private MissionInfo[] _currentMissions;

	// Token: 0x04000741 RID: 1857
	private bool timeDeathMission;
}
