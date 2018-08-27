using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
	public static SensorManager Instance;
    public Sensor[] sensors { get; set; }

	public void Awake()
    {
		if (Instance == null)
        {
			Instance = this;
			Instance.sensors = new Sensor[36];
			for(int i = 0; i < 36; i ++)
			{
				sensors[i] = new Sensor();
			}
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}
}
