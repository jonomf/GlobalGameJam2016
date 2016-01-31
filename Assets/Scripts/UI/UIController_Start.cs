using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController_Start : MonoBehaviour {
    public static UIController_Start instance;
    private InputDevice inputDevice;

	void Awake(){
        instance = this;
    }

    void Update() {
        //Check for A button to change scene to correct boss
        inputDevice = InputManager.ActiveDevice;
        if(Input.GetKeyDown(KeyCode.A) || inputDevice.GetControl(InputControlType.Action1).WasPressed){
            SceneManager.LoadScene("HowToPlay_1");
        }
    }
}
