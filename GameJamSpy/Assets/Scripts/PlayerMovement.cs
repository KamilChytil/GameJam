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
	// Start is called before the first frame update
	void Start()
	{
		controller = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (FinishArea.playerFinished != 0)
		{
			Vector3 dir = PlayerPositionLoader.moveDir;
			animator.SetFloat("moveX", dir.z);
			animator.SetFloat("moveY", dir.x);
			return;
		}
	/*	else
        {
            cameraRoot.gameObject.SetActive(false);
            enabled = false;
        }
*/
		Quaternion animationRot = new Quaternion();

		Ray r = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
		cameraRoot.position = Vector3.Lerp(cameraRoot.position, transform.position, Time.deltaTime * 5);
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
		controller.Move(movementDir * speed * Time.deltaTime);

		//Quaternion r90d = Quaternion.Euler(0, -90, 0);
		Vector3 animatorDir = (animationRot) * movementDir;
		animator.SetFloat("moveX", animatorDir.z);
		animator.SetFloat("moveY", animatorDir.x);

		lightRoot.position = transform.position;

		if (Input.GetMouseButtonDown(0))
		{
			gunFX.gameObject.SetActive(true);
			timeSinceGunshot = 0;
			gunParticles.Emit(1);
		}
		else
		{
			timeSinceGunshot += Time.deltaTime;
			if (timeSinceGunshot >= .1f)
				gunFX.gameObject.SetActive(false);
		}

	}
}
