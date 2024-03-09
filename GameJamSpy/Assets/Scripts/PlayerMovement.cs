using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 10f;
	CharacterController controller;
	public Transform cameraRoot;
	public Transform gunFX;
	float timeSinceGunshot = 0;
	public ParticleSystem gunParticles;
	// Start is called before the first frame update
	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		Ray r = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
		if (Physics.Raycast(r, out RaycastHit hitInfo, 100, LayerMask.GetMask("Ground")))
		{
			//  transform.rotation = Quaternion.LookRotation(hitInfo.point - transform.position, Vector3.up);
			transform.eulerAngles = new Vector3(0, Quaternion.LookRotation(hitInfo.point - transform.position, Vector3.up).eulerAngles.y, 0);
		}
		controller.Move(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime);
		cameraRoot.position = Vector3.Lerp(cameraRoot.position, transform.position, Time.deltaTime * 5);

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
