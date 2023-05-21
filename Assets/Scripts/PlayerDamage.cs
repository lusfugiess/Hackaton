using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public int initialHP = 300;
    public float damageRadius = 1f;
    public float damageInterval = 15f;

    public int currentHP;
    private bool canTakeDamage = true;
    private bool isPlayerAlive = true;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI deathText;

    void Start()
    {
        currentHP = initialHP;
        
    }

    void Update()
    {
        if (isPlayerAlive) 
        {
            if (canTakeDamage) 
            {
                CheckPopeCollisions();
                CheckPopeyCollisions();
                CheckPopusMagnifikusCollisions();
                CheckIfEnemiesAlive();
            }
        }
    }

    public AudioSource audioSource;
    public AudioClip hurtClip;
    public AudioClip deadClip;
    public AudioClip winClip;
    public float volume = 0.5f;
    void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, volume);
    }
    public bool winGame = false;
    private void CheckIfEnemiesAlive()
    {
        int aliveEnemies = 0;
        aliveEnemies += GameObject.FindGameObjectsWithTag("popey").Length;
        aliveEnemies += GameObject.FindGameObjectsWithTag("pope").Length;
        aliveEnemies += GameObject.FindGameObjectsWithTag("popusMagnifikus").Length;
        if (aliveEnemies == 0 && winGame == false)
        {
            PlayAudio(winClip);
            livesText.SetText("You have won!");
            winGame = true;
            SceneManager.LoadScene("introMenu");
        }
    }

    private void CheckCollisionsEnemy(GameObject[] enemies, int damage)
    {
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= damageRadius)
            {
                TakeDamage(damage);
            }
        }
    }

    private void CheckPopeCollisions()
    {
        CheckCollisionsEnemy(GameObject.FindGameObjectsWithTag("pope"), 15);
    }

    private void CheckPopeyCollisions()
    {
        CheckCollisionsEnemy(GameObject.FindGameObjectsWithTag("popey"), 5);
    }

    private void CheckPopusMagnifikusCollisions()
    {
        CheckCollisionsEnemy(GameObject.FindGameObjectsWithTag("popusMagnifikus"), 50);
    }
   

    private void TakeDamage(int damage)
    {
        //if (hurtClip == null){Debug.Break();}
        PlayAudio(hurtClip);

        if (!isPlayerAlive)
        {
            return;
        }

        currentHP = currentHP - damage;
        canTakeDamage = false;
        UpdateHPText();

        if (currentHP <= 0)
        {
            Die();
        }
        else 
        {
            Invoke("EnableDamage", damageInterval);
        }
    }

    private void EnableDamage()
    {
        canTakeDamage = true;
    }

    private void Die()
    {
        PlayAudio(deadClip);
        isPlayerAlive = false;
        deathText.text = "Player has died!";
    }

    private void UpdateHPText()
    {
        livesText.text = "Current HP: " + currentHP.ToString();
    }
}