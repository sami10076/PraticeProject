using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch_Gameplay{
    public class CardClickListiner : MonoBehaviour
    {
        private GameObject cardImage = null;
        private GameObject cardBlooker = null;

        public Sprite CardSprite = null;

        public CardState cardState = CardState.CLOSE;

        private void OnEnable()
        {
            cardImage = gameObject.transform.GetChild(0).transform.gameObject;
            cardBlooker = gameObject.transform.GetChild(1).transform.gameObject;
            resetCard();
        }

        public void onCardClick(int cardNumber) {
            //  openCard();
            if (cardState == CardState.CLOSE)
            {
                GameManager.Instance.onCardInput(cardNumber);
                SoundManager.instance.onCardClickSound();
            }
        }

        public void shakeAndReset() {
            if (cardImage != null && cardBlooker != null && CardSprite != null)
            {

                cardImage.GetComponent<SpriteShake>().ShakeSprite();
                resetCard();
            }

        }
        public void resetCard() {
            Invoke("doRest",1);
        }
        public void doRest() {
            if (cardImage != null && cardBlooker != null )
            {
                cardBlooker.SetActive(true);
                cardState = CardState.CLOSE;
                if (CardSprite != null)
                {
                    cardImage.GetComponent<Image>().sprite = CardSprite;
                }
            }
        }
        public void openCard() {
            if (cardImage != null && cardBlooker != null)
            {
                cardBlooker.SetActive(false);
                cardState = CardState.OPEN;
            }
        }

        public void turnOfCard() {
            Invoke("off",1);
        }
        private void off() {
            gameObject.SetActive(false);
        }
    }
}

