using UnityEngine;
using UnityEngine.UI;

public class NoiseScroll : MonoBehaviour
{
    public RawImage noise;
    public float speedX = 1.2f;
    public float speedY = 2.5f;
    public float tile = 10f;

    [Header("Flicker")]
    public float baseAlpha = 0.25f;
    public float flickerAmount = 0.08f;
    public float flickerSpeed = 18f;

    void Start()
    {
        if (noise != null)
            noise.uvRect = new Rect(0, 0, tile, tile);
    }

    void Update()
    {
        if (noise == null) return;

        var r = noise.uvRect;
        r.x += speedX * Time.deltaTime;
        r.y += speedY * Time.deltaTime;
        noise.uvRect = r;

        var c = noise.color;
        float a = baseAlpha + Mathf.PerlinNoise(Time.time * flickerSpeed, 0f) * flickerAmount;
        c.a = Mathf.Clamp01(a);
        noise.color = c;
    }
}

