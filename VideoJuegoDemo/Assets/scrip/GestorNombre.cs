using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GestorNombre : MonoBehaviour
{
    public TMP_InputField inputNombre; // campo para escribir el nombre

    // Método que se ejecuta al dar clic en Confirmar
    public void GuardarNombre()
    {
        string nombreJugador = inputNombre.text;

        if (!string.IsNullOrEmpty(nombreJugador))
        {
            // Guardamos en PlayerPrefs
            PlayerPrefs.SetString("NombreJugador", nombreJugador);
            PlayerPrefs.Save();

            Debug.Log("Nombre guardado: " + nombreJugador);

            // Cambiamos a la escena 1 (selección de personaje, escenarios, botones, etc.)
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("El nombre no puede estar vacío.");
        }
    }
}
