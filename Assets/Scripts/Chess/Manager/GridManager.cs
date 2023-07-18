using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;
    [SerializeField] Transform chessboard;
    float _chessWidth, _chessHeight;
    [SerializeField] int _width, _height;
    [SerializeField] Tile _tilePrefab; 

    Dictionary<Vector2, Tile> _tiles;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else if(instance != null){
            Destroy(gameObject);
        }
    }
    public void GenerateGrid(){
        _tiles = new Dictionary<Vector2, Tile>();
        SpriteRenderer tileSprite = _tilePrefab._spriteRender;
        float tileWidth = tileSprite.bounds.size.x;
        float tileHeight = tileSprite.bounds.size.y;
        _chessHeight = tileHeight * _height;
        _chessWidth = tileWidth * _width;
        float   offsetX = - _chessWidth / 2f - tileWidth / 2f,
                offsetY = - _chessHeight /2f - tileHeight /2f;
        for(int x = 1; x <= _width; x++){
            for(int y = 1; y <= _width; y++){
                float posX = offsetX + x * tileWidth;
                float posY = offsetY + y * tileHeight;

                GameObject tileSpawned = Instantiate(_tilePrefab.gameObject, new Vector3(posX, posY), Quaternion.identity, chessboard);
                
                tileSpawned.name = $"Tile{x}{y}";
                
                _tiles[new Vector2(posX, posY)] = tileSpawned.GetComponent<Tile>();
                tileSpawned.GetComponent<Tile>().Setup(x, y);
            }
        }

        GameManager.instance.ChangeState(GameState.SpawnHero);
        Debug.Log("Ca");
    }

    public Tile GetHeroSpawnTile(){
        return _tiles.Where(t => t.Value.y <= _width /2 && t.Value.walkable).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetEnemySpawnTile(){
        return _tiles.Where(t => t.Value.y > _width /2 && t.Value.walkable).OrderBy(t => Random.value).First().Value;
    }
    public Tile GetTile(Vector2 pos){
        if(_tiles.TryGetValue(pos, out var tile)){
            return tile;
        }

        return null;
    }
}


