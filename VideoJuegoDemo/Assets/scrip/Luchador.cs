using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class Luchador : MonoBehaviour
{
    public string nombre;
    public int vidaMax = 100;
    [HideInInspector] public int vidaActual;

    public Animator animator;

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
        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;

        OnHealthChanged?.Invoke(vidaActual, vidaMax);

        if (animator != null) animator.SetTrigger("Hit");

        if (vidaActual == 0)
            Morir();
    }

    void Morir()
    {
        if (animator != null) animator.SetTrigger("Morir");
        OnDeath?.Invoke(this);
        // opcional: desactivar colliders o scripts aquí
    }

    public bool EstaVivo() => vidaActual > 0;
}
