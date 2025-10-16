using UnityEngine;
using UnityEngine.SceneManagement;

public class ForzarInicioEscena0 : MonoBehaviour
{
    // Variable est�tica que persiste durante toda la ejecuci�n
    private static bool juegoYaIniciado = false;

    void Start()
    {
        // Solo forzar escena 0 si el juego NO se ha iniciado todav�a
        if (!juegoYaIniciado && SceneManager.GetActiveScene().buildIndex != 0)
        {
            Debug.Log("Primera ejecuci�n - Forzando inicio desde Escena 0...");
            juegoYaIniciado = true; // Marcar que ya se inici�
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("Juego ya iniciado - Flujo normal en escena: " + SceneManager.GetActiveScene().buildIndex);
        }
    }
}