using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private CharacterController controller;
    public float totalHealth;
    public float speed;
    public float gravity;
    public float damage;

    private Animator anim;
    private Transform cam;

    Vector3 moveDirection;

    private bool isWalking;

    public bool isDead;
    public bool isBeating;

    public float smoothRotTime;
    private float turnSmoothVelocity;

    public float colliderRadius;


    void Start()
    {
        Cursor.visible = false;
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (!isDead && !anim.GetBool("isDancing"))
        {
            Move();
        }
       // GetMouseInput();
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical);

            if (direction.magnitude > 0)
            {
                    float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, smoothRotTime);

                    transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                    moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * speed;

                    anim.SetInteger("transition", 1);
                    anim.SetBool("walking", true);

                    isWalking = true;

            }
            else if (isWalking)
            {
                anim.SetBool("walking", false);
                anim.SetInteger("transition", 0);
                moveDirection = Vector3.zero;

                isWalking = false;
            }

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (anim.GetBool("walking"))
            {
                anim.SetBool("walking", false);
                anim.SetInteger("transition", 0);
                StartCoroutine("Attack");
            }
            if (!anim.GetBool("walking"))
            {
                StartCoroutine("Attack");
            }
        }
    }

    IEnumerator Attack()
    {
        if (!isBeating)
        {
            isBeating = true;
            anim.SetBool("attacking", true);
            anim.SetInteger("transition", 2);



            yield return new WaitForSeconds(1.5f);

            anim.SetInteger("transition", 0);
            anim.SetBool("attacking", false);
            isBeating = false;
        }
    }
}
