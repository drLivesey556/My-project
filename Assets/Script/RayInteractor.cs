using UnityEngine;
using UnityEngine.UI;

public class RayInteractor : MonoBehaviour
{
    public Camera cam;
    public float distance = 3.0f;
    public LayerMask mask = ~0; // everything by default

    [Header("UI")]
    public Text hintText;

    Interactable current;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
        if (hintText != null) hintText.enabled = false;
    }

    void Update()
    {
        current = null;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, distance, mask, QueryTriggerInteraction.Ignore))
            current = hit.collider.GetComponentInParent<Interactable>();

        if (hintText != null)
        {
            if (current != null)
            {
                hintText.text = current.prompt;
                hintText.enabled = true;
            }
            else hintText.enabled = false;
        }

        if (current != null && Input.GetKeyDown(KeyCode.E))
            current.Interact();
    }
}