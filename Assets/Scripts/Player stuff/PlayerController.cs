using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController cc;
    [Range(0f, 30f)]
    public float speed = 5f;

    [SerializeField] public float mouseSensitivity = 100f;
    [SerializeField] private float xRotation = 0f;
    [SerializeField] public float jumpForce = 3.0f;

    [SerializeField] public float gravity;
    [SerializeField] private Vector2 direction;

    public Animator anim;

    public GameObject weapon;
    public Transform attachPoint;
   
    public GameObject reticle;

    [SerializeField] public Transform cameraTransform;
    [SerializeField] public GameObject canvas;
    [SerializeField] public bool enable = true;
    public LayerMask enemyLayerMask;

    
    //public PlayerHealth playerHealth;
    //private GameManager gameManager;
    //private PlayerShoot playerShoot;

    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float deceleration = 2f;
    private float currentSpeed = 0f;


    public List<string> inventoryItems = new List<string>();
    [SerializeField] private SaveLoad saveLoadManager;

    void Start()
    {
        saveLoadManager = FindObjectOfType<SaveLoad>();
        cc = GetComponent<CharacterController>();
        canvas.SetActive(false);
        gravity = Physics.gravity.y;
        cameraTransform = Camera.main.transform;
        anim = GetComponent<Animator>();
       // playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Rotate the player to match the camera's Y rotation
        MouseLookAround();

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * direction.y + camRight * direction.x;

        // Gradually adjust the current speed based on input
        if (moveDir.magnitude > 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, moveDir.magnitude * speed, Time.deltaTime * acceleration);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * deceleration);
        }

        Vector3 horizontalMoveDir = moveDir * currentSpeed * Time.deltaTime;
        float YVel = cc.isGrounded ? 0 : gravity * Time.deltaTime;
        horizontalMoveDir.y = YVel;

        cc.Move(horizontalMoveDir);

        // Update Blend Tree parameters
        anim.SetFloat("Forward/Back Speed", currentSpeed);
        //anim.SetFloat("DirectionX", direction.x);
        //anim.SetFloat("DirectionY", direction.y);

        // CheckEnemyVisibility(); (commented if not needed in this case)
    }

    void MouseLookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate player horizontally (Y-axis)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically (X-axis)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit camera vertical rotation to avoid flipping
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }



    public void MoveStarted(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();
        Debug.Log($"Move Started: {direction}");
    }

    public void MoveCanceled(InputAction.CallbackContext ctx)
    {
        direction = Vector2.zero;
        Debug.Log("Move Canceled");
    }





    private void HandleWeaponPickup(GameObject newWeapon)
    {
   

        // Pick up the new weapon
        weapon = newWeapon;
        weapon.GetComponent<Rigidbody>().isKinematic = true;
        weapon.transform.SetPositionAndRotation(attachPoint.position, attachPoint.rotation);
        weapon.transform.SetParent(attachPoint);
        Physics.IgnoreCollision(GetComponent<Collider>(), weapon.GetComponent<Collider>());
        reticle.SetActive(true);
        anim.SetBool("Aiming", true);

 

     
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Weapon2"))
        {
            Debug.Log("Weapon collision detected. Picking up...");
            HandleWeaponPickup(collision.gameObject);
        }


    }

    private void OnTriggerStay(Collider other)
    {

    }

    public void Punch(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            anim.SetTrigger("Punch");
            Debug.Log("Punch action triggered");
        }
    }

    public void Kick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            anim.SetTrigger("Kick");
            Debug.Log("Kick action triggered");
        }
    }

    public void Die(InputAction.CallbackContext ctx)
    {
        // Add death logic here if needed
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            anim.SetTrigger("Jump");
            Debug.Log("Jump action triggered");
        }
    }


    public void TakeDamage(float damage)
    {
        //playerHealth.TakeDamage(damage);
    }

    public void Heal(float amount)
    {
        //playerHealth.Heal(amount);
    }



}