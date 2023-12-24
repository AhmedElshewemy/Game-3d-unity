using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform player;

    public int score;
    public int levelItem=0;

    public AudioClip[] levelSound;

   public Transform[] Particles;

    public Transform MainCanvas;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       // Cursor.visible = false;
        //Cursor.lockState= CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip sound,Vector3 ownerPos) 
    {
        GameObject obj =SoundFXPooler.current.GetpooledObject();
        AudioSource audio = obj.GetComponent<AudioSource>();

        obj.transform.position = ownerPos;
        obj.SetActive(true);
        audio.PlayOneShot(sound);
        StartCoroutine(DisableSound(audio));
    }

    IEnumerator DisableSound(AudioSource audio)
    {
        while (audio.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }
        audio.gameObject.SetActive(false);
    }

    public void loadslevel1()
    {
        Cursorfalse();
        SceneManager.LoadScene("level1");
    }
    public void winlevel1()
    {
        CursorTrue();
        SceneManager.LoadScene("winAndNextLevel");
    }

    public void loadslevel2()
    {
        Cursorfalse();
        SceneManager.LoadSceneAsync("level2");
    }
    public void exitt()
    {
        CursorTrue();
        Application.Quit();
    }
    public void win()
    {
        CursorTrue();
        SceneManager.LoadScene("win");
    }
    public void loadmain()
    {
        CursorTrue();
        SceneManager.LoadScene("main_menu");
    }
    public void gameOver()
    {
        CursorTrue();
        
        SceneManager.LoadScene("gameover");
    }





    public void CursorTrue()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Cursorfalse()
    {
        Cursor.visible = false;
        Cursor.lockState= CursorLockMode.Locked;
    }
}
