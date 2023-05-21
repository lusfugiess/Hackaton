using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopeyDamage : MonoBehaviour
{
    public int initialHP = 50;
    public float damageRadius = 3f;
    public float damageInterval = 5f;

    public int currentHP;
    private bool canTakeDamage = true;
    private bool isPopeyAlive = true;


    public TextMeshProUGUI livesText;
    public TextMeshProUGUI deathText;



    void Start()
    {
        currentHP = initialHP;

    }

    void Update()
    {
        if (isPopeyAlive)
        {
            if (canTakeDamage)
            {
                CheckSmallObjectsCollisions();
                CheckMediumObjectsCollisions();
                CheckLargeObjectsCollisions();
            }
        }
    }

    private void CheckSmallObjectsCollisions()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("SmallObjects");

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);

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
    private void CheckMediumObjectsCollisions()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MediumObjects");

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);

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
    private void CheckLargeObjectsCollisions()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("LargeObjects");

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            if (distance <= damageRadius)
            {
                for (int i = 0; i <= 30; i++)
                {
                    TakeDamage();
                }
                break;
            }
        }
    }

    private void TakeDamage()
    {
        if (!isPopeyAlive)
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("SmallObjects") ||
                collider.gameObject.CompareTag("MediumObjects") ||
                collider.gameObject.CompareTag("LargeObjects"))
            {
                Destroy(collider.gameObject);
            }
        }
    }

    private void EnableDamage()
    {
        canTakeDamage = true;
    }

    private void Die()
    {
        isPopeyAlive = false;
        Disapire();
    }

    private void Disapire()
    {
        GameObject[] smallObjects = GameObject.FindGameObjectsWithTag("SmallObjects");
        GameObject[] mediumObjects = GameObject.FindGameObjectsWithTag("MediumObjects");
        GameObject[] largeObjects = GameObject.FindGameObjectsWithTag("LargeObjects");

        foreach (GameObject obj in smallObjects)
        {
            Destroy(obj);
        }

        foreach (GameObject obj in mediumObjects)
        {
            Destroy(obj);
        }

        foreach (GameObject obj in largeObjects)
        {
            Destroy(obj);
        }
    }

    private void UpdateHPText()
    {
        livesText.text = "Popey HP: " + currentHP;
    }

    
}