using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    Transform pivot, target, back, f18;

    GameManager gameManager;

    void Awake()
    {
        target = transform.parent.transform;
        pivot = target.parent.transform;
        back = GameObject.Find("F-18_TargetCul").transform;
        f18 = pivot.parent.transform;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        target.SetPositionAndRotation(back.position, Quaternion.Euler(0, 0, 0));
    }

    void FixedUpdate()
    {
        if (target)
        {
            pivot.Rotate(0, gameManager.camMove.y * gameManager.camYSpeed * Time.fixedDeltaTime, 0, Space.World);
            pivot.Rotate(gameManager.camMove.x * gameManager.camXSpeed * Time.fixedDeltaTime, 0, 0, Space.Self);

            if (Input.GetButtonDown(gameManager.changeCenterCam)) reset();

            transform.LookAt(pivot, f18.up);
        }
    }

    void reset()
    {
        pivot.rotation = Quaternion.Euler(0, 0, 0);
        target.SetPositionAndRotation(back.position, Quaternion.Euler(0, 0, 0));
    }
}