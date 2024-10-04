using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvenNumberGenerator : MonoBehaviour
{
    public static int lastEvenNumber = -1; // Store the last generated even number

    // Function to return a random even number between 6 and 30, avoiding consecutive repeats
    public static int GetRandomEvenNumber()
    {
        int newEvenNumber;

        // Repeat until a new number different from the last one is generated
        do
        {
            int randomBase = Random.Range(3, 16);
            newEvenNumber = randomBase * 2;
        } while (newEvenNumber == lastEvenNumber);

        // Update the last generated number
        lastEvenNumber = newEvenNumber;

        return newEvenNumber;
    }
}
