using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000124 RID: 292
public class UIPowerupHandler : MonoBehaviour
{
	// Token: 0x06000894 RID: 2196 RVA: 0x0002DBA0 File Offset: 0x0002BDA0
	private void Update()
	{
		List<ActivePowerup> activePowerups = GameStats.Instance.GetActivePowerups();
		int i = 0;
		for (int j = activePowerups.Count - 1; j >= 0; j--)
		{
			if (this._powerupSlots[i] == null)
			{
				GameObject gameObject = NGUITools.AddChild(base.gameObject, this.PowerupPrefab);
				gameObject.transform.localPosition = this.slotPositions[i];
				UIPowerupHelper component = gameObject.GetComponent<UIPowerupHelper>();
				component.SetPowerup(activePowerups[j]);
				this._powerupSlots[i] = component;
			}
			else
			{
				this._powerupSlots[i].SetPowerup(activePowerups[j]);
			}
			i++;
		}
		while (i < 4)
		{
			if (this._powerupSlots[i] != null)
			{
				Object.Destroy(this._powerupSlots[i].gameObject);
			}
			i++;
		}
	}

	// Token: 0x0400078F RID: 1935
	public GameObject PowerupPrefab;

	// Token: 0x04000790 RID: 1936
	private Vector3[] slotPositions = new Vector3[]
	{
		new Vector3(-135f, 10f, 0f),
		new Vector3(20f, 10f, 0f),
		new Vector3(-135f, 50f, 0f),
		new Vector3(20f, 50f, 0f)
	};

	// Token: 0x04000791 RID: 1937
	private UIPowerupHelper[] _powerupSlots = new UIPowerupHelper[4];
}
