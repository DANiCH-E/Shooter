using Shooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;

    void Start()
    {
        _gameManager.Win += ShowPanel;
        gameObject.SetActive(false);
    }

    private void ShowPanel()
    {
        gameObject.SetActive(true);
    }
}
