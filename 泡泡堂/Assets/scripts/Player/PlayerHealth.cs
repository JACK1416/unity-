using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool dead = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Explosion"))
        {
            gameObject.SetActive(false);
            dead = true;
        }
    }
}
