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

    public static IEnumerator DecreaseOnTimer() {
        while (true){
            yield return new WaitForSeconds(DECREASE_SECONDS);
            decreaseBars();
        }
    }
    
    //UPDATE 3 bars by specified bars (called by player/enemy functions)
    public static void updateBars(int d1, int d2, int d3){
        bar1Value += d1;
        bar2Value += d2;
        bar3Value += d3;

        checkEndGame();
        updateSliders();
    }

    //Decrease all bars by 1 (called at some regular interval)
    private static void decreaseBars(){
        bar1Value--;
        bar2Value--;
        bar3Value--;

        checkEndGame();
        updateSliders();
    }

    //If a bar has hit 0 or 100, end the game
    public static void checkEndGame(){
        if(bar1Value >= 100 || bar1Value <=0 || bar2Value >= 100 || bar2Value <=0 || bar3Value >= 100 || bar3Value <=0){
            GameController.Win();
        }
    }

    //updates the actual sliders
    private static void updateSliders() {
	    if (UIController_GameScene.instance == null) return; // Not in the game scene.
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
    private static void checkBuffs(){
        if(bar1Value >= BUFF_TARGET){
            //apply god1 buff
        }
        if(bar2Value >= BUFF_TARGET){
            //apply god1 buff
        }
        if(bar3Value >= BUFF_TARGET){
            //apply god1 buff
        }
    }

    //Called at end-game. There can be multiple penalties but only ONE boss
    private static void checkPenalties(){
        int bossNum = 0;
        
        //FIRST - determine the boss
        //NOTE - <= gives preference to certain bosses
        if(bar1Value <= bar2Value){
            if(bar1Value <= bar3Value){
                bossNum = 1;
            }
            else{
                bossNum = 3;
            }
        }
        else{
            if(bar2Value <= bar3Value){
                bossNum = 2;
            }
            else{
                bossNum = 3;
            }
        }

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

        //We have the bossNum - DO BOSS STUFF!!
    }
}
