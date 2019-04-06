using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int ringNumber;
    private int _score;
	private bool _levelEnded;
	private bool _pause = false;
    [SerializeField] private AudioSource _ringSound;
    [SerializeField] private AudioSource _winSound;
    [SerializeField] private AudioSource _level1;
    [SerializeField] private AudioSource _level2;
    [SerializeField] private GameObject _levelScore;
    [SerializeField] private Text _ringText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _time;
    [SerializeField] private GameObject _ringPrefab;
	[SerializeField] private Transform _player;
	[SerializeField] private GameObject _pauseMenu;
	// Start is called before the first frame update
	void Start()
	{
		_pause = false;
        _levelEnded = false;
        if (SceneManager.GetActiveScene().name == "Level1.0")
            _level1.Play();
        else if (SceneManager.GetActiveScene().name == "Level2.0")
            _level2.Play();
        ringNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
		    _pause = !_pause;
		    if (_pause)
			    Time.timeScale = 0;
		    else
			    Time.timeScale = 1;
		    _pauseMenu.SetActive(_pause);
	    }
        int minutes = Mathf.CeilToInt(Time.timeSinceLevelLoad) / 60 ;
        int seconds = Mathf.CeilToInt(Time.timeSinceLevelLoad) % 60;
        if (!_levelEnded && _time)
        {
            _time.text = minutes + ":";
            if (seconds < 10)
                _time.text += "0";
            _time.text += seconds;
        }
        if (_ringText)
            _ringText.text = ringNumber.ToString();
        if (_scoreText)
            _scoreText.text = _score.ToString();
    }

    public void AddRing()
    {
        _ringSound.Play();
        AddScore(100);
        ringNumber++;
    }

	public void LoseRings(Vector2 where)
	{
		int loseNumber = 0;
		if (ringNumber > 1)
		{
			loseNumber = ringNumber / 2;
		}
		else
		{
			loseNumber = ringNumber;
		}

		ringNumber -= loseNumber;
		GameObject ring;
		for (int i = 0; i < loseNumber; i++)
		{
			Debug.Log("insta");
			ring = Instantiate(_ringPrefab, _player);
			ring.transform.localPosition = Vector2.zero;
			ring.transform.SetParent(null);
			ring.GetComponent<RingScript>().Explode(where);
		}
			
	}

    public void AddScore(int score)
    {
        _score += score;
    }

    public IEnumerator EndLevel(int levelNum)
    {
        _levelEnded = true;
        _level1.Stop();
        _level2.Stop();
        _winSound.Play();
        int time = Mathf.CeilToInt(Time.time);
        if (time < 2000)
        _score += 2000 - time;
		PlayerPrefs.SetInt("Rings", PlayerPrefs.GetInt("Rings") + ringNumber);
		//PlayerPrefs.SetInt("Deth", PlayerPrefs.GetInt("Deth") + _ringNumber);
		yield return new WaitForSeconds(6);
        _levelScore.GetComponent<Text>().text = "TOTAL SCORE " + _score;
		if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name) < _score)
			PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, _score);
        _levelScore.SetActive(true);
		yield return new WaitForSeconds(2);
		int openLevels = PlayerPrefs.GetInt("OpenLevels");
		if (openLevels < levelNum)
			PlayerPrefs.SetInt("OpenLevels", openLevels + 1);
		SceneManager.LoadScene("DataSelect");
	}

	public void OnExitClicked()
	{
		SceneManager.LoadScene("DataSelect");
	}
	
	public void OnResumeClicked()
	{
		Time.timeScale = 1;
		_pauseMenu.SetActive(false);
		_pause = !_pause;
	}
}
