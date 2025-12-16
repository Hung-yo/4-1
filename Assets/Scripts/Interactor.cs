using UnityEngine;

public class Interactor : MonoBehaviour
{
    public KeyCode interactKey;
    public float _castDistance = 5f;
    public Vector3 _raycastOffset = new Vector3(0, 1f, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Always draw the ray for visualization
        DrawInteractionRay();

        if (Input.GetKeyDown(interactKey))
        {
            if(DoInteractionTest(out IInteractable interactable))
            {
                if (interactable.CanInteract())
                {
                    interactable.Interact(this);
                }
            }
        }
    }

    private void DrawInteractionRay()
    {
        Camera cam = null;
        GameObject camObj = GameObject.Find("PlayerCamera");
        if (camObj != null)
        {
            cam = camObj.GetComponent<Camera>();
        }

        Ray ray;
        if (cam != null)
        {
            ray = new Ray(cam.transform.position, cam.transform.forward);
        }
        else
        {
            ray = new Ray(transform.position + _raycastOffset, transform.forward);
        }
    }

    private bool DoInteractionTest(out IInteractable interactable)
    {
        interactable = null;

        // Try to use PlayerCamera if available
        Camera cam = null;
        GameObject camObj = GameObject.Find("PlayerCamera");
        if (camObj != null)
        {
            cam = camObj.GetComponent<Camera>();
        }

        Ray ray;
        if (cam != null)
        {
            ray = new Ray(cam.transform.position, cam.transform.forward);
        }
        else
        {
            ray = new Ray(transform.position + _raycastOffset, transform.forward);
        }

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _castDistance))
        {
            interactable = hitInfo.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                return true;
            }
            return false;
        }
        return false;
    }
}
