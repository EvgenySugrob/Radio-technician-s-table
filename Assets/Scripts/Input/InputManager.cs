using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static InputManager _instance;
    private bool isRotationCamera;

    public static InputManager Instance
    {
        get { return _instance; }
    }

    private PlayerControl playerControl;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance= this;
        }
        playerControl = new PlayerControl();

        playerControl.Action.RightMousePress.performed += x => RightMousePressed();
        playerControl.Action.RightMouseRelease.performed += x => RightMouseRelease();
    }



    private void OnEnable()
    {
        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControl.Player.Movement.ReadValue<Vector2>();
    }
    public Vector2 GetMouseDelta()
    {
        return playerControl.Player.Look.ReadValue<Vector2>();
    }

    public bool GetCameraRotationState()
    {
        return isRotationCamera;
    }

    private void RightMousePressed()
    {
        isRotationCamera=true;
    }
    private void RightMouseRelease()
    {
        isRotationCamera=false;
    }
}
