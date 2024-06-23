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
    public List<EnemyCharacterView> Enemies { get; private set; }

    private void Start()
    {
        Enemies = FindObjectsOfType<EnemyCharacterView>().ToList();
        _format = _outputText.text;
    }

    private void Update()
    {
        Enemies = FindObjectsOfType<EnemyCharacterView>().ToList();
        //Debug.Log(Enemies.Count);
        _outputText.text = string.Format(_format, Enemies.Count.ToString());
    }
}
