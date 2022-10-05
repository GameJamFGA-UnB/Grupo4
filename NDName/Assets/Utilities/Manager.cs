using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    bool playerWasMoving = false;
    bool win = false;
    float clipLength;
    public ProgressBar progress;
    public MenuManager menuManager;
    public TutorialManager tutorialManager;
    public EndManager endManager;
    public int state;
    public Vector3 initialPlayerPos;
    public Vector3 initialFanPos;
    
    void Start()
    {
        state = 0;
        clipLength = AudioManager.Instance.mapSounds["Queen"].clip.length;
        progress.minimum = 0;
        progress.maximum = (int) (clipLength - 35);
        progress.current = 0;
        Debug.Log("Clip lenght: " + clipLength);
        initialPlayerPos = Player.Instance.transform.position;
        initialFanPos = Fan.Instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!win && AudioManager.Instance.mapSounds["Queen"].source.time >= (clipLength - 35)){
            Player.Instance.Win();
            Win();
        }

        if(!win)
            UpdateProgressBar();
        
        if(!win && Player.Instance.isMoving){
            if(playerWasMoving == false){
                if(AudioManager.Instance.mapSounds["Queen"].source.isPlaying)
                    AudioManager.Instance.UnPause("Queen");
                else AudioManager.Instance.Play("Queen");
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
        if(AudioManager.Instance.mapSounds["Queen"].source.isPlaying)
            AudioManager.Instance.UnPause("Queen");
        else AudioManager.Instance.Play("Queen");
        win = true;
        endManager.Show(win);
        state++;
        Debug.Log("Win!");
    }

    public void Lose(){
        win = false;
        endManager.Show(win);
        state++;
        Debug.Log("Lose!");
    }

    public void Restart(){
        AudioManager.Instance.Stop("Queen");
        Player player = Player.Instance;
        player._rigid.velocity = Vector2.zero;
        player.transform.position = initialPlayerPos;
        player.isMoving = false;
        player.isDead = false;
        player.isWinner = false;
        player._playerAnim.SetTrigger("Idle");
        Fan.Instance.transform.position = initialFanPos;
        state = 2;
    }

    public void Reset(){
        AudioManager.Instance.Stop("Queen");
        Player player = Player.Instance;
        player._rigid.velocity = Vector2.zero;
        player.transform.position = initialPlayerPos;
        player.isMoving = false;
        player.isDead = false;
        player.isWinner = false;
        player._playerAnim.SetTrigger("Idle");
        Fan.Instance.transform.position = initialFanPos;
        menuManager.Show();
        tutorialManager.Hide();
        endManager.Hide();
        state = 0;
    }
}
