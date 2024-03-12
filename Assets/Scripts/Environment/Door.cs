using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(ParadoxCauser))]
public class Door : MonoBehaviour, IResettable
{
	ParadoxCauser paradoxCauser;
	MeshRenderer meshRenderer;
	public bool open = false;
	// Start is called before the first frame update
	void Start()
	{
		paradoxCauser = GetComponent<ParadoxCauser>();
		meshRenderer = GetComponent<MeshRenderer>();
		ParadoxManager.resetList.Add(this);
	}

	// Update is called once per frame
	void Update()
	{

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (!FinishArea.recording && !other.GetComponent<PlayerMovement>().passive) return;
			if (!this.open)
			{
				this.paradoxCauser.CauseParadox("The agent tried to pass through a closed door.");
			}
		}
	}

	public void Open()
	{
		this.open = true;
		this.meshRenderer.enabled = false;
		this.paradoxCauser.ResolveParadox();
	}

	public void Reset()
	{
		this.open = false;
		this.meshRenderer.enabled = true;
	}
}
