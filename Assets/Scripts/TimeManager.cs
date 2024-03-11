using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public static float elapsedTime = 0;
	public static bool running;

	// Start is called before the first frame update
	void Start()
	{
		running = true;
		elapsedTime = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (running)
		{
			elapsedTime += Time.deltaTime;
			UIReference.i.timerText.text = TimeSpan.FromSeconds(elapsedTime).ToString(@"mm\:ss\.ff");
		}
	}
}
