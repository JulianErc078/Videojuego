using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CarruselSeleccion : MonoBehaviour
{
    [Header("Panels (toggle entre selección)")]
    public GameObject panelPersonajes;   // Panel que contiene el carrusel de personajes
    public GameObject panelRivales;      // Panel que contiene el carrusel de rivales

    [Header("Personajes (Sprites -> UI Image)")]
    public Sprite[] personajesSprites;
    public GameObject[] personajesPrefabs;
    public Image imagenPersonaje;                     
    public TextMeshProUGUI textoNombrePersonaje;       // TextoNombrePersonaje
    public Button btnIzquierda;
    public Button btnDerecha;
    public Button btnSeleccionarPersonaje;

    [Header("Rivales (Sprites -> UI Image)")]
    public Sprite[] rivalesSprites;
    public GameObject[] rivalesPrefabs;
    public Image imagenRival;
    public TextMeshProUGUI textoNombreRival;
    public Button btnIzquierdaRival;
    public Button btnDerechaRival;
    public Button btnSeleccionarRival;

    [Header("Botones globales")]
    public Button btnPelear;    // INICIAR PELEA
    public Button btnVolver;    // VOLVER / regresar al menú

    // Estado interno
    private int indicePersonajeActual = 0;
    private int indiceRivalActual = 0;
    private bool personajeSeleccionado = false;
    private bool rivalSeleccionado = false;
    private bool isSelectingPersonaje = true; // true = navegando personajes, false = navegando rivales

    void Start()
    {
        // Listeners de botones (UI)
        if (btnIzquierda != null) btnIzquierda.onClick.AddListener(() => CambiarPersonaje(-1));
        if (btnDerecha != null) btnDerecha.onClick.AddListener(() => CambiarPersonaje(1));
        if (btnSeleccionarPersonaje != null) btnSeleccionarPersonaje.onClick.AddListener(SeleccionarPersonaje);

        if (btnIzquierdaRival != null) btnIzquierdaRival.onClick.AddListener(() => CambiarRival(-1));
        if (btnDerechaRival != null) btnDerechaRival.onClick.AddListener(() => CambiarRival(1));
        if (btnSeleccionarRival != null) btnSeleccionarRival.onClick.AddListener(SeleccionarRival);

        if (btnPelear != null) btnPelear.onClick.AddListener(IniciarPelea);
        if (btnVolver != null) btnVolver.onClick.AddListener(VolverMenu);

        // Estado inicial de UI
        if (panelPersonajes != null) panelPersonajes.SetActive(true);
        if (panelRivales != null) panelRivales.SetActive(false);
        if (btnPelear != null) btnPelear.interactable = false;

        // Mostrar primeros elementos
        MostrarPersonaje(0);
        MostrarRival(0);
    }

    void Update()
    {
        // Navegación por teclado (opcional pero útil)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isSelectingPersonaje) CambiarPersonaje(-1); else CambiarRival(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isSelectingPersonaje) CambiarPersonaje(1); else CambiarRival(1);
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (isSelectingPersonaje) SeleccionarPersonaje(); else SeleccionarRival();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si estamos en selección de rivales, volver a personajes (cancelar)
            if (!isSelectingPersonaje)
            {
                // volver a selección de personajes
                isSelectingPersonaje = true;
                if (panelRivales != null) panelRivales.SetActive(false);
                if (panelPersonajes != null) panelPersonajes.SetActive(true);
                rivalSeleccionado = false;
                if (btnPelear != null) btnPelear.interactable = false;
            }
            else
            {
                VolverMenu();
            }
        }
    }

    // Cambia el índice y muestra el sprite correspondiente (personajes)
    public void CambiarPersonaje(int direccion)
    {
        if (personajesSprites == null || personajesSprites.Length == 0) return;
        indicePersonajeActual = (indicePersonajeActual + direccion + personajesSprites.Length) % personajesSprites.Length;
        MostrarPersonaje(indicePersonajeActual);
    }

    // Cambia el índice y muestra el sprite correspondiente (rivales)
    public void CambiarRival(int direccion)
    {
        if (rivalesSprites == null || rivalesSprites.Length == 0) return;
        indiceRivalActual = (indiceRivalActual + direccion + rivalesSprites.Length) % rivalesSprites.Length;
        MostrarRival(indiceRivalActual);
    }

    // Actualiza la UI del personaje en pantalla
    void MostrarPersonaje(int indice)
    {
        if (personajesSprites == null || personajesSprites.Length == 0) return;

        // Cambiar sprite y nombre
        imagenPersonaje.sprite = personajesSprites[indice];
        textoNombrePersonaje.text = ObtenerNombrePersonaje(indice);

        // Siempre habilitado
        if (btnSeleccionarPersonaje != null)
            btnSeleccionarPersonaje.interactable = true;
    }

    // Actualiza la UI del rival en pantalla
    void MostrarRival(int indice)
    {
        if (rivalesSprites == null || rivalesSprites.Length == 0) return;
        imagenRival.sprite = rivalesSprites[indice];
        textoNombreRival.text = ObtenerNombreRival(indice);

        bool desbloqueado = true;
        if (GestorDatos.Instancia != null)
        {
            desbloqueado = GestorDatos.Instancia.RivalDesbloqueado(indice);
        }
        if (btnSeleccionarRival != null) btnSeleccionarRival.interactable = desbloqueado;

        if (!desbloqueado) textoNombreRival.text += " (Bloqueado)";
    }

    // Confirmar personaje -> pasa a selección de rivales
    void SeleccionarPersonaje()
    {
        personajeSeleccionado = true;
        Debug.Log("Personaje seleccionado index: " + indicePersonajeActual);

        // Guardar el personaje elegido para la próxima escena
        PlayerPrefs.SetInt("PersonajeSeleccionado", indicePersonajeActual);
        PlayerPrefs.Save(); // asegura que quede guardado

        // Cambiar UI a rivales
        isSelectingPersonaje = false;
        if (panelPersonajes != null) panelPersonajes.SetActive(false);
        if (panelRivales != null) panelRivales.SetActive(true);

        // Opcional: si quieres que la navegación por teclado empiece en el rival actual 0:
        indiceRivalActual = 0;
        MostrarRival(indiceRivalActual);
    }


    // Confirmar rival -> habilita botón pelear
    void SeleccionarRival()
    {
        rivalSeleccionado = true;
        Debug.Log("Rival seleccionado index: " + indiceRivalActual);

        // Guardar el rival elegido
        PlayerPrefs.SetInt("RivalSeleccionado", indiceRivalActual);
        PlayerPrefs.Save();

        if (btnPelear != null)
            btnPelear.interactable = (personajeSeleccionado && rivalSeleccionado);
    }


    // Iniciar pelea: guarda en PlayerPrefs y carga la escena de pelea
    public void IniciarPelea()
    {
        if (personajeSeleccionado && rivalSeleccionado)
        {
            int rivalSeleccionadoIndex = PlayerPrefs.GetInt("RivalSeleccionado", 0);

            string nombreEscena = "nombreEscena" + rivalSeleccionadoIndex;
            Debug.Log("Cargando escena: " + nombreEscena);
            SceneManager.LoadScene("nombreEscena");
        }
        else
        {
            Debug.LogWarning("Debes seleccionar personaje y rival antes de pelear.");
        }
    }



    // Volver al menú principal o escena anterior
    void VolverMenu()
    {
        SceneManager.LoadScene("SelectorNiveles");
    }

    // Puedes personalizar nombres aquí o conectarlo a una data real
    string ObtenerNombrePersonaje(int indice)
    {
        string[] nombres = { "FidelCastro-Cuba", "SalvadorAllende-Chile", "AlanGarcia-Peru", "HugoChavez-Venezuela", "JuanPeron-Argentina", "GetulioVargas-Brasil", "JoseMujica-Uruguay", "EmmanuelMacron-Francia" };
        return indice < nombres.Length ? nombres[indice] : $"Personaje {indice + 1}";
    }

    string ObtenerNombreRival(int indice)
    {
        string[] nombresRivales = { "UribeVelez-Colombia", "ClaudiaSheinbaum-Mexico", "DiomayeFaye-Senegal", "NayibBukele-Salvador", "VladimirPutin-Rusia", "DonaldTrump-EEUU", "XiJinping-China", "OsamaBin-Saudi" };
        return indice < nombresRivales.Length ? nombresRivales[indice] : $"Rival {indice + 1}";
    }


}
