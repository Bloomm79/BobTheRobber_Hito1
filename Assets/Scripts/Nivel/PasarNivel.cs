using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarNivel : MonoBehaviour
{
    [Header("Nombre de la siguiente escena")]
    [Tooltip("Escribe aquí el nombre exacto de la escena a cargar (ej: Nivel 3)")]
    [SerializeField] private string siguienteNivel = "Nivel 3";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta si entra el gato (por tag o por el nombre del sprite)
        if (collision.CompareTag("Player") || collision.gameObject.name.Contains("cat"))
        {
            // Opción 1: Carga por el nombre escrito en el Inspector
            if (!string.IsNullOrEmpty(siguienteNivel))
            {
                SceneManager.LoadScene(siguienteNivel);
            }
            else
            {
                // Opción 2: Carga el siguiente índice en Build Settings
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}