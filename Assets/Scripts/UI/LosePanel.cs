using Shooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;

    void Start()
    {
        _gameManager.Loss += ShowPanel;
        gameObject.SetActive(false);
    }

    private void ShowPanel()
    {
        gameObject.SetActive(true);
    }
}
