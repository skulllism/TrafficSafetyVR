using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AirVRServerExampleDisplay : MonoBehaviour {
    private class CameraPane {
        private Transform _cameraTransform;
        private AirVRCameraRig _cameraRig;
        private Camera _camera;

        public CameraPane(AirVRCameraRig cameraRig, Camera camera) {
            _cameraRig = cameraRig;
            _camera = camera;
            _cameraTransform = camera.gameObject.transform;
        }

        public AirVRCameraRig cameraRig {
            get {
                return _cameraRig;
            }
        }

        public Camera camera {
            get {
                return _camera;
            }
        }

        public bool enabled {
            get {
                return _camera.gameObject.activeSelf;
            }
            set {
                _camera.gameObject.SetActive(value);
            }
        }

        public void SetViewport(Rect viewport) {
            _camera.rect = viewport;
        }

        public void Update() {
            if (_camera.gameObject.activeSelf) {
                _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _cameraRig.centerEyeAnchor.position, LowpassCoef);
                _cameraTransform.rotation = Quaternion.Slerp(_cameraTransform.rotation, _cameraRig.centerEyeAnchor.rotation, LowpassCoef);
            }
        }
    }

    private const float LowpassCoef = 0.15f;

    private Transform _thisTransform;
    private List<CameraPane> _cameraPanes;

    public Camera cameraPrefab;

    private CameraPane getCameraPane(AirVRCameraRig cameraRig) {
        foreach (CameraPane pane in _cameraPanes) {
            if (pane.cameraRig == cameraRig) {
                return pane;
            }
        }
        return null;
    }

    private bool doesCameraPaneExist(AirVRCameraRig cameraRig) {
        return getCameraPane(cameraRig) != null;
    }

    private CameraPane createCameraPane(AirVRCameraRig cameraRig) {
        Camera camInstance = GameObject.Instantiate(cameraPrefab).GetComponent<Camera>();
        camInstance.gameObject.transform.parent = _thisTransform;

        return new CameraPane(cameraRig, camInstance);
    }

    private void destroyCameraPane(CameraPane pane) {
        GameObject.Destroy(pane.camera.gameObject);
    }

    private Rect getCameraPaneViewport(int index, int paneCount) {
        return new Rect((float)index / paneCount, 0.0f, 1.0f / paneCount, 1.0f);
    }

    private void recalcCameraPaneLayout() {
        List<CameraPane> panesEnabled = new List<CameraPane>();
        for (int i = 0; i < _cameraPanes.Count; i++) {
            if (_cameraPanes[i].enabled) {
                panesEnabled.Add(_cameraPanes[i]);
            }
        }

        for (int i = 0; i < panesEnabled.Count; i++) {
            _cameraPanes[i].SetViewport(getCameraPaneViewport(i, panesEnabled.Count));
        }
    }

    void Awake() {
        _thisTransform = transform;
        _cameraPanes = new List<CameraPane>();
    }

    void LateUpdate() {
        foreach (CameraPane pane in _cameraPanes) {
            pane.Update();
        }
    }

    public int cameraPaneCount {
        get {
            return _cameraPanes.Count;
        }
    }

    public void AddCameraPane(AirVRCameraRig cameraRig) {
        if (doesCameraPaneExist(cameraRig) == false) {
            CameraPane pane = createCameraPane(cameraRig);
            pane.enabled = true;
            _cameraPanes.Add(pane);
            recalcCameraPaneLayout();
        }
    }

    public void RemoveCameraPane(AirVRCameraRig cameraRig) {
        CameraPane pane = getCameraPane(cameraRig);
        if (pane != null) {
            _cameraPanes.Remove(pane);
            destroyCameraPane(pane);

            recalcCameraPaneLayout();
        }
    }
}
