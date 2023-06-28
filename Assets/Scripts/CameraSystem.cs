using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour {

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float followOffsetMinY = 10f;
    [SerializeField] private float followOffsetMaxY = 50f;
    
    private Vector3 followOffset;
    
    private void Awake() {
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    private void Update() {
        HandleCameraMovement();
        HandleCameraRotation();
        HandleCameraZoom_LowerY();
    }

    private void HandleCameraMovement() {
        Vector3 inputDir = new Vector3(0, 0, 0);
        inputDir.z = Input.GetAxis("Vertical");
        inputDir.x = Input.GetAxis("Horizontal");
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        float moveSpeed = 50f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraRotation() {
        float rotateDir = Input.GetAxis("Rotation");
        float rotateSpeed = 100f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }

    private void HandleCameraZoom_LowerY() {
        followOffset.y += Input.GetAxis("Mouse ScrollWheel") * -15f;
        followOffset.y = Mathf.Clamp(followOffset.y, followOffsetMinY, followOffsetMaxY);
        float zoomSpeed = 10f;
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
    }

}