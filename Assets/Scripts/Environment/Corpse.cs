using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour, IResettable
{
	Animator animator;
	public Paradox paradox;
	bool resolved = false;
	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		animator.SetTrigger("death1");
		ParadoxManager.resetList.Add(this);
		SetColor(Color.red);
	}

	// Update is called once per frame
	void Update()
	{
		if (paradox != null && paradox.resolved && !this.resolved)
		{
			this.resolved = true;
			SetColor(Color.green);
		}
	}

	public void Reset()
	{
		SetColor(Color.red);
		this.resolved = false;
	}

	public void SetColor(Color color)
	{
		var mr = GetComponentInChildren<SkinnedMeshRenderer>();
		if (mr == null) return;
		foreach (var mat in mr.materials)
		{
			mat.color = color;
			mat.SetColor("Color", color);
			mat.SetColor("_Color", color);
			mat.SetColor("_BaseColor", color);
		}
	}
}
