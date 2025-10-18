using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class Luchador : MonoBehaviour
{
    public string nombre;
    public int vidaMax = 100;
    [HideInInspector] public int vidaActual;

    public Animator animator;
    private bool muerto = false;

    // Eventos
    public event Action<Luchador> OnDeath;
    public event Action<int, int> OnHealthChanged; // current, max

    void Awake()
    {
        vidaActual = vidaMax;
        if (animator == null) animator = GetComponent<Animator>();
    }

    public void RecibirDanio(int cantidad)
    {
        if (muerto) return; 

        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;

        OnHealthChanged?.Invoke(vidaActual, vidaMax);

        if (animator != null)
        {
            animator.ResetTrigger("Hit");   // evita que se quede pegado
            animator.SetTrigger("Hit");
        }

        if (vidaActual == 0)
            Morir();
    }


    void Morir()
    {
        if (muerto) return;
        muerto = true;

        Debug.Log($"{name} ha muerto");

        if (animator != null)
        {
            animator.SetBool("Correr", false);
            animator.SetTrigger("Morir");
        }

        //Desactivar colisiones y físicas
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        //Detener IA o control del jugador
        var control = GetComponent<ControlJugador>();
        if (control != null) control.enabled = false;

        var ia = GetComponent<RivalIA>();
        if (ia != null) ia.enabled = false;

        //Invocar evento de muerte (CombateManager lo escucha)
        OnDeath?.Invoke(this);

    }


    public bool EstaVivo() => !muerto;
}
