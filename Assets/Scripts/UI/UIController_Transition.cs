using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController_Transition : MonoBehaviour {
	public static UIController_Transition instance;

    private InputDevice inputDevice;

    public Text aggressionText;
    public Text serenityText;
    public Text greedText;

    public Text bossNameText;

    private int bossNum;

    //private string originalText;

    void Awake() {
        instance = this;

        displayStats();
    }

    void displayStats(){
        //SET boss num based on stats
        bossNum = GodManager.getBossNum();
        Debug.Log("Boss Num: " + bossNum);

        //set buff booleans (for later)
        GodManager.checkBuffs(bossNum);

        //get values
        int val1 = GodManager.bar1Value;
        int val2 = GodManager.bar2Value;
        int val3 = GodManager.bar3Value;

        //set secondary strings based on buffs
        string buffStr1 = "";
        string buffStr2 = "";
        string buffStr3 = "";
        if(GodManager.applyBuff1){
            buffStr1 = "(BUFF ATTACK!)";
        }
        if(GodManager.applyBuff2){
            buffStr2 = "(BUFF DEFENSE!)";
        }
        if(GodManager.applyBuff3){
            buffStr3 = "(BUFF SPEED!)";
        }

        //set text
        aggressionText.text = string.Format("aggression: {0} {1}",val1,buffStr1);
        serenityText.text = string.Format("serenity: {0} {1}",val2,buffStr2);
        greedText.text = string.Format("greed: {0} {1}",val3,buffStr3);

        if(bossNum == 1) bossNameText.text = "Boss: Aggression";
        else if(bossNum == 2) bossNameText.text = "Boss: Serenity";
        else if(bossNum == 3) bossNameText.text = "Boss: Greed";
    }

    void Update() {
        //Check for A button to change scene to correct boss
        inputDevice = InputManager.ActiveDevice;
        if(Input.GetKeyDown(KeyCode.A) || inputDevice.GetControl(InputControlType.Action1).WasPressed){
            if(bossNum == 1){
                SceneManager.LoadScene("Boss - Aggression");
            }
            else if(bossNum == 2){
                SceneManager.LoadScene("Boss - Serenity");
            }
            else{
                //Load Greed!
                //SceneManager.LoadScene("Transition");
            }
        }
    }
}
