using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        //update controller
        UpdateController();

        //cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    //public void respawn()
    //{

    //}


    //-----Controller-Choice-----//
    [Header("Controller Choice")]

    public bool controllerUse = false;

    //Gameplay
    public string dirX = "Joystick X";
    public string dirY = "Joystick Y";
    public string rudder = "Bumper";
    public string speedUp = "RightTrigger";
    public string speedDown = "LeftTrigger";

    //Camera
    string camX = "CamX";
    string camY = "Camy";
    public Vector2 camMove;
    public string changeCenterCam = "Realign";
    public float camYSpeed;
    public float camXSpeed;

    public int throttleSensitivity;

    public void ControllerChoice()
    {
        controllerUse = !controllerUse;

        if (controllerUse)
        {
            //Gameplay
            dirX = "Joystick X";
            dirY = "Joystick Y";
            rudder = "Bumper";
            speedUp = "RightTrigger";
            speedDown = "LeftTrigger";
            throttleSensitivity = 1;

            //Camera
            camX = "CamX";
            camY = "CamY";
            changeCenterCam = "Realign";
            camYSpeed = 80;
            camXSpeed = 120;
        }
        else
        {
            //Gameplay
            dirX = "Mouse X";
            dirY = "Mouse Y";
            rudder = "KeyRudder";
            speedUp = "Mouse ScrollWheel";
            speedDown = "Mouse ScrollWheel";
            throttleSensitivity = 10;

            //Camera
            camX = "Mouse X";
            camY = "Mouse Y";
            changeCenterCam = "mouse scroll wheel click";
            camYSpeed = 150;
            camXSpeed = 190;
        }
    }

    void UpdateController()
    {
        if (controllerUse)
        {
            camMove = new Vector2(-Input.GetAxis(camY), Input.GetAxis(camX));
        }
        else
        {
            camMove = Input.GetMouseButton(1) ? new Vector2(-Input.GetAxis(camY), Input.GetAxis(camX)) : new Vector2(0, 0);
        }
    }
}
