using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEevents : MonoBehaviour
{
    public CharacterMovement charmove;


    public void PlayerAttack()
    {
        Debug.Log("player attacked");
        charmove.DoAttack();
    }

    public void PlayerDamage()
    {
        transform.GetComponentInParent<EnemyController>().DamagePlayer();
    }
    public void MoveSound()
    {
        LevelManager.instance.PlaySound(LevelManager.instance.levelSound[0], LevelManager.instance.player.position);
    }
    
}
