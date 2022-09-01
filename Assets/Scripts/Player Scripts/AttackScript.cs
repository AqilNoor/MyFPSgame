using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float damage = 2f;
    public float radius = 1f;
    public LayerMask layerMask;

    void Update()
    {
        // this code will creat a sphere of given radius at given position
        // // and detect the collision layermask which is equal to Enemy
        // any gameobject which has Enemy Layer
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        // it detects at least on collision
        if(hits.Length > 0)
        {
             hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);
            // deactivate that gameobject
            gameObject.SetActive(false);
        }
    }

}
