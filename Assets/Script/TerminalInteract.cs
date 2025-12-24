using UnityEngine;

public class TerminalInteract : Interactable
{
    public TerminalScreen screen;

    int usedCount = 0;

    public override void Interact()
    {
        if (screen == null) return;

        usedCount++;

        if (usedCount == 1)
        {
            screen.PlayBoot();
        }
        else if (usedCount == 2)
        {
            screen.PrintMessage("C:\\> WHO_ARE_YOU");
        }
        else
        {
            screen.PrintMessage("C:\\> хдх_мю_уси.");
        }
    }
}

