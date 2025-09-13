using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    [Header("Referencia al panel del nombre")]
    public GameObject panelNombre; // arrastras aquí el PanelNombre desde el Hierarchy

  public void Start()
{
        Debug.Log("=== INICIANDO MENÚ PRINCIPAL ===");
        Debug.Log("Nombre guardado: " + GestorDatos.Instancia.ObtenerNombre());

        // SIEMPRE ocultar el panel al inicio, sin importar si hay nombre o no
        if (panelNombre != null)
        {
            panelNombre.SetActive(false);
            Debug.Log("Panel de nombre oculto al inicio");
        }
        // Revisa si ya existe un nombre en GestorDatos
        if (!string.IsNullOrEmpty(GestorDatos.Instancia.ObtenerNombre()) && 
        GestorDatos.Instancia.ObtenerNombre() != "Invitado")
    {
        // SI hay nombre guardado → OCULTAR panel de nombre
        if (panelNombre != null) panelNombre.SetActive(false);
        Debug.Log("Nombre guardado encontrado: " + GestorDatos.Instancia.ObtenerNombre());
    }
    else
    {
        // NO hay nombre guardado → MOSTRAR panel de registro
        Debug.Log("No hay nombre guardado, mostrando panel de registro");
    }
}

// Método para el botón Jugar
public void OnJugarClick()
{
    // Verificar si ya hay nombre guardado
    if (!string.IsNullOrEmpty(GestorDatos.Instancia.ObtenerNombre()) && 
        GestorDatos.Instancia.ObtenerNombre() != "Invitado")
    {
        // YA tiene nombre → Ir directamente a Escena 1
        SceneManager.LoadScene(1);
    }
    else
    {
        // NO tiene nombre → Mostrar panel para ingresar nombre
        MostrarPanelNombre();
    }
}

// Método para mostrar panel de nombre
public void MostrarPanelNombre()
{
    if (panelNombre != null) 
    {
        panelNombre.SetActive(true);
        Debug.Log("Mostrando panel de nombre");
    }
}

    // Opcional: asigna al botón Salir
    public void SalirJuego()
    {
        Application.Quit();
    }
}
