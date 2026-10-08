using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class Chronometer : MonoBehaviour
{

    public TextMeshProUGUI Text;
    float time = 0;
    float mil_seconds = 0f;
    float seconds = 0f;
    float minutes = 0f;

    bool active = false;

    // Update is called once per frame
    void Update(){
        if (active){
            cronometro();
        }  
    }

    public void start_count(){
        active = true;
    }

    public void stop_count(){
        active = false;
    }

    void cronometro(){
        time += Time.deltaTime;
        //Mathf.FloorToInt Redondea el valor hacia abajo y elimina los decimales 
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);
        mil_seconds = Mathf.FloorToInt((time % 1) * 100);

        Text.text = string.Format("{0:00}:{1:00}:{2:00}",minutes,seconds,mil_seconds);
    }


}
