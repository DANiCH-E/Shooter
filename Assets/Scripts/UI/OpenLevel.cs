using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenLevel : MonoBehaviour
{
    private Button _openLevelButton;

    [SerializeField]
    private List<int> _levelsList = new List<int>();

    private void Start()
    {
        _openLevelButton = GetComponent<Button>();
        _openLevelButton.onClick.RemoveAllListeners();
        _openLevelButton.onClick.AddListener(OpenRandomLevel);
    }

    private void OpenRandomLevel()
    {
        int level = _levelsList[Random.Range(0, _levelsList.Count)];
        SceneManager.LoadScene(level);
        Time.timeScale = 1f;
    }
}
