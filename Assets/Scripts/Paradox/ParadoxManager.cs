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
	public GameObject playerInstance;
	public GameObject protectorInstance;
	public GameObject corpsePrefab;

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
				UIReference.i.nextParadoxCounter.text = "Next paradox in: " + TimeSpan.FromSeconds(nextParadox.time - TimeManager.elapsedTime).ToString(@"mm\:ss\.ff");
				UIReference.i.nextParadoxCounter.color = Color.red;
				UIReference.i.nextParadoxDescription.text = nextParadox.name;
				UIReference.i.nextParadoxDescription.color = Color.red;
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
				UIReference.i.nextParadoxCounter.text = "All paradoxes resolved!";
				UIReference.i.nextParadoxCounter.color = Color.green;
				UIReference.i.nextParadoxDescription.text = "Don't get spotted by the agent and they will safely finish the mission.";
				UIReference.i.nextParadoxDescription.color = Color.green;
			}
		}
	}

	public static void GameOver(string reason = "Bad luck.")
	{
		Debug.Log("Game over!");
		UIReference.i.loseUI.SetActive(true);
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
		UIReference.i.winUI.SetActive(true);
		UIReference.i.winUI.GetComponentInChildren<TextMeshProUGUI>().text += winString;
		TimeManager.running = false;
		//Time.timeScale = 0;
	}

	public static void UpdateParadoxCounter()
	{
		UIReference.i.paradoxCounter.text = paradoxAmount.ToString();
		if (paradoxAmount == 0) UIReference.i.paradoxCounter.color = Color.green;
		else UIReference.i.paradoxCounter.color = Color.red;
		UIReference.i.paradoxIcon.color = UIReference.i.paradoxCounter.color;
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
		PlayerMovement pm = i.playerInstance.GetComponent<PlayerMovement>();
		pm.passive = true;
		i.protectorInstance.SetActive(true);
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
		UIReference.i.loseUI.SetActive(false);
		UIReference.i.winUI.SetActive(false);
		paradoxAmount = Paradox.nextOrder;
		i.protectorInstance.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
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
			UIReference.i.nextParadoxCounter.transform.parent.gameObject.SetActive(true);
			Debug.Log(UIReference.i.nextParadoxCounter.transform.parent.gameObject);
		}
		else
		{
			UIReference.i.nextParadoxCounter.transform.parent.gameObject.SetActive(false);
		}
	}

	public static List<IResettable> resetList = new List<IResettable>();
}

public interface IResettable
{
	public void Reset()
	{

	}
}
