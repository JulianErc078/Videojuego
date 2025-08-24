using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PopupNombre : MonoBehaviour
{
    public TMP_InputField inputNombre;  // arrastra el TMP Input Field
    public TextMeshProUGUI textoError;  // opcional, para mostrar mensaje si está vacío

    // Llamar desde el botón Confirmar
    public void Aceptar()
    {
        string nombre = inputNombre != null ? inputNombre.text.Trim() : "";

        if (string.IsNullOrEmpty(nombre))
        {
            if (textoError != null) textoError.text = "Ingresa un nombre válido.";
            return;
        }

        // Guardar en el GestorDatos (singleton)
        if (GestorDatos.Instancia != null)
            GestorDatos.Instancia.GuardarNombre(nombre);
        else
            PlayerPrefs.SetString("NombreJugador", nombre);

        // Cargar la escena de selección (asegúrate que el nombre coincide con el de Build Settings)
        SceneManager.LoadScene("Seleccion"); // o SceneManager.LoadScene(1);
    }

    public void Cancelar()
    {
        gameObject.SetActive(false); // oculta popup sin guardar
    }
}
