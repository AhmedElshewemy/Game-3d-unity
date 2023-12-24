using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class CharacterStats : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    float maxHealth = 100;
    public float power = 10;
    int killScore = 200;
     

    public float currentHealth { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();

    }

    public void ChangelevelItem()
    {
        LevelManager.instance.MainCanvas.Find("PanelStats").Find("Score").GetComponent<TextMeshProUGUI>().text = string.Format("{0}/5", LevelManager.instance.levelItem);

    }

    public void ChangeHealth(float value)
    {
        // currentHealth += value;

      
        currentHealth =Mathf.Clamp(currentHealth+value, 0, maxHealth);
        
        Debug.Log("current health" + currentHealth + "/" + maxHealth);


       if (transform.CompareTag("Enemy"))
         {
            transform.Find("Canvas").GetChild(1).GetComponent<UnityEngine.UI.Image>().fillAmount = currentHealth / maxHealth;
         }else if (transform.CompareTag("Player"))
        {
            LevelManager.instance.MainCanvas.Find("PanelStats").Find("ImageHealthBar").GetComponent<UnityEngine.UI.Image>().fillAmount = currentHealth / maxHealth;
            LevelManager.instance.MainCanvas.Find("PanelStats").Find("TextHealth").GetComponent<TextMeshProUGUI>().text=string.Format("{0:0.##}%",(currentHealth/maxHealth)*100);

        }


        if (currentHealth <=0) 
        {
            Die();
        }
    }


    



    void Die()
    {
        if (transform.CompareTag("Player"))
        {
            //game over
            anim.SetTrigger("Die");
            StartCoroutine(HideCollider());
            LevelManager.instance.gameOver();
           
        }
        else if(transform.CompareTag("Enemy"))
        {
            LevelManager.instance.score += killScore;
            Destroy(gameObject);
            //destory enemy
            Instantiate(LevelManager.instance.Particles[2], transform.position, transform.rotation);

        }
    }

    IEnumerator HideCollider()
    {
        yield return new WaitForSeconds(2f);
        
    }


}
