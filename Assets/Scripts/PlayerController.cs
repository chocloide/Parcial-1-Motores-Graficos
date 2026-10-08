using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float rapidezDesplazamiento = 10.0f;
    public float magnitudSalto;

    Vector3 Movimiento = Vector3.zero;
    Vector2 input;

    public LayerMask capaPiso;
    public CapsuleCollider col;
    Animation Animation;
    Rigidbody rb;
    public CamaraController camaraController;
    public AudioManager audioManager;

    private enum States {Idle, Run, Jump, Slide,Knockback };
    States current_state;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        Animation = GetComponent<Animation>();
        col = GetComponent<CapsuleCollider>();

    }

    void osbtacleDetection()
    {
        Ray rayTop = new Ray(transform.position + col.center * 2, transform.forward);
        Ray rayBotton = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(rayTop.origin, rayTop.direction * 0.65f, Color.blue);
        Debug.DrawRay(rayBotton.origin, rayBotton.direction * 0.65f, Color.green);
        
        if (Physics.Raycast(rayTop, out hit, 0.65f) || Physics.Raycast(rayBotton, out hit, 0.65f))
        {
            if (hit.collider.tag == "Obstaculo" && current_state != States.Knockback)
            {
                camaraController.setTrauma(1f);
                rb.AddForce(Vector3.back * 10, ForceMode.Force);
                rb.AddForce(Vector3.up * 10, ForceMode.Force);

                Animation.Play("PlayerKnock");
                current_state = States.Knockback;
                audioManager.PlayAudio(2,0.2f);

                col.height = 1.8f;
                col.center = new Vector3(0, 0.45f, 0);
            }
        }
    }

    void Update(){
        input.x = Input.GetAxis("Horizontal");
        Movimiento = new Vector3(input.x, 0.0f, 1.0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Movimiento),15 * Time.deltaTime);

        osbtacleDetection();

        //Maquina de estados simple
        switch (current_state)
        {
            case States.Run:
                RunState();
                break;
            case States.Jump:
                JumpState();
                break;
            case States.Slide:
                SlideState();
                break;
            case States.Knockback:
                KnockState();
                break;
        }
    }

    public void startPlayer(){
        current_state= States.Run;
    }

    private void RunState(){
        Animation.Play("PlayerMovement");
        transform.Translate(Movimiento * rapidezDesplazamiento * Time.deltaTime, Space.World);

        if (EstaEnPiso()){
            if (Input.GetKeyDown(KeyCode.Space)){
                rb.AddForce(Vector3.up * magnitudSalto, ForceMode.Impulse);
                current_state = States.Jump;
                audioManager.PlayAudio(0);
            }
            if (Input.GetKeyDown(KeyCode.S)){
                col.height = 0.9f;
                col.center = new Vector3(0, 0, 0);
                current_state = States.Slide;
                audioManager.PlayAudio(1);
            }
        }
    }

    private void JumpState(){
        transform.Translate(Movimiento * rapidezDesplazamiento * Time.deltaTime, Space.World);
        Animation.Play("PlayerJump");

        if (!Input.GetKey(KeyCode.Space) && rb.velocity.y >= 3.5f){
            rb.AddForce(Vector3.down , ForceMode.Impulse);
        }
        if (EstaEnPiso() && rb.velocity.y < 0){
            current_state = States.Run;
        }
    }

    private void SlideState(){
        Animation.Play("PlayerSlide");
        transform.Translate(Movimiento * rapidezDesplazamiento * Time.deltaTime, Space.World);

        if (EstaEnPiso()){
            if (Input.GetKeyDown(KeyCode.Space)){
                rb.AddForce(Vector3.up * magnitudSalto, ForceMode.Impulse);
                rb.AddForce(Vector3.forward * 5, ForceMode.Impulse);
                current_state = States.Jump;
                audioManager.PlayAudio(0);
                col.height = 1.8f;
                col.center = new Vector3(0, 0.45f, 0);
            }
            if (!Input.GetKey(KeyCode.S) && EstaEnPiso()){
                current_state = States.Run;
                col.height = 1.8f;
                col.center = new Vector3(0, 0.45f, 0);
            }
        }
    }
    private void KnockState(){
        

        if (EstaEnPiso()){
            current_state = States.Run;
        }
    }

    private bool EstaEnPiso(){
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
        col.bounds.min.y, col.bounds.center.z), col.radius * .1f, capaPiso);
        
    }
}

