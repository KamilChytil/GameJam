using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler,IPointerClickHandler
{
	Button button;
	TextMeshProUGUI[] texts;
	Image image;
	public Material activeMaterial;

	private void Start()
	{
		button = this.GetComponent<Button>();
		image = this.GetComponent<Image>();
	}
	private ButtonStatus lastButtonStatus = ButtonStatus.Normal;
	private bool isHighlightDesired = false;
	private bool isPressedDesired = false;

	void Update()
	{
		ButtonStatus desiredButtonStatus = ButtonStatus.Normal;
		if (!button.interactable)
			desiredButtonStatus = ButtonStatus.Disabled;
		else
		{
			if (isHighlightDesired)
				desiredButtonStatus = ButtonStatus.Highlighted;
			if (isPressedDesired)
				desiredButtonStatus = ButtonStatus.Pressed;
		}

		if (desiredButtonStatus != this.lastButtonStatus)
		{
			this.lastButtonStatus = desiredButtonStatus;
			switch (this.lastButtonStatus)
			{
				case ButtonStatus.Normal:
					SetHighlight(false);
					break;
				case ButtonStatus.Highlighted:
					SetHighlight(true);
					AudioManager.ui.PlayClip(UIAudioManager.AudioClips.Hover);
					break;
			}
		}
	}
	void SetHighlight(bool highlight)
	{
		if (highlight)
		{
			image.material = activeMaterial;
		}
		else
		{
			image.material = null;
		}

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isHighlightDesired = true;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		isPressedDesired = true;
		isHighlightDesired = false;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isPressedDesired = false;
		isHighlightDesired = false;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHighlightDesired = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		AudioManager.ui.PlayClip(UIAudioManager.AudioClips.Click);
	}

	public enum ButtonStatus
	{
		Normal,
		Disabled,
		Highlighted,
		Pressed
	}
}