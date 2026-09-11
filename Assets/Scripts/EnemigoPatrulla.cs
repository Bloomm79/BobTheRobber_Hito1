using UnityEngine;

public class EnemigoPatrulla : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    [Tooltip("Velocidad de desplazamiento del personaje")]
    [SerializeField] private float velocidad = 2f;

    [Tooltip("Distancia en metros que se aleja del centro a cada lado")]
    [SerializeField] private float distanciaPatrulla = 3f;

    [Tooltip("Activa esta casilla si el sprite original mira a la izquierda por defecto")]
    [SerializeField] private bool spriteOriginalMiraIzquierda = false;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float centroX;
    private bool haciaDerecha = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        centroX = transform.position.x;
    }

    void FixedUpdate()
    {
        float limiteDerecho = centroX + distanciaPatrulla;
        float limiteIzquierdo = centroX - distanciaPatrulla;

        if (haciaDerecha)
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);
            ActualizarGiro(true);

            if (transform.position.x >= limiteDerecho)
            {
                haciaDerecha = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
            ActualizarGiro(false);

            if (transform.position.x <= limiteIzquierdo)
            {
                haciaDerecha = true;
            }
        }
    }

    void ActualizarGiro(bool moviendoALaDerecha)
    {
        if (sr == null) return;

        // Si el dibujo original mira a la derecha:
        // al ir a la derecha no se voltea (false), al ir a la izquierda sí (true).
        if (!spriteOriginalMiraIzquierda)
        {
            sr.flipX = !moviendoALaDerecha;
        }
        else
        {
            sr.flipX = moviendoALaDerecha;
        }
    }

    // Dibuja una línea roja en la ventana Scene para ver los límites antes de darle Play
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float origen = Application.isPlaying ? centroX : transform.position.x;
        Vector3 inicio = new Vector3(origen - distanciaPatrulla, transform.position.y, transform.position.z);
        Vector3 fin = new Vector3(origen + distanciaPatrulla, transform.position.y, transform.position.z);

        Gizmos.DrawLine(inicio, fin);
        Gizmos.DrawSphere(inicio, 0.1f);
        Gizmos.DrawSphere(fin, 0.1f);
    }
}