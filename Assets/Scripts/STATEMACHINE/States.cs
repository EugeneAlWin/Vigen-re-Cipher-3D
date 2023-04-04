using static ENUMS;

public static class STATES
{
    public static DIRECTIONS CURRENT_DIRECTION { get; set; } = DIRECTIONS.RIGHT;
    public static ALPHABETS CURRENT_ALPHABET { get; set; } = ALPHABETS.CYRILLIC;

    #region DEMO
    public static int EXAMINE_CURRENT_CHAR_POSITION { get; set; } = 0;
    public static STEPS CURRENT_EXAMINE_STEP { get; set; } = STEPS.NONE;
    public static ACTIONS CURRENT_EXAMINE_ACTION { get; set; } = ACTIONS.NONE;
    public static char EXAMINE_CURRENT_LETTER { get; set; } = ' ';
    public static string EXAMINE_MESSAGE { get; set; }
    public static string EXAMINE_KEY { get; set; }
    public static string EXAMINE_CODED_LETTER { get; set; }
    public static string EXAMINE_KEY_LETTER { get; set; }
    public static int EXAMINE_DEPTH { get; set; }
    public static int EXAMINE_STEP { get; set; }
    public static DIRECTIONS EXAMINE_DIRECTION { get; set; }
    #endregion

    public static string CURRENT_MESSAGE { get; set; } = "";
    public static string CURRENT_KEY { get; set; } = "";
    public static string CURRENT_DEPTH { get; set; } = $"<color=#fff>1</color>";
    public static string CURRENT_STEP { get; set; } = $"<color=#fff>1</color>";
}

