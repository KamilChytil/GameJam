using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 10f;
	CharacterController controller;
	public Transform cameraRoot;
	public Transform lightRoot;
	public Transform gunFX;
	float timeSinceGunshot = 0;
	public ParticleSystem gunParticles;
	public Animator animator;
	public Material fullscreenMaterial;
	public float screenFlash = 0;
	// Start is called before the first frame update
	void Start()
	{
		controller = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (FinishArea.replayActive != 0)
		{
			Vector3 dir = PlayerPositionLoader.moveDir;
			animator.SetFloat("moveX", dir.z);
			animator.SetFloat("moveY", dir.x);
		}
		else
		{



			Quaternion animationRot = new Quaternion();
			Ray r;
			try
			{
				r = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

			}
			catch
			{
				r = Camera.main.ScreenPointToRay(new Vector2(.5f, .5f));
			}
			if (Physics.Raycast(r, out RaycastHit hitInfo, 100, LayerMask.GetMask("Ground")))
			{
				//  transform.rotation = Quaternion.LookRotation(hitInfo.point - transform.position, Vector3.up);
				Vector3 mouseDiff = hitInfo.point - transform.position;
				//mouseDiff -= cameraRoot.position - transform.position;
				transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(mouseDiff, Vector3.up).eulerAngles.y, 0);


				mouseDiff.x *= -1;
				animationRot = Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(mouseDiff, Vector3.up).eulerAngles.y, 0));
			}
			Vector3 movementDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
			float runningMultiplier = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
			controller.Move(movementDir * speed * runningMultiplier * Time.deltaTime);

			//Quaternion r90d = Quaternion.Euler(0, -90, 0);
			Vector3 animatorDir = (animationRot) * movementDir;
			animator.SetFloat("moveX", animatorDir.z);
			animator.SetFloat("moveY", animatorDir.x);
			animator.SetBool("isRunning", Input.GetKey(KeyCode.LeftShift));


			if (Input.GetMouseButtonDown(0))
			{
				gunFX.gameObject.SetActive(true);
				timeSinceGunshot = 0;
				gunParticles.Emit(1);
				ParadoxEvent();
			}
			else
			{
				timeSinceGunshot += Time.deltaTime;
				if (timeSinceGunshot >= .1f)
					gunFX.gameObject.SetActive(false);
			}

		}
		lightRoot.position = transform.position;
		cameraRoot.position = Vector3.Lerp(cameraRoot.position, transform.position, Time.deltaTime * 5);
		screenFlash = Mathf.Lerp(screenFlash, 0, Time.deltaTime * 5);
		fullscreenMaterial.SetFloat("_ScreenFlash", screenFlash);
	}

	public void ParadoxEvent()
	{
		screenFlash = 1;
		fullscreenMaterial.SetFloat("_ScreenFlash", 1);
	}
}
