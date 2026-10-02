using UnityEngine;

public class CamaraSigue : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform objetivo;

    [Header("Ajustes de Movimiento")]
    [Range(0.01f, 1f)]
    public float suavizado = 0.125f;
    public Vector3 compensacion = new Vector3(0f, 1f, -10f);

    private void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 posicionDeseada = objetivo.position + compensacion;
        posicionDeseada.z = -10f; // Mantiene fija la profundidad de la camara 2D

        Vector3 posicionSuave = Vector3.Lerp(transform.position, posicionDeseada, suavizado);
        transform.position = posicionSuave;
    }
}