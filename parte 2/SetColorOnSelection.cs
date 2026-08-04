using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Examples/Set Color on Selection")]
[ExecuteInEditMode]
public class SetColorOnSelection : MonoBehaviour
{
	// Token: 0x06000579 RID: 1401 RVA: 0x0001B924 File Offset: 0x00019B24
	private void OnSelectionChange(string val)
	{
		if (this.mWidget == null)
		{
			this.mWidget = base.GetComponent<UIWidget>();
		}
		if (val != null)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(val);
			if (num <= 2743015548U)
			{
				if (num != 382078856U)
				{
					if (num != 1827351814U)
					{
						if (num != 2743015548U)
						{
							return;
						}
						if (!(val == "Red"))
						{
							return;
						}
						this.mWidget.color = Color.red;
						return;
					}
					else
					{
						if (!(val == "White"))
						{
							return;
						}
						this.mWidget.color = Color.white;
						return;
					}
				}
				else
				{
					if (!(val == "Magenta"))
					{
						return;
					}
					this.mWidget.color = Color.magenta;
				}
			}
			else if (num <= 3381604954U)
			{
				if (num != 2840840028U)
				{
					if (num != 3381604954U)
					{
						return;
					}
					if (!(val == "Cyan"))
					{
						return;
					}
					this.mWidget.color = Color.cyan;
					return;
				}
				else
				{
					if (!(val == "Green"))
					{
						return;
					}
					this.mWidget.color = Color.green;
					return;
				}
			}
			else if (num != 3654151273U)
			{
				if (num != 3923582957U)
				{
					return;
				}
				if (!(val == "Blue"))
				{
					return;
				}
				this.mWidget.color = Color.blue;
				return;
			}
			else
			{
				if (!(val == "Yellow"))
				{
					return;
				}
				this.mWidget.color = Color.yellow;
				return;
			}
		}
	}

	// Token: 0x040004BB RID: 1211
	private UIWidget mWidget;
}
