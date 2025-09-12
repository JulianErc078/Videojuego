using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DatosJugador
{
    public string nombreJugador = "Invitado";
    public int peleasGanadas = 0;
    public List<string> personajesDesbloqueados = new List<string>();
    public List<string> escenariosDesbloqueados = new List<string>();
}

public class GestorDatos : MonoBehaviour
{
    public static GestorDatos Instancia;        // Singleton sencillo
    public DatosJugador datos = new DatosJugador();

    private void Awake()
    {
        // Asegurar una sola instancia que persiste entre escenas
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);

        Cargar(); // Cargar al iniciar el juego
    }

    // ---------- API pública cómoda ----------

    public string ObtenerNombre() => datos.nombreJugador;

    public void GuardarNombre(string nuevoNombre)
    {
        datos.nombreJugador = string.IsNullOrWhiteSpace(nuevoNombre) ? "Invitado" : nuevoNombre.Trim();
        Guardar();
         Debug.Log("Guardado nombre: " + datos.nombreJugador);
    }

    public int ObtenerPeleasGanadas() => datos.peleasGanadas;

    public void SumarVictoria()
    {
        datos.peleasGanadas++;
        Guardar();
    }

    public bool PersonajeDesbloqueado(string id) => datos.personajesDesbloqueados.Contains(id);
    public bool EscenarioDesbloqueado(string id) => datos.escenariosDesbloqueados.Contains(id);

    public void DesbloquearPersonaje(string id)
    {
        if (!datos.personajesDesbloqueados.Contains(id))
        {
            datos.personajesDesbloqueados.Add(id);
            Guardar();
        }
    }

    public void DesbloquearEscenario(string id)
    {
        if (!datos.escenariosDesbloqueados.Contains(id))
        {
            datos.escenariosDesbloqueados.Add(id);
            Guardar();
        }
    }

    public void ReiniciarTodo()
    {
        PlayerPrefs.DeleteKey("NombreJugador");
        PlayerPrefs.DeleteKey("PeleasGanadas");
        PlayerPrefs.DeleteKey("Personajes");
        PlayerPrefs.DeleteKey("Escenarios");

        datos = new DatosJugador();
        // Valores por defecto (opción 2): solo URIBE y CIUDAD desbloqueados
        datos.personajesDesbloqueados.Add("uribe");
        datos.escenariosDesbloqueados.Add("ciudad");
        Guardar();
    }



    // ---------- Persistencia (PlayerPrefs) ----------

    private void Guardar()
    {
        PlayerPrefs.SetString("NombreJugador", datos.nombreJugador);
        PlayerPrefs.SetInt("PeleasGanadas", datos.peleasGanadas);

        // Guardar listas como CSV
        PlayerPrefs.SetString("Personajes", string.Join(",", datos.personajesDesbloqueados));
        PlayerPrefs.SetString("Escenarios", string.Join(",", datos.escenariosDesbloqueados));

        PlayerPrefs.Save();
    }

    private void Cargar()
    {
        datos.nombreJugador = PlayerPrefs.GetString("NombreJugador", "Invitado");
        datos.peleasGanadas = PlayerPrefs.GetInt("PeleasGanadas", 0);

        string csvPersonajes = PlayerPrefs.GetString("Personajes", "");
        string csvEscenarios = PlayerPrefs.GetString("Escenarios", "");

        datos.personajesDesbloqueados = new List<string>();
        datos.escenariosDesbloqueados = new List<string>();

        // Si no hay nada guardado aún, configurar por defecto opción 2
        if (string.IsNullOrEmpty(csvPersonajes))
            datos.personajesDesbloqueados.Add("uribe");   // personaje inicial
        else
            datos.personajesDesbloqueados.AddRange(SplitCSV(csvPersonajes));

        if (string.IsNullOrEmpty(csvEscenarios))
            datos.escenariosDesbloqueados.Add("ciudad");  // escenario inicial
        else
            datos.escenariosDesbloqueados.AddRange(SplitCSV(csvEscenarios));

        // Asegurar nombre por defecto
        if (string.IsNullOrWhiteSpace(datos.nombreJugador))
            datos.nombreJugador = "Invitado";
            
            Debug.Log("Cargado nombre: " + datos.nombreJugador);
    }

    private List<string> SplitCSV(string csv)
    {
        var lista = new List<string>();
        var partes = csv.Split(',');
        foreach (var p in partes)
        {
            var t = p.Trim();
            if (!string.IsNullOrEmpty(t) && !lista.Contains(t))
                lista.Add(t);
        }
        return lista;
    }
}
