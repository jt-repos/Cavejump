using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScript : MonoBehaviour
{
    [SerializeField] RuleTile foregroundTile;
    [SerializeField] Tile ladder;
    [SerializeField] float tileSpawnInterval = 0.2f;
    [SerializeField] List<Vector3Int> level2Platform;
    [SerializeField] List<Vector3Int> level3Platform;
    [SerializeField] List<Vector3Int> level4Ladder;
    [SerializeField] List<Vector3Int> level5Ladder;
    int coroutinesFinished;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("levelsCompleted", 2);
        if (PlayerPrefs.GetInt("levelsCompleted") >= 1)
        {
            StartCoroutine(SpawnPlatform(8, level2Platform));
            if (PlayerPrefs.GetInt("levelsCompleted") >= 2)
            {
                StartCoroutine(SpawnPlatform(8, level3Platform));
                if (PlayerPrefs.GetInt("levelsCompleted") >= 3)
                {
                    StartCoroutine(SpawnPlatform(9, level4Ladder));
                    if (PlayerPrefs.GetInt("levelsCompleted") >= 4)
                    {
                        StartCoroutine(SpawnPlatform(9, level5Ladder));
                        if (PlayerPrefs.GetInt("levelsCompleted") >= 5)
                        {
                            FindObjectOfType<DisplayCoinsHub>().ChangeText(6, 0);
                        }
                    }
                }
            }
        }
    } 

    private IEnumerator SpawnPlatform(int layerIndex, List<Vector3Int> tilePositions)
    {
        foreach (Tilemap tilemapLayer in transform.GetComponentsInChildren<Tilemap>())
        {
            if (tilemapLayer.gameObject.layer == 8 && layerIndex == 8) //ground
            {
                foreach (Vector3Int tilePos in tilePositions)
                {
                    tilemapLayer.SetTile(tilePos, foregroundTile);
                    yield return new WaitForSeconds(tileSpawnInterval);
                }
            }
            if (tilemapLayer.gameObject.layer == 9 && layerIndex == 9) //ladder
            {
                foreach (Vector3Int tilePos in tilePositions)
                {
                    tilemapLayer.SetTile(tilePos, ladder);
                    yield return new WaitForSeconds(tileSpawnInterval);
                }
            }
        }
        coroutinesFinished++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
