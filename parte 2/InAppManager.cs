using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class InAppManager : MonoBehaviour
{
	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060003DE RID: 990 RVA: 0x00011605 File Offset: 0x0000F805
	public static InAppManager Instance
	{
		get
		{
			InAppManager inAppManager;
			if ((inAppManager = InAppManager._instance) == null)
			{
				inAppManager = (InAppManager._instance = Object.FindObjectOfType(typeof(InAppManager)) as InAppManager);
			}
			return inAppManager;
		}
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0001162A File Offset: 0x0000F82A
	public static bool IsInstanced()
	{
		return InAppManager._instance != null;
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00011637 File Offset: 0x0000F837
	private void Awake()
	{
		InAppManager._instance = this;
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0001163F File Offset: 0x0000F83F
	private void Start()
	{
		this.inAppData = new InAppData();
		this.RetryProductRequest();
		if (!InAppPurchaseHandler.isInitializedForPurchase())
		{
			InAppPurchaseHandler.initPurchase(base.gameObject.name, "PurchaseSuccess", "PurchaseFailure");
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00011673 File Offset: 0x0000F873
	public void RetryProductRequest()
	{
		if (!this.productRequestSucceeded)
		{
			base.StartCoroutine(this.QueryInAppPurchases());
			return;
		}
		Debug.Log("Retried product request, but already succeeded");
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00011695 File Offset: 0x0000F895
	private IEnumerator QueryInAppPurchases()
	{
		if (this.WillQueryInAppPurchases())
		{
			if (!InAppPurchaseHandler.isInitializedForProductRequest())
			{
				InAppPurchaseHandler.initProductRequest(base.gameObject.name, "ProductRequestSuccess", "ProductRequestFailure");
			}
			InAppPurchaseHandler.queryProducts(this.inAppData.CommaSeparatedProductIds);
		}
		yield break;
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x000116A4 File Offset: 0x0000F8A4
	public void BuyInAppNow(GameObject sender)
	{
		string key = sender.GetComponent<CoinButtonHelper>().Key;
		this.StartPurchase(key);
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x000116C4 File Offset: 0x0000F8C4
	public void BuyFromPopup(string purchaseId)
	{
		this.StartPurchase(purchaseId);
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x000116CD File Offset: 0x0000F8CD
	private void StartPurchase(string inAppPurchaseId)
	{
		UIScreenController.Instance.ShowInAppPurchaseOverlay();
		InAppPurchaseHandler.startPurchase(inAppPurchaseId);
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x000116E0 File Offset: 0x0000F8E0
	public void ProductRequestSuccess(string validProductIdsAndPrices)
	{
		string[] array = validProductIdsAndPrices.Split(new char[] { ";"[0] });
		int num = array.Length / 2;
		string[] array2 = new string[num];
		string[] array3 = new string[num];
		for (int i = 0; i < num; i++)
		{
			array2[i] = array[i * 2];
			array3[i] = array[i * 2 + 1];
		}
		for (int j = 0; j < num; j++)
		{
			InAppData.inAppData[array2[j]].price = array3[j];
			InAppData.inAppData[array2[j]].validInApp = true;
		}
		Action action = this.onProductRequestSuccess;
		if (action != null)
		{
			action();
		}
		this._inAppPurchaseState = InAppManager.InAppPurchaseState.Complete;
		this.productRequestSucceeded = true;
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x0001179E File Offset: 0x0000F99E
	public void ProductRequestFailure()
	{
		Debug.Log("Inapp product request failure!");
		this._inAppPurchaseState = InAppManager.InAppPurchaseState.Failed;
		this.productRequestSucceeded = false;
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x000117B8 File Offset: 0x0000F9B8
	public void PurchaseSuccess(string transactionAndProductId)
	{
		string text = InAppPurchaseHandler.parseProductIdFromCallbackString(transactionAndProductId);
		PlayerInfo instance = PlayerInfo.Instance;
		int inAppPurchaseCount = instance.inAppPurchaseCount;
		instance.inAppPurchaseCount = inAppPurchaseCount + 1;
		PlayerInfo.Instance.amountOfCoins += InAppData.inAppData[text].amountOfCoins;
		PlayerInfo.Instance.Save();
		Action action = this.onPurchaseSuccess;
		if (action != null)
		{
			action();
		}
		InAppPurchaseHandler.callbackHasBeenHandled(transactionAndProductId);
		UIScreenController.Instance.HideInAppPurchaseOverlay();
		Flurry.LogEventWithAParameter("InApp purchase completed", "Id", text);
		if (text == InAppData.inAppTier1)
		{
			Flurry.LogEventWithAParameter("InApp Coin Pack 1 purchased", "Mission Set", PlayerInfo.Instance.currentMissionSet.ToString());
			return;
		}
		if (text == InAppData.inAppTier2)
		{
			Flurry.LogEventWithAParameter("InApp Coin Pack 2 purchased", "Mission Set", PlayerInfo.Instance.currentMissionSet.ToString());
			return;
		}
		if (text == InAppData.inAppTier3)
		{
			Flurry.LogEventWithAParameter("InApp Coin Pack 3 purchased", "Mission Set", PlayerInfo.Instance.currentMissionSet.ToString());
		}
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x000118C8 File Offset: 0x0000FAC8
	public void PurchaseFailure(string transactionAndProductId)
	{
		InAppPurchaseHandler.parseProductIdFromCallbackString(transactionAndProductId);
		Action action = this.onPurchaseFailure;
		if (action != null)
		{
			action();
		}
		DeviceUtility.showNativePopup("Purchase Failed", "An error occurred while handling your purchase. Please try again later.", "Ok");
		InAppPurchaseHandler.callbackHasBeenHandled(transactionAndProductId);
		UIScreenController.Instance.HideInAppPurchaseOverlay();
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x00011910 File Offset: 0x0000FB10
	private void OnDestroy()
	{
		InAppPurchaseHandler.resetForPurchase();
		InAppPurchaseHandler.resetForProductRequest();
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0001191C File Offset: 0x0000FB1C
	public bool WillQueryInAppPurchases()
	{
		if (this._inAppPurchaseState == InAppManager.InAppPurchaseState.NotStarted || this._inAppPurchaseState == InAppManager.InAppPurchaseState.Failed)
		{
			this._inAppPurchaseState = InAppManager.InAppPurchaseState.Started;
			return true;
		}
		return false;
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00011939 File Offset: 0x0000FB39
	public bool HasQueriedInAppPurchases()
	{
		return this._inAppPurchaseState == InAppManager.InAppPurchaseState.Complete;
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00011944 File Offset: 0x0000FB44
	public bool IsQueryingInAppPurchases()
	{
		return this._inAppPurchaseState == InAppManager.InAppPurchaseState.Started;
	}

	// Token: 0x04000332 RID: 818
	private InAppData inAppData;

	// Token: 0x04000333 RID: 819
	private static InAppManager _instance;

	// Token: 0x04000334 RID: 820
	private InAppManager.InAppPurchaseState _inAppPurchaseState;

	// Token: 0x04000335 RID: 821
	[HideInInspector]
	public bool productRequestSucceeded;

	// Token: 0x04000336 RID: 822
	public Action onProductRequestSuccess;

	// Token: 0x04000337 RID: 823
	public Action onPurchaseSuccess;

	// Token: 0x04000338 RID: 824
	public Action onPurchaseFailure;

	// Token: 0x020001AB RID: 427
	private enum InAppPurchaseState
	{
		// Token: 0x040009E1 RID: 2529
		NotStarted,
		// Token: 0x040009E2 RID: 2530
		Started,
		// Token: 0x040009E3 RID: 2531
		Failed,
		// Token: 0x040009E4 RID: 2532
		Complete
	}
}
