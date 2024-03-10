using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonArea : MonoBehaviour, IResettable
{
	public bool highlight = false;
	public bool active = true;
	MeshRenderer meshRenderer;
	// Start is called before the first frame update
	void Start()
	{
		meshRenderer = GetComponent<MeshRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (highlight)
		{
			if (Input.GetKeyUp(KeyCode.F))
			{
				this.SendMessage("ButtonPush");
				this.meshRenderer.enabled = false;
				this.active = false;
			}
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && this.active)
		{
			this.highlight = true;
			ParadoxManager.i.interactPrompt.SetActive(true);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.highlight = false;
			ParadoxManager.i.interactPrompt.SetActive(false);
		}
	}
	public void Reset()
	{
		this.meshRenderer.enabled = true;
		this.highlight = false;
		this.active = true;
	}
}
