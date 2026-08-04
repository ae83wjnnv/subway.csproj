using System;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class PickupDefault : MonoBehaviour
{
	// Token: 0x060004E6 RID: 1254 RVA: 0x00017AC0 File Offset: 0x00015CC0
	private void Awake()
	{
		Pickup component = base.GetComponent<Pickup>();
		component.OnPickup = (Pickup.OnPickupDelegate)Delegate.Combine(component.OnPickup, new Pickup.OnPickupDelegate(this.OnPickup));
		TrackObject trackObject = base.GetComponent<TrackObject>() ?? base.gameObject.AddComponent<TrackObject>();
		trackObject.OnActivate = (TrackObject.OnActivateDelegate)Delegate.Combine(trackObject.OnActivate, new TrackObject.OnActivateDelegate(this.OnActivate));
		trackObject.OnDeactivate = (TrackObject.OnDeactivateDelegate)Delegate.Combine(trackObject.OnDeactivate, new TrackObject.OnDeactivateDelegate(this.OnDeactivate));
		this.parentCollider = this.FindParentCollider(base.transform);
		if (this.parentCollider == null)
		{
			Debug.Log("Error: No collider for PickupDefault.");
		}
		this.SetVisible(false);
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00017B7D File Offset: 0x00015D7D
	private Collider FindParentCollider(Transform current)
	{
		if (current.GetComponent<Collider>() != null)
		{
			return current.GetComponent<Collider>();
		}
		if (current.parent != null)
		{
			return this.FindParentCollider(current.parent);
		}
		return null;
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x00017BB0 File Offset: 0x00015DB0
	private void SetVisible(bool visible)
	{
		this.meshRenderer.enabled = visible;
		if (this.glow != null)
		{
			this.glow.SetVisible(visible);
		}
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00017BD8 File Offset: 0x00015DD8
	private void OnActivate()
	{
		this.parentCollider.enabled = true;
		this.SetVisible(true);
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00017BED File Offset: 0x00015DED
	private void OnDeactivate()
	{
		this.SetVisible(false);
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x00017BF6 File Offset: 0x00015DF6
	private void OnPickup(CharacterPickupParticles particles)
	{
		if (base.gameObject != null)
		{
			this.parentCollider.enabled = false;
		}
		this.SetVisible(false);
		if (this.ShouldSpawnParticles)
		{
			particles.PickedUpDefaultPowerUp();
		}
	}

	// Token: 0x04000422 RID: 1058
	public MeshRenderer meshRenderer;

	// Token: 0x04000423 RID: 1059
	public Glow glow;

	// Token: 0x04000424 RID: 1060
	public bool ShouldSpawnParticles;

	// Token: 0x04000425 RID: 1061
	private Collider parentCollider;
}
