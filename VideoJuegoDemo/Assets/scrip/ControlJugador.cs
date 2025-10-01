using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Luchador))]
public class ControlJugador : MonoBehaviour
{
    public float velocidad = 3f;
    public int danioAtaque = 20;
    public Transform puntoGolpe;       // hijo vacío colocado frente al jugador
    public float rangoGolpe = 0.7f;
    public LayerMask capaEnemigo;      // set en Inspector (ej. "Enemigo")

    Luchador luchador;
    Animator animator;
    bool atacando = false;

    void Awake()
    {
        luchador = GetComponent<Luchador>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento lateral simple
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0) transform.Translate(Vector2.right * h * velocidad * Time.deltaTime);

        // Ataque
        if (!atacando && Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        atacando = true;
        animator?.SetTrigger("Atacar");

        // espera un momento para sincronizar con animación (ajusta 0.15 según tu animación)
        yield return new WaitForSeconds(0.15f);

        Collider2D[] cols = Physics2D.OverlapCircleAll(puntoGolpe.position, rangoGolpe, capaEnemigo);
        foreach (var c in cols)
        {
            c.GetComponent<Luchador>()?.RecibirDanio(danioAtaque);
        }

        yield return new WaitForSeconds(0.2f); // pequeño cooldown
        atacando = false;
    }

    void OnDrawGizmosSelected()
    {
        if (puntoGolpe != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(puntoGolpe.position, rangoGolpe);
        }
    }
}
