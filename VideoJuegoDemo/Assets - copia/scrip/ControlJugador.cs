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
        if (!luchador.EstaVivo())
        {
            animator.SetBool("Correr", false);
            return;
        }

        // Movimiento lateral
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(h, 0, 0);

        if (!atacando)
        {
            transform.Translate(move * velocidad * Time.deltaTime);

            // Actualiza animación de movimiento
            animator.SetBool("Correr", h != 0);
        }

        // Volteo (mirar dirección)
        if (h > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (h < 0) transform.localScale = new Vector3(-1, 1, 1);

        // Límite del escenario
        float minX = -8f;
        float maxX = 8f;
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;

        // Ataque
        if (!atacando && Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(DoAttack());

        // Agacharse
        if (Input.GetKey(KeyCode.DownArrow))
            animator.SetBool("Agachado", true);
        else
            animator.SetBool("Agachado", false);
    }

    IEnumerator DoAttack()
    {
        atacando = true;
        animator.SetTrigger("Atacar");

        // espera para sincronizar golpe con la animación
        yield return new WaitForSeconds(0.15f);

        Collider2D[] cols = Physics2D.OverlapCircleAll(puntoGolpe.position, rangoGolpe, capaEnemigo);
        foreach (var c in cols)
        {
            c.GetComponent<Luchador>()?.RecibirDanio(danioAtaque);
        }

        yield return new WaitForSeconds(0.3f);
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
