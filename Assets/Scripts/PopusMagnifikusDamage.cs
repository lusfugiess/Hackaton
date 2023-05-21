using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PopusMagnifikusDamage : MonoBehaviour
{
    public int initialHP = 50;
    public float damageRadius = 3f;
    public float damageInterval = 5f;

    public int currentHP;
    private bool canTakeDamage = true;
    private bool isPopusMagnifikusAlive = true;


    public TextMeshProUGUI livesText;
    public TextMeshProUGUI deathText;

    public Transform playerTransform;
    private NavMeshAgent navMeshAgent;


    void Start()
    {
        currentHP = initialHP;
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        if (isPopusMagnifikusAlive)
        {
            if (canTakeDamage)
            {
                CheckSmallObjectsCollisions();
                CheckMediumObjectsCollisions();
                CheckLargeObjectsCollisions();

                if (playerTransform != null)
                {
                    navMeshAgent.SetDestination(playerTransform.position);
                }
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
        if (!isPopusMagnifikusAlive)
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
        isPopusMagnifikusAlive = false;
        Disapire();
    }

   private void Disapire()
    {
        gameObject.SetActive(false);
    }
    private void UpdateHPText()
    {
        livesText.text = "PopusMagnifikus HP: " + currentHP.ToString();
    }


}