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
        // Guardamos usando GestorDatos
        GestorDatos.Instancia.GuardarNombre(nombreJugador);

        Debug.Log("Nombre guardado: " + nombreJugador);

        // Cambiamos a la escena 1
        SceneManager.LoadScene(1);
    }
    else
    {
        Debug.Log("El nombre no puede estar vacío.");
    }
}

}
