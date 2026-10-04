using UnityEngine;
public class CamaraController : MonoBehaviour
{
    Vector3 offset = Vector3.zero;
    public GameObject jugador;
    float distance;

    void Start()
    {
        offset = transform.position - jugador.transform.position;
    }
    void LateUpdate()
    {
        distance = Vector3.Distance(transform.position, jugador.transform.position);
        Debug.Log(distance);
        transform.position = Vector3.Lerp(transform.position, jugador.transform.position + offset, 15 * Time.deltaTime);
    }
}