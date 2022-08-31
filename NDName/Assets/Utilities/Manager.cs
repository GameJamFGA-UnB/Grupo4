using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    bool playerWasMoving = false;
    public bool pause = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Player.Instance.isMoving){
            if(playerWasMoving == false){
                AudioManager.Instance.Play("Queen");
                playerWasMoving = true;
            }
        } else {
            AudioManager.Instance.Pause("Queen");
            if(playerWasMoving){
                AudioManager.Instance.Play("Break");
                playerWasMoving = false;
            }
        }
    }
}
