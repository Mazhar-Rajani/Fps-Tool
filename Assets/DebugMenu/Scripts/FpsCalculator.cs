using UnityEngine;

public class FpsCalculator : MonoBehaviour
{
    private const int FRAME_RANGE = 60;

    public int CurrentFps { get; private set; }
    public int AverageFps { get; private set; }
    public int MinFps { get; private set; }
    public int MaxFps { get; private set; }

    private int[] fpsBuffer;
    private int bufferIndex;

    private void Awake()
    {
        fpsBuffer = new int[FRAME_RANGE];
        bufferIndex = 0;
    }

    private void Update()
    {
        CurrentFps = Mathf.RoundToInt(1f / Time.unscaledDeltaTime);

        fpsBuffer[bufferIndex++] = CurrentFps;
        bufferIndex = bufferIndex >= FRAME_RANGE ? 0 : bufferIndex;

        int fpsCount = 0;
        int min = int.MaxValue;
        int max = 0;
        for (int i = 0; i < FRAME_RANGE; i++)
        {
            int k = fpsBuffer[i];
            fpsCount += k;
            min = k < min ? k : min;
            max = k > max ? k : max;
        }
        AverageFps = fpsCount / FRAME_RANGE;
        MinFps = min;
        MaxFps = max;
    }
}