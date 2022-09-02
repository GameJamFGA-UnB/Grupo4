using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.Instance.state == 2){
            if(win){
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
