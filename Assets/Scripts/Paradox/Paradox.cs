using System.Collections.Generic;
using UnityEngine;

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
		ParadoxManager.paradoxAmount++;
		ParadoxManager.ParadoxEvent();
		Debug.Log("Creating new paradox!" + this.causer.name + this.indicator);
	}

}
