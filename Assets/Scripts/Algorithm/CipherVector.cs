using static ENUMS;

public class CipherVector
{
    public string Message { get; set; }
    public string Key { get; set; }
    public int Depth { get; set; }
    public DIRECTIONS Direction { get; set; }
    public int Step { get; set; }
    public ALPHABETS AlphabetType { get; set; }
}
