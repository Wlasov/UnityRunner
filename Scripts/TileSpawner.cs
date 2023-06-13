using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tiles;
    [SerializeField] private GameObject _startTile;

    private GameObject _currentTile;

    private void Start()
    {
        _currentTile = _startTile;       
    }

    private void LateUpdate()
    {
        
        if (_currentTile.transform.position.z <= 15)
        {
            SpawnTile(_tiles);
        }

        DeleteTile(_currentTile);
    }

    private GameObject NextTile(List<GameObject> tiles)
    {
        return _tiles[Random.Range(0, tiles.Count - 1)];
    }

    private void SpawnTile(List<GameObject> tiles)
    {
        _currentTile = Instantiate(NextTile(tiles), transform.position, Quaternion.identity);
    }

    private void DeleteTile(GameObject tile)
    {
        if (tile.transform.position.z <= -22)
        {
            Destroy(tile);
        }
    }

}
