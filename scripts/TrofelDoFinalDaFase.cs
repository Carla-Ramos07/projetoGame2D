using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrofelDoFinalDaFase : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other)
   {
        if(other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().RodarCoroutinePassarDeFase();
        }
   }
}
