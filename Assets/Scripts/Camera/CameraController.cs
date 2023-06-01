using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 dragOrigin;
    Camera mainCamera;
    public static CameraController Instance;
    [SerializeField] private CameraSettings _settings;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }
    void LateUpdate()
    {
        HandleMovementClickAndDrag();
    }

    void HandleMovementClickAndDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0))
            return;

        Vector3 pos = mainCamera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(0, 0, -pos.y * _settings.dragSpeed);

        transform.Translate(move, Space.World);

        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, _settings.minZ, _settings.maxZ));
    }
}