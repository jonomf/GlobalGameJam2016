using UnityEngine;

//manages "happiness" bars for the 3 gods
public class GodManager {

    //TODO: connect system to actual UNITY bars

	public static int bar1Value = 50;//Aggression
    public static int bar2Value = 50;//Serenity
    public static int bar3Value = 50;//Collection
    
    //UPDATE 3 bars by specified bars (called by player/enemy functions)
    public static void updateBars(int d1, int d2, int d3){
        bar1Value += d1;
        bar2Value += d2;
        bar3Value += d3;

        clampBars();
        checkEndGame();
    }

    //Decrease all bars by 1 (called at some regular interval)
    public static void decreaseBars(){
        bar1Value--;
        bar2Value--;
        bar3Value--;

        clampBars();
    }

    //clamps all bar values to 0-100 range
    private static void clampBars(){
        if(bar1Value > 100){
            bar1Value = 100;
        }
        if(bar1Value <0){
            bar1Value = 0;
        }
        if(bar2Value > 100){
            bar2Value = 100;
        }
        if(bar2Value <0){
            bar2Value = 0;
        }
        if(bar3Value > 100){
            bar3Value = 100;
        }
        if(bar3Value <0){
            bar3Value = 0;
        }
    }

    /*void Update(){
        checkEndGame();
    }*/

    private static void checkEndGame(){
        if(bar1Value == 0 || bar2Value == 0 || bar3Value == 0){
            //TODO: Throw an event to end the game!!
        }
    }
}
