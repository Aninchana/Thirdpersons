using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float playerspeed = 5f;
    [SerializeField] private Camera followCamera;

    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 playervelocity;
    [SerializeField] private float gravityValue = -13f;

    public bool groundedPlayer;
    [SerializeField] private float jumpHeight = 2.5f;

    public Animator animator;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (CheckWin.instance.isWin) 
        {
            case true:
                animator.SetBool("Victory", CheckWin.instance.isWin);
                break;
            case false:
                Movement();
                break;
        }
        Movement();
    }
    void Movement()
    {
        groundedPlayer = characterController.isGrounded;
        if (characterController.isGrounded && playervelocity.y < -2f)
        {
            playervelocity.y = -1f;
        }

        groundedPlayer = characterController.isGrounded;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0)
                                * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirecton = movementInput.normalized;

        characterController.Move(movementDirecton * playerspeed * Time.deltaTime);

        if (movementDirecton != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirecton, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump") && groundedPlayer) 
        {
            playervelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);
        }

        playervelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playervelocity * Time.deltaTime);

        animator.SetFloat("speed", Mathf.Abs(movementDirecton.x) + Mathf.Abs(movementDirecton.z));
        animator.SetBool("Ground", characterController.isGrounded);

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playervelocity.y += Mathf.Sqrt(jumpHeight * -0.3f * gravityValue);
            animator.SetTrigger("Jumping");
        }
    }
    
}
