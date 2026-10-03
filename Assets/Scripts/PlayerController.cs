using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float rapidezDesplazamiento = 10.0f;
    Animation Animation;

    Vector3 Movimiento = Vector3.zero;
    Vector2 input;
    public float magnitudSalto;
    Rigidbody rb;

    public LayerMask capaPiso;
    public CapsuleCollider col;

    private enum States {Piso, Aire };
    States current_state;

    void Start()
    {
        current_state = States.Piso;
        rb = GetComponent<Rigidbody>();
        Animation = GetComponent<Animation>();
        col = GetComponent<CapsuleCollider>();

    }

    void Update(){
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        Movimiento = new Vector3(input.x, 0.0f, input.y);

        if (input != Vector2.zero){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Movimiento),15 * Time.deltaTime);
            //Mathf.LerpAngle(transform.rotation, Vector3.Angle(transform.forward, Movimiento), Time.deltaTime),
        }

        if (current_state == States.Piso)
        {
            PisoState();
        }
        else if (current_state == States.Aire)
        {
            AireState();
        }
    }

    private void PisoState()
    {
        transform.Translate(Movimiento * rapidezDesplazamiento * Time.deltaTime, Space.World);

        if (input != Vector2.zero)
        {
            Animation.Play("PlayerMovement");
        }
        else
        {
            Animation.Play("PlayerIdle");
        }

        if (Input.GetKeyDown(KeyCode.Space) && EstaEnPiso()){
            rb.AddForce(Vector3.up * magnitudSalto, ForceMode.Impulse);
            Animation.Play("PlayerJump");
            current_state = States.Aire;
        }
    }

    private void AireState(){

        transform.Translate(Movimiento * rapidezDesplazamiento * Time.deltaTime, Space.World);

        Animation.Play("PlayerJump");
        if (EstaEnPiso() && rb.velocity.y < 0)
        {
            current_state = States.Piso;
        }
    }

    private bool EstaEnPiso()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
        col.bounds.min.y, col.bounds.center.z), col.radius * .1f, capaPiso);
    }

}

