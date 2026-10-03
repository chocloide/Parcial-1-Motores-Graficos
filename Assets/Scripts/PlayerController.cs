using UnityEngine;
public class ControlJugador : MonoBehaviour
{
    public float rapidezDesplazamiento = 10.0f;
    Animation Animation;

    Vector3 Movimiento = Vector2.zero;
    Vector2 input;
    public float magnitudSalto;
    Rigidbody rb;

    public LayerMask capaPiso;
    public CapsuleCollider col;

    bool saltando = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Animation = GetComponent<Animation>();
        col = GetComponent<CapsuleCollider>();

    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space) && EstaEnPiso()){
            rb.AddForce(Vector3.up * magnitudSalto, ForceMode.Impulse);
            Animation.Play("PlayerJump");
            saltando = true;
        }

        if (EstaEnPiso() && rb.velocity.y < 0){
            saltando = false;
        }

        input.x = Input.GetAxis("Horizontal") ;
        input.y = Input.GetAxis("Vertical");

        Movimiento.x = input.x * rapidezDesplazamiento;
        Movimiento.z = input.y * rapidezDesplazamiento;
        
        if (saltando == false)
        {
            if (input != Vector2.zero)
            {
                Animation.Play("PlayerMovement");
            }
            else
            {
                Animation.Play("PlayerIdle");
            }
        }
        
        Movimiento *= Time.deltaTime;
        transform.Translate(Movimiento.x, 0, Movimiento.z);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private bool EstaEnPiso()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
        col.bounds.min.y, col.bounds.center.z), col.radius * .1f, capaPiso);
    }

}

