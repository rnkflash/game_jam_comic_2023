    using UnityEngine;
    using UnityEngine.InputSystem;

    public class CameraController : MonoBehaviour
    {
        [Header("Horizontal Translation")]
        [SerializeField] private float maxSpeed = 10f;
        private float speed;
        [Header("Horizontal Translation")]
        [SerializeField] private float acceleration = 10f;
        [Header("Horizontal Translation")]
        [SerializeField] private float damping = 15f;

        [Header("Vertical Translation")]
        [SerializeField] private float stepSize = 2f;
        [Header("Vertical Translation")]
        [SerializeField] private float zoomDampening = 7.5f;
        [Header("Vertical Translation")]
        [SerializeField] private float minHeight = 5f;
        [Header("Vertical Translation")]
        [SerializeField] private float maxHeight = 50f;
        [Header("Vertical Translation")]
        [SerializeField] private float zoomSpeed = 2f;

        [Header("Rotation")]
        [SerializeField] private float maxMouseRotationSpeed = 0.2f;
        [SerializeField] private float maxKeyboardRotationSpeed = 1.0f;

        [Header("Edge Movement")]
        [SerializeField] [Range(0f,0.1f)] private float edgeTolerance = 0.05f;

        [Header("Edge Movement")]
        [SerializeField] private bool enableEdgeMovement = false;

        [SerializeField] private Transform looktAtTarget;

        private CameraControl cameraActions;
        private InputAction keyboardMovement;
        private InputAction keyboardRotate;
        private Transform cameraTransform;
        private Vector3 targetPosition;
        private float zoomHeight;
        private Vector3 horizontalVelocity;
        private Vector3 lastPosition;
        Vector3 startDrag;
        private new Camera camera;

        private void Awake()
        {
            cameraActions = new CameraControl();
            camera = this.GetComponentInChildren<Camera>();
            cameraTransform = camera.transform;
        }

        private void OnEnable()
        {
            zoomHeight = cameraTransform.localPosition.y;
            cameraTransform.LookAt(looktAtTarget);

            lastPosition = this.transform.position;

            keyboardMovement = cameraActions.Camera.Movement;
            keyboardRotate = cameraActions.Camera.KeyboardRotate;
            cameraActions.Camera.Rotate.performed += OnRotateCamera;
            cameraActions.Camera.Zoom.performed += OnZoomCamera;
            cameraActions.Camera.Enable();
        }

        private void OnDisable()
        {
            cameraActions.Camera.Rotate.performed -= OnRotateCamera;
            cameraActions.Camera.Zoom.performed -= OnZoomCamera;
            cameraActions.Camera.Disable();
        }

        private void Update()
        {
            //inputs
            GetKeyboardMovement();
            GetKeyboardRotate();
            CheckMouseAtScreenEdge();
            DragCamera();

            //move base and camera objects
            UpdateVelocity();
            UpdateBasePosition();
            UpdateCameraPosition();
        }

        private void UpdateVelocity()
        {
            horizontalVelocity = (this.transform.position - lastPosition) / Time.deltaTime;
            horizontalVelocity.y = 0f;
            lastPosition = this.transform.position;
        }

        private void GetKeyboardMovement()
        {
            Vector3 inputValue = keyboardMovement.ReadValue<Vector2>().x * GetCameraRight()
                        + keyboardMovement.ReadValue<Vector2>().y * GetCameraForward();

            inputValue = inputValue.normalized;

            if (inputValue.sqrMagnitude > 0.1f)
                targetPosition += inputValue;
        }

        private void GetKeyboardRotate()
        {
            float inputValue = keyboardRotate.ReadValue<Vector2>().x * Time.deltaTime * 100 * 2;
            transform.rotation = Quaternion.Euler(0f, inputValue * maxKeyboardRotationSpeed + transform.rotation.eulerAngles.y, 0f);
        }

        private Plane dragCameraPlane = new Plane(Vector3.up, Vector3.zero);
        private void DragCamera()
        {
            if (!Mouse.current.middleButton.isPressed)
                return;

            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
            if(dragCameraPlane.Raycast(ray, out float distance))
            {
                if (Mouse.current.middleButton.wasPressedThisFrame)
                    startDrag = ray.GetPoint(distance);
                else
                    targetPosition += startDrag - ray.GetPoint(distance);
            }
        }

        private void CheckMouseAtScreenEdge()
        {
            if (!enableEdgeMovement)
                return;
            //mouse position is in pixels
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 moveDirection = Vector3.zero;

            //horizontal scrolling
            if (mousePosition.x < edgeTolerance * Screen.width)
                moveDirection += -GetCameraRight();
            else if (mousePosition.x > (1f - edgeTolerance) * Screen.width)
                moveDirection += GetCameraRight();

            //vertical scrolling
            if (mousePosition.y < edgeTolerance * Screen.height)
                moveDirection += -GetCameraForward();
            else if (mousePosition.y > (1f - edgeTolerance) * Screen.height)
                moveDirection += GetCameraForward();

            targetPosition += moveDirection;
        }

        private void UpdateBasePosition()
        {
            if (targetPosition.sqrMagnitude > 0.1f)
            {
                //create a ramp up or acceleration
                speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
                transform.position += targetPosition * speed * Time.deltaTime;
            }
            else
            {
                //create smooth slow down
                horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
                transform.position += horizontalVelocity * Time.deltaTime;
            }

            //reset for next frame
            targetPosition = Vector3.zero;
        }

        private void OnZoomCamera(InputAction.CallbackContext obj)
        {
            float inputValue = -obj.ReadValue<Vector2>().y / 100f;

            if (Mathf.Abs(inputValue) > 0.1f)
            {
                zoomHeight = cameraTransform.localPosition.y + inputValue * stepSize;

                if (zoomHeight < minHeight)
                    zoomHeight = minHeight;
                else if (zoomHeight > maxHeight)
                    zoomHeight = maxHeight;
            }
        }

        private void UpdateCameraPosition()
        {
            //set zoom target
             Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);
            //add vector for forward/backward zoom
            zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);
            cameraTransform.LookAt(looktAtTarget);
        }
     
        private void OnRotateCamera(InputAction.CallbackContext obj)
        {
            if (!Mouse.current.rightButton.isPressed)
                return;
                
            float inputValue = obj.ReadValue<Vector2>().x;
            transform.rotation = Quaternion.Euler(0f, inputValue * maxMouseRotationSpeed + transform.rotation.eulerAngles.y, 0f);
        }

        //gets the horizontal forward vector of the camera
        private Vector3 GetCameraForward()
        {
            Vector3 forward = cameraTransform.forward;
            forward.y = 0f;
            return forward;
        }

        //gets the horizontal right vector of the camera
        private Vector3 GetCameraRight()
        {
            Vector3 right = cameraTransform.right;
            right.y = 0f;
            return right;
        }
    }