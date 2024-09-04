using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    Transform canvasTrans;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI jamText;
    TextMeshProUGUI bombText;
    [SerializeField]
    Image[] hearting;

    TextMeshProUGUI ScoreText
    {
        get
        {
            if (scoreText == null)
            {
                scoreText = canvasTrans.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            }
            return scoreText;
        }
    }
    TextMeshProUGUI JamText
    {
        get
        {
            if (jamText == null)
            {
                jamText = canvasTrans.Find("jamText").GetComponent<TextMeshProUGUI>();
            }
            return jamText;
        }
    }
    TextMeshProUGUI BombText
    {
        get
        {
            if (bombText == null)
            {
                bombText = canvasTrans.Find("bombText").GetComponent<TextMeshProUGUI>();
            }
            return bombText;
        }
    }

    private void Awake()
    {
        GameObject obj = GameObject.Find("Canvas");
        canvasTrans = obj.GetComponent<Transform>();

        //canvasTrans.Find("ScoreText");
        // GameObject obj = GameObject.Find("ScoreText"); 한줄로 해도 되지만 효율이 나쁘니.
    }

    private void OnEnable()
    {
        ScoreManager.OnChangeScore += UpdateScoreText;
        ScoreManager.OnChangeBomb += UpdateBobmText;
        ScoreManager.OnChangeJamCount += UpdateJamText;
    }

    private void OnDisable()
    {
        ScoreManager.OnChangeScore -= UpdateScoreText;
        ScoreManager.OnChangeBomb -= UpdateBobmText;
        ScoreManager.OnChangeJamCount -= UpdateJamText;
    }

    private void UpdateScoreText(int score)
    {
        ScoreText.text = score.ToString();
    }

    private void UpdateBobmText(int score)
    {
        BombText.text = "X : " + score.ToString();
    }

    private void UpdateJamText(int score)
    {
        JamText.text = "X : " + score.ToString();
    }

}


