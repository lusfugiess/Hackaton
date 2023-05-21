using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDamage : MonoBehaviour
{
   public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;

    void Update()
    {
        if (Input.GetButtonDown("RX Rig Advanced")) // Replace with the appropriate input for VR controllers
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            // Check if the object can take damage (you can use tags or components to identify damageable objects)
            DamageableObject damageableObject = hit.transform.GetComponent<DamageableObject>();
            if (damageableObject != null)
            {
                damageableObject.TakeDamage(damage);
            }

            // Apply impact force to rigidbodies
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(transform.forward * impactForce, ForceMode.Impulse);
            }
        }
    }
}