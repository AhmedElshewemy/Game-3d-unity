using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    CharacterStats stats;
    //public Transform player;
    NavMeshAgent agent;

    Animator anim;

    public float attckRaduis = 8;

    bool canAttack = true;
    float attckCoolDown=2f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
        //float distance =Vector3.Distance(transform.position, LevelManager.instance.player.position);

        Vector2 pos1 = new Vector2(transform.position.x, transform.position.z);
        Vector2 pos2 = new Vector2(LevelManager.instance.player.position.x, LevelManager.instance.player.position.z);
        float distance = Vector2.Distance(pos1, pos2);


        if (distance < attckRaduis)
        {
            agent.SetDestination(LevelManager.instance.player.position);
            if (distance<=agent.stoppingDistance)
            {
                if (canAttack)
                {
                    StartCoroutine(cooldown());
                    anim.SetTrigger("Attack");
                    

                }
            }
        
        }
    }
    IEnumerator cooldown()
    {
        canAttack= false;
        yield return new WaitForSeconds(attckCoolDown);
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            stats.ChangeHealth(-other.GetComponentInParent<CharacterStats>().power);
        }
        
    }

    public void DamagePlayer()
    {
        LevelManager.instance.player.GetComponent<CharacterStats>().ChangeHealth(-stats.power);
    }
}
