using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIReference : MonoBehaviour
{
	public static UIReference i;

	public GameObject winUI;
	public GameObject loseUI;
	public TextMeshProUGUI paradoxCounter;
	public RawImage paradoxIcon;
	public GameObject interactPrompt;
	public TextMeshProUGUI timerText;
	public TextMeshProUGUI nextParadoxCounter;
	public TextMeshProUGUI nextParadoxDescription;
	private void Start()
	{
		i = this;
	}
}
