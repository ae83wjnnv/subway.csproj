using System;

// Token: 0x020000EE RID: 238
public class UIButtonGame : UIBasicButton
{
	// Token: 0x060006C1 RID: 1729 RVA: 0x00021642 File Offset: 0x0001F842
	protected override void Send()
	{
		if (this.messageType == UIButtonGame.GameMessage.StartNewRun)
		{
			if (Game.Instance != null)
			{
				Game.Instance.StartNewRun();
				return;
			}
		}
		else
		{
			UIButtonGame.GameMessage gameMessage = this.messageType;
		}
	}

	// Token: 0x040005BD RID: 1469
	public UIButtonGame.GameMessage messageType;

	// Token: 0x020001F9 RID: 505
	public enum GameMessage
	{
		// Token: 0x04000BB4 RID: 2996
		_notSet,
		// Token: 0x04000BB5 RID: 2997
		StartNewRun,
		// Token: 0x04000BB6 RID: 2998
		RestartFromPause
	}
}
