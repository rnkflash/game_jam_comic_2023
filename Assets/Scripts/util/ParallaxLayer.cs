using UnityEngine;

public class ParallaxLayer : MonoBehaviour
    {
        public Vector3 movementScale = Vector3.one;

        private float width;
        private float cameraWidth;
        private Transform _camera;
        private Vector3 offsetX;

        void Awake()
        {
            _camera = Camera.main.transform;
            width = GetComponent<SpriteRenderer>().bounds.size.x;
            var cameraBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
            cameraWidth = cameraBounds.x * 2 - 0.5f;
            offsetX = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        void LateUpdate()
        {
            transform.position = offsetX + Vector3.Scale(new Vector3(_camera.position.x, 0, 0), movementScale);

            if (_camera.position.x > transform.position.x + width) {
                var newOffset = width + offsetX.x + 20.48f;
                offsetX = new Vector3(newOffset, offsetX.y, offsetX.z);
            }
        }

    }