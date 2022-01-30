using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(order = 2, menuName = "Config/SceneObjects")]
    public class SceneObjects:ScriptableObject
    {
        public GameObject PlayerPrefab;
        public GameObject StairPrefab;
        public GameObject[] EnemyPrefab;
    }
}