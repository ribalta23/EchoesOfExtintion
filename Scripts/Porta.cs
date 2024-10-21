using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public GameObject portaEsquerra;
    public GameObject portaDreta;

    public float distanciaEsquerra = 8f;
    public float distanciaDreta = -8f;

    public float duracio = 2f;

    // Posiciones originales de las puertas
    private Vector3 posicionOriginalEsquerra;
    private Vector3 posicionOriginalDreta;

    private void Start()
    {
        // Guardamos las posiciones iniciales de las puertas
        posicionOriginalEsquerra = portaEsquerra.transform.position;
        posicionOriginalDreta = portaDreta.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Mover puertas a nuevas posiciones
        StartCoroutine(MoureEsquerra(portaEsquerra, posicionOriginalEsquerra + new Vector3(0, 0, distanciaEsquerra)));
        StartCoroutine(MoureDreta(portaDreta, posicionOriginalDreta + new Vector3(0, 0, distanciaDreta)));
    }

    private void OnTriggerExit(Collider other)
    {
        // Mover puertas de vuelta a sus posiciones originales
        StartCoroutine(MoureEsquerra(portaEsquerra, posicionOriginalEsquerra));
        StartCoroutine(MoureDreta(portaDreta, posicionOriginalDreta));
    }

    private IEnumerator MoureDreta(GameObject objeto, Vector3 posicionFinal)
    {
        Vector3 posicionInicial = objeto.transform.position;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracio)
        {
            objeto.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempoTranscurrido / duracio);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        objeto.transform.position = posicionFinal;
    }

    private IEnumerator MoureEsquerra(GameObject objeto, Vector3 posicionFinal)
    {
        Vector3 posicionInicial = objeto.transform.position;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracio)
        {
            objeto.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempoTranscurrido / duracio);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        objeto.transform.position = posicionFinal;
    }
}
