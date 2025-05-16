using System.Collections.Generic;
using UnityEngine;

public class ZonaDanio : MonoBehaviour
{
    public int da�oPorTic = 1;
    public float intervalo = 0.1f;

    private Dictionary<Portador, float> tiempoSiguienteTic = new Dictionary<Portador, float>();

    private void OnTriggerStay(Collider other)
    {
        Portador portador = other.GetComponent<Portador>();
        if (portador != null)
        {
            // Inicializar si no est� en el diccionario
            if (!tiempoSiguienteTic.ContainsKey(portador))
                tiempoSiguienteTic[portador] = Time.time;

            // Aplicar da�o si pas� el intervalo
            if (Time.time >= tiempoSiguienteTic[portador])
            {
                portador.RecibirDa�o(da�oPorTic);
                tiempoSiguienteTic[portador] = Time.time + intervalo;
                Debug.Log("El personaje esta recibiendo da�o");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Limpiar del diccionario cuando sale
        Portador portador = other.GetComponent<Portador>();
        if (portador != null && tiempoSiguienteTic.ContainsKey(portador))
        {
            tiempoSiguienteTic.Remove(portador);
        }
    }
}
