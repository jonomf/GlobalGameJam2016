using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController_HowTo_1 : MonoBehaviour {
    public static UIController_HowTo_1 instance;
    private InputDevice inputDevice;

    void Awake(){
        instance = this;
    }

    void Update() {
        //Check for A button to change scene to correct boss
        inputDevice = InputManager.ActiveDevice;
        if(Input.GetKeyDown(KeyCode.A) || inputDevice.GetControl(InputControlType.Action1).WasPressed){
            SceneManager.LoadScene("Game scene");
        }
    }
}

