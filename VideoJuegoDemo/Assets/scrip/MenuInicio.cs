using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    [Header("Referencia al panel del nombre")]
    public GameObject panelNombre; // arrastras aquí el PanelNombre desde el Hierarchy

    public void Start()
    {
        // Revisa si ya existe un nombre en GestorDatos
        if (!string.IsNullOrEmpty(GestorDatos.Instancia.ObtenerNombre()) && 
            GestorDatos.Instancia.ObtenerNombre() != "Invitado")
        {
            // Si ya hay nombre guardado -> ir directo a escena 1
            SceneManager.LoadScene(1);
        }
        else
        {
            // Si no hay nombre guardado -> mostrar panel de registro
            if (panelNombre != null) panelNombre.SetActive(false);
        }
    
    }

    // Asignar este método al OnClick del botón Jugar
    public void MostrarPanelNombre()
    {
        if (panelNombre != null) panelNombre.SetActive(true);
    }

    // Opcional: asigna al botón Salir
    public void SalirJuego()
    {
        Application.Quit();
    }
}
