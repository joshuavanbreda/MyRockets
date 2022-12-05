using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrigger : MonoBehaviour
{
    public GameObject explosion1;
    private GameObject currentExplosion;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            currentExplosion = Instantiate(explosion1, transform.position, Quaternion.identity, null);
            Destroy(currentExplosion, 10f);
        }
    }
}
