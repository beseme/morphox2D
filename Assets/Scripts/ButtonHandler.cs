using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    private AudioClip _sound;
    [SerializeField]
    private AudioClip _next;
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private int _textIndex;
    [SerializeField]
    private Text[] _texts = new Text[3];
    [SerializeField]
    private Image _pauseMenu;
    [SerializeField]
    private AudioSource _globalSource;
    [SerializeField]
    private int _level;
    [SerializeField]
    private int _restart;

    private void Start()
    {
       // _texts[1].gameObject.SetActive(false);
       // _texts[2].gameObject.SetActive(false);
       // _texts[3].gameObject.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        UnityEngine.Cursor.visible = true;
    }

    public void start()
    {
        SceneManager.LoadScene(1);
        if(Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(_restart);
        if (Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
    }

    public void HowTo()
    {
        SceneManager.LoadScene(2);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void Hover()
    {
        _source.PlayOneShot(_sound, 1);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _pauseMenu.gameObject.SetActive(false);
        _globalSource.volume = .3f;
        UnityEngine.Cursor.visible = false;
    }

    public void Next()
    {
        _textIndex++;
        LoadText(_textIndex);
        if (_textIndex < 4)
            _source.PlayOneShot(_next, 1);
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        UnityEngine.Cursor.visible = false;
        SceneManager.LoadScene(_level);
    }

    private void LoadText(int i)
    {
        _texts[i - 1].gameObject.SetActive(false);
        _texts[i].gameObject.SetActive(true);
    }

}
