using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCone : MonoBehaviour
{
	public Transform visiblePlayer = null;

	public bool directSight = false;
	// Start is called before the first frame update
	void Start()
	{
		visiblePlayer = null;
	}

	// Update is called once per frame
	void Update()
	{
		if (visiblePlayer != null)
		{
			Vector3 offset = new Vector3(0, 0.2f, 0);
			if (Physics.Raycast(transform.position + offset, visiblePlayer.position - transform.position + offset, out RaycastHit hit, 10, LayerMask.GetMask("Walls") + LayerMask.GetMask("Player"), QueryTriggerInteraction.Collide))
			{
				if (hit.transform == visiblePlayer)
				{
					directSight = true;
					return;
				}
			}
		}
		directSight = false;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && other.transform != transform.parent)
		{
			visiblePlayer = other.transform;
		}
	}
	public void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player") && other.transform != transform.parent)
		{
			visiblePlayer = null;
		}
	}
}
