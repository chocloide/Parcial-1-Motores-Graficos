using UnityEngine;
public class ControlJugador : MonoBehaviour
{
    public float rapidezDesplazamiento = 10.0f;
    Animation Animation;
    Vector3 Movimiento = Vector2.zero;
    Rigidbody rb;
    public LayerMask capaPiso;
    public float magnitudSalto;
    public CapsuleCollider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Animation = GetComponent<Animation>();
        col = GetComponent<CapsuleCollider>();

    }
    void Update()
    {
        Movimiento.x = Input.GetAxis("Horizontal") * rapidezDesplazamiento;
        Movimiento.z =Input.GetAxis("Vertical") * rapidezDesplazamiento;

        Movimiento *= Time.deltaTime;
        transform.Translate(Movimiento.x, 0, Movimiento.z);

        if (EstaEnPiso())
        {
            if (Movimiento != Vector3.zero)
            {
                Animation.Play("Movement");
            }
            else
            {
                Animation.Play("Idle");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && EstaEnPiso())
        {
            rb.AddForce(Vector3.up * magnitudSalto, ForceMode.Impulse);
            Animation.Play("Jump");
        }

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private bool EstaEnPiso()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
        col.bounds.min.y, col.bounds.center.z), col.radius * .9f, capaPiso);
    }

}

