using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private const float playerDistance = 200f;

    [SerializeField] private Transform initialLevel;
    [SerializeField] private Transform level1;
    [SerializeField] private Transform player;


    private Vector3 lastEndPosition;

   private void Awake()
   {

        lastEndPosition = initialLevel.Find("endPosition").position;

        for (int i = 0; i< 5; i++){ //quantidade de partes iniciais
            SpawnLevelPart();
        }

   }    

   private void Update(){

        if (Vector3.Distance(player.transform.position, lastEndPosition) < playerDistance)
        {

            SpawnLevelPart();

        }



   }

   private void SpawnLevelPart(){

        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("endPosition").position;

   }

   private Transform SpawnLevelPart(Vector3 spawnPosition){  

        Transform levelPartTransform = Instantiate(level1, spawnPosition, Quaternion.identity);
        return levelPartTransform;

   }
}
