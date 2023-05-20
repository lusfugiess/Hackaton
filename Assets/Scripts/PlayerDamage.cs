using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
            }
        }
    }

    private void CheckPopeCollisions()
    {
        GameObject[] popes = GameObject.FindGameObjectsWithTag("pope");

        foreach (GameObject pope in popes)
        {
            float distance = Vector3.Distance(transform.position, pope.transform.position);

            if (distance <= damageRadius)
            {
                for (int i = 0; i <= 15; i++)
                {
                    TakeDamage();
                }
                break;
            }
        }
    }

    private void CheckPopeyCollisions()
    {
        GameObject[] popeys = GameObject.FindGameObjectsWithTag("popey");

        foreach (GameObject popey in popeys)
        {
            float distance = Vector3.Distance(transform.position, popey.transform.position);

            if (distance <= damageRadius)
            {
                for (int i = 0; i <= 5; i++)
                {
                    TakeDamage();
                }
                break;
            }
        }
    }

    private void CheckPopusMagnifikusCollisions()
    {
        GameObject[] PopusMagnifikuss = GameObject.FindGameObjectsWithTag("popusMagnifikus");

        foreach (GameObject PopusMagnifikus in PopusMagnifikuss)
        {
            float distance = Vector3.Distance(transform.position, PopusMagnifikus.transform.position);

            if (distance <= damageRadius)
            {
                for (int i = 0; i <= 50; i++)
                {
                    TakeDamage();
                }
                break;
            }
        }
    }
   

    private void TakeDamage()
    {
        if (!isPlayerAlive)
        {
            return;
        }

        currentHP--;
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
        isPlayerAlive = false;
        deathText.text = "Player has died!";
    }

    private void UpdateHPText()
    {
        livesText.text = "Current HP: " + currentHP;
    }


}