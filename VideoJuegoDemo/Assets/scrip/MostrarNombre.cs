using UnityEngine;
using TMPro;  // Necesario para usar TextMeshPro

public class MostrarNombre : MonoBehaviour
{
    public TextMeshProUGUI textoNombre; // Referencia al texto en pantalla

    void Start()
    {
        // Recuperamos el nombre guardado en PlayerPrefs
        string nombreJugador = PlayerPrefs.GetString("NombreJugador", "Sin Nombre");

        // Mostramos el nombre en el texto
        textoNombre.text = "Jugador: " + nombreJugador;
    }
}
