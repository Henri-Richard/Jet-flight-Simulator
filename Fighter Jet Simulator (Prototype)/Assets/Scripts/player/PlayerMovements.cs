using System.Collections;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    GameManager gameManager;

    Rigidbody rb;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //Calculate Speed
        StartCoroutine(Speed());

        //Get Controls
        gameManager.ControllerChoice();
    }

    void Update()
    {
        //General
        GeneralUpdate();

        //Rotation
        updateSteering();

        //Throttle
        updateThrottle();
    }

    //-----Update-General-----//
    [Header("General")]

    [SerializeField] LayerMask _GroundLayerMask;

    [SerializeField] bool grounded;

    void GeneralUpdate()
    {
        //Get grounded
        Ray ray = new Ray(transform.position + new Vector3(0, 0, 3.93f), -transform.up);
        grounded = Physics.Raycast(ray, 6f, _GroundLayerMask) ? true : false;

        //call liftOff
        lift();
    }


    //-----Update-Speed-----//
    [Header("Speed")]

    public float speedMS;

    IEnumerator Speed()
    {
        Vector3 lastPos = transform.position;
        yield return new WaitForSeconds(.1f);
        float dist = Vector3.Distance(lastPos, transform.position);
        speedMS = dist / .1f;
        StartCoroutine(Speed());
    }


    //-----lift-----//

    void lift()
    {
        if (speedMS > 79 && grounded) rb.AddForce(transform.up * 1000 * Time.deltaTime, ForceMode.Acceleration);
    }


    //-----Update-Steering-----//
    [Header("Rotate")]

    [SerializeField] AnimationCurve turnCurve;

    [SerializeField] Vector3 direction;
    [SerializeField] Vector3 turn;

    void updateSteering()
    {
        //Get stick direction
        if (gameManager.controllerUse)
        {
            direction.x = Input.GetAxis(gameManager.dirX);
            direction.y = Input.GetAxis(gameManager.dirY);
        }
        else
        {
            direction.x = Input.mousePosition.x;
            direction.x -= Screen.width / 2;
            direction.x /= (Screen.width / 2);
            direction.y = Input.mousePosition.y;
            direction.y -= Screen.height / 2;
            direction.y /= (Screen.height / 2);
        }

        //Get rudder direction
        direction.z = Input.GetAxis(gameManager.rudder);

        //Get turn speed
        turn.z = Mathf.Lerp(turn.z, turnCurve.Evaluate(-direction.x), 1);
        turn.x = Mathf.Lerp(turn.x, turnCurve.Evaluate(-direction.y), 1);
        turn.y = Mathf.Lerp(turn.y, direction.z / 4, 1);

        //Apply Rotation
        if (!grounded && !Input.GetButton("Fire2")) transform.Rotate(turn * (speedMS) * Time.deltaTime, Space.Self);
    }


    //-----Update-Throttle-----//
    [Header("Throttle")]

    [SerializeField] float desiredSpeed;
    [SerializeField] float speed;

    void updateThrottle()
    {
        //Get Trigger Input
        desiredSpeed += (Input.GetAxis(gameManager.speedUp) * gameManager.throttleSensitivity) / 2;
        desiredSpeed += (Input.GetAxis(gameManager.speedDown) * gameManager.throttleSensitivity) / 2;

        //Get Speed Inpulse
        if (speed < desiredSpeed) speed += .5f;
        if (speed > desiredSpeed) speed -= .2f;

        //Fix limits
        if (speed < 0) speed = 0;
        if (speed > 175) speed = 175;
        if (desiredSpeed < 0) desiredSpeed = 0;
        if (desiredSpeed > 175) desiredSpeed = 175;

        //Dragforce
        rb.drag = .049f * speedMS;

        //Apply Throttle
        rb.AddForce(transform.forward * speed * 100 * Time.deltaTime, ForceMode.Acceleration);
    }


    //-----Destroy-----//

    public void destroy(GameObject parent)
    {
        //GameObject.Find("GameManager").GetComponent<GameManager>().respawn();
        Destroy(parent);
    }
}
