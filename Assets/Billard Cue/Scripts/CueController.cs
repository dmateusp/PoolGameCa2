using UnityEngine;

public class CueController : MonoBehaviour {

    private GameObject whiteball;
    private Vector3 lastMousePos;
    private Collider cueCollider;
    private Rigidbody cueRigidBody;
    private bool shooting;
    private GameObject gameController;

	// Use this for initialization
	void Start () {
        whiteball = GameObject.FindGameObjectWithTag("WhiteBall");
        lastMousePos = Input.mousePosition;
        cueRigidBody = transform.GetComponent<Rigidbody>();
        cueCollider = cueRigidBody.GetComponent<Collider>();
        shooting = false;
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

	void FixedUpdate () {
        if (!shooting)
        {
            cueCollider.enabled = false;

            Vector3 mousePos = Input.mousePosition;
            Vector3 diffMousePos = mousePos - lastMousePos;
            lastMousePos = mousePos;

            if (diffMousePos.x > 0)
                transform.Translate(Vector3.right * 0.1f * diffMousePos.x);
            else
                transform.Translate(Vector3.left * 0.1f * -diffMousePos.x);



            transform.LookAt(whiteball.transform);

            transform.position = (transform.position - whiteball.transform.position).normalized * 10 + whiteball.transform.position;

            if (Input.GetMouseButtonDown(0))
            { // left click
                Vector3 shoot = 1000 * transform.forward;
                cueRigidBody.AddForce(shoot);
                shooting = true;
            }
        } else
        {
            cueCollider.enabled = true;
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Border")
        {
            shooting = false;
            cueRigidBody.velocity = Vector3.zero;
            cueRigidBody.angularVelocity = Vector3.zero;
            gameController.SendMessage("ChangePlayer", true);
        }
    }
}
