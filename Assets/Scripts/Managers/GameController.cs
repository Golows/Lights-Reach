using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public GameObject character;
    public GameObject bull;
    public UpgradeManager upgradeManager;
    public EnemyManager enemyManager;
    public AbilityManager abilityManager;
    public Spawner spawner;
    public UIManager uiManager;
    public StatueManager statueManager;
    public DamageDoneManager damageDoneManager;
    public ProgressManager progressManager;
    public PostProcessingManager postProcessingManager;
    public AudioManager audioManager;
    [NonSerialized] public PlayerCharacter playerCharacter;
    [NonSerialized] public Transform playerTransform;
    public GameObject xpOrb;
    public LevelManager levelManager;
    public TimeManager timeManager;
    public GameObject UI;

    public bool startingArea = false;

    private void Awake()
    {
        instance = this;
        character = Instantiate(character, new Vector3(0, 0, 0), Quaternion.identity);
        playerCharacter = character.GetComponent<PlayerCharacter>();
        playerTransform = character.transform;
        Time.timeScale = 1f;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 144;
    }

}
