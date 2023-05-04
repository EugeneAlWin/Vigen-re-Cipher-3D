using System;
using System.Collections.Generic;
using TMPro;
using static ENUMS;

public class Algorithm
{
    public static string Encode(CipherVector cipherText, TMP_Text elapsed = null)
    {
        var startTime = DateTime.Now;
        var (message, key, depth, direction, step, alphabetType) = UnwrapCipherVector(cipherText);
        var (charToIndexDict, alphabet, alphabetLen) = GetAlphabetParams(alphabetType);

        string encodedMessage = "";
        for (int i = 0; i < message.Length; i++)
        {
            var (messageIndex, keyIndex, P) = (
               charToIndexDict[$"{message[i]}"],
               charToIndexDict[$"{key[i % key.Length]}"],
                (direction == DIRECTIONS.RIGHT || direction == DIRECTIONS.BOTTOM) ? step : -step);
            var indexOfEncodedLetter = (messageIndex + keyIndex + depth + P) % alphabetLen;

            if (indexOfEncodedLetter < 0)
                indexOfEncodedLetter = (indexOfEncodedLetter + alphabetLen) % alphabetLen;
            encodedMessage += alphabet[indexOfEncodedLetter];
        }
        if (elapsed != null)
            elapsed.text = (DateTime.Now - startTime).ToString();
        return encodedMessage;
    }

    public static string Decode(CipherVector cipherText, TMP_Text elapsed = null)
    {
        var startTime = DateTime.Now;
        var (message, key, depth, direction, step, alphabetType) = UnwrapCipherVector(cipherText);
        var (charToIndexDict, alphabet, alphabetLen) = GetAlphabetParams(alphabetType);

        string decodedMessage = "";
        for (int i = 0; i < message.Length; i++)
        {
            var (messageIndex, keyIndex, P) = (
                charToIndexDict[$"{message[i]}"],
                charToIndexDict[$"{key[i % key.Length]}"],
                (direction == DIRECTIONS.RIGHT || direction == DIRECTIONS.BOTTOM) ? -step : step);
            var indexOfDecodedLetter = (messageIndex + P - keyIndex - depth) % alphabetLen;

            if (indexOfDecodedLetter < 0)
                indexOfDecodedLetter = (indexOfDecodedLetter + alphabetLen) % alphabetLen;
            decodedMessage += alphabet[indexOfDecodedLetter];
        }
        if (elapsed != null)
            elapsed.text = (DateTime.Now - startTime).ToString();
        return decodedMessage;
    }
    private static (string message, string key, int depth, DIRECTIONS direction, int step, ALPHABETS alphabetType) UnwrapCipherVector(CipherVector cipherText)
        => (cipherText.Message, cipherText.Key, cipherText.Depth, cipherText.Direction, cipherText.Step, cipherText.AlphabetType);

    private static (Dictionary<string, int> charToIndexDict, string[] alphabet, int alphabetLen) GetAlphabetParams(ALPHABETS alphabetType)
    {
        return alphabetType switch
        {
            ALPHABETS.LATIN => (Alphabets.LatinDictionary, Alphabets.LatinAlphabet, Alphabets.LatinAlphabet.Length),
            _ => (Alphabets.CyrillicDictionary, Alphabets.CyrillicAlphabet, Alphabets.CyrillicAlphabet.Length),
        };
    }
}
