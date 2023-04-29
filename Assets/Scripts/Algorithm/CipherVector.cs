using static ENUMS;

public class CipherVector
{
    public string Message { get; set; }
    public string Key { get; set; }
    public int Depth { get; set; }
    public DIRECTIONS Direction { get; set; }
    public int Step { get; set; }
    public ALPHABETS AlphabetType { get; set; }

    public CipherVector() { }
    public CipherVector(string message, string key, int depth, DIRECTIONS direction, int step, ALPHABETS alphabetType)
    {
        Message = message;
        Key = key;
        Depth = depth;
        Direction = direction;
        Step = step;
        AlphabetType = alphabetType;
    }
    public override string ToString()
    {
        return Message + " " + Key + " " + Depth + " " + Direction + Step + " " + AlphabetType;
    }
}
