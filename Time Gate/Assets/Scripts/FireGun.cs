using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour, GunInterface
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

    public GameObject projectile;
    public float projectileSpeed = 10;

    public AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        if(isFiring && !firingLock && (Time.timeScale != 0))
        {
            //fire the bullet
            StartCoroutine(fire());
        }
    }

    public IEnumerator fire()
    {
        firingLock = true;
        //display the prefab
        GameObject obj = Instantiate(muzzleFire, transform.position, transform.rotation);

        //create a gunshot
        GameObject theProjectile = Instantiate(projectile, transform.position, transform.rotation);

        //set direction to the normal of our rotation for our projectile
        float rotation = theProjectile.transform.rotation.eulerAngles.z;
        rotation = (rotation + 360) % 360;
        float rotationRads = Mathf.Deg2Rad * rotation;

        float rotationY = (theProjectile.transform.rotation.eulerAngles.y + 360) % 360;
        float rotationYRads = rotationY * Mathf.Deg2Rad;

        theProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(rotationRads) * Mathf.Cos(rotationYRads),
            Mathf.Sin(rotationRads)) * projectileSpeed;
        theProjectile.GetComponent<ProjectileDamage>().SetDamage(damage);
        theProjectile.GetComponent<ProjectileDamage>().AttachPlayer(gameObject.GetComponentInParent<PlayerData>());

        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        cameraToShake.GetComponent<ScreenShake1>().ShakeScreen();
        src.Play();
        //wait
        yield return new WaitForSeconds(fireRate);
        firingLock = false;
    }
}
