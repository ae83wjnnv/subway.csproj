using System;
using UnityEngine;

// Token: 0x0200008E RID: 142
public class MessageCenter : MonoBehaviour
{
	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600044B RID: 1099 RVA: 0x00012F5C File Offset: 0x0001115C
	public static MessageCenter Instance
	{
		get
		{
			if (MessageCenter._instance == null)
			{
				Debug.Log("Instance requested before being instantiated");
				MessageCenter._instance = Object.FindObjectOfType(typeof(MessageCenter)) as MessageCenter;
				if (MessageCenter._instance == null)
				{
					Debug.LogError("MessageCenter not found in the scene.");
				}
			}
			return MessageCenter._instance;
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x0600044C RID: 1100 RVA: 0x00012FB5 File Offset: 0x000111B5
	public static bool IsInstanced
	{
		get
		{
			return MessageCenter._instance != null;
		}
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00012FC2 File Offset: 0x000111C2
	private void Awake()
	{
		MessageCenter._instance = this;
	}

	// Token: 0x040003A8 RID: 936
	private static MessageCenter _instance;
}
