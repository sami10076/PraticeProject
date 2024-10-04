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
        #region Variables
        public GAMESTATES state = GAMESTATES.NONE;

        private int currentGridSize = 0;
        private int FirstInputCard = -1;
        private int SecondInputCard = -1;

        private bool hasMadeFirstMatch = false;

        public static GameManager Instance = null;
        public CardManager cardManager = null;
        public ScoreManager scoreManager = null;
        public GridLayoutGroup grid = null;
        #endregion

        #region State
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
                    currentGridSize = RandomEvenNumberGenerator.GetRandomEvenNumber();
                    cardManager.setupCard(currentGridSize);
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
                            if (!hasMadeFirstMatch)
                            {
                                hasMadeFirstMatch = true;
                            }
                            else {
                                scoreManager.AddComnbo(1);
                            }
                            Invoke("checkGameOver", 2);
                 
                        }
                        else {
                            card1.shakeAndReset();
                            card2.shakeAndReset();
                            scoreManager.AddTurn(1);
                            SoundManager.instance.playsound(SoundManager.instance.wrong);
                            scoreManager.resetCombo();
                            hasMadeFirstMatch = false;
                        }
                    
                    }
                    else {
                        CardClickListiner card1 = cardManager.getCard(FirstInputCard);
                        card1.shakeAndReset();
                        scoreManager.AddTurn(1);
                        SoundManager.instance.playsound(SoundManager.instance.wrong);
                        scoreManager.resetCombo();
                        hasMadeFirstMatch = false;
                    }
                    doChangeState(GAMESTATES.READYFORINPUT);
                    state = target;
                    break;
            }

        }
        #endregion

        #region Events
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
        void checkGameOver() {
            if (!cardManager.isCardAvaiable())
            {
                SoundManager.instance.playsound(SoundManager.instance.win);
                doChangeState(GAMESTATES.START);
            }
        }
        #endregion

        private void Awake()
        {
            Instance = this;
            doChangeState(GAMESTATES.START);
        }


    }
}
