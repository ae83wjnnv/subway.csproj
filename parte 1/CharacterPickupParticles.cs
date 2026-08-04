using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class CharacterPickupParticles : MonoBehaviour
{
	// Token: 0x06000209 RID: 521 RVA: 0x00008F4C File Offset: 0x0000714C
	public void Awake()
	{
		this.lastCoinPosition = base.transform.position.z;
		this.offset = base.transform.position - this.master.position;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00008F88 File Offset: 0x00007188
	public void PickedUpCoin(Pickup pickup)
	{
		if (80f < pickup.transform.position.y)
		{
			this.coinStairway = 0;
			this.CoinPickup.maxPitch = Mathf.Pow(2f, (float)this.flyWay / 48f);
			this.CoinPickup.minPitch = Mathf.Pow(2f, (float)this.flyWay / 48f);
			this.flyWay++;
		}
		else if (pickup.transform.position.y < 0.1f || (8.795f < pickup.transform.position.y && pickup.transform.position.y < 8.805f) || (9.95f < pickup.transform.position.y && pickup.transform.position.y < 10.05f) || (28.95f < pickup.transform.position.y && pickup.transform.position.y < 29.05f) || (34.95f < pickup.transform.position.y && pickup.transform.position.y < 35.05f))
		{
			this.flyWay = 0;
			this.coinStairway = 0;
			this.CoinPickup.maxPitch = Mathf.Pow(2f, (float)this.pentatonicScale[this.coinStairway % this.pentatonicScale.Length] / 12f) * 0.5f;
			this.CoinPickup.minPitch = Mathf.Pow(2f, (float)this.pentatonicScale[this.coinStairway % this.pentatonicScale.Length] / 12f) * 0.5f;
		}
		else
		{
			this.flyWay = 0;
			if (this.coinStairway < this.pentatonicScale.Length - 1)
			{
				this.coinStairway++;
			}
			this.CoinPickup.maxPitch = Mathf.Pow(2f, (float)this.pentatonicScale[this.coinStairway % this.pentatonicScale.Length] / 12f) * 0.5f;
			this.CoinPickup.minPitch = Mathf.Pow(2f, (float)this.pentatonicScale[this.coinStairway % this.pentatonicScale.Length] / 12f) * 0.5f;
		}
		So.Instance.playSound(this.CoinPickup);
		this.DoCoinEFX();
		this.lastCoinPosition = pickup.transform.position.y;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000922C File Offset: 0x0000742C
	private void DoCoinEFX()
	{
		float num = Random.Range(0f, 360f);
		this.CoinEFX.transform.Rotate(0f, 0f, num);
		this.CoinEFX.GetComponent<Animation>().Stop("pickup");
		this.CoinEFX.GetComponent<Animation>().Play("pickup");
		base.StartCoroutine(this.AnimateAlpha(this.CoinEFX, this.CoinEFX.GetComponent<Animation>()["pickup"].length));
	}

	// Token: 0x0600020C RID: 524 RVA: 0x000092BC File Offset: 0x000074BC
	public void PickedUpPowerUp()
	{
		So.Instance.playSound(this.PowerUpPickup);
		this.PickedUpDefaultPowerUp();
	}

	// Token: 0x0600020D RID: 525 RVA: 0x000092D8 File Offset: 0x000074D8
	public void PickedUpDefaultPowerUp()
	{
		this.DoCoinEFX();
		float num = Random.Range(0f, 360f);
		this.PowerUpEFX.transform.Rotate(0f, 0f, num);
		this.PowerUpEFX.GetComponent<Animation>().Stop("pickup");
		this.PowerUpEFX.GetComponent<Animation>().Play("pickup");
		base.StartCoroutine(this.AnimateAlpha(this.PowerUpEFX, this.PowerUpEFX.GetComponent<Animation>()["pickup"].length));
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00009370 File Offset: 0x00007570
	private IEnumerator AnimateAlpha(GameObject efx, float time)
	{
		return pTween.To(time, delegate(float t)
		{
			this.transform.position = this.master.position + this.offset;
			efx.GetComponent<Renderer>().material.SetColor("_MainColor", Color.Lerp(Color.white, Color.black, t));
		});
	}

	// Token: 0x0600020F RID: 527 RVA: 0x000093A3 File Offset: 0x000075A3
	private IEnumerator TimeScaleTest(float time)
	{
		Time.timeScale = 0.5f;
		yield return new WaitForSeconds(time);
		Time.timeScale = 1f;
		yield break;
	}

	// Token: 0x0400014B RID: 331
	public GameObject CoinEFX;

	// Token: 0x0400014C RID: 332
	public GameObject PowerUpEFX;

	// Token: 0x0400014D RID: 333
	public Transform master;

	// Token: 0x0400014E RID: 334
	private Vector3 offset;

	// Token: 0x0400014F RID: 335
	public AudioClipInfo CoinPickup;

	// Token: 0x04000150 RID: 336
	public AudioClipInfo PowerUpPickup;

	// Token: 0x04000151 RID: 337
	public float CoinDistanceForStairway;

	// Token: 0x04000152 RID: 338
	private float lastCoinPosition;

	// Token: 0x04000153 RID: 339
	private int coinStairway;

	// Token: 0x04000154 RID: 340
	private int flyWay;

	// Token: 0x04000155 RID: 341
	private int[] pentatonicScale = new int[]
	{
		12, 13, 14, 15, 16, 17, 18, 19, 20, 21,
		22, 23, 24, 25, 26, 27, 28
	};
}
