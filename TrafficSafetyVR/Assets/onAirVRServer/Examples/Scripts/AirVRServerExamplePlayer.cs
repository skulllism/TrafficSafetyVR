using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class AirVRServerExamplePlayer : MonoBehaviour {
    private const float ThrowSpeed = 12.0f;
    private const float ThrowTorqueSpeed = 20.0f;

    private Transform _thisTransform;
    private CharacterController _thisCharacterController;
    private AirVRClient _client;
    private float _fallingSpeed;

    private bool _touchpadTouched;
    private AirVRServerExampleAudioSource _soundShot;

    public AirVRServerExampleCan canPrefab;
    public float gravity;
    public float speed;

    private void resetFalling() {
        _fallingSpeed = 0.0f;
    }

    private void updateFalling(float deltaTime) {
		if (_thisCharacterController.isGrounded) {
			_fallingSpeed = 0.0f;
		}
		else {
			_fallingSpeed += gravity * deltaTime;
		}
	}

    private float horizontal() {
        if (_client.connected) {
            return Mathf.Clamp(_client.input.touchpad.dragX + _client.input.gamepad.GetAxis(AirVRInput.Gamepad.Axis.LThumbstickX), -1.0f, 1.0f);
        }
        return 0.0f;
    }

    private float vertical() {
        if (_client.connected) {
            return Mathf.Clamp(_client.input.touchpad.dragY + _client.input.gamepad.GetAxis(AirVRInput.Gamepad.Axis.LThumbstickY), -1.0f, 1.0f);
        }
        return 0.0f;
    }

    private void processMovement() {
        if (_thisCharacterController.enabled) {
            Vector3 moveDirection = Vector3.forward * vertical() + Vector3.right * horizontal();
            if (moveDirection.sqrMagnitude > 1.0f) {
                moveDirection = moveDirection.normalized;
            }

            Vector3 velocity = speed * _client.cameraRig.centerEyeAnchor.TransformDirection(moveDirection);
            Vector3 horizontalDir = new Vector3(velocity.x, 0.0f, velocity.z).normalized;

            Vector3 movingDir = velocity.magnitude * horizontalDir * Time.deltaTime;
            if (_fallingSpeed > 0.0f) {
                _thisCharacterController.Move(movingDir + Mathf.Max(_fallingSpeed * Time.deltaTime, movingDir.magnitude / Mathf.Tan(_thisCharacterController.slopeLimit)) * Vector3.down);
            }
            else {
                _thisCharacterController.Move(movingDir);
            }
            updateFalling(Time.deltaTime);
        }
    }

    private void processInput() {
        if (_client.connected) {
            if (_touchpadTouched == false && (_client.input.touchpad.GetTouch() || _client.input.gamepad.GetButton(AirVRInput.Gamepad.Button.A))) {
                throwCan();
                _touchpadTouched = true;
            }
            else if (_touchpadTouched && (_client.input.touchpad.GetTouch() == false && _client.input.gamepad.GetButton(AirVRInput.Gamepad.Button.A) == false)) {
                _touchpadTouched = false;
            }
        }
    }

    public void throwCan() {
        if (_client.connected) {
            Vector3 forward = _client.cameraRig.centerEyeAnchor.forward;

            AirVRServerExampleCan can = Instantiate(canPrefab, transform.position, _client.cameraRig.centerEyeAnchor.rotation) as AirVRServerExampleCan;
            can.Throw(forward * ThrowSpeed, Vector3.right * ThrowTorqueSpeed);

            _soundShot.Play();
        }
    }

    void Awake() {
        _thisTransform = transform;
        _thisCharacterController = gameObject.GetComponent<CharacterController>();
        _client = gameObject.GetComponentInChildren<AirVRClient>();
        _soundShot = transform.FindChild("SoundShot").GetComponent<AirVRServerExampleAudioSource>();
    }

    void Update() {
        processMovement();
        processInput();
    }

    public void SetPosition(Transform pos) {
        _thisTransform.position = pos.position;
        _thisTransform.rotation = pos.rotation;
    }

    public void EnableMovement(bool enable) {
        _thisCharacterController.enabled = enable;
        if (enable == false) {
            resetFalling();
        }
    }
}
