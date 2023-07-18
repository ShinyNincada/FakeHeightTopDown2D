using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;
    public BaseHero selectedHero;
    public List<ChessObject> _units;
    private void Awake() {
        instance = this;

        _units = Resources.LoadAll<ChessObject>("Chess").ToList();
    }

    public void SpawnHeroes(){
        int heroCount = 1;
        for(int i = 0; i < heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            var randomTile = GridManager.instance.GetHeroSpawnTile();

            randomTile.SetUnit(spawnedHero);
        }

        GameManager.instance.ChangeState(GameState.SpawnEnemies);
        
    }
    public void SpawnEnemies(){
        int heroCount = 1;
        for(int i = 0; i < heroCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomTile = GridManager.instance.GetHeroSpawnTile();

            randomTile.SetUnit(spawnedEnemy);
        }
        GameManager.instance.ChangeState(GameState.HeroTurn);
    }


    public BaseHero SelectHero {
        get { return selectedHero; }
        set { selectedHero = value; }
    }
    T GetRandomUnit<T>(Faction faction) where T : BaseUnit{
        return (T) _units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

}
