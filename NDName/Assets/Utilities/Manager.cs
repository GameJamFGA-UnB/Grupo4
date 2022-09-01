using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    bool playerWasMoving = false;
    bool win = false;
    float clipLength;
    public ProgressBar progress;
    public bool started;
    
    void Start()
    {
        started = false;
        clipLength = AudioManager.Instance.mapSounds["Queen"].clip.length;
        progress.minimum = 0;
        progress.maximum = (int) (clipLength - (clipLength * 0.10));
        progress.current = 0;
        Debug.Log(clipLength);
    }

    // Update is called once per frame
    void Update()
    {
        if(!win && AudioManager.Instance.mapSounds["Queen"].source.time >= (clipLength - (clipLength*0.10))){
            Player.Instance.Win();
            win = true;
            Win();
        }

        if(!win)
            UpdateProgressBar();
        
        if(!win && Player.Instance.isMoving){
            if(playerWasMoving == false){
                AudioManager.Instance.Play("Queen");
                playerWasMoving = true;
            }
        } else if(!win && !Player.Instance.isMoving) {
            AudioManager.Instance.Pause("Queen");
            if(playerWasMoving){
                AudioManager.Instance.Play("Break");
                playerWasMoving = false;
            }
        }
    }

    void UpdateProgressBar(){
        progress.current = (int) AudioManager.Instance.mapSounds["Queen"].source.time;
    }

    public void Win(){
        AudioManager.Instance.Play("Queen");
        Debug.Log("Win!");
    }

    public void Lose(){
        Debug.Log("Lose!");
    }
}
