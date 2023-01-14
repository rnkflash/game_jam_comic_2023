using UnityEngine;
using System;
using System.Collections.Generic;

namespace StaticData {
    [CreateAssetMenu(fileName = "Balance", menuName = "Static Data/Balance")]
    public class Balance : ScriptableObject
    {
        public int start_fuel;
        public int max_fuel;
        public int start_food;
        public int max_food;
        public int start_money;
        public int max_money;
        public int start_distance;
        public int max_distance;

        public List<ResourceEachCard> each_card_resources;

        [Serializable]
        public class ResourceEachCard{
            public Resource type;
            public int amount = 0;
        }

    }
}