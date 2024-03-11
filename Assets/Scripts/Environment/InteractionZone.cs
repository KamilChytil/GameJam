using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionZone : MonoBehaviour, IResettable
{
	public bool highlight = false;
	public bool active = true;

	public string text = "interact";
	MeshRenderer meshRenderer;
	TextMeshProUGUI promptText;

	public bool availableForProtector = true;
	// Start is called before the first frame update
	void Start()
	{
		promptText = UIReference.i.interactPrompt.GetComponentsInChildren<TextMeshProUGUI>()[1];
		UIReference.i.interactPrompt.SetActive(false);
		meshRenderer = GetComponent<MeshRenderer>();
		this.highlight = false;
		ParadoxManager.resetList.Add(this);
	}

	// Update is called once per frame
	void Update()
	{
		if (highlight)
		{
			if (Input.GetKeyUp(KeyCode.F))
			{
				this.SendMessage("ButtonPush");

				this.highlight = false;
				UIReference.i.interactPrompt.SetActive(false);
				promptText.text = text;

				this.meshRenderer.enabled = false;
				this.active = false;
			}
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && this.active)
		{
			if (other.gameObject == ParadoxManager.i.protectorInstance && !availableForProtector) return;
			this.highlight = true;
			UIReference.i.interactPrompt.SetActive(true);
			promptText.text = text;
			
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			this.highlight = false;
			UIReference.i.interactPrompt.SetActive(false);
			promptText.text = text;
		}
	}
	public void Reset()
	{
		UIReference.i.interactPrompt.SetActive(false);
		this.meshRenderer.enabled = true;
		this.highlight = false;
		this.active = true;
	}
}
