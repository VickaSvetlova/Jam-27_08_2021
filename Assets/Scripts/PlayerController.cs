using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Params")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [HideInInspector] public Idea idea;

    [SerializeField] private float gravityForce = -9.5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float fallinDistance = 3f;
    [SerializeField] private float stepOffset = 0.3f;
    public bool isActive { get; set; } = true;
    public bool isHidden { get; private set; }
    public bool isGrounded {
        get { return controller.stepOffset != 0; }
        private set { controller.stepOffset = (value) ? stepOffset : 0; }
    }
    
    private CharacterController controller;
    private Animator animator;
    private Transform modelTransfrom;
    private Transform cameraTransform;

    private float horizontal, vertical;
    private Vector3 inputDirection;
    private float gravity = -2f;
    private float fallinStartY = 0;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cameraTransform = Camera.main.transform;
        modelTransfrom = animator.transform;
    }

    private void Start() {

    }

    public void SetHidden(bool value) {
        isHidden = value;
        if (idea) {
            var renders = idea.GetComponentsInChildren<Renderer>();
            foreach (var render in renders) {
                render.enabled = !value;
            }
        }
    }

    private void Update() {
        if (isHidden || !isActive) {
            horizontal = 0;
            vertical = 0;
        } else {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            var input = new Vector3(horizontal, 0, vertical);
            inputDirection = Vector3.ClampMagnitude(input, 1f);
            Grounding();
            if (horizontal != 0 || vertical != 0) {
                var rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
                var moveDirection = rotation * inputDirection;
                controller.Move(moveDirection * moveSpeed * Time.deltaTime);
                modelTransfrom.forward = Vector3.RotateTowards(modelTransfrom.forward, moveDirection, 10f * Time.deltaTime, 1f);
            }
        }
        controller.Move(Vector3.up * gravity * Time.deltaTime);
    }

    private void Grounding() {
        if (controller.isGrounded) {
            if (!isGrounded) {
                isGrounded = true;
                gravity = gravityForce;
                if (fallinStartY - transform.position.y > fallinDistance) animator.SetTrigger("Roll");
            }
            if (Input.GetButtonDown("Jump")) {
                isGrounded = false;
                gravity = jumpForce;
                fallinStartY = transform.position.y;
                controller.Move(Vector3.up * gravity * Time.deltaTime);
                animator.SetTrigger("Jump");
            }
        } else {
            if (isGrounded) {
                isGrounded = false;
                gravity = 0;
                fallinStartY = transform.position.y;
            }
            if (gravity > gravityForce) gravity += gravityForce * Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        animator.SetFloat("Speed", inputDirection.magnitude);
        animator.SetBool("IsGrounded", isGrounded);
    }
}
