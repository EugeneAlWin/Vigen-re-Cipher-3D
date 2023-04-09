using static ENUMS;

public static class STATES
{
    public static DIRECTIONS CURRENT_DIRECTION { get; set; } = DIRECTIONS.RIGHT;
    public static ALPHABETS CURRENT_ALPHABET { get; set; } = ALPHABETS.CYRILLIC;

    #region DEMO
    public static string STUDY_MESSAGE { get; set; }
    public static string STUDY_KEY { get; set; }
    public static char STUDY_CURRENT_CHAR { get; set; } = ' ';
    public static int STUDY_CURRENT_CHAR_POSITION { get; set; } = 0;
    public static string STUDY_CODED_CHAR { get; set; }
    public static ACTIONS STUDY_CURRENT_ACTION { get; set; } = ACTIONS.NONE;
    public static STEPS STUDY_CURRENT_STEP { get; set; } = STEPS.NONE;
    public static string STUDY_KEY_CHAR { get; set; }
    public static int STUDY_DEPTH { get; set; }
    public static int STUDY_STEP { get; set; }
    public static DIRECTIONS STUDY_DIRECTION { get; set; }
    #endregion

    public static string CURRENT_MESSAGE { get; set; } = "";
    public static string CURRENT_KEY { get; set; } = "";
    public static string CURRENT_DEPTH { get; set; } = $"<color=#fff>1</color>";
    public static string CURRENT_STEP { get; set; } = $"<color=#fff>1</color>";
}

