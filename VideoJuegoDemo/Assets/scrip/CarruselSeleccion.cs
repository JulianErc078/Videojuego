using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CarruselSeleccion : MonoBehaviour
{
    [Header("Configuración Personajes")]
    public GameObject[] personajesPrefabs;
    public Transform contenedorPersonajes;
    public TextMeshProUGUI textoNombrePersonaje;
    public Button btnSeleccionarPersonaje;
    public Button btnIzquierda;
    public Button btnDerecha;

    [Header("Configuración Rivales")]
    public GameObject[] rivalesPrefabs;
    public Transform contenedorRivales;
    public TextMeshProUGUI textoNombreRival;
    public Button btnSeleccionarRival;
    public Button btnIzquierdaRival;
    public Button btnDerechaRival;

    private int indicePersonajeActual = 0;
    private int indiceRivalActual = 0;
    private GameObject personajeActualInstancia;
    private GameObject rivalActualInstancia;

    void Start()
    {
        InicializarCarrusel();
        ActualizarBotones();
    }

    void InicializarCarrusel()
    {
        // Configurar listeners de botones
        btnIzquierda.onClick.AddListener(() => CambiarPersonaje(-1));
        btnDerecha.onClick.AddListener(() => CambiarPersonaje(1));
        btnIzquierdaRival.onClick.AddListener(() => CambiarRival(-1));
        btnDerechaRival.onClick.AddListener(() => CambiarRival(1));
        btnSeleccionarPersonaje.onClick.AddListener(SeleccionarPersonaje);
        btnSeleccionarRival.onClick.AddListener(SeleccionarRival);

        // Mostrar primer personaje y rival
        MostrarPersonaje(0);
        MostrarRival(0);
    }

    void MostrarPersonaje(int indice)
    {
        indicePersonajeActual = indice;

        // Destruir instancia anterior
        if (personajeActualInstancia != null)
            Destroy(personajeActualInstancia);

        // Crear nueva instancia
        personajeActualInstancia = Instantiate(personajesPrefabs[indice], contenedorPersonajes);
        textoNombrePersonaje.text = ObtenerNombrePersonaje(indice);

        // Verificar si está desbloqueado
        bool desbloqueado = GestorDatos.Instancia.PersonajeDesbloqueado(personajesPrefabs[indice].name.ToLower());
        btnSeleccionarPersonaje.interactable = desbloqueado;

        if (!desbloqueado)
            textoNombrePersonaje.text += " (Bloqueado)";
    }

    void MostrarRival(int indice)
    {
        indiceRivalActual = indice;

        if (rivalActualInstancia != null)
            Destroy(rivalActualInstancia);

        rivalActualInstancia = Instantiate(rivalesPrefabs[indice], contenedorRivales);
        textoNombreRival.text = $"Rival {indice + 1}";

        // Verificar si está desbloqueado
        bool desbloqueado = GestorDatos.Instancia.RivalDesbloqueado(indice);
        btnSeleccionarRival.interactable = desbloqueado;

        if (!desbloqueado)
            textoNombreRival.text += " (Bloqueado)";
    }

    void CambiarPersonaje(int direccion)
    {
        int nuevoIndice = (indicePersonajeActual + direccion + personajesPrefabs.Length) % personajesPrefabs.Length;
        MostrarPersonaje(nuevoIndice);
        ActualizarBotones();
    }

    void CambiarRival(int direccion)
    {
        int nuevoIndice = (indiceRivalActual + direccion + rivalesPrefabs.Length) % rivalesPrefabs.Length;
        MostrarRival(nuevoIndice);
        ActualizarBotones();
    }

    void ActualizarBotones()
    {
        // Actualizar estado de botones de navegación
        btnIzquierda.interactable = true;
        btnDerecha.interactable = true;
        btnIzquierdaRival.interactable = true;
        btnDerechaRival.interactable = true;
    }

    string ObtenerNombrePersonaje(int indice)
    {
        // Puedes personalizar los nombres aquí
        string[] nombres = { "Uribe", "Petro", "Santos", "Pastrana", "Gaviria", "Samper", "Betancur", "Turbay" };
        return indice < nombres.Length ? nombres[indice] : $"Personaje {indice + 1}";
    }

    void SeleccionarPersonaje()
    {
        string personajeId = personajesPrefabs[indicePersonajeActual].name.ToLower();
        Debug.Log($"Personaje seleccionado: {personajeId}");
        // Aquí guardas la selección para usar en la pelea
    }

    void SeleccionarRival()
    {
        Debug.Log($"Rival seleccionado: {indiceRivalActual}");
        // Aquí guardas la selección del rival
    }

    public void IniciarPelea()
    {
        if (btnSeleccionarPersonaje.interactable && btnSeleccionarRival.interactable)
        {
            // Guardar selecciones y cargar escena de pelea
            PlayerPrefs.SetInt("PersonajeSeleccionado", indicePersonajeActual);
            PlayerPrefs.SetInt("RivalSeleccionado", indiceRivalActual);
            UnityEngine.SceneManagement.SceneManager.LoadScene("EscenaPelea1");
        }
    }
}