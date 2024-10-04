using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CardMatch_Gameplay
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private Text scoreText = null;
        [SerializeField] private Text turnText = null;

        private void OnEnable()
        {
            AddScore(0);
            AddTurn(0);
        }



        public void AddScore(int target)
        {
            setScore(getScore() + target); ;
            scoreText.text = getScore() + "";
        }
        public void AddTurn(int target)
        {
            setTurn(getTurn() + target); ;
            turnText.text = getTurn() + "";
        }

        private int getScore() {

            return  PlayerPrefs.GetInt("Score",0);
        }
        private void setScore(int target)
        {

             PlayerPrefs.SetInt("Score", target);
            PlayerPrefs.Save();
        }

        private int getTurn()
        {

            return PlayerPrefs.GetInt("Turn", 0);
        }
        private void setTurn(int target)
        {

            PlayerPrefs.SetInt("Turn", target);
            PlayerPrefs.Save();
        }
    }
}
