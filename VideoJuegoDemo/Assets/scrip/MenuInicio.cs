using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelNombre; // popup de "Ingresa tu nombre"

    private void Start()
    {
        // Asegúrate de que el panel esté oculto al iniciar
        if (panelNombre != null) panelNombre.SetActive(false);
    }

    // Botón JUGAR en la Escena Inicio
    public void BotonJugar()
    {
        // Si no hay nombre o es "Invitado", pedimos el nombre
        string nombre = GestorDatos.Instancia != null ? GestorDatos.Instancia.ObtenerNombre() : "Invitado";
        if (string.IsNullOrEmpty(nombre) || nombre == "Invitado")
        {
            if (panelNombre != null) panelNombre.SetActive(true);
        }
        else
        {
            // Ya hay nombre guardado: ir directo a la escena 1 (Seleccion)
            SceneManager.LoadScene("Seleccion"); // o SceneManager.LoadScene(1);
        }
    }

    // Botón SALIR en la Escena Inicio
    public void BotonSalir()
    {
        Application.Quit();
    }
}
