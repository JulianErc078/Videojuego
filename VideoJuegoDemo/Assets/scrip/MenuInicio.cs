using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    [Header("Referencia al panel del nombre")]
    public GameObject panelNombre; // arrastras aquí el PanelNombre desde el Hierarchy

    void Start()
    {
        // Asegurarnos que el panel esté oculto al iniciar
        if (panelNombre != null) panelNombre.SetActive(false);
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
