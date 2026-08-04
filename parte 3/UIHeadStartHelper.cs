using System;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class UIHeadStartHelper : MonoBehaviour
{
	// Token: 0x060007E3 RID: 2019 RVA: 0x00028EC8 File Offset: 0x000270C8
	private void Start()
	{
		if (!this.hasInited)
		{
			this.InitHelper();
		}
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x00028ED8 File Offset: 0x000270D8
	private void InitHelper()
	{
		this.headStart1.GetComponent<Collider>().enabled = false;
		this.headStart2.GetComponent<Collider>().enabled = false;
		this.headStart1.transform.localPosition = this.hs1PositionOff;
		this.headStart2.transform.localPosition = this.hs2PositionOff;
		PlayerInfo instance = PlayerInfo.Instance;
		instance.onPowerupAmountChanged = (Action)Delegate.Combine(instance.onPowerupAmountChanged, new Action(this.UpdateHeadstartLabels));
		this.UpdateHeadstartLabels();
		this.hasInited = true;
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00028F66 File Offset: 0x00027166
	private void OnDestroy()
	{
		PlayerInfo instance = PlayerInfo.Instance;
		instance.onPowerupAmountChanged = (Action)Delegate.Remove(instance.onPowerupAmountChanged, new Action(this.UpdateHeadstartLabels));
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00028F90 File Offset: 0x00027190
	private void UpdateHeadstartLabels()
	{
		this.hs1AmountLabel.text = string.Empty + PlayerInfo.Instance.GetUpgradeAmount(PowerupType.headstart500).ToString();
		this.hs2AmountLabel.text = string.Empty + PlayerInfo.Instance.GetUpgradeAmount(PowerupType.headstart2000).ToString();
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00028FF0 File Offset: 0x000271F0
	public void ShowHeadStart()
	{
		if (!this.hasInited)
		{
			this.InitHelper();
		}
		if (PlayerInfo.Instance.GetUpgradeAmount(PowerupType.headstart500) > 0)
		{
			SpringPosition.Begin(this.headStart1, this.hs1PositionOn, 10f);
			this.headStart1.GetComponent<Collider>().enabled = true;
		}
		if (PlayerInfo.Instance.GetUpgradeAmount(PowerupType.headstart2000) > 0)
		{
			SpringPosition.Begin(this.headStart2, this.hs2PositionOn, 10f);
			this.headStart2.GetComponent<Collider>().enabled = true;
		}
		base.Invoke("HideHeadStart", 5f);
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00029087 File Offset: 0x00027287
	public void HideHeadStart()
	{
		this.HideHeadStart(false);
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x00029090 File Offset: 0x00027290
	public void HideHeadStart(bool instant)
	{
		if (!this.hasInited)
		{
			this.InitHelper();
		}
		if (instant)
		{
			this.headStart1.transform.position = this.hs1PositionOff;
			this.headStart2.transform.position = this.hs2PositionOff;
		}
		else
		{
			SpringPosition.Begin(this.headStart1, this.hs1PositionOff, 10f);
			SpringPosition.Begin(this.headStart2, this.hs2PositionOff, 10f);
		}
		this.headStart1.GetComponent<Collider>().enabled = false;
		this.headStart2.GetComponent<Collider>().enabled = false;
	}

	// Token: 0x040006DC RID: 1756
	public GameObject headStart1;

	// Token: 0x040006DD RID: 1757
	public GameObject headStart2;

	// Token: 0x040006DE RID: 1758
	public UILabel hs1AmountLabel;

	// Token: 0x040006DF RID: 1759
	public UILabel hs2AmountLabel;

	// Token: 0x040006E0 RID: 1760
	private SpringPosition hs1Spring;

	// Token: 0x040006E1 RID: 1761
	private SpringPosition hs2Spring;

	// Token: 0x040006E2 RID: 1762
	private Vector3 hs1PositionOff = new Vector3(-100f, 160f, 0f);

	// Token: 0x040006E3 RID: 1763
	private Vector3 hs2PositionOff = new Vector3(-100f, 60f, 0f);

	// Token: 0x040006E4 RID: 1764
	private Vector3 hs1PositionOn = new Vector3(50f, 160f, 0f);

	// Token: 0x040006E5 RID: 1765
	private Vector3 hs2PositionOn = new Vector3(50f, 60f, 0f);

	// Token: 0x040006E6 RID: 1766
	private bool hasInited;
}
