using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//manages "happiness" bars for the 3 gods
public class GodManager : MonoBehaviour {

	public static int bar1Value = 50;//Aggression
    public static int bar2Value = 50;//Serenity
    public static int bar3Value = 50;//Collection

    public const int BUFF_TARGET = 75;
    public const int HATE_TARGET = 25;

    public const float DECREASE_SECONDS = 3;

    private bool stopChange = false;

    public static bool applyBuff1 = false;
    public static bool applyBuff2 = false;
    public static bool applyBuff3 = false;

    public static bool applyPenalty1 = false;
    public static bool applyPenalty2 = false;
    public static bool applyPenalty3 = false;

    public static IEnumerator DecreaseOnTimer() {
        while (true){
            yield return new WaitForSeconds(DECREASE_SECONDS);
            decreaseBars();
        }
    }
    
    //UPDATE 3 bars by specified bars (called by player/enemy functions)
    public static void updateBars(int d1, int d2, int d3){
		if (SceneManager.GetActiveScene().name != "Game scene") return;
        bar1Value += d1;
        bar2Value += d2;
        bar3Value += d3;

	    if (bar1Value < HATE_TARGET || bar2Value < HATE_TARGET || bar3Value < HATE_TARGET){
		    AudioController.instance.godWarning.Play();
	    }

		DoBarChangePopup(d1, d2, d3);
        checkEndGame();
        updateSliders();
    }

	private static void DoBarChangePopup(int dAggro, int dSerenity, int dGreed) {
		int popupNum = 0;
		if (dAggro > 0) {
			// NB: For some reason, using a variable for the color doesn't work.
			FeedbackPopup.DoPopup("+", Color.red, popupNum++);
		}
		else if (dAggro < 0) {
			FeedbackPopup.DoPopup("-", Color.red, popupNum++);
		}
		if (dSerenity > 0) {
			FeedbackPopup.DoPopup("+", Color.blue, popupNum++);
		}
		else if (dSerenity< 0) {
			FeedbackPopup.DoPopup("-", Color.blue, popupNum++);
		}
		if (dGreed> 0) {
			FeedbackPopup.DoPopup("+", Color.green, popupNum++);
		}
		else if (dGreed < 0) {
			FeedbackPopup.DoPopup("-", Color.green, popupNum++);
		}
	}

    //Decrease all bars by 1 (called at some regular interval)
    public static void decreaseBars(){
        if (SceneManager.GetActiveScene().name != "Game scene") return;
		updateBars(0, 0, -3);
    }

    //If a bar has hit 0 or 100, end the game
    public static void checkEndGame(){
        if(bar1Value >= 100 || bar1Value <=0 || bar2Value >= 100 || bar2Value <=0 || bar3Value >= 100 || bar3Value <=0){
            GameController.Transition();
        }
    }

    //updates the actual sliders
    private static void updateSliders() {
	    if (SceneManager.GetActiveScene().name != "Game scene") return;
        //create local copies
        int val1 = bar1Value;
        int val2 = bar2Value;
        int val3 = bar3Value;

        //clamp to 0-100
        if(val1 > 100) val1 = 100;
        if(val1 < 0) val1 = 0;
        if(val2 > 100) val2 = 100;
        if(val2 < 0) val2 = 0;
        if(val3 > 100) val3 = 100;
        if(val3 < 0) val3 = 0;

        //update!
        UIController_GameScene.instance.boss1Slider.value = val1;
        UIController_GameScene.instance.boss2Slider.value = val2;
        UIController_GameScene.instance.boss3Slider.value = val3;
    }

    //Called at end-game. Apply buffs (not mutually exclusive)
    public static void checkBuffs(int bossNum){
        applyBuff1 = bar1Value >= BUFF_TARGET && bossNum != 1;
        applyBuff2 = bar2Value >= BUFF_TARGET && bossNum != 2;
        applyBuff3 = bar3Value >= BUFF_TARGET && bossNum != 3;
    }

    //Called at Transition - gets boss NUM based on transition values
    public static int getBossNum(){
        //NOTE - <= gives preference to certain bosses
        if(bar1Value <= bar2Value){
            if(bar1Value <= bar3Value){
                return 1;
            }
            else{
                return 3;
            }
        }
        else{
            if(bar2Value <= bar3Value){
                return 2;
            }
            else{
                return 3;
            }
        }
    }  

    //There can be multiple penalties but only one Boss
    private static void checkPenalties(){
        int bossNum = getBossNum(); //TEMP!!

        //NEXT - determine any extra penalties 
        if(bar1Value < HATE_TARGET && bossNum != 1){
            //Do secondary penalty

        }
        if(bar2Value < HATE_TARGET && bossNum != 2){
            //Do secondary penalty
        }
        if(bar3Value < HATE_TARGET && bossNum != 3){
            //Do secondary penalty
        }
    }
}
