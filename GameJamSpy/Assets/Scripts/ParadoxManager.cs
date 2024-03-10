using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ParadoxManager : MonoBehaviour
{
	public static ParadoxManager i;
	public GameObject paradoxPrefab;
	public TextMeshProUGUI paradoxCounter;
	public GameObject interactPrompt;
	public GameObject player;
	public GameObject protector;
	public GameObject winUI;
	public GameObject loseUI;

	public Material fullscreenMaterial;
	public static float screenFlash = 0;

	public static int paradoxAmount = 0;



	// Start is called before the first frame update
	void Start()
	{
		Paradox.nextOrder = 0;
		paradoxAmount = 0;
		screenFlash = 0;
		i = this;
		i.paradoxCounter.text = paradoxAmount.ToString();
		ParadoxManager.resetList.Clear();
	}

	// Update is called once per frame
	void Update()
	{
		screenFlash = Mathf.Lerp(screenFlash, 0, Time.deltaTime * 5);
		fullscreenMaterial.SetFloat("_ScreenFlash", screenFlash);
	}

	public static void GameOver()
	{
		Debug.Log("Game over!");
		i.loseUI.SetActive(true);
	}

	public static void Win()
	{
		Debug.Log("You win!");
		i.winUI.SetActive(true);
	}

	public static void ParadoxEvent()
	{
		screenFlash = 1;
		i.fullscreenMaterial.SetFloat("_ScreenFlash", 1);
		i.paradoxCounter.text = paradoxAmount.ToString();
	}

	public static void EndRecording()
	{
		PlayerMovement pm = i.player.GetComponent<PlayerMovement>();
		pm.passive = true;

		i.protector.SetActive(true);
	}

	public static void ResetAll()
	{
		i.loseUI.SetActive(false);
		i.winUI.SetActive(false);
		paradoxAmount = Paradox.nextOrder;
		foreach (IResettable resettable in resetList)
		{
			resettable.Reset();
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
		public static List<Paradox> list = new List<Paradox>();
		public Paradox(ParadoxCauser causer, float time, Vector3 position)
		{
			this.causer = causer;
			this.time = time;
			this.order = nextOrder++;
			this.position = position;
			this.resolved = false;
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
