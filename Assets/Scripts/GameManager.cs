using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int _ringNumber;
    private int _score;
    private bool _levelEnded;
    [SerializeField] private AudioSource _ringSound;
    [SerializeField] private AudioSource _winSound;
    [SerializeField] private AudioSource _level1;
    [SerializeField] private AudioSource _level2;
    [SerializeField] private GameObject _levelScore;
    [SerializeField] private Text _ringText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _time;
    // Start is called before the first frame update
    void Start()
    {
        _levelEnded = false;
        if (SceneManager.GetActiveScene().name == "Level1.0")
            _level1.Play();
        else if (SceneManager.GetActiveScene().name == "Level2.0")
            _level2.Play();
        _ringNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.CeilToInt(Time.time) / 60 ;
        int seconds = Mathf.CeilToInt(Time.time) % 60;
        if (!_levelEnded && _time)
        {
            _time.text = minutes + ":";
            if (seconds < 10)
                _time.text += "0";
            _time.text += seconds;
        }
        if (_ringText)
            _ringText.text = _ringNumber.ToString();
        if (_scoreText)
            _scoreText.text = _score.ToString();
    }

    public void AddRing()
    {
        _ringSound.Play();
        AddScore(100);
        _ringNumber++;
    }

    public void AddScore(int score)
    {
        _score += score;
    }

    public IEnumerator EndLevel()
    {
        _levelEnded = true;
        _level1.Stop();
        _level2.Stop();
        _winSound.Play();
        int time = Mathf.CeilToInt(Time.time);
        if (time < 2000)
        _score += 2000 - time;
        yield return new WaitForSeconds(6);
        _levelScore.GetComponent<Text>().text = "TOTAL SCORE " + _score;
        _levelScore.SetActive(true);
    }
}
