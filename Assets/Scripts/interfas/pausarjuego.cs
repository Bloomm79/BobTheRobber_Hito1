using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar o volver al menú principal

public class PausarJuego : MonoBehaviour
{
    [Header("Interfaz de Pausa")]
    [Tooltip("Arrastra aquí el panel o ventana del menú de pausa")]
    public GameObject menuPausa;
    
    [Header("Estado")]
    public bool juegoPausado = false;

    private void Start()
    {
        // Aseguramos que el tiempo comience corriendo normalmente y el menú oculto
        Time.timeScale = 1f;
        if (menuPausa != null)
        {
            menuPausa.SetActive(false);
        }
    }

    private void Update()
    {
        // Detecta la tecla Escape para pausar o reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    // Función para continuar jugando
    public void Reanudar()
    {
        if (menuPausa != null)
        {
            menuPausa.SetActive(false);
        }

        Time.timeScale = 1f; // Reanuda la velocidad normal del juego
        juegoPausado = false;
    }

    // Función para congelar el juego
    public void Pausar()
    {
        if (menuPausa != null)
        {
            menuPausa.SetActive(true);
        }

        Time.timeScale = 0f; // Congela físicas, movimientos y animaciones
        juegoPausado = true;
    }

    // Función para el botón 'Reiniciar'
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // IMPORTANTE: reactivar el tiempo antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Función para el botón 'Volver al Menú Principal'
    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f; // IMPORTANTE: reactivar el tiempo antes de salir
        SceneManager.LoadScene("MenuPrincipal");
    }
}