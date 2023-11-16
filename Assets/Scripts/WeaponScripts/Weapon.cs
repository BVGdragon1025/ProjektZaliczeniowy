using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
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
    protected ObjectPooler _pooler;

    // Start is called before the first frame update
    void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        canShoot = true;
        muzzle = gameObject.transform.GetChild(0);
        audioSource = gameObject.GetComponent<AudioSource>();
        _pooler.pooledObject = bullet;
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
    protected void GetPooledBullet(Transform weaponMuzzle)
    {
        GameObject pooledObject = _pooler.GetObjectPool();
        pooledObject.transform.parent = null;
        pooledObject.transform.position = weaponMuzzle.transform.position;
        pooledObject.transform.rotation = weaponMuzzle.transform.rotation;
        //pooledObject.transform.SetPositionAndRotation(muzzle.transform.position, muzzle.transform.rotation);
        pooledObject.SetActive(true);
    }

    public abstract IEnumerator ShootBullet();

}
