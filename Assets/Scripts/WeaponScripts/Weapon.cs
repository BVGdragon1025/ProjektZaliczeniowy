using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public abstract class Weapon : MonoBehaviour
{
    //Public Variables
    public AmmoHolder ammoHolder;
    // ENCAPSULATION
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
    protected ObjectPooler pooler;

    private void Awake()
    {
        pooler = GetComponent<ObjectPooler>();
        pooler.pooledObject = bullet;
    }

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

    /// <summary>
    /// Function gets bullet object from Object Pool, then uses SetPositionAndRotation to set it in world space
    /// </summary>
    protected void GetPooledBullet()
    {
        GameObject pooledObject = pooler.GetObjectPool();
        pooledObject.transform.parent = null;
        //pooledObject.transform.position = muzzle.transform.position;
        //pooledObject.transform.rotation = muzzle.transform.rotation;
        pooledObject.transform.SetPositionAndRotation(muzzle.transform.position, muzzle.transform.rotation);
        pooledObject.SetActive(true);
    }

    /// <summary>
    /// Function gets bullet object from Object Pool, then uses SetPositionAndRotation to set it in world space and includes shotgun spread. 
    /// </summary>
    /// <param name="spreadX"></param>
    /// <param name="spreadY"></param>
    protected void GetPooledBullet(float spreadX, float spreadY)
    {
        GameObject pooledObject = pooler.GetObjectPool();
        pooledObject.transform.parent = null;
        Debug.Log($"Received spread: {spreadX}, {spreadY}");
        pooledObject.transform.SetPositionAndRotation(muzzle.transform.position, muzzle.transform.rotation * Quaternion.Euler(spreadX, spreadY, 0));
        pooledObject.SetActive(true);
    }

    public abstract IEnumerator ShootBullet();

}
