using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UIElements;

public class EventsController : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    private TimerController timerController;

    private float currentTime;
    private int lastLoggedMinute = -1;

    [System.Serializable]
    public class Event
    {
        [SerializeField, Tooltip("Código do evento (não editável)")]
        private int code;  // Agora privado para não ser editado no Inspector
        [SerializeField, Tooltip("Duração do evento (em segundos)")]
        public float duration;
        [SerializeField, Tooltip("Chance de ativação (%)")]
        public int chance;

        
        public Event(int code, float duration, int chance)
        {
            this.code = code;
            this.duration = duration;
            this.chance = chance;
        }

        public int getCode()
        {
            return code;
        }
    }

    [SerializeField] private List<Event> eventsList = new List<Event>();

    private void Awake()
    {
        // Predefinição da lista de eventos (somente `duration` e `chance` serão editáveis)
        eventsList = new List<Event>
        {
            new Event(0, 0, 100),  // Nenhum evento
            new Event(1, 10, 20),  // Inverter Teclado
            new Event(2, 10, 20),  // Inverter Mouse
            new Event(3, 5, 15),   // Tela Preta por segundos
            new Event(4, 10, 25),  // Congela o personagem
            new Event(5, 10, 30),  // Personagem não pode atirar
            new Event(6, 15, 10),  // God Mode
            new Event(7, 20, 15),  // Inimigos somem
            new Event(8, 15, 20),  // Inimigos mais rápidos
            new Event(9, 20, 15),  // Inimigos tanque
            new Event(10, 15, 25)  // Player toma dano duplo
        };
    }

    private void Start()
    {
        timerController =canvas.GetComponent<TimerController>();
    }

    void Update()
    {
        currentTime = timerController.getElapsedTime();

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        if (seconds == 0 && minutes != 0 && minutes != lastLoggedMinute)
        {
            Debug.Log("Teste");
            lastLoggedMinute = minutes; 
        }
    }
}
