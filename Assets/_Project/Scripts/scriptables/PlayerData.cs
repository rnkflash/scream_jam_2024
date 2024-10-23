using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.scriptables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/PlayerData")]
    public class PlayerData : SerializedScriptableObject
    {
        public int startingMoney = 0;
        public int goalMoney = 60000;
        public int oneMissionMoney = 10000;
        
    }
}