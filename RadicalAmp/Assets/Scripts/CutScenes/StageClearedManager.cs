using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageClearedManager : MonoBehaviour {

    [SerializeField] private List<Transform> _allEnemies = new List<Transform>();
    [SerializeField] private UnityEvent _onStageCleared;
    public static StageClearedManager instance;

	void Start ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
	}

    public void DeRegisterFromList(Transform toDeregister)
    {
        if(_allEnemies.Contains(toDeregister))
        {
            _allEnemies.Remove(toDeregister);
        }
        if (ConditionsMet)
        {
            _onStageCleared.Invoke();
        }
    }
	
    private bool ConditionsMet { get { return _allEnemies.Count <= 0; } }
    
}
