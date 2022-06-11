using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public Variables

    public float walkSpeed = 7.5f;
    public float sprintSpeed = 11.0f;

    //Private Variables
    private float _moveDirectionX;
    private float _moveDirectionZ;

    // Start is called before the first frame update
    void Start()
    {
        _moveDirectionX = Input.GetAxis("Horizontal");
        _moveDirectionZ = Input.GetAxis("Veritcal");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
