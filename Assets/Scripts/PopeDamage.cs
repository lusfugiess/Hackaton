using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopeDamage : MonoBehaviour
{
    public int initialHP = 100;
    public float damageRadius = 3f;
    public float damageInterval = 5f;

    public int currentHP;
    private bool canTakeDamage = true;
    private bool isPopeAlive = true;


    void Start()
    {
        currentHP = initialHP;

    }

    void Update()
    {
        if (isPopeAlive)
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
            if (!isPopeAlive)
            {
                return;
            }

            currentHP--;
            canTakeDamage = false;

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
        isPopeAlive = false;
        Disapire();
    }

    private void Disapire()
    {

    }
}