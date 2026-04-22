using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AK.Wwise.Event footstepEvent;
    public float walkStepDistance = 2.0f;
    private float distanceTraveled = 0f;
    private Vector3 lastPosition;

    void Start() {
        lastPosition = transform.position;
    }

    void Update() {
        float distanceMove = Vector3.Distance(transform.position, lastPosition);
        distanceTraveled += distanceMove;

        if (distanceTraveled >= walkStepDistance) {
            PlayFootstep();
            distanceTraveled = 0f;
        }
        lastPosition = transform.position;
    }

    void PlayFootstep() {
        // Trigger the Wwise event on this GameObject
        footstepEvent.Post(gameObject); 
    }
}
