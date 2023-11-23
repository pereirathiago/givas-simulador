using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável pelo controle do player do projeto
/// </summary>
public class Player : MonoBehaviour
{
    //variáveis de atributos do player
    private CharacterController controller;
    public float totalHealth;
    public float speed;
    public float gravity;

    //componentes
    private Animator anim;
    private Transform cam;

    //variáveis de controle
    Vector3 moveDirection;

    private bool isWalking;

    public bool isDead;

    public float smoothRotTime;
    private float turnSmoothVelocity;

    public float colliderRadius;


    // Start é chamado uma vez antes do primeiro frame 
    void Start()
    {
        Cursor.visible = false;
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    // Update é chamado a cada frame
    void Update()
    {
        //se o player não estiver morto...
        if (!isDead && !anim.GetBool("dancing-twerk"))
        {
            //metodo que o permite mover
            Move();
        }

    }

    /// <summary>
    /// Método de movimentação do player
    /// </summary>
    void Move()
    {
        //se o player estiver tocando o chão
        if (controller.isGrounded)
        {
            //pega o input horizontal (teclas direita/esquerda)
            float horizontal = Input.GetAxisRaw("Horizontal");
            //pega o input vertical (teclas cima/baixo)
            float vertical = Input.GetAxisRaw("Vertical");

            //variavel local que armazena os eixos horizontal e vertical
            Vector3 direction = new Vector3(horizontal, 0f, vertical);

            //verifica se o personagem está movimentando (caso o input seja maior que zero)
            if (direction.magnitude > 0)
            {
                    //variavel local que armazena a rotaçao e o angulo de vizualicao da camera
                    float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    //variavel local que armazena a rotacao porem mais suave
                    float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, smoothRotTime);

                    //passamos a rotacao suave ao personagem
                    transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                    //armazena direcao com base na direcao do mouse
                    moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * speed;

                    //muda a animação para andando
                    anim.SetInteger("transition", 1);
                    anim.SetBool("walking", true);

                    isWalking = true;

            }
            else if (isWalking)
            {
                //é executado quando o player está parado

                anim.SetBool("walking", false);
                anim.SetInteger("transition", 0);
                moveDirection = Vector3.zero;

                isWalking = false;
            }

        }

        //adiciona gravidade ao player
        moveDirection.y -= gravity * Time.deltaTime;

        //move o personagem
        controller.Move(moveDirection * Time.deltaTime);

    }
}
