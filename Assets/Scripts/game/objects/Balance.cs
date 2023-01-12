using UnityEngine;

public class Balance {
    private static StaticData.Balance _values = null;
    public static StaticData.Balance values {
        get {
            if (_values == null) 
                Load();
            return _values;
        }
    }

    public static void Load() {
        _values = Resources.Load<StaticData.Balance>("Balance/Balance");
    }
}