using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class InAppPurchaseHandler
{
	// Token: 0x06000088 RID: 136 RVA: 0x000025B6 File Offset: 0x000007B6
	public static bool isInitializedForPurchase()
	{
		return InAppPurchaseHandler.initializedForPurchase;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x000025BD File Offset: 0x000007BD
	public static bool isInitializedForProductRequest()
	{
		return InAppPurchaseHandler.initializedForProductRequest;
	}

	// Token: 0x0600008A RID: 138 RVA: 0x000025C4 File Offset: 0x000007C4
	public static string parseProductIdFromCallbackString(string transactionAndProductId)
	{
		return transactionAndProductId.Split(new char[] { ',' })[1];
	}

	// Token: 0x0600008B RID: 139
	[DllImport("__Internal")]
	private static extern bool purchaseHandlerCanMakePayments();

	// Token: 0x0600008C RID: 140 RVA: 0x000025D9 File Offset: 0x000007D9
	public static bool canMakePayments()
	{
		return true;
	}

	// Token: 0x0600008D RID: 141
	[DllImport("__Internal")]
	private static extern void purchaseHandlerInitPurchase(string gameobjectName, string onSuccessMethodName, string onFailureMethodName);

	// Token: 0x0600008E RID: 142 RVA: 0x000025DC File Offset: 0x000007DC
	public static void initPurchase(string gameobjectName, string onSuccessMethodName, string onFailureMethodName)
	{
		if (InAppPurchaseHandler.initializedForPurchase)
		{
			Debug.LogError("PurchaseHandler already initialized for purchase");
			return;
		}
		InAppPurchaseHandler.editorPurchaseGameObjectName = gameobjectName;
		InAppPurchaseHandler.editorOnPurchaseSuccessMethodName = onSuccessMethodName;
		InAppPurchaseHandler.editorOnPurchaseFailureMethodName = onFailureMethodName;
		InAppPurchaseHandler.initializedForPurchase = true;
	}

	// Token: 0x0600008F RID: 143
	[DllImport("__Internal")]
	private static extern void purchaseHandlerResetForPurchase();

	// Token: 0x06000090 RID: 144 RVA: 0x00002608 File Offset: 0x00000808
	public static void resetForPurchase()
	{
		InAppPurchaseHandler.editorPurchaseGameObjectName = null;
		InAppPurchaseHandler.editorOnPurchaseSuccessMethodName = null;
		InAppPurchaseHandler.editorOnPurchaseFailureMethodName = null;
		InAppPurchaseHandler.initializedForPurchase = false;
	}

	// Token: 0x06000091 RID: 145
	[DllImport("__Internal")]
	private static extern void purchaseHandlerInitProductRequest(string gameobjectName, string onSuccessMethodName, string onFailureMethodName);

	// Token: 0x06000092 RID: 146 RVA: 0x00002622 File Offset: 0x00000822
	public static void initProductRequest(string gameobjectName, string onSuccessMethodName, string onFailureMethodName)
	{
		if (InAppPurchaseHandler.initializedForProductRequest)
		{
			Debug.LogError("PurchaseHandler already initialized for purchase");
			return;
		}
		InAppPurchaseHandler.editorProductRequestGameObjectName = gameobjectName;
		InAppPurchaseHandler.editorOnProductRequestSuccessMethodName = onSuccessMethodName;
		InAppPurchaseHandler.editorOnProductRequestFailureMethodName = onFailureMethodName;
		InAppPurchaseHandler.initializedForProductRequest = true;
	}

	// Token: 0x06000093 RID: 147
	[DllImport("__Internal")]
	private static extern void purchaseHandlerResetForProductRequest();

	// Token: 0x06000094 RID: 148 RVA: 0x0000264E File Offset: 0x0000084E
	public static void resetForProductRequest()
	{
		InAppPurchaseHandler.editorProductRequestGameObjectName = null;
		InAppPurchaseHandler.editorOnProductRequestSuccessMethodName = null;
		InAppPurchaseHandler.editorOnProductRequestFailureMethodName = null;
		InAppPurchaseHandler.initializedForProductRequest = false;
	}

	// Token: 0x06000095 RID: 149
	[DllImport("__Internal")]
	private static extern void purchaseHandlerStartPurchase(string productIdentifier);

	// Token: 0x06000096 RID: 150 RVA: 0x00002668 File Offset: 0x00000868
	public static void startPurchase(string productIdentifier)
	{
		Debug.Log("PurchaseHandler.startPurchase(" + productIdentifier + ")");
		if (!InAppPurchaseHandler.initializedForPurchase)
		{
			Debug.LogError("PurchaseHandler not initialized for purchase");
			return;
		}
		GameObject.Find(InAppPurchaseHandler.editorPurchaseGameObjectName).SendMessage(InAppPurchaseHandler.editorOnPurchaseSuccessMethodName, "," + productIdentifier);
	}

	// Token: 0x06000097 RID: 151
	[DllImport("__Internal")]
	private static extern void purchaseHandlerCallbackHasBeenHandled(string transactionAndProductId);

	// Token: 0x06000098 RID: 152 RVA: 0x000026BB File Offset: 0x000008BB
	public static void callbackHasBeenHandled(string transactionAndProductIdentifier)
	{
		if (!InAppPurchaseHandler.initializedForPurchase)
		{
			Debug.LogError("PurchaseHandler not initialized for purchase");
		}
	}

	// Token: 0x06000099 RID: 153
	[DllImport("__Internal")]
	private static extern void purchaseHandlerQueryProducts(string productIds);

	// Token: 0x0600009A RID: 154 RVA: 0x000026D0 File Offset: 0x000008D0
	public static void queryProducts(string productIds)
	{
		if (!InAppPurchaseHandler.initializedForProductRequest)
		{
			Debug.LogError("PurchaseHandler not initialized for product request");
			return;
		}
		string[] array = productIds.Split(new char[] { ',' });
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			if (i > 0)
			{
				text += ";";
			}
			text = text + array[i] + ";0,99GBP";
		}
		GameObject.Find(InAppPurchaseHandler.editorProductRequestGameObjectName).SendMessage(InAppPurchaseHandler.editorOnProductRequestSuccessMethodName, text);
	}

	// Token: 0x04000036 RID: 54
	private static bool initializedForPurchase;

	// Token: 0x04000037 RID: 55
	private static bool initializedForProductRequest;

	// Token: 0x04000038 RID: 56
	private static string editorPurchaseGameObjectName;

	// Token: 0x04000039 RID: 57
	private static string editorProductRequestGameObjectName;

	// Token: 0x0400003A RID: 58
	private static string editorOnPurchaseSuccessMethodName;

	// Token: 0x0400003B RID: 59
	private static string editorOnPurchaseFailureMethodName;

	// Token: 0x0400003C RID: 60
	private static string editorOnProductRequestSuccessMethodName;

	// Token: 0x0400003D RID: 61
	private static string editorOnProductRequestFailureMethodName;
}
