using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch_Gameplay
{
    public enum GAMESTATES
    {
        NONE,
        START,
        READYFORINPUT,
        FIRSTINPUT,
        SECONDINPUT,
        DECISION
    }

  

    public class GameManager : MonoBehaviour
    {
        public GAMESTATES state = GAMESTATES.NONE;

        public static GameManager Instance = null;
        public CardManager cardManager = null;
        public ScoreManager scoreManager = null;
        public GridLayoutGroup grid = null;


        private int FirstInputCard = -1;
        private int SecondInputCard = -1;


        #region state
        public void doChangeState(GAMESTATES target)
        {

            StartCoroutine(changeGameState(target));
        }
        IEnumerator changeGameState(GAMESTATES target)
        {

            yield return new WaitForSeconds(0);
            switch (target)
            {
                case GAMESTATES.START:
                    grid.enabled = true;
                    cardManager.setupCard(GetRandomEvenNumber());
                    doChangeState(GAMESTATES.READYFORINPUT);
                    state = target;
                    break;
                case GAMESTATES.READYFORINPUT:
                    grid.enabled = false;
                  
                    FirstInputCard = -1;
                    SecondInputCard = -1;
                    state = target;
                    break;
                case GAMESTATES.FIRSTINPUT:
                    state = target;
                    cardManager.getCard(FirstInputCard).openCard();
                    break;
                case GAMESTATES.SECONDINPUT:
                    state = target;
                    cardManager.getCard(SecondInputCard).openCard();
                    doChangeState(GAMESTATES.DECISION);

                    break;
                case GAMESTATES.DECISION:
                  
                    if (FirstInputCard != SecondInputCard)
                    {
                        CardClickListiner card1=cardManager.getCard(FirstInputCard);
                        CardClickListiner card2=cardManager.getCard(SecondInputCard);
                        if (card1.CardSprite == card2.CardSprite)
                        {
                            card1.turnOfCard();
                            card2.turnOfCard();
                            SoundManager.instance.stopalleffect();
                            SoundManager.instance.playsound(SoundManager.instance.cardMatch);
                            scoreManager.AddScore(1);
                            Invoke("checkGameOver", 2);
                 
                        }
                        else {
                            card1.resetCard();
                            card2.resetCard();
                            scoreManager.AddTurn(1);
                            SoundManager.instance.playsound(SoundManager.instance.wrong);

                        }
                    
                    }
                    else {
                        CardClickListiner card1 = cardManager.getCard(FirstInputCard);
                        card1.resetCard();
                        scoreManager.AddTurn(1);
                        SoundManager.instance.playsound(SoundManager.instance.wrong);
                    }
                    doChangeState(GAMESTATES.READYFORINPUT);
                    state = target;
                    break;
            }

        }
        #endregion


        public void onCardInput(int cardNumber) {
            if (state == GAMESTATES.DECISION) {
                return;
            }
            if (state == GAMESTATES.READYFORINPUT)
            {
                FirstInputCard = cardNumber;
                doChangeState(GAMESTATES.FIRSTINPUT);

            }
            else if (state == GAMESTATES.FIRSTINPUT)
            {
                SecondInputCard = cardNumber;
                doChangeState(GAMESTATES.SECONDINPUT);
            }
            else {

            }

        }

        private int lastEvenNumber = -1; // Store the last generated even number

        // Function to return a random even number between 6 and 30, avoiding consecutive repeats
        public int GetRandomEvenNumber()
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

        void checkGameOver() {
            if (!cardManager.isCardAvaiable())
            {
                SoundManager.instance.playsound(SoundManager.instance.win);
                doChangeState(GAMESTATES.START);
            }
        }

        private void Awake()
        {
            Instance = this;
            doChangeState(GAMESTATES.START);
        }


    }
}
