using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
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
	public GameObject corpse;
	public TextMeshProUGUI nextParadoxCounter;
	public TextMeshProUGUI nextParadoxDescription;

	public AudioClip calmMusic;
	public AudioClip aggressiveMusic;
	AudioSource audioSource;

	public static bool intelDone = false;
	public static bool timeRiftDone = false;


	public Material fullscreenMaterial;
	public static float screenFlash = 0;

	public static int paradoxAmount = 0;

	public static Paradox nextParadox;
	public static int nextParadoxIndex = 0;

	static bool shouldReset = true;

	public static float lastTime = 0;


	private void Awake()
	{
		//Time.timeScale = 1;
		i = this;
		ParadoxManager.resetList.Clear();
		Application.targetFrameRate = 60;
	}
	// Start is called before the first frame update
	void Start()
	{
		Paradox.list.Clear();
		audioSource = GetComponent<AudioSource>();
		Paradox.nextOrder = 0;
		paradoxAmount = 0;
		screenFlash = 0;
		i = this;
		shouldReset = true;
		audioSource.clip = calmMusic;

	}

	// Update is called once per frame
	void Update()
	{
		if (shouldReset) ResetAll();
		screenFlash = Mathf.Lerp(screenFlash, 0, Time.deltaTime * 5);
		fullscreenMaterial.SetFloat("_ScreenFlash", screenFlash);
		UpdateParadoxCounter();
		if (!FinishArea.recording)
		{
			if (nextParadox == null) NextParadox();
			while (nextParadox != null && nextParadox.resolved) NextParadox();
			if (nextParadox != null)
			{
				nextParadoxCounter.text = "Next paradox in: " + TimeSpan.FromSeconds(nextParadox.time - TimeManager.elapsedTime).ToString(@"mm\:ss\.ff");
				nextParadoxCounter.color = Color.red;
				nextParadoxDescription.text = nextParadox.name;
				nextParadoxDescription.color = Color.red;
				if (nextParadox.time <= TimeManager.elapsedTime)
				{
					if (!nextParadox.resolved)
					{
						nextParadox.causer.CauseParadox(nextParadox.name);
					}
					NextParadox();
				}
			}
			else
			{
				nextParadoxCounter.text = "All paradoxes resolved!";
				nextParadoxCounter.color = Color.green;
				nextParadoxDescription.text = "Don't get spotted by the agent and they will safely finish the mission.";
				nextParadoxDescription.color = Color.green;
			}
		}
	}

	public static void GameOver(string reason = "Bad luck.")
	{
		Debug.Log("Game over!");
		i.loseUI.SetActive(true);
		GameObject.Find("lose_text").GetComponent<TextMeshProUGUI>().text = reason;
		TimeManager.running = false;
		//Time.timeScale = 0;
	}

	public static void Win()
	{
		int starCount = 5 - Mathf.Clamp((int)(TimeManager.elapsedTime / 30f), 0, 4);
		string winString = " ";
		for (int i = 0; i < starCount; i++)
		{
			winString += "+";
		}
		Debug.Log("You win! ");
		i.winUI.SetActive(true);
		i.winUI.GetComponentInChildren<TextMeshProUGUI>().text += winString;
		TimeManager.running = false;
		//Time.timeScale = 0;
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

	public static void NextParadox()
	{
		Debug.Log(Paradox.list);
		if (nextParadoxIndex < Paradox.list.Count)
		{
			nextParadox = Paradox.list[nextParadoxIndex];
			nextParadoxIndex++;
		}
		else
		{
			nextParadox = null;
		}
		Debug.Log("Next paradox is:" + nextParadox);
	}

	public static void EndRecording()
	{
		lastTime = TimeManager.elapsedTime;
		PlayerMovement pm = i.player.GetComponent<PlayerMovement>();
		pm.passive = true;
		i.protector.SetActive(true);
		float t = i.audioSource.time;
		i.audioSource.clip = i.aggressiveMusic;
		i.audioSource.time = t;
		i.audioSource.volume = .8f;
		i.audioSource.Play();
		GameObject.Find("checkbox_intel").transform.parent.localScale = new Vector3();
	}

	public static void ResetAll()
	{
		//Time.timeScale = 1;
		shouldReset = false;
		nextParadoxIndex = 0;
		TimeManager.running = true;
		TimeManager.elapsedTime = 0;
		intelDone = false;
		timeRiftDone = false;
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
		if (!FinishArea.recording)
		{
			NextParadox();
			i.nextParadoxCounter.transform.parent.gameObject.SetActive(true);
			Debug.Log(i.nextParadoxCounter.transform.parent.gameObject);
		}
		else
		{
			i.nextParadoxCounter.transform.parent.gameObject.SetActive(false);
		}
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
			list.Add(this);
			paradoxAmount++;
			ParadoxEvent();
			Debug.Log("Creating new paradox!" + this.causer.name + this.indicator);
		}
	}
}

public interface IResettable
{
	public void Reset()
	{

	}
}
