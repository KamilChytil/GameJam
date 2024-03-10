using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ParadoxManager;

public class ParadoxCauser : MonoBehaviour, IResettable
{
	public bool resolved = false;
	public Paradox paradox;
	// Start is called before the first frame update
	void Start()
	{
		this.paradox = null;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public Paradox CauseParadox(string name)
	{
		if (FinishArea.recording)
		{
			if (this.paradox == null)
			{
				Paradox p = new Paradox(this, TimeManager.elapsedTime, this.transform.position, name);
				Debug.Log(p.indicator);

				this.paradox = p;
				Debug.Log(this.paradox.indicator);
				ParadoxManager.resetList.Add(this);
				return p;
			}
		}
		else if (this.paradox != null)
		{
			if (!this.resolved)
			{
				ParadoxManager.GameOver("Time paradox: " + name);
			}
		}
		return null;
	}
	public void ResolveParadox()
	{
		if (FinishArea.recording) return;
		if (this.paradox == null) return;
		this.paradox.resolved = true;
		this.resolved = true;
		Debug.Log("Paradox resolved");
		foreach (var mr in this.paradox.indicator.GetComponentsInChildren<MeshRenderer>())
		{
			mr.material.color = Color.green;
			mr.material.SetColor("_Color", Color.green);
			mr.material.SetColor("_BaseColor", Color.green);
		}
		ParadoxManager.paradoxAmount--;
		ParadoxManager.i.paradoxCounter.text = paradoxAmount.ToString();
	}
	public void Reset()
	{
		this.resolved = false;
		if (this.paradox != null)
		{

			this.paradox.resolved = false;
			foreach (var mr in this.paradox.indicator.GetComponentsInChildren<MeshRenderer>())
			{
				mr.material.color = Color.red;
				mr.material.SetColor("_Color", Color.red);
				mr.material.SetColor("_BaseColor", Color.red);
			}
		}
	}
}
