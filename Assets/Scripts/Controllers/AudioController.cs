using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    //Public Variables
    public static AudioController Instance;

    public AudioClip pistolShot;
    public AudioClip carbineShot;
    public AudioClip shotgunShot;
    public AudioClip shotgunReload;
    public AudioClip healthPickUp;
    public AudioClip ammoPickUp;
    public AudioClip gunPickUp;
    public AudioClip playerHit;
    public AudioClip enemyHit;
    public AudioClip menuSelect;
    public AudioClip menuHover;

    // Start is called before the first frame update

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }

}
