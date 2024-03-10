using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParadoxCauser))]
public class EnemyPatrol : MonoBehaviour, IResettable
{
	ParadoxCauser paradoxCauser;

	public float enemySpeed;
	public float enemyWait;
	public Transform[] enemyPoints;
	private int currentPointIndex = 0;

	ViewCone viewCone;

	Animator animator;
	ParticleSystem gunParticles;

	private float aimingTime = 0f;

	public bool alive = true;

	public bool alreadyShot = false;


	void Start()
	{
		alive = true;
		viewCone = GetComponentInChildren<ViewCone>();
		paradoxCauser = GetComponent<ParadoxCauser>();
		animator = GetComponentInChildren<Animator>();
		gunParticles = GetComponentInChildren<ParticleSystem>();
		if (enemyPoints.Length == 0)
		{
			enemyPoints = new Transform[] { transform };
		}
		transform.position = enemyPoints[currentPointIndex].position;
		ParadoxManager.resetList.Add(this);
	}

	void Update()
	{
		if (alive == true)
		{
			if (viewCone.directSight == true && aimingTime <= 1.5f)
			{
				animator.SetBool("isWalking", false);
				ShootingAtPlayer();
			}
			else
			{
				if (!alreadyShot)
				{
					aimingTime = 0f;
				}

				Rotate();

				if (enemyPoints.Length > 1)
				{
					animator.SetBool("isWalking", true);
					transform.position = Vector3.MoveTowards(transform.position, enemyPoints[currentPointIndex].position, enemySpeed * Time.deltaTime);
					if (Vector3.Distance(transform.position, enemyPoints[currentPointIndex].position) < 1f)
					{
						ChangePoint();
					}
				}


			}

		}
		else
		{

		}
	}


	private void ChangePoint()
	{
		currentPointIndex++;

		if (currentPointIndex == enemyPoints.Length)
		{
			currentPointIndex = 0;
		}

	}


	private void ShootingAtPlayer()
	{
		aimingTime += Time.deltaTime;
		if (viewCone.visiblePlayer == null) return;
		Vector3 targetDirection = viewCone.visiblePlayer.position - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
		if (!alreadyShot && aimingTime >= .5f)
		{
			transform.rotation = targetRotation;
			animator.SetTrigger("Shoot");
			ParadoxManager.Paradox p = paradoxCauser.CauseParadox("A guard was allowed to kill the agent.");
			if (p != null)
			{
				Corpse corpse = GameObject.Instantiate(ParadoxManager.i.corpse).GetComponent<Corpse>();
				corpse.paradox = p;
				corpse.transform.position = viewCone.visiblePlayer.position;
				corpse.transform.rotation = Quaternion.Euler(0, 180, 0) * this.transform.rotation;
				p.indicator.transform.SetParent(transform, false);
				p.indicator.transform.localPosition = new Vector3();
			}
			alreadyShot = true;
			gunParticles.Emit(1);
		}

	}

	private void Rotate()
	{
		Quaternion targetRotation;
		if (enemyPoints.Length > 1)
		{
			Vector3 targetDirection = enemyPoints[currentPointIndex].position - transform.position;
			targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

		}
		else
		{
			targetRotation = enemyPoints[0].rotation;
		}
		float angleToRotate = Quaternion.Angle(transform.rotation, targetRotation);

		//float direction = Mathf.Sign(Vector3.Cross(transform.forward, targetDirection).y);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 5f * angleToRotate);
	}

	public void Die()
	{
		if (this.alive)
		{
			this.alive = false;
			animator.SetTrigger("death1");
			paradoxCauser.ResolveParadox();
		}
	}
	public void Reset()
	{
		Debug.Log("Reset Guard");
		currentPointIndex = 0;
		transform.position = enemyPoints[currentPointIndex].position;
		alive = true;
		alreadyShot = false;
		aimingTime = 0;

	}

}
