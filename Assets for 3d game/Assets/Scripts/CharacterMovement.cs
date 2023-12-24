using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class CharacterMovement : MonoBehaviour
{

    

    public float speed=5;
    
    Transform cam;

    Animator anim;

    CharacterController Controller;

    float gravity = 10;

    float verticalVelocitey = 10;
    
    public float jumpValue = 7;

    CharacterStats stats;


    void Start()
    {
        Controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
        stats = GetComponent<CharacterStats>();

    }

   
    void Update()
    {
       
        float horizontal = Input.GetAxis("Horizontal");
        
        float vertical = Input.GetAxis("Vertical");

        bool isSprint = Input.GetKey(KeyCode.LeftShift);

        bool IsMove = (horizontal > 0 || vertical > 0);

      //  float sprint = (isSprint ) ? 2.5f : 1;

        float sprint = (isSprint && IsMove) ? 2.5f : 1;


        if (Input.GetMouseButtonDown(0) )
        {
            anim.SetTrigger("Attack");
        }

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        //anim.SetFloat("Speed", Mathf.Clamp(moveDirection.magnitude, 0, 0.5f) + (isSprint  ? 0.5f : 0));

        anim.SetFloat("Speed", Mathf.Clamp(moveDirection.magnitude, 0, 0.5f) + ((isSprint && IsMove) ? 0.5f : 0));


        if (Controller.isGrounded)
        {
            if (Input.GetAxis("Jump") > 0 ) 
            { 
                 verticalVelocitey = jumpValue; 
            }
        }
        else
        {
            verticalVelocitey-= gravity*Time.deltaTime;
        }
        
        
        if (moveDirection.magnitude > 0.1f )
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        moveDirection=cam.TransformDirection(moveDirection);

        moveDirection = new Vector3(moveDirection.x * speed * sprint, verticalVelocitey, moveDirection.z * speed * sprint);

        Controller.Move(moveDirection * Time.deltaTime );
        
    }








    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            GetComponent<CharacterStats>().ChangeHealth(20);
            LevelManager.instance.PlaySound(LevelManager.instance.levelSound[1], LevelManager.instance.player.position);
            Instantiate(LevelManager.instance.Particles[1],other.transform.position,other.transform.rotation);

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Item"))
        {
            LevelManager.instance.levelItem++;
            GetComponent<CharacterStats>().ChangelevelItem();
            Debug.Log(LevelManager.instance.levelItem);

            LevelManager.instance.PlaySound(LevelManager.instance.levelSound[2], LevelManager.instance.player.position);
            Instantiate(LevelManager.instance.Particles[0], other.transform.position, other.transform.rotation);
            
            Destroy(other.gameObject);
            
            if (LevelManager.instance.levelItem >= 5 )
            {
                LevelManager.instance.levelItem = 0;
                if (SceneManager.GetActiveScene().name == "level1")
                {
                    LevelManager.instance.winlevel1();
                }else if (SceneManager.GetActiveScene().name == "level2")
                {
                    LevelManager.instance.win();

                }
                else
                {
                    LevelManager.instance.win();
                }
            }
            
            
        }
  
    }







    public void DoAttack()
    {
        transform.Find("Colider").GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(HideCollider());
    }





    IEnumerator HideCollider()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Find("Colider").GetComponent<BoxCollider>().enabled = false;
    }
}
