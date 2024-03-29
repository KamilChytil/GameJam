using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 10f;
	CharacterController controller;
	Transform cameraRoot;
	Transform lightRoot;
	Transform gunFlash;
	float timeSinceGunshot = 0;
	ParticleSystem gunParticles;
	Animator animator;
	public bool passive = false;

	ViewCone viewCone;

	public static int kills = 0;

	// Start is called before the first frame update
	void Start()
	{
		viewCone = GetComponentInChildren<ViewCone>();
		controller = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
		gunParticles = GetComponentInChildren<ParticleSystem>();
		cameraRoot = GameObject.Find("CameraRoot").transform;
		lightRoot = GameObject.Find("LightRoot").transform;
		gunFlash = transform.Find("Gun Flash");
		if (gunFlash != null) gunFlash.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (!TimeManager.running) return;
		if (passive)
		{
			Vector3 dir = PlayerPositionLoader.moveDir;
			animator.SetFloat("moveX", dir.x);
			animator.SetFloat("moveY", dir.z);
			if (viewCone != null)
			{
				if (viewCone.directSight)
				{
					ParadoxManager.GameOver("You got spotted by your alternate self.");
				}
			}
		}
		else
		{



			Quaternion animationRot = new Quaternion();
			Ray r = Camera.main.ScreenPointToRay(new Vector2(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width), Mathf.Clamp(Input.mousePosition.y, 0, Screen.height)));

			if (Physics.Raycast(r, out RaycastHit hitInfo, 100, LayerMask.GetMask("Ground")))
			{
				Vector3 mouseDiff = hitInfo.point - transform.position;
				float angle = Vector3.SignedAngle(Vector3.forward, mouseDiff, Vector3.up);

				transform.eulerAngles = new Vector3(0, angle, 0);

				mouseDiff.x *= -1;
				angle = Vector3.SignedAngle(Vector3.forward, mouseDiff, Vector3.up);
				animationRot = Quaternion.Euler(new Vector3(0, angle, 0));
			}
			Vector3 movementDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
			float runningMultiplier = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
			controller.Move(movementDir * speed * runningMultiplier * Time.deltaTime);
			transform.position.Set(transform.position.x, 0, transform.position.y);

			//Quaternion r90d = Quaternion.Euler(0, -90, 0);
			Vector3 animatorDir = (animationRot) * movementDir;
			animator.SetFloat("moveX", animatorDir.x);
			animator.SetFloat("moveY", animatorDir.z);
			if (FinishArea.recording)
			{
				animator.SetBool("isRunning", Input.GetKey(KeyCode.LeftShift));
			}

			if (!FinishArea.recording)
			{
				if (Input.GetMouseButtonDown(0))
				{
					gunFlash.gameObject.SetActive(true);
					timeSinceGunshot = 0;
					gunParticles.Emit(1);
					animator.SetTrigger("shoot");
					Ray shootRay = new Ray(transform.position, transform.rotation * Vector3.forward);
					if (Physics.Raycast(shootRay, out RaycastHit hit, 10, LayerMask.GetMask("Enemy") + LayerMask.GetMask("Walls"), QueryTriggerInteraction.Collide))
					{
						Debug.Log("hit" + hit.transform.gameObject);
						if (hit.transform.CompareTag("Enemy"))
						{
							Debug.Log("actually hit enemy");
							EnemyPatrol enemy = hit.transform.GetComponent<EnemyPatrol>();
							enemy.Die();
							kills++;
						}
					}

					ParadoxManager.ParadoxEvent();
				}
				else
				{
					timeSinceGunshot += Time.deltaTime;
					if (timeSinceGunshot >= .1f)
						gunFlash.gameObject.SetActive(false);
				}
				if (Input.GetKeyDown(KeyCode.Space))
				{
					Time.timeScale = 4;
				}
				if (Input.GetKeyUp(KeyCode.Space))
				{
					Time.timeScale = 1;
				}
			}

			lightRoot.position = transform.position;
			cameraRoot.position = Vector3.Lerp(cameraRoot.position, transform.position, Time.deltaTime * 5);
		}

	}
}
