using UnityEngine;

public class Interactable : MonoBehaviour
{
    [TextArea] public string prompt = "Press E";
    public virtual void Interact()
    {
        Debug.Log($"{name}: Interact()");
    }
}
