using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderMovement : MonoBehaviour
{
    public float speed = 10f;
    CharacterController controller;
    public Transform cameraRoot;

    public GameObject cameraGameObject;
    public FinishArea finishArea;

    public GameObject mainCamera;
    // Start is called before the first frame update

    private void Awake()
    {
        cameraGameObject.SetActive(true);
    }

    void Start()
    {

        finishArea = FindAnyObjectByType<FinishArea>();

        controller = GetComponent<CharacterController>();


        // cameraGameObject = GameObject.Find("CameraRoot");
        // cameraRoot = cameraGameObject.transform;

        mainCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

            Ray r = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            if (Physics.Raycast(r, out RaycastHit hitInfo, 100, LayerMask.GetMask("Ground")))
            {
                transform.rotation = Quaternion.LookRotation(hitInfo.point - transform.position, Vector3.up);
            }
            controller.Move(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime);
            cameraRoot.position = Vector3.Lerp(cameraRoot.position, transform.position, Time.deltaTime * 5);

        

    }
}
