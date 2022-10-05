using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    const string ShowKey = "Show";
    const string HideKey = "Hide";
    public Panel panel;
    public float timer;
    public Text continueText;
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.Instance.state == 1 && !videoPlayer.isPlaying)
            videoPlayer.Play();
        else if(Manager.Instance.state != 1 && videoPlayer.isPlaying)
            videoPlayer.Stop();

        if(Manager.Instance.state == 1){
                timer = timer + Time.deltaTime;
                if(timer >= 0.5){
                    continueText.enabled = true;
                }
                if(timer >= 1){
                    continueText.enabled = false;
                    timer = 0;
                }
                if(Input.GetKeyDown(KeyCode.Return)){
                    Manager.Instance.state++;
                    Hide();
                }
        }
    }

    void TogglePos(string pos){
        Tweener t = panel.SetPosition(pos, true);
        t.duration = 1f;
        t.equation = EasingEquations.EaseOutQuad;
    }

    public void Show(){
        TogglePos(ShowKey);
    }

    public void Hide(){
        TogglePos(HideKey);
        AudioManager.Instance.Stop("Intro");
    }
}
