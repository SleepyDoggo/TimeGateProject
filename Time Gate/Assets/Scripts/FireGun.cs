using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    //muzzlefire specified on a per weapon basis
    public GameObject muzzleFire;
    public GameObject cameraToShake;
    public bool shakeCamera = false;

    //different behavior if it is a player
    public bool isPlayer = true;

    [Range(1, 4)]
    public int playerNumber = 1;

    //fire rate
    public float fireRate = 1;
    //damage
    public int damage = 1;

    private bool isFiring;
    private bool firingLock;

    //for weapon firing logic
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        //check if firing
        if (isPlayer)
        {
            if (InputController.instance.useJoySticks)
            {
                //implement controller and player number later
                isFiring = InputController.instance.isShooting[playerNumber - 1];
            }
            else
            {
                isFiring = Input.GetMouseButtonDown(0);//0 is left click
            }
        }
        else
        {
            //implement ai firing later
        }

        if(isFiring && !firingLock)
        {
            //fire the bullet
            StartCoroutine(fire());
        }
    }

    IEnumerator fire()
    {
        firingLock = true;
        //display the prefab
        GameObject obj = Instantiate(muzzleFire, transform.position, transform.rotation);
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        cameraToShake.GetComponent<ScreenShake1>().ShakeScreen();

        //shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        //wait
        yield return new WaitForSeconds(fireRate);
        firingLock = false;
    }
}
