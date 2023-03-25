using System.Collections.Generic;
using UnityEngine;
public class Algorithm : MonoBehaviour
{
    private readonly Dictionary<string, int> cyrDictionary = new()
    {
        {"�", 0}, {"�", 1}, {"�", 2}, {"�", 3}, {"�", 4},
        {"�", 5}, {"�", 6}, {"�", 7}, {"�", 8}, {"�", 9},
        {"�", 10}, {"�", 11}, {"�", 12}, {"�", 13}, {"�", 14},
        {"�", 15}, {"�", 16}, {"�", 17}, {"�", 18}, {"�", 19},
        {"�", 20}, {"�", 21}, {"�", 22}, {"�", 23}, {"�", 24},
        {"�", 25}, {"�", 26}, {"�", 27}, {"�", 28}, {"�", 29},
        {"�", 30}, {"�", 31}, {"�", 32}
    };
    private readonly string[] cyrAlphabet = new string[]
    {
        "�", "�", "�", "�", "�", "�", "�", "�", "�", "�",
        "�", "�", "�", "�", "�", "�", "�", "�", "�", "�",
        "�", "�", "�", "�", "�", "�", "�", "�", "�", "�",
        "�", "�", "�"
    };
    private readonly Dictionary<string, int> latDictionary = new()
    {
        {"a", 0}, {"b", 1}, {"c", 2}, {"d", 3}, {"e", 4},
        {"f", 5}, {"g", 6}, {"h", 7}, {"i", 8}, {"j", 9},
        {"k", 10}, {"l", 11}, {"m", 12}, {"n", 13}, {"o", 14},
        {"p", 15}, {"q", 16}, {"r", 17}, {"s", 18}, {"t", 19},
        {"u", 20}, {"v", 21}, {"w", 22}, {"x", 23}, {"y", 24},
        {"z", 25}
    };
    private readonly string[] latAlphabet = new string[]
    {
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
        "n","o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
    };

    public string Encode(CipherVector cipherText)
    {
        var (message, key, depth, direction, step, alphabetType) = UnwrapCipherVector(cipherText);
        var currentAlphabet = message == "Lat" ? latAlphabet : cyrAlphabet;
        var currentDict = alphabetType == "Lat" ? latDictionary : cyrDictionary;
        var alphabetLen = currentAlphabet.Length;

        string encodedMessage = "";
        for (int i = 0; i < message.Length; i++)
        {
            var (messageIndex, keyIndex, P) = (
               currentDict[$"{message[i]}"],
               currentDict[$"{key[i % key.Length]}"],
               direction == 'R' ? step : -step);
            var indexOfEncodedLetter = (messageIndex + keyIndex + depth + P) % alphabetLen;

            if (indexOfEncodedLetter < 0)
                indexOfEncodedLetter = (indexOfEncodedLetter + alphabetLen) % alphabetLen;
            encodedMessage += currentAlphabet[indexOfEncodedLetter];
        }
        return encodedMessage;
    }

    public string Decode(CipherVector cipherText)
    {
        var (message, key, depth, direction, step, alphabetType) = UnwrapCipherVector(cipherText);
        var currentAlphabet = alphabetType == "Lat" ? latAlphabet : cyrAlphabet;
        var currentDict = alphabetType == "Lat" ? latDictionary : cyrDictionary;
        var alphabetLen = currentAlphabet.Length;

        string decodedMessage = "";
        for (int i = 0; i < message.Length; i++)
        {
            var (messageIndex, keyIndex, P) = (
                currentDict[$"{message[i]}"],
                currentDict[$"{key[i % key.Length]}"],
                direction == 'R' ? -step : step);
            var indexOfDecodedLetter = (messageIndex + P - keyIndex - depth) % alphabetLen;

            if (indexOfDecodedLetter < 0)
                indexOfDecodedLetter = (indexOfDecodedLetter + alphabetLen) % alphabetLen;
            decodedMessage += currentAlphabet[indexOfDecodedLetter];
        }
        return decodedMessage;
    }
    private (string message, string key, int depth, char direction, int step, string alphabetType) UnwrapCipherVector(CipherVector cipherText)
        => (cipherText.Message, cipherText.Key, cipherText.Depth, cipherText.Direction, cipherText.Step, cipherText.AlphabetType);
}
