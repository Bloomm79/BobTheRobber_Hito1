using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenu : MonoBehaviour
{
    [Header("Paneles de la Interfaz")]
    [SerializeField] private GameObject panelPrincipal;     // El cuadro donde está Play y Niveles
    [SerializeField] private GameObject panelNiveles;       // El cuadro con los botones Nivel 1, 2, 3

    // Botón PLAY: Inicia directamente en el primer nivel
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel_1"); // Nombre exacto de tu escena
    }

    // Funciones para cargar cada nivel específico
    public void CargarNivel1()
    {
        SceneManager.LoadScene("Nivel_1");
    }

    public void CargarNivel2()
    {
        SceneManager.LoadScene("Nivel_2");
    }

    public void CargarNivel3()
    {
        SceneManager.LoadScene("Nivel_3");
    }

    // Abrir o cerrar el panel selector de niveles
    public void AbrirPanelNiveles()
    {
        if (panelPrincipal != null) panelPrincipal.SetActive(false);
        if (panelNiveles != null) panelNiveles.SetActive(true);
    }

    public void VolverAlMenuPrincipal()
    {
        if (panelNiveles != null) panelNiveles.SetActive(false);
        if (panelPrincipal != null) panelPrincipal.SetActive(true);
    }

    // Botón Salir (por si lo necesitas)
    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Juego cerrado");
    }
}
