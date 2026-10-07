using UnityEngine;
public class CamaraController : MonoBehaviour
{
    Vector3 offset = Vector3.zero;
    Vector3 shakeOffset = Vector3.zero;
    float traumaPower = 2f;
    float trauma = 0f;
    public float speed = 15f;
    public GameObject jugador;

    void Start(){
        offset = transform.position - jugador.transform.position;
    }

    void LateUpdate(){
        if (trauma > 0f){
            trauma = Mathf.Max(trauma - Time.deltaTime,0);
            shake();
        }
       
        transform.position = Vector3.Lerp(transform.position, 
            jugador.transform.position + offset + shakeOffset,
            speed * Time.deltaTime);
    }

    private void shake(){
        float Shake = Mathf.Pow(trauma, traumaPower);
        shakeOffset = new Vector3(Shake * Random.Range(-1f, 1f), Shake * Random.Range(-1f, 1f),0);
        
    }

    public void addTrauma(float Trauma){
        trauma = Trauma;
    }

}