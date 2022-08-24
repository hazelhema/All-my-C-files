using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerLogic playerLogic = other.GetComponent<PlayerLogic>();
            if(playerLogic)
            {
                playerLogic.TakeDamage(20);
            }
        }
    }
}
