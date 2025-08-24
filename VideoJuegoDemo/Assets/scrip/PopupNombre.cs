using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupNombre : MonoBehaviour
{
    public InputField inputNombre;   // arrastra el InputField del popup aquí
    public Text textoError;          // opcional, para mostrar aviso si está vacío

    public void Aceptar()
    {
        string nombre = inputNombre != null ? inputNombre.text : "";

        if (string.IsNullOrWhiteSpace(nombre))
        {
            if (textoError != null) textoError.text = "Ingresa un nombre válido.";
            return;
        }

        GestorDatos.Instancia.GuardarNombre(nombre);
        SceneManager.LoadScene("Seleccion"); // o SceneManager.LoadScene(1);
    }

    public void Cancelar()
    {
        // Cierra el popup (por si quieres permitir volver atrás)
        gameObject.SetActive(false);
    }
}
