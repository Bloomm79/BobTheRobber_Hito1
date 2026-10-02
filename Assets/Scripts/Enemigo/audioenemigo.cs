using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioEnemigo : MonoBehaviour
{
    [Header("Clips de Sonido")]
    [Tooltip("Sonido de pasos al patrullar")]
    [SerializeField] private AudioClip clipPasos;

    [Tooltip("Sonido al topar un límite y cambiar de dirección")]
    [SerializeField] private AudioClip clipGiro;

    [Header("Ajustes de Reproducción")]
    [Tooltip("Tiempo en segundos entre cada sonido de paso")]
    [SerializeField] private float intervaloPasos = 0.4f;

    [Range(0f, 1f)]
    [SerializeField] private float volumen = 0.8f;

    private AudioSource audioSource;
    private float temporizadorPasos;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volumen;
    }

    public void ReproducirPasos()
    {
        if (clipPasos == null) return;

        temporizadorPasos -= Time.deltaTime;
        if (temporizadorPasos <= 0f)
        {
            audioSource.PlayOneShot(clipPasos, volumen);
            temporizadorPasos = intervaloPasos;
        }
    }

    public void ReproducirGiro()
    {
        if (clipGiro != null)
        {
            audioSource.PlayOneShot(clipGiro, volumen);
        }
    }
}