using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    private TimerController timerController;

    private float currentTime;
    private int lastLoggedMinute = -1;

    public class Event
    {
        public int code;
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
        public float duration;
        public int chance;

        public Event(int code, float duration)
        {
            this.code = code;
            this.duration = duration;
        }
    }

    private void Start()
    {
        timerController =canvas.GetComponent<TimerController>();
    }

   

    void Update()
    {
        currentTime = timerController.elapsedTime;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        if (seconds == 0 && minutes != 0 && minutes != lastLoggedMinute)
        {
            Debug.Log("Teste");
            lastLoggedMinute = minutes; 
        }
    }
}
