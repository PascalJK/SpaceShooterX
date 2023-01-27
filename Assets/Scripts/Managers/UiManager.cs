using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _gameoverText;
    [SerializeField] TextMeshProUGUI _restartGameText;
    [SerializeField] Image _livesImage;
    [SerializeField] List<Sprite> _livesSprites;
    [SerializeField] GameManager gameManager;

    void Start()
    {
        _scoreText.text = $"Score: 0";
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = $"Score: {score}";
    }

    public void UpdateLivesSprive(int index)
    {
        _livesImage.sprite = _livesSprites[index];
        if (index != 0)
            return;
        _gameoverText.gameObject.SetActive(true);
        _restartGameText.gameObject.SetActive(true);
        gameManager.IsGameOver();
    }
}
