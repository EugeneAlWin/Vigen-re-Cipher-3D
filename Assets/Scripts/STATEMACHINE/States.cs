using static ENUMS;

public static class STATES
{
    public static DIRECTIONS CURRENT_DIRECTION { get; set; } = DIRECTIONS.RIGHT;
    public static STEPS CURRENT_EXAMINE_STEP { get; set; } = STEPS.NONE;
    public static ACTIONS CURRENT_EXAMINE_ACTION { get; set; } = ACTIONS.NONE;
    public static ALPHABETS CURRENT_ALPHABET { get; set; } = ALPHABETS.CYRILLIC;

    public static string CURRENT_MESSAGE { get; set; } = "";
    public static string CURRENT_KEY { get; set; } = "";
    public static string CURRENT_DEPTH { get; set; } = $"<color=#fff>1</color>";
    public static string CURRENT_STEP { get; set; } = $"<color=#fff>1</color>";
}

