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

    [SerializeField] private Transform GroundCheck;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask isGroundLayer;
    [SerializeField] private float groundCheckRadius = 0.3f;

    [SerializeField] public float mouseSensitivity = 100f;
    [SerializeField] private float xRotation = 0f;
    [SerializeField] public float jumpForce = 3.0f;

    [SerializeField] public float gravity;
    [SerializeField] private Vector2 direction;

    public Animator anim;

    public GameObject weapon;
    public GameObject weapon2;
    public Transform attachPoint;
    public Transform backAttachPoint; // New attach point for the previously held weapon
    public GameObject reticle;

    [SerializeField] public Transform cameraTransform;
    [SerializeField] public GameObject canvas;
    [SerializeField] public bool enable = true;
    public LayerMask enemyLayerMask;

    public GameObject enemy;
    //public PlayerHealth playerHealth;
    //private GameManager gameManager;
    //private PlayerShoot playerShoot;

    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float deceleration = 2f;
    private float currentSpeed = 0f;

    public bool hasWeapon1 = false;
    public bool hasWeapon2 = false;

    private bool isWeapon1Equipped = true;

    public int playerScore;
    public List<string> inventoryItems = new List<string>();
    //[SerializeField] private SaveLoad saveLoadManager;

    void Start()
    {
        //saveLoadManager = FindObjectOfType<SaveLoad>();
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
        Vector3 cameraRotation = cameraTransform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);

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
        anim.SetFloat("Speed", currentSpeed);
        anim.SetFloat("DirectionX", direction.x);
        anim.SetFloat("DirectionY", direction.y);

        //CheckEnemyVisibility();

        if (Input.GetKeyDown(KeyCode.P))
        {
           // saveLoadManager.SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
           // saveLoadManager.LoadGame();
        }
    }

    private void CheckEnemyVisibility()
    {
        Vector3 startingPos = transform.position;
        startingPos.y += 1f;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawLine(startingPos, transform.position + (transform.forward * 10), Color.red);

        if (Physics.Raycast(ray, out hit, 100f, enemyLayerMask))
        {
           // enemy.GetComponent<EnemyAI>().StopAndDisappear();
        }
        else
        {
           // enemy.GetComponent<EnemyAI>().ResumeBehavior();
        }
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

    public void DropWeapon(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && weapon != null)
        {
            Debug.Log("Dropping weapon");

            // Detach the weapon from the player
            attachPoint.transform.DetachChildren();

            // Ensure the weapon can interact with physics
            Rigidbody weaponRB = weapon.GetComponent<Rigidbody>();
            if (weaponRB != null)
            {
                weaponRB.isKinematic = false; // Allow physics to affect the weapon
                weaponRB.AddForce(transform.forward * 5, ForceMode.Impulse); // Apply a force to drop the weapon
                weaponRB.useGravity = true;
            }
            else
            {
                Debug.LogWarning("Weapon does not have a Rigidbody component.");
            }

            // Reset weapon state
            weapon = null;
            reticle.SetActive(false);
            anim.SetBool("Armed", false);

            Debug.Log("Weapon dropped and detached");
        }
        else
        {
            Debug.LogWarning("No weapon to drop or input not performed.");
        }
    }



    private void HandleWeaponPickup(GameObject newWeapon)
    {
        if (weapon != null)
        {
            // Move the currently held weapon to the back attach point
            weapon.transform.SetParent(backAttachPoint);
            weapon.transform.position = backAttachPoint.position;
            weapon.transform.rotation = backAttachPoint.rotation;
            weapon.GetComponent<Rigidbody>().isKinematic = true; // Keep it from moving around
        }

        // Pick up the new weapon
        weapon = newWeapon;
        weapon.GetComponent<Rigidbody>().isKinematic = true;
        weapon.transform.SetPositionAndRotation(attachPoint.position, attachPoint.rotation);
        weapon.transform.SetParent(attachPoint);
        Physics.IgnoreCollision(GetComponent<Collider>(), weapon.GetComponent<Collider>());
        reticle.SetActive(true);
        anim.SetBool("Armed", true);

        // Update weapon flags
        hasWeapon1 = weapon.CompareTag("Weapon");
        hasWeapon2 = weapon.CompareTag("Weapon2");

        Debug.Log("hasWeapon1: " + hasWeapon1 + ", hasWeapon2: " + hasWeapon2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Weapon2"))
        {
            HandleWeaponPickup(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("enemyAxe"))
        {
           // playerHealth.TakeDamage(5);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
           // playerHealth.TakeDamage(5);
        }

        if (other.gameObject.CompareTag("Fire"))
        {
           // playerHealth.TakeDamage(5);
        }

        if (other.gameObject.CompareTag("EndTrigger"))
        {
            SceneManager.LoadScene(3);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (other.gameObject.CompareTag("enemyAxe"))
        {
           // playerHealth.TakeDamage(5);
        }
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

    public void ChangeWeapon(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (hasWeapon1 && isWeapon1Equipped)
            {
                // Switch to weapon2
                EquipWeapon(weapon2);
                isWeapon1Equipped = false;
            }
            else if (hasWeapon2 && !isWeapon1Equipped)
            {
                // Switch to weapon1
                EquipWeapon(weapon);
                isWeapon1Equipped = true;
            }
            else
            {
                Debug.LogWarning("No weapon to switch to.");
            }
        }
    }



    public void EquipWeapon(GameObject weaponToEquip)
    {
        if (weaponToEquip != null)
        {
            // Move the currently equipped weapon to the back attach point if it exists
            if (weapon != null)
            {
                weapon.transform.SetParent(backAttachPoint);
                weapon.transform.position = backAttachPoint.position;
                weapon.transform.rotation = backAttachPoint.rotation;
                weapon.GetComponent<Rigidbody>().isKinematic = true; // Ensure it stays in place
            }

            // Equip the new weapon
            weapon = weaponToEquip;
            weapon.GetComponent<Rigidbody>().isKinematic = true;
            weapon.transform.SetParent(attachPoint);
            weapon.transform.position = attachPoint.position;
            weapon.transform.rotation = attachPoint.rotation;

            // Update weapon flags
            hasWeapon1 = weapon.CompareTag("Weapon");
            hasWeapon2 = weapon.CompareTag("Weapon2");

            // Set reticle and animation state
            reticle.SetActive(true);
            anim.SetBool("Armed", true);

            Debug.Log($"Equipped weapon: {weapon.name}");
        }
        else
        {
            Debug.LogWarning("Weapon to equip is null.");
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