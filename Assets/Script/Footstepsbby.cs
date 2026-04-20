using UnityEngine;
private enum CURRENT_TERRAIN { CARPET, METAL, RAMP, WATER };
[SerializeField]
private CURRENT_TERRAIN currentTerrain;

[SerializeField]
private AK.Wwise.Event footstepsEvent;

[SerializeField]
private AK.Wwise.Switch[] terrainSwitch;

  private void CheckTerrain()
    {
        RaycastHit[] hit;

        hit = Physics.RaycastAll(transform.position, Vector3.down, 10.0f);

        foreach (RaycastHit rayhit in hit)
        {
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Carpet"))
            {
                currentTerrain = CURRENT_TERRAIN.CARPET;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Metal"))
            {
                currentTerrain = CURRENT_TERRAIN.METAL;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Ramp"))
            {
                currentTerrain = CURRENT_TERRAIN.RAMP;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                currentTerrain = CURRENT_TERRAIN.WATER;
            }
        }
    }
private void Update() 
    {
        CheckTerrain();
    }
private void PlayFootstep(int terrain)
    {
       terrainSwitch[terrain].SetValue(this.gameObject);
       AkSoundEngine.PostEvent(footstepsEvent.Id, this.gameObject);
    }
public void SelectAndPlayFootstep()
    {     
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.CARPET:
                PlayFootstep(0);
                break;

            case CURRENT_TERRAIN.METAL:
                PlayFootstep(1);
                break;

            case CURRENT_TERRAIN.RAMP:
                PlayFootstep(2);
                break;

            case CURRENT_TERRAIN.WATER:
                PlayFootstep(3);
                break;

            default:
                PlayFootstep(0);
                break;
        }
    }
private Rigidbody rb;

float timer = 0.0f;

[SerializeField]
float footstepSpeed = 0.3f;
private void Awake()
{
        rb = GetComponent<Rigidbody>();
}
private void Update()
    {
        CheckTerrain();

        if (rb.velocity.magnitude > 1f && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            if (timer > footstepSpeed)
            {
                SelectAndPlayFootstep();
                timer = 0.0f;
            }

            timer += Time.deltaTime;
        }
    }