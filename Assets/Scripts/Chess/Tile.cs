using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Color _baseColor, _offsetColor;
    public SpriteRenderer _spriteRender;
    [SerializeField] Color _highLightColor;
    [SerializeField] bool isWalkable;
    public BaseUnit occupiedUnit;
    public int x, y;
    
    public bool walkable => isWalkable && occupiedUnit == null;
    
    void OnMouseEnter() {
        _spriteRender.color = _highLightColor;
    }

    void OnMouseExit() {
        _spriteRender.color = _baseColor;
    }

    private void OnMouseDown() {
        if(GameManager.instance.state != GameState.HeroTurn) return;
        if(occupiedUnit != null){
            if(occupiedUnit.faction == Faction.Hero){
                UnitManager.instance.SelectHero = (BaseHero) occupiedUnit;
            }
            else{
                if(UnitManager.instance.selectedHero != null){
                    var enemy = (BaseEnemy) occupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitManager.instance.SelectHero = null;
                }
            }
        }
        else{
            if(UnitManager.instance.SelectHero != null){
                SetUnit(UnitManager.instance.SelectHero);
                UnitManager.instance.SelectHero = null;
            }
        }
    }
    public void Setup(int x, int y){
        this.x = x;
        this.y = y;
    }

    public void SetUnit(BaseUnit unit){
            if(unit.occupiedTile != null) unit.occupiedTile.occupiedUnit = null;
            unit.transform.position = transform.position;
            occupiedUnit = unit;
            unit.occupiedTile = this;
    }


}
