using UnityEngine;
[RequireComponent (typeof(CharacterController))]

public class PlayerController : MonoBehaviour, IHealth
{
    //Public Variables

    public float walkSpeed = 7.5f;
    public float sprintSpeed = 11.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    [HideInInspector]
    public bool canMove = true;
    
    //Private Variables
    [SerializeField] private Camera playerCamera;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private UIController _uIController;
    private AudioSource _audioSource;
    private AudioController _audioController;
    private PlayerInputActions _inputActions;

    public float CurrentHealth { get; set; }
    [field: SerializeField]
    public float MaxHealth { get; set; }

    private void Awake() => _inputActions = new PlayerInputActions();

    private void OnEnable() => _inputActions.Enable();

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        _audioSource = GetComponent<AudioSource>();
        _audioController = AudioController.Instance;
        _uIController = UIController.Instance;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        _uIController.DisplayHealth(CurrentHealth);

        Vector2 walkVector = _inputActions.Player.Walk.ReadValue<Vector2>();

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float isRunning = _inputActions.Player.Run.ReadValue<float>();
        float curSpeedX = canMove ? (isRunning > 0 ? sprintSpeed : walkSpeed) * walkVector.y : 0;
        float curSpeedY = canMove ? (isRunning > 0 ? sprintSpeed : walkSpeed) * walkVector.x : 0;
        float movementDirectionY  = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if(_inputActions.Player.Jump.WasPressedThisFrame() && canMove && characterController.isGrounded)
            moveDirection.y = jumpSpeed;
        else
            moveDirection.y = movementDirectionY;

        if(!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        if(canMove)
        {
            Vector2 mouseInput = _inputActions.Player.Look.ReadValue<Vector2>();

            rotationX += -mouseInput.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, mouseInput.x * lookSpeed, 0);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out EnemyBullet bullet))
        {
            ChangeHealth(-bullet.damage);
            _audioSource.PlayOneShot(_audioController.playerHit);
            Debug.Log("Player Health: " + CurrentHealth);
        }

    }

    public void ChangeHealth(float healthAmount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + healthAmount, 0, MaxHealth);
        _uIController.DisplayHealth(CurrentHealth);
        CheckRemainingHealth();
    }

    public void CheckRemainingHealth()
    {
        if (CurrentHealth <= 0)
            KillCharacter();
    }

    public void KillCharacter()
    {
        _uIController.ShowDeathScreen();
        canMove = false;
        Time.timeScale = 0;
    }
}
