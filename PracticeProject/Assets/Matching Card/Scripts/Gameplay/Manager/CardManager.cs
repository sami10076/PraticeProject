using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CardMatch_Gameplay
{
    public class CardManager : MonoBehaviour
    {
        public CardClickListiner[] cards = null;
        public Sprite[] cardSprites = null;


        public CardClickListiner getCard(int target) {
            CardClickListiner result = cards[0];
            if (target < cards.Length) {
                result = cards[target];
            }
            return result;

        }

        public void enableCard(int NumberOfCards)
        {

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].gameObject.SetActive(i < NumberOfCards);
            }

           

            // Shuffle the array of sprites to ensure random assignment (optional)
         

        }
        public void setupCard(int NumberOfCards) {

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].gameObject.SetActive(i<NumberOfCards);
            }

            int numberOfPairs = NumberOfCards / 2;

            // Shuffle the array of sprites to ensure random assignment (optional)
            Shuffle(cardSprites);

            // Assign pairs of sprites to images, reusing sprites as needed
            int imageIndex = 0;
            for (int i = 0; i < numberOfPairs; i++)
            {
                // Reuse sprites if the pairs exceed available sprites
                Sprite spriteToAssign = cardSprites[i % cardSprites.Length];

                // Assign the sprite to two consecutive images
                cards[imageIndex].CardSprite = spriteToAssign;
                cards[imageIndex + 1].CardSprite = spriteToAssign;

                // Move to the next pair of images
                imageIndex += 2;
            }

            for (int i = 0; i < NumberOfCards; i++)
            {
                cards[i].resetCard();
            }

        }
        void Shuffle(Sprite[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Sprite temp = array[i];
                int randomIndex = Random.Range(i, array.Length);
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }

        public bool isCardAvaiable() {

            foreach (var item in cards)
            {
                if (item.gameObject.activeSelf) {
                    return true;
                }
            }
            return false;
        }

    }
}
