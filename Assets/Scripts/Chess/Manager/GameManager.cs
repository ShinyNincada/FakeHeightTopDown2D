using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    // Start is called before the first frame update
    void Awake() {
        if(instance == null){
            instance = this;
        }
        else if(instance != null){
            Destroy(gameObject);
        }
    }

    void Start(){
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        state = newState;
        switch(newState){
            case GameState.Menu:
                break;
            case GameState.GenerateGrid:
                GridManager.instance.GenerateGrid();
                break;
            case GameState.SpawnHero:
                UnitManager.instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                UnitManager.instance.SpawnEnemies();
                break;
            case GameState.HeroTurn:
                break;
            case GameState.EnemiesTurn:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}

 public enum GameState{
        Menu,
        GenerateGrid,
        SpawnHero,
        SpawnEnemies,
        HeroTurn,
        EnemiesTurn
    }