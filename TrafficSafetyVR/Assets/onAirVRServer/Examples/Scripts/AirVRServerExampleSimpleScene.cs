using UnityEngine;
using System.Collections;

public class AirVRServerExampleSimpleScene : MonoBehaviour, AirVRServer.EventHandler {
    public AudioSource music;
    public AirVRServerExampleDisplay display;

    void Awake() {
        AirVRServer.Delegate = this;
    }

    // implements AirVRServer.EventHandler
    public void AirVRServerFailed(string reason) {
        Debug.Log("AirVRServer failed: " + reason);
    }

    public string AirVRLicenseRequired() {
        return "noncommercial.license";
    }

    public void AirVRServerClientConnected(ref AirVRClientConfig config) {
        // do nothing
    }

    public AirVRClient AirVRServerSelectClientToBind() {
        return null;
    }

    public void AirVRServerClientBound(AirVRClient client) {
        AirVRServerExamplePlayer player = client.GetComponentInParent<AirVRServerExamplePlayer>();
        player.EnableMovement(true);

        if (AirVRServer.connectedClients.Count == 1) {
            music.Play();
        }
        display.AddCameraPane(client.cameraRig);
    }

    public void AirVRServerClientDisconnected(AirVRClient client) {
        AirVRServerExamplePlayer player = client.GetComponentInParent<AirVRServerExamplePlayer>();
        player.EnableMovement(false);

        if (AirVRServer.connectedClients.Count == 0) {
            music.Stop();
        }
        display.RemoveCameraPane(client.cameraRig);
    }
}
