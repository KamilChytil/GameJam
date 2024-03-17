using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turet : MonoBehaviour
{

    ParadoxCauser paradoxCauser;
    ViewCone viewCone;

    ParticleSystem gunParticles;

    private float aimingTime = 0f;


    private bool alreadyShot = false;


    public float rotationSpeed = 50f; 
    public float finalTargetRotation = 90f;

    private float startRotationY;
    private bool rotateBackwards = false;


    private void Start()    
    {
        viewCone = GetComponentInChildren<ViewCone>();
        paradoxCauser = GetComponent<ParadoxCauser>();
        gunParticles = GetComponentInChildren<ParticleSystem>();


        startRotationY = transform.eulerAngles.y;

    }

    private void Update()
    {
        

        if (!TimeManager.running) return;
        if (DisableTuret.isTuretDisable == false)
        {
            RotateTurret();
            transform.position.Set(transform.position.x, 0, transform.position.y);
            if (viewCone.directSight == true && aimingTime <= 1.5f)
            {
                ShootingAtPlayer();
            }

        }
        else
        {

            
            
        }
    }



    private void RotateTurret()
    {

        float targetRotationY = rotateBackwards ? startRotationY : finalTargetRotation;
        Quaternion targetRotation = Quaternion.Euler(0f, targetRotationY, 0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (transform.rotation == targetRotation)
        {
            rotateBackwards = !rotateBackwards;
        }
    }





    private void ShootingAtPlayer()
    {
        aimingTime += Time.deltaTime;
        if (viewCone.visiblePlayer == null) return;
        Vector3 targetDirection = viewCone.visiblePlayer.position - transform.position;
        float targetAngle = Vector3.SignedAngle(Vector3.forward, targetDirection, Vector3.up);
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        if (!alreadyShot && aimingTime >= .5f)
        {
            transform.eulerAngles = new Vector3(0, targetAngle, 0);
            //animator.SetTrigger("Shoot");
            alreadyShot = true;
            gunParticles.Emit(1);
            PlayerMovement playerMovement = viewCone.visiblePlayer.GetComponent<PlayerMovement>();
            Paradox p = paradoxCauser.CauseParadox("A turet was allowed to kill the agent.", (!playerMovement.passive && !FinishArea.recording));
            if (p != null)
            {
                Corpse corpse = GameObject.Instantiate(ParadoxManager.i.corpsePrefab).GetComponent<Corpse>();
                corpse.paradox = p;
                corpse.transform.position = viewCone.visiblePlayer.position;
                corpse.transform.rotation = Quaternion.Euler(0, 180, 0) * this.transform.rotation;
                p.indicator.transform.SetParent(transform, false);
                p.indicator.transform.localPosition = new Vector3();
            }
        }

    }

}
