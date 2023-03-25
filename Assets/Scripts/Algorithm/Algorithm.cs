using System.Collections.Generic;

public class Algorithm
{
    public static string Encode(CipherVector cipherText)
    {
        var (message, key, depth, direction, step, alphabetType) = UnwrapCipherVector(cipherText);
        var (charToIndexDict, alphabet, alphabetLen) = GetAlphabetParams(alphabetType);

        string encodedMessage = "";
        for (int i = 0; i < message.Length; i++)
        {
            var (messageIndex, keyIndex, P) = (
               charToIndexDict[$"{message[i]}"],
               charToIndexDict[$"{key[i % key.Length]}"],
               direction == 'R' ? step : -step);
            var indexOfEncodedLetter = (messageIndex + keyIndex + depth + P) % alphabetLen;

            if (indexOfEncodedLetter < 0)
                indexOfEncodedLetter = (indexOfEncodedLetter + alphabetLen) % alphabetLen;
            encodedMessage += alphabet[indexOfEncodedLetter];
        }
        return encodedMessage;
    }

    public static string Decode(CipherVector cipherText)
    {
        var (message, key, depth, direction, step, alphabetType) = UnwrapCipherVector(cipherText);
        var (charToIndexDict, alphabet, alphabetLen) = GetAlphabetParams(alphabetType);

        string decodedMessage = "";
        for (int i = 0; i < message.Length; i++)
        {
            var (messageIndex, keyIndex, P) = (
                charToIndexDict[$"{message[i]}"],
                charToIndexDict[$"{key[i % key.Length]}"],
                direction == 'R' ? -step : step);
            var indexOfDecodedLetter = (messageIndex + P - keyIndex - depth) % alphabetLen;

            if (indexOfDecodedLetter < 0)
                indexOfDecodedLetter = (indexOfDecodedLetter + alphabetLen) % alphabetLen;
            decodedMessage += alphabet[indexOfDecodedLetter];
        }
        return decodedMessage;
    }
    private static (string message, string key, int depth, char direction, int step, string alphabetType) UnwrapCipherVector(CipherVector cipherText)
        => (cipherText.Message, cipherText.Key, cipherText.Depth, cipherText.Direction, cipherText.Step, cipherText.AlphabetType);

    private static (Dictionary<string, int> charToIndexDict, string[] alphabet, int alphabetLen) GetAlphabetParams(string alphabetType)
    {
        return alphabetType switch
        {
            "Lat" => (Alphabets.LatinDictionary, Alphabets.LatinAlphabet, Alphabets.LatinAlphabet.Length),
            _ => (Alphabets.CyrillicDictionary, Alphabets.CyrillicAlphabet, Alphabets.CyrillicAlphabet.Length),
        };
    }
}
