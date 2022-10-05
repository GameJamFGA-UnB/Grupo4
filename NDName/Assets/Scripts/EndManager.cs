using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndManager : MonoBehaviour
{
    const string ShowKey = "Show";
    const string HideKey = "Hide";
    private bool win;
    public Panel panel;
    public float timer;
    public GameObject bgWin;
    public GameObject bgLose;
    public Text playAgain;
    public VideoPlayer videoPlayer;
    bool videoPlayed = false;
    long frames;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
        frames = (long) videoPlayer.frameCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.Instance.state == 3){
            if(win){
                if(!videoPlayed){
                    videoPlayer.Play();
                    videoPlayed = true;
                }
                if(videoPlayed && videoPlayer.frame > (frames - 5)){
                    videoPlayer.Stop();
                    videoPlayed = false;
                    Manager.Instance.Reset();
                }
                Debug.Log("Win");
            }else{
                timer = timer + Time.deltaTime;
                if(timer >= 0.5){
                        playAgain.enabled = true;
                }
                if(timer >= 1){
                        playAgain.enabled = false;
                        timer = 0;
                }
                if(Input.GetKeyDown(KeyCode.Return)){
                    Manager.Instance.Restart();
                    Hide();
                }
            }
        }
    }

    void TogglePos(string pos){
        Tweener t = panel.SetPosition(pos, true);
        t.duration = 1f;
        t.equation = EasingEquations.EaseOutQuad;
    }

    public void Show(bool _win){
        win = _win;
        bgWin.SetActive(win);
        bgLose.SetActive(!win);
        TogglePos(ShowKey);
    }

    public void Hide(){
        TogglePos(HideKey);
    }
}
