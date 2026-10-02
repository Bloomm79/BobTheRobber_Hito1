using UnityEngine;

public class Gatoinventario : MonoBehaviour
{
    [Header("Inventario de Llaves")]
    [SerializeField] private bool tieneLlave = false;
    [SerializeField] private AudioClip sonidoLlave;

    // Propiedad pública accesible desde las puertas
    public bool TieneLlave => tieneLlave;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Llave"))
        {
            tieneLlave = true;

            if (sonidoLlave != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(sonidoLlave, Camera.main.transform.position);
            }

            Destroy(collision.gameObject);
        }
    }
}