using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Shooter.Enemy;
using System.Linq;

public class CountEnemyUI : MonoBehaviour
{

    public List<EnemyCharacter> Enemies { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _outputText;
    private string _format;


    private void Start()
    {
        Enemies = FindObjectsOfType<EnemyCharacter>().ToList();
        _format = _outputText.text;
    }

    private void Update()
    {
        //Debug.Log(Enemies.Count);
        _outputText.text = string.Format(_format, Enemies.Count);
    }
}
