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

    private enum States {Idle, Run, Jump };
    States current_state;

    void Start()
    {
        current_state = States.Idle;
        rb = GetComponent<Rigidbody>();
        Animation = GetComponent<Animation>();
        col = GetComponent<CapsuleCollider>();

    }

    void Update(){
        input.x = Input.GetAxis("Horizontal");
        Movimiento = new Vector3(input.x, 0.0f, 1.0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Movimiento),15 * Time.deltaTime);

        switch (current_state)
        {
            case States.Idle:
                IdleState();
                break;
            case States.Run:
                RunState();
                break;
            case States.Jump:
                JumpState();
                break;
        }
    }

    private void IdleState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            current_state = States.Run;
        }
    }

    private void RunState()
    {
        Animation.Play("PlayerMovement");
        transform.Translate(Movimiento * rapidezDesplazamiento * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && EstaEnPiso()){
            rb.AddForce(Vector3.up * magnitudSalto, ForceMode.Impulse);
            Animation.Play("PlayerJump");
            current_state = States.Jump;
        }
    }

    private void JumpState(){

        transform.Translate(Movimiento * rapidezDesplazamiento * Time.deltaTime, Space.World);

        Animation.Play("PlayerJump");
        if (EstaEnPiso() && rb.velocity.y < 0)
        {
            current_state = States.Run;
        }
    }

    private bool EstaEnPiso()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
        col.bounds.min.y, col.bounds.center.z), col.radius * .1f, capaPiso);
    }

}

