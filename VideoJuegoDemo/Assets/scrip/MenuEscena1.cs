using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // <--- necesario para TMP_Text


public class MenuEscena1 : MonoBehaviour
{
    public GameObject panelManual;  // arrastrar el panel de manual desde el inspector
    public GameObject panelProgreso; // arrastrar el panel de progreso desde el inspector
    public GameObject panelEditarNombre;  // arrastrar PanelEditarNombre
    public TMP_InputField inputNombre;        // arrastrar InputNombre
    public TMP_Text textoNombreJugador;



    // --- BOTÓN EDITAR NOMBRE ---
    public void EditarNombre()
    {
        // Mostrar panel para editar nombre
        panelEditarNombre.SetActive(true);

        // Colocar el nombre actual en el input
        inputNombre.text = GestorDatos.Instancia.ObtenerNombre();
        SceneManager.LoadScene(0);
    }

    // --- Botón Confirmar dentro del panel ---
    public void ConfirmarNombre()
    {
         string nuevoNombre = inputNombre.text;
    GestorDatos.Instancia.GuardarNombre(nuevoNombre);

    // Mantener el prefijo "Jugador: "
    if(textoNombreJugador != null)
        textoNombreJugador.text = "Jugador: " + nuevoNombre;

    Debug.Log("Nombre actualizado a: " + nuevoNombre);
    panelEditarNombre.SetActive(false);
    }

    // --- BOTÓN PROGRESO ---
    public void MostrarProgreso()
    {
        if (panelProgreso != null)
        {
            panelProgreso.SetActive(true);

            TMP_Text texto = panelProgreso.GetComponentInChildren<TMP_Text>();
            texto.text = $"Jugador: {GestorDatos.Instancia.ObtenerNombre()}\n" +
                 $"Victorias: {GestorDatos.Instancia.ObtenerPeleasGanadas()}\n" +
                 $"Personajes desbloqueados: {string.Join(", ", GestorDatos.Instancia.datos.personajesDesbloqueados)}\n" +
                 $"Escenarios desbloqueados: {string.Join(", ", GestorDatos.Instancia.datos.escenariosDesbloqueados)}";
                 
            
        AbrirPanel(panelProgreso);     
        }
    }

    // --- BOTÓN MANUAL ---

    private void AbrirPanel(GameObject panel)
    {
        // Cierra cualquier otro panel abierto
        if (panelManual.activeSelf) panelManual.SetActive(false);
        if (panelProgreso.activeSelf) panelProgreso.SetActive(false);

        // Abre el panel que se quiere
        panel.SetActive(true);
    }
    public void CerrarPanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void MostrarManual()
    {
        if (panelManual != null)
            panelManual.SetActive(true);
            AbrirPanel(panelManual); 

    }

    // --- BOTÓN REINICIAR ---
    public void ReiniciarProgreso()
    {
        // 1. Reinicia todos los datos guardados
        GestorDatos.Instancia.ReiniciarTodo();

        // 2. Debug opcional
        Debug.Log("Juego reiniciado: nombre, peleas y desbloqueos borrados.");

        // 3. Volver a Escena 0 (pantalla de ingreso de nombre)
        SceneManager.LoadScene(0);
    }

    // --- BOTÓN JUGAR ---
    public void Jugar()
    {
        // Aquí verificas que haya personaje elegido antes de cargar la Escena 2
        // Por ahora simplemente cargamos Escena 2
        SceneManager.LoadScene(2);
    }
}
