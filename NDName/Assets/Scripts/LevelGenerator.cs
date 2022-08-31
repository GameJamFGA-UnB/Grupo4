using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] private Transform Level1;

   private void Awake()
   {

    Instantiate (Level1, new Vector3 (2,0,75), Quaternion.identity);

   }    
}
