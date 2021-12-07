using UnityEngine;
using TMPro;

public class DebugMenu : MonoBehaviour
{
    [SerializeField] private KeyCode inputKey = default;
    [SerializeField] private bool enabledByDefault = default;
    [SerializeField] private string buildType = default;
    [SerializeField] private string sdkVersion = default;
    [SerializeField] private string userId = default;
    [SerializeField] private string logLocation = default;
    [SerializeField] private DebugInfo infoPrefab = default;
    [SerializeField] private TMP_Text input = default;
    [SerializeField] private Transform fpsModule = default;
    [SerializeField] private Transform appModule = default;
    [SerializeField] private Transform hardwareModule = default;
    [SerializeField] private Transform userModule = default;
    [Range(0, 1)] [SerializeField] private float fpsUpdateRate = default;

    private bool isEnabled;
    private float elapsedTime;
    private FpsCalculator fpsCalculator;
    private GameObject canvasObj;
    private DebugInfo fpsInfo;
    private DebugInfo avgfpsInfo;
    private DebugInfo ver;
    private DebugInfo build;
    private DebugInfo sdk;
    private DebugInfo res;
    private DebugInfo ram;
    private DebugInfo vram;
    private DebugInfo cpu;
    private DebugInfo gapi;
    private DebugInfo uid;
    private DebugInfo log;

    private void Awake()
    {
        fpsCalculator = gameObject.AddComponent<FpsCalculator>();
        canvasObj = GetComponentInChildren<Canvas>().gameObject;
        isEnabled = !enabledByDefault;
        Init();
        TogglePanel();
    }

    private void Update()
    {
        if (Input.GetKeyUp(inputKey))
        {
            TogglePanel();
        }
        if (isEnabled) RefreshFps();
    }

    private void TogglePanel()
    {
        isEnabled = !isEnabled;
        canvasObj.SetActive(isEnabled);
        if (isEnabled) Refresh();
    }

    private void Init()
    {
        fpsInfo = Instantiate(infoPrefab, fpsModule);
        avgfpsInfo = Instantiate(infoPrefab, fpsModule);
        ver = Instantiate(infoPrefab, appModule);
        build = Instantiate(infoPrefab, appModule);
        sdk = Instantiate(infoPrefab, appModule);
        res = Instantiate(infoPrefab, appModule);
        ram = Instantiate(infoPrefab, hardwareModule);
        vram = Instantiate(infoPrefab, hardwareModule);
        cpu = Instantiate(infoPrefab, hardwareModule);
        gapi = Instantiate(infoPrefab, hardwareModule);
        uid = Instantiate(infoPrefab, userModule);
        log = Instantiate(infoPrefab, userModule);
    }

    private void Refresh()
    {
        input.text = $"Press <font={FONT_UbuntuBold}><color={DARK_GREEN}>{inputKey}</color></font> to show/close the debug menu";
        ver.SetText($"<color={DEEP_YELLOW}>Version No:</color>", $"<color={DEEP_BLUE}>{Application.version}</color>");
        build.SetText($"<color={LIGHT_YELLOW}>Build Type:</color>", $"<color={LIGHT_BLUE}>{buildType}</color>");
        sdk.SetText($"<color={DEEP_YELLOW}>SDK Version:</color>", $"<color={DEEP_BLUE}>{sdkVersion}</color>");
        res.SetText($"<color={LIGHT_YELLOW}>Resolution:</color>", $"<color={LIGHT_BLUE}>{Screen.width}X{Screen.height}</color>");
        ram.SetText($"<color={DEEP_YELLOW}>RAM:</color>", $"<color={DEEP_BLUE}>{Mathf.RoundToInt(SystemInfo.systemMemorySize / 1000)} GB</color>");
        vram.SetText($"<color={LIGHT_YELLOW}>VRAM:</color>", $"<color={LIGHT_BLUE}>{Mathf.RoundToInt(SystemInfo.graphicsMemorySize / 1000)} GB</color>");
        cpu.SetText($"<color={DEEP_YELLOW}>CPU:</color>", $"<color={DEEP_BLUE}>{SystemInfo.processorType}</color>");
        gapi.SetText($"<color={LIGHT_YELLOW}>Graphics API:</color>", $"<color={LIGHT_BLUE}>{SystemInfo.graphicsDeviceVersion}</color>");
        uid.SetText($"<color={DEEP_YELLOW}>User ID:</color>", $"<color={DEEP_BLUE}>{userId}</color>");
        log.SetText($"<color={LIGHT_YELLOW}>Log Location:</color>", $"<color={LIGHT_BLUE}>{logLocation}</color>");
    }

    private void RefreshFps()
    {
        if (elapsedTime < fpsUpdateRate)
        {
            elapsedTime += Time.unscaledDeltaTime;
        }
        else
        {
            int fps = fpsCalculator.CurrentFps;
            int avgFps = fpsCalculator.AverageFps;
            int minFps = fpsCalculator.MinFps;
            int maxFps = fpsCalculator.MaxFps;

            fpsInfo.SetText($"<color={DEEP_YELLOW}>FPS:</color>", $"<size=32><color={(fps > 60 ? GREEN : RED)}>{fps}</color></size>");

            avgfpsInfo.SetText($"<color={LIGHT_YELLOW}>Avg:</color>",
                $"<color={(avgFps > 60 ? DARK_GREEN : RED)}>{avgFps}</color>" +
                $"      <font={FONT_PTSansNarrowRegular}><color={LIGHT_YELLOW}>Min:</color></font> <color={(minFps > 60 ? DARK_GREEN : RED)}>{minFps}</color>" +
                $"      <font={FONT_PTSansNarrowRegular}><color={LIGHT_YELLOW}>Max:</color></font> <color={(maxFps > 60 ? DARK_GREEN : RED)}>{maxFps}</color>");

            elapsedTime = 0f;
        }
    }

    public void SetSDKVersion(string value)
    {
        sdkVersion = value;
        Refresh();
    }

    public void SetUserId(string value)
    {
        userId = value;
        Refresh();
    }

    public void SetLogLocation(string value)
    {
        logLocation = value;
        Refresh();
    }

    // Colors
    private const string DEEP_YELLOW = "#E8DB9E";
    private const string LIGHT_YELLOW = "#EEEDEE";
    private const string DEEP_BLUE = "#58DBFC";
    private const string LIGHT_BLUE = "#D0F6FF";
    private const string GREEN = "#67FE1D";
    private const string DARK_GREEN = "#56D617";
    private const string RED = "#FE2929";

    // Fonts
    private const string FONT_PTSansNarrowBold = "PTSansNarrow-Bold SDF";
    private const string FONT_PTSansNarrowRegular = "PTSansNarrow-Regular SDF";
    private const string FONT_UbuntuBold = "Ubuntu-Bold SDF";
}