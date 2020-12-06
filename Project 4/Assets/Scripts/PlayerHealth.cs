using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//for enemy to affect player health: playerHealth.TakeDamage(hitAttack);

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    private CapsuleCollider cap;

    public Slider healthSlider;
    public Image damageImage;

   // public AudioClip deathClip;
    public float flashSpeed = 2f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    //PlayerController playerCont;
    public bool isDead;
    bool damaged;

    float timer;
    void Start()
    {
        anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        //playerCont = GetComponent<PlayerController>();
        currentHealth = startingHealth;
        cap = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false; //reset damaged flag
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;

        healthSlider.value = currentHealth;
        //playerAudio.Play();

        if (currentHealth <= 0 /*&& !isDead*/)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        //playerCont.enabled = false;
        cap.enabled = false;

        Debug.Log("death");
        StartCoroutine(TitleScreen());
    }

    IEnumerator TitleScreen()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Title Scene");
    }

}
