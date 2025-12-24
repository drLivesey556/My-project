using System.Collections;
using TMPro;
using UnityEngine;

public class TerminalScreen : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text terminalText;

    [Header("Typing")]
    public float charsPerSecond = 35f;
    public int maxLines = 18;

    [Header("Audio")]
    public AudioSource bootSource;      
    public AudioSource loopSource;
    public AudioSource audioSource;
    public AudioClip bootClip;
    public AudioClip keyTickClip;
    [Range(0f, 1f)] public float tickVolume = 0.25f;

    Coroutine running;

    public void Clear()
    {
        if (terminalText == null) return;
        terminalText.text = "";
    }

    public void PlayBoot()
    {
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(BootRoutine());
    }

    IEnumerator BootRoutine()
    {
        Clear();

        if (audioSource != null && bootClip != null)
            audioSource.PlayOneShot(bootClip);

        // Небольшой "старый DOS" сценарий
        yield return TypeLine("INIT SYSTEM v3.1");
        yield return TypeLine("CHECKING INTEGRITY... OK");
        yield return TypeLine("LOADING MODULES:");
        yield return TypeLine(" - IO............. OK");
        yield return TypeLine(" - SECURITY........ OK");
        yield return TypeLine(" - OBSERVER........ ACTIVE");
        yield return TypeLine("");
        yield return TypeLine("C:\\> _", instant: true);
    }

    public void PrintMessage(string message)
    {
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(TypeLine(message));
    }

    IEnumerator TypeLine(string line, bool instant = false)
    {
        if (terminalText == null) yield break;

        // Ограничение по строкам (чтобы не разрасталось)
        TrimLinesIfNeeded();

        if (instant)
        {
            terminalText.text += line + "\n";
            yield break;
        }

        foreach (char c in line)
        {
            terminalText.text += c;

            if (audioSource != null && keyTickClip != null && c != ' ')
                audioSource.PlayOneShot(keyTickClip, tickVolume);

            yield return new WaitForSeconds(1f / Mathf.Max(1f, charsPerSecond));
        }

        terminalText.text += "\n";
    }

    void TrimLinesIfNeeded()
    {
        var text = terminalText.text;
        var lines = text.Split('\n');
        if (lines.Length <= maxLines) return;

        int start = lines.Length - maxLines;
        terminalText.text = string.Join("\n", lines, start, maxLines);
    }
}

