using UnityEngine;
using Cinemachine;

public class POVExtension : CinemachineExtension
{
    [SerializeField] private float clampMaxVerticalAngle = 80f;
    [SerializeField] private float clampMinVerticalAngle = 25f;
    [SerializeField] private float clampHorizontalAngle = 30f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;

    private InputManager inputManager;
    private Vector3 startingRotation;

    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if(startingRotation == null)
                    startingRotation = transform.localRotation.eulerAngles;

                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaInput.x * horizontalSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * verticalSpeed  * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y,-clampMinVerticalAngle,clampMaxVerticalAngle);
                startingRotation.x = Mathf.Clamp(startingRotation.x, -clampHorizontalAngle, clampHorizontalAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y,startingRotation.x,0f);
            }
        }
    }
}
