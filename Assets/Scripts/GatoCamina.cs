using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class GatoCamina : MonoBehaviour
{
    [Header("Movimiento Horizontal")]
    [SerializeField] private float velocidadCaminar = 4.5f;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto = 6.5f;
    [SerializeField] private Transform detectorSuelo;
    [SerializeField] private float radioDeteccion = 0.25f;
    [SerializeField] private LayerMask capaSuelo;

    [Header("Escaleras (W / S)")]
    [SerializeField] private float velocidadEscalar = 3.5f;

    [Header("Inventario")]
    [SerializeField] private bool tieneLlave = false;
    [SerializeField] private AudioClip sonidoLlave; // Audio asignable desde el Inspector

    [Header("Orientación")]
    [SerializeField] private bool spriteOriginalMiraDerecha = true;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Collider2D col;

    private float entradaHorizontal;
    private float entradaVertical;
    private bool estaEnSuelo;
    private bool estaEnEscalera;
    private float gravedadInicial;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        gravedadInicial = rb.gravityScale;
    }

    void Update()
    {
        // Controles A (-1) y D (+1)
        entradaHorizontal = Input.GetAxisRaw("Horizontal");

        // Controles S (-1) y W (+1)
        entradaVertical = Input.GetAxisRaw("Vertical");

        // Detección de suelo
        if (detectorSuelo != null)
        {
            estaEnSuelo = Physics2D.OverlapCircle(detectorSuelo.position, radioDeteccion, capaSuelo);
        }
        else if (col != null)
        {
            estaEnSuelo = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + 0.15f, capaSuelo);
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo && !estaEnEscalera)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        }

        // Control de animación
        if (anim != null)
        {
            bool seMueve = Mathf.Abs(entradaHorizontal) > 0.01f;
            anim.SetBool("estaCaminando", seMueve);
        }

        // Giro visual
        if (entradaHorizontal > 0.01f)
        {
            sr.flipX = !spriteOriginalMiraDerecha;
        }
        else if (entradaHorizontal < -0.01f)
        {
            sr.flipX = spriteOriginalMiraDerecha;
        }
    }

    void FixedUpdate()
    {
        if (estaEnEscalera)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(entradaHorizontal * velocidadCaminar, entradaVertical * velocidadEscalar);
        }
        else
        {
            rb.gravityScale = gravedadInicial;
            rb.velocity = new Vector2(entradaHorizontal * velocidadCaminar, rb.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Detectar escaleras
        if (collision.CompareTag("Escalera"))
        {
            estaEnEscalera = true;
        }

        // Detectar y recoger la Llave
        if (collision.CompareTag("Llave"))
        {
            tieneLlave = true;

            // Reproduce el sonido si está asignado
            if (sonidoLlave != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(sonidoLlave, Camera.main.transform.position);
            }

            Destroy(collision.gameObject); // Destruye el objeto de la llave
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escalera"))
        {
            estaEnEscalera = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Daño de los hongos
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            VidaGato vida = GetComponent<VidaGato>();
            if (vida != null)
            {
                vida.RecibirDanio(1);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (detectorSuelo != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(detectorSuelo.position, radioDeteccion);
        }
    }

    // Propiedad pública por si una puerta necesita consultar si ya tiene la llave
    public bool TieneLlave => tieneLlave;
}   