using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowObstacle : MonoBehaviour
{
    [SerializeField]
    int weight = 3;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Debug.Log("TriggerEnter on" + this.name);
            Player.Instance.UpdateSpeed(-weight);
            Player.Instance._playerAnim.SetTrigger("Stumble");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Player.Instance.UpdateSpeed(weight);
        }
    }
}
