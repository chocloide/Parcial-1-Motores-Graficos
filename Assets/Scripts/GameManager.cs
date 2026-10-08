using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController Jugador;
    public Chronometer Cronometro;

    bool Started = false;

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Space) && Started == false){
            Started = true;
            ComenzarJuego();
            
        }
    }

    void ComenzarJuego(){
        Jugador.startPlayer();
        Started = true;
        Jugador.transform.position = new Vector3(0f, 0f, -4f);
        Cronometro.start_count();
    }

}