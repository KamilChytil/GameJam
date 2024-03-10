using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ParadoxManager : MonoBehaviour
{
	public static ParadoxManager i;
	public GameObject paradoxPrefab;
	public TextMeshProUGUI paradoxCounter;
	public RawImage paradoxIcon;
	public GameObject interactPrompt;
	public GameObject player;
	public GameObject protector;
	public GameObject winUI;
	public GameObject loseUI;

	public static bool intelDone = false;
	public static bool timeRiftDone = false;


	public Material fullscreenMaterial;
	public static float screenFlash = 0;

	public static int paradoxAmount = 0;


	private void Awake()
	{
		i = this;
	}
	// Start is called before the first frame update
	void Start()
	{
		Paradox.nextOrder = 0;
		paradoxAmount = 0;
		screenFlash = 0;
		i = this;
		
		ParadoxManager.resetList.Clear();
		ResetAll();
	}

	// Update is called once per frame
	void Update()
	{
		screenFlash = Mathf.Lerp(screenFlash, 0, Time.deltaTime * 5);
		fullscreenMaterial.SetFloat("_ScreenFlash", screenFlash);
		UpdateParadoxCounter();
	}

	public static void GameOver(string reason = "Bad luck.")
	{
		Debug.Log("Game over!");
		i.loseUI.SetActive(true);
		GameObject.Find("lose_text").GetComponent<TextMeshProUGUI>().text = reason;
	}

	public static void Win()
	{
		Debug.Log("You win!");
		i.winUI.SetActive(true);
	}

	public static void UpdateParadoxCounter()
	{
		i.paradoxCounter.text = paradoxAmount.ToString();
		if (paradoxAmount == 0) i.paradoxCounter.color = Color.green;
		else i.paradoxCounter.color = Color.red;
		i.paradoxIcon.color = i.paradoxCounter.color;
	}

	public static void ParadoxEvent()
	{
		screenFlash = 1;
		i.fullscreenMaterial.SetFloat("_ScreenFlash", 1);
		UpdateParadoxCounter();
	}

	public static void EndRecording()
	{
		PlayerMovement pm = i.player.GetComponent<PlayerMovement>();
		pm.passive = true;

		i.protector.SetActive(true);
	}

	public static void ResetAll()
	{
		intelDone = true;
		timeRiftDone = true;
		i.loseUI.SetActive(false);
		i.winUI.SetActive(false);
		paradoxAmount = Paradox.nextOrder;
		i.protector.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
		foreach (IResettable resettable in resetList)
		{
			resettable.Reset();
		}
		GameObject.Find("checkbox_intel").GetComponent<Toggle>().isOn = intelDone;
		GameObject.Find("checkbox_restore").GetComponent<Toggle>().isOn = timeRiftDone;
		GameObject.Find("checkbox_escape").GetComponent<Toggle>().isOn = false;
	}

	public static List<IResettable> resetList = new List<IResettable>();

	public class Paradox
	{
		public ParadoxCauser causer;
		public float time;
		public int order;
		public Vector3 position;
		public bool resolved;
		public static int nextOrder = 0;
		public GameObject indicator;
		public string name;
		public static List<Paradox> list = new List<Paradox>();
		public Paradox(ParadoxCauser causer, float time, Vector3 position, string name)
		{
			this.causer = causer;
			this.time = time;
			this.order = nextOrder++;
			this.position = position;
			this.resolved = false;
			this.name = name;
			this.indicator = GameObject.Instantiate(ParadoxManager.i.paradoxPrefab);
			this.indicator.transform.position = this.position;
			paradoxAmount++;
			ParadoxEvent();
			Debug.Log("Creating new paradox!"+this.causer.name+this.indicator);
		}
	}
}

public interface IResettable
{
	public void Reset()
	{

	}
}
