using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(order = 2, menuName = "Config/SceneObjects")]
    public class SceneObjects:ScriptableObject
    {
        public GameObject[] EnemyPrefab;
    }
}