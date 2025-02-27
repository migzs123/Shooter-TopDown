using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    [SerializeField] TimerController timerController;
    private float currentTime;

    public int eventCode;

    /*
     0 - Nenhum evento
     1 - Inverter Teclado
     2 - Inventer Mouse
     3 - Tela Preta por segundos
     4 - Congela o personagem
     5 - Personagem nao pode atirar
     6 - God mode (recupera vida e invencivel e dano ao tocar os inimigos)
     7 - Inimigos somem e param de spawnar por um tempo
     8 - Inimigos mais rapidos e dao mais dano
     9 - Inimigos tank
     10 - Player toma dano duplo
     */

    // Update is called once per frame
    void Update()
    {
        currentTime = timerController.elapsedTime;
    }
}
