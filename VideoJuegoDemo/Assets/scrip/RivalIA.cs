using UnityEngine;

[RequireComponent(typeof(Luchador))]
public class RivalIA : MonoBehaviour
{
    public float velocidad = 2f;
    public int danioAtaque = 10;
    public float distanciaAtaque = 1.1f;
    public float tiempoEntreAtaques = 1.2f;
    public LayerMask capaJugador; // asignar en Inspector (ej. "Jugador" layer)

    private Transform target;
    private Animator animator;
    private float nextAttackTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        // encontraremos al jugador por tag (asegúrate que el jugador instanciado tenga tag "Jugador")
        var go = GameObject.FindGameObjectWithTag("Jugador");
        if (go) target = go.transform;
    }

    void Update()
    {
        if (target == null)
        {
            var g = GameObject.FindGameObjectWithTag("Jugador");
            if (g) target = g.transform; else return;
        }

        float dist = Vector2.Distance(transform.position, target.position);

        if (dist > distanciaAtaque)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, velocidad * Time.deltaTime);
            animator?.SetBool("Correr", true);
        }
        else
        {
            animator?.SetBool("Correr", false);
            if (Time.time >= nextAttackTime)
            {
                Atacar();
                nextAttackTime = Time.time + tiempoEntreAtaques;
            }
        }
    }

    void Atacar()
    {
        animator?.SetTrigger("Atacar");
        // golpe en área (puedes ajustar origen/rango)
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
