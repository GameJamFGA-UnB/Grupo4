using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    const string ShowKey = "Show";
    const string HideKey = "Hide";

    public Panel panel;
    public Image startOut;
    public Image quitOut;
    Image[] selection;
    int selectionIdx;

    void Start(){
        selectionIdx = 0;
        selection = new Image[2];
        selection[0] = startOut;
        selection[1] = quitOut;
        selection[0].gameObject.SetActive(true);
        selection[1].gameObject.SetActive(false);
        Show();
    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.Instance.state == 0){
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
                selection[selectionIdx].gameObject.SetActive(false);
                selectionIdx = (selectionIdx - 1 < 0? 1 : 0);
                selection[selectionIdx].gameObject.SetActive(true);
            }
            if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
                selection[selectionIdx].gameObject.SetActive(false);
                selectionIdx = (selectionIdx + 1 > 1? 0 : 1);
                selection[selectionIdx].gameObject.SetActive(true);
            }
            if(Input.GetKeyDown(KeyCode.Return)){
                if(selectionIdx == 0){
                    Debug.Log("Start");
                    Manager.Instance.state++;
                    Manager.Instance.tutorialManager.Show();
                }
                else{
                    Debug.Log("Quit");
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #else
                    Application.Quit();
                    #endif
                }
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
    }
}
