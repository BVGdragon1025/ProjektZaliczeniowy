using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //Public Variables
    public AmmoHolder ammoHolder;
    // ENCAPSULATION
    [SerializeField]
    public float ShotDelay {
        get
            {
                if (_shotDelay < 0)
                {
                    return _shotDelay = 0;
                }
                else
                {
                    return _shotDelay;
                }
            }
        set { _shotDelay = value;} 
    }
    public bool canShoot = true;

    //Private Variables
    [SerializeField]
    private float _shotDelay;

    //Protected Variables
    [SerializeField]
    protected GameObject bullet;
    protected Transform muzzle;
    protected AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        muzzle = gameObject.transform.GetChild(0);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !SceneController.Instance.isInMenu)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (ammoHolder.ammoCount > 0 && canShoot)
        {
            StartCoroutine(ShootBullet());

        }
    }

    public abstract IEnumerator ShootBullet();

}
