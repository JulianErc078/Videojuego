using UnityEngine;
using UnityEngine.SceneManagement;

public class ForzarInicioEscena0 : MonoBehaviour
{
    // Variable estática que persiste durante toda la ejecución
    private static bool juegoYaIniciado = false;

    void Start()
    {
        // Solo forzar escena 0 si el juego NO se ha iniciado todavía
        if (!juegoYaIniciado && SceneManager.GetActiveScene().buildIndex != 0)
        {
            Debug.Log("Primera ejecución - Forzando inicio desde Escena 0...");
            juegoYaIniciado = true; // Marcar que ya se inició
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("Juego ya iniciado - Flujo normal en escena: " + SceneManager.GetActiveScene().buildIndex);
        }
    }
}