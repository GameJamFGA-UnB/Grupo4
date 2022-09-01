using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObstacle : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Debug.Log("EndGame");
            Player.Instance.Die();
        }
    }
}
