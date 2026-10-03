using UnityEngine;
public class CamaraController : MonoBehaviour
{
    Vector3 offset = Vector3.zero;
    public GameObject jugador;

    void Start()
    {
        offset = transform.position - jugador.transform.position;
    }
    void LateUpdate()
    {
        transform.position = jugador.transform.position + offset;
        //transform.position.x = offset.x + jugador.transform.position.x;
        //transform.position.y = offset.y + jugador.transform.position.y;
        //jugador.transform.localRotation = Quaternion.AngleAxis(mouseMirar.x, jugador.transform.up);
    }
}