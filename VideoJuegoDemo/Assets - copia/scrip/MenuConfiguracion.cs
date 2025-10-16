using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuConfiguracion : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelFondo;
    public GameObject submenuConfiguracion;
    
    [Header("Paneles Modales")]
    public GameObject panelEditarNombre;
    public GameObject panelProgreso;
    public GameObject panelManual;

    public GameObject panelSeleccion;
    
    [Header("Botones Submenú")]
    public Button btnEditarNombre;
    public Button btnProgreso;
    public Button btnManual;
    public Button btnReiniciar;
    public Button btnVolver;


    private void Start()
    {
        // Configurar listeners de los botones
        btnEditarNombre.onClick.AddListener(MostrarEditarNombre);
        btnProgreso.onClick.AddListener(MostrarProgreso);
        btnManual.onClick.AddListener(MostrarManual);
        if (btnVolver != null) btnVolver.onClick.AddListener(OnVolverPressed);


        // Ocultar todo al inicio
        OcultarTodo();
    }

    public void OnVolverPressed()
    {
        Debug.Log("[MenuConfiguracion] btnVolver pulsado.");
        CerrarPanelActual();
        // Asegurar que el panelSeleccion vuelva a mostrarse
        if (panelSeleccion != null)
        {
            panelSeleccion.SetActive(true);
            Debug.Log("[MenuConfiguracion] panelSeleccion reactivado.");
        }
        else
        {
            Debug.LogWarning("[MenuConfiguracion] panelSeleccion no asignado en el inspector.");
        }
    }
    public void ToggleConfiguracion()
    {
        if (panelFondo.activeSelf)
        {
            CerrarPanelActual();
        }
        else
        {
            MostrarSubmenu();
        }
    }

    private void MostrarSubmenu()
    {
        if (panelSeleccion != null) panelSeleccion.SetActive(false); // oculta selección
        panelFondo.SetActive(true);
        submenuConfiguracion.SetActive(true);
        OcultarPanelesModales();
    }

    private void OcultarTodo()
    {
        panelFondo.SetActive(false);
        submenuConfiguracion.SetActive(false);
        OcultarPanelesModales();
    }

    private void OcultarPanelesModales()
    {
        panelEditarNombre.SetActive(false);
        panelProgreso.SetActive(false);
        panelManual.SetActive(false);
    }

    public void MostrarEditarNombre()
    {
        OcultarPanelesModales();
        panelEditarNombre.SetActive(true);
    }

    public void MostrarProgreso()
    {
        OcultarPanelesModales();
        panelProgreso.SetActive(true);
    }

    public void MostrarManual()
    {
        OcultarPanelesModales();
        panelManual.SetActive(true);
    }

    public void MostrarConfirmacionReinicio()
    {
        // Ocultar menú primero
        OcultarTodo();

        // Llamar función de reinicio
        MenuEscena1 menuPrincipal = FindFirstObjectByType<MenuEscena1>();
        if (menuPrincipal != null)
        {
            menuPrincipal.MostrarConfirmacionReinicio();
        }
    }

    public void CerrarPanelActual()
    {
        // Si hay un panel modal abierto, cerrarlo
        if (panelEditarNombre.activeSelf || panelProgreso.activeSelf || panelManual.activeSelf)
        {
            OcultarPanelesModales();
            MostrarSubmenu();
        }
        else
        {
            // Si no hay panel modal, cerrar todo
            OcultarTodo();
        }
    }
}