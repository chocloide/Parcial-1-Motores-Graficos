using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public PlayerController Jugador;
    public Chronometer Cronometro;

    void Start(){
        ComenzarJuego();
    }
    
    void Update(){
    }

    void ComenzarJuego(){
        Jugador.startPlayer();
        Jugador.transform.position = new Vector3(0f, 0f, -4f);
        Cronometro.start_count();
    }
}