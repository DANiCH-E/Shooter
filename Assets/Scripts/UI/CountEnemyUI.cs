using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Shooter.Enemy;
using System.Linq;

public class CountEnemyUI : MonoBehaviour
{

    GameObject[] enemies;

    [SerializeField]
    private TextMeshProUGUI _outputText;
    private string _format;


    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _format = _outputText.text;
    }

    private void Update()
    {
        //Debug.Log(Enemies.Count);
        _outputText.text = string.Format(_format, enemies.Length.ToString());
    }
}
