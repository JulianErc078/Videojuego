using UnityEngine;

public class MovimientoJugador1 : MonoBehaviour
{
    public float velocidad = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movX = 0f;
        float movY = 0f;

        if (Input.GetKey(KeyCode.A)) movX = -1f;
        if (Input.GetKey(KeyCode.D)) movX = 1f;
        if (Input.GetKey(KeyCode.W)) movY = 1f;
        if (Input.GetKey(KeyCode.S)) movY = -1f;

        Vector2 movimiento = new Vector2(movX, movY);
        rb.linearVelocity = movimiento * velocidad;
    }
}
