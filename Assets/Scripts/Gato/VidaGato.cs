using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaGato : MonoBehaviour
{
    [Header("Configuración de Vidas")]
    [SerializeField] private int vidas = 3;
    [SerializeField] private GameObject[] corazones;

    [Header("Efecto Invulnerabilidad")]
    [SerializeField] private float tiempoInvulnerabilidad = 1.2f;
    [SerializeField] private float velocidadParpadeo = 0.1f;

    private bool esInvulnerable = false;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        ActualizarCorazones();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            RecibirDanio(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            RecibirDanio(1);
        }
    }

    public void RecibirDanio(int cantidad)
    {
        if (esInvulnerable) return;

        vidas -= cantidad;
        ActualizarCorazones();

        if (vidas <= 0)
        {
            // Si existe GameManager, activa el panel de Game Over
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ActivarGameOver();
            }
            else
            {
                // Respaldo por si juegas en una escena sin GameManager
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            StartCoroutine(RutinaInvulnerabilidad());
        }
    }

    private void ActualizarCorazones()
    {
        if (corazones == null) return;

        for (int i = 0; i < corazones.Length; i++)
        {
            if (corazones[i] != null)
            {
                corazones[i].SetActive(i < vidas);
            }
        }
    }

    private IEnumerator RutinaInvulnerabilidad()
    {
        esInvulnerable = true;

        float tiempoPasado = 0f;
        while (tiempoPasado < tiempoInvulnerabilidad)
        {
            sr.color = new Color(1f, 1f, 1f, 0.2f);
            yield return new WaitForSeconds(velocidadParpadeo);
            sr.color = Color.white;
            yield return new WaitForSeconds(velocidadParpadeo);

            tiempoPasado += (velocidadParpadeo * 2);
        }

        sr.color = Color.white;
        esInvulnerable = false;
    }
}