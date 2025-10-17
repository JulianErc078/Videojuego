using UnityEngine;

[RequireComponent(typeof(Luchador))]
public class RivalIA : MonoBehaviour
{
    public float velocidad = 2f;
    public int danioAtaque = 10;
    public float distanciaAtaque = 1.1f;
    public float tiempoEntreAtaques = 1.2f;
    public LayerMask capaJugador;

    private Transform target;
    private Animator animator;
    private float nextAttackTime;
    private Luchador luchador;

    void Start()
    {
        animator = GetComponent<Animator>();
        luchador = GetComponent<Luchador>();
        var go = GameObject.FindGameObjectWithTag("Jugador");
        if (go) target = go.transform;
    }

    void Update()
    {
        if (luchador == null || !luchador.EstaVivo()) return;

        if (target == null)
        {
            var g = GameObject.FindGameObjectWithTag("Jugador");
            if (g) target = g.transform;
            else return;
        }

        // Detener si el jugador ya murió
        Luchador jugador = target.GetComponent<Luchador>();
        if (jugador != null && !jugador.EstaVivo())
        {
            animator.SetBool("Correr", false);
            return;
        }

        if (!luchador.EstaVivo())
        {
            animator.SetBool("Correr", false);
            return;
        }

        if (target == null)
        {
            var g = GameObject.FindGameObjectWithTag("Jugador");
            if (g) target = g.transform; else return;
        }

        float dist = Vector2.Distance(transform.position, target.position);

        if (dist > distanciaAtaque)
        {
            // Moverse hacia el jugador
            transform.position = Vector2.MoveTowards(transform.position, target.position, velocidad * Time.deltaTime);
            animator.SetBool("Correr", true);
        }
        else
        {
            animator.SetBool("Correr", false);
            if (Time.time >= nextAttackTime)
            {
                Atacar();
                nextAttackTime = Time.time + tiempoEntreAtaques;
            }
        }

        // Voltear para mirar al jugador
        if (target.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void Atacar()
    {
        animator.SetTrigger("Atacar");
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, distanciaAtaque, capaJugador);
        foreach (var c in cols)
            c.GetComponent<Luchador>()?.RecibirDanio(danioAtaque);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaque);
    }
}
