using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParadoxCauser))]
public class EnemyPatrol : MonoBehaviour
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
		transform.position = enemyPoints[currentPointIndex].position;
		alive = true;
		viewCone = GetComponentInChildren<ViewCone>();
		paradoxCauser = GetComponent<ParadoxCauser>();
		animator = GetComponentInChildren<Animator>();
		gunParticles = GetComponentInChildren<ParticleSystem>();
	}

	void Update()
	{
		if (alive == true)
		{
			if (viewCone.directSight == true && aimingTime <= 1f)
			{
				animator.SetFloat("moveX", 1);
				ShootingAtPlayer();
			}
			else
			{
				animator.SetFloat("moveX", 0);
				if (!alreadyShot)
				{
					aimingTime = 0f;
				}

				Rotate();

				transform.position = Vector3.MoveTowards(transform.position, enemyPoints[currentPointIndex].position, enemySpeed * Time.deltaTime);
				if (Vector3.Distance(transform.position, enemyPoints[currentPointIndex].position) < 1f)
				{
					ChangePoint();
				}

			}

		}
		else
		{
			Debug.Log("EnemyDead");

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
			animator.SetTrigger("shoot");
			ParadoxManager.Paradox p = paradoxCauser.CauseParadox();
			if (p != null)
			{
				p.indicator.transform.SetParent(transform, false);
				p.indicator.transform.localPosition = new Vector3();
			}
			alreadyShot = true;
			gunParticles.Emit(1);
		}

	}

	private void Rotate()
	{
		Vector3 targetDirection = enemyPoints[currentPointIndex].position - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		float angleToRotate = Quaternion.Angle(transform.rotation, targetRotation);

		float direction = Mathf.Sign(Vector3.Cross(transform.forward, targetDirection).y);
		direction = 1;

		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 5f * angleToRotate * direction);
	}

	public void Die()
	{
		this.alive = false;
		animator.SetTrigger("death1");
	}

}
