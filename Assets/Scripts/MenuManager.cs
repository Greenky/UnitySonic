using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _doneText; 
    [SerializeField] private Button[] _levels = null;
    [SerializeField] private GameObject[] _levelsButtons;
    [SerializeField] private Text _score;
    [SerializeField] private Text _rings;
    [SerializeField] private Text _death;


    private void Start()
    {
        
        if (SceneManager.GetActiveScene().name == "DataSelect")
        {
            if (_rings)
                _rings.text = PlayerPrefs.GetInt("Rings").ToString();
            if (_death)
                _death.text = PlayerPrefs.GetInt("Death").ToString();
            int levels = PlayerPrefs.GetInt("OpenLevels");
            for (int i = 0; i < levels; i++)
            {
                _levels[i + 1].interactable = true;
            }
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen" && Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("DataSelect");
        }
    }

    public void OnResetClick()
    {
        _doneText.SetActive(true);
        PlayerPrefs.DeleteAll();
        StartCoroutine(OffDone());
    }
    
    public void OnLevelClick(GameObject button)
    {
        //Debug.Log();
        foreach (var lb in _levelsButtons)
        {
            lb.SetActive(false);
        }
        button.SetActive(true);
        _score.text = "Score: " + PlayerPrefs.GetInt(button.name);
    }
    
    public void OnLoadClick(GameObject level)
    {
        SceneManager.LoadScene(level.transform.name);
    }

    private IEnumerator OffDone()
    {
        yield return new WaitForSeconds(1);
        _doneText.SetActive(false);
    }
}
