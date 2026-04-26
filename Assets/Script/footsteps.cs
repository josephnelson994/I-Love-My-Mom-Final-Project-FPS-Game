using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AK.Wwise.Event footstepEvent;
    public float stepDistance = 2.0f; // Distance between steps
    private float distanceTravelled = 0f;
    private Vector3 lastPosition;

    void Start() {
        lastPosition = transform.position;
    }

    void Update() {
        // Calculate distance moved this frame
        float moved = Vector3.Distance(transform.position, lastPosition);
        distanceTravelled += moved;
        lastPosition = transform.position;

        // Trigger sound if distance threshold is met and player is grounded
        if (distanceTravelled >= stepDistance) {
            // Optional: Add a check for "IsGrounded" here
            footstepEvent.Post(gameObject);
            distanceTravelled = 0f;
        }
    }
}

