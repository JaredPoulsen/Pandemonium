using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowingTutorial : MonoBehaviour
{
    public ThirdPersonController tpc;

    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    private GameObject objectToThrow;
    public GameObject handgun;
    public GameObject shotgun;
    public GameObject smg;


    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        
        readyToThrow = true;
    }

    private void Update()
    {
        for (int i = tpc.Weapons.Length - 1; i > -1; i--)
        {
            if (tpc.WeaponInUse == tpc.Weapons[0])
            {
                objectToThrow = handgun;
            }
            else if (tpc.WeaponInUse == tpc.Weapons[2])
            {
                objectToThrow = shotgun;
            }
            else if (tpc.WeaponInUse == tpc.Weapons[1])
            {
                objectToThrow = smg;
            }  
            else if (tpc.WeaponInUse == tpc.Weapons[-1])
            {
                objectToThrow = null;
            }
        }
        

        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
            
        }
    }

    private void Throw()
    {
        Debug.Log("Out");

        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);

       
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}