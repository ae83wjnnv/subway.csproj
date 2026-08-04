using System;
using UnityEngine;

// Token: 0x02000094 RID: 148
[AddComponentMenu("GUI/Model Trigger")]
public class ModelTrigger : MonoBehaviour
{
	// Token: 0x040003C0 RID: 960
	public ModelTrigger.ModelPosition modelPosition;

	// Token: 0x020001BB RID: 443
	public enum ModelPosition
	{
		// Token: 0x04000A79 RID: 2681
		CharacterScreen,
		// Token: 0x04000A7A RID: 2682
		LoseScreen
	}
}
