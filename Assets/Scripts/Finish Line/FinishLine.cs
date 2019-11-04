using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject finishLine;

    // Start is called before the first frame update
    void Start()
    {
        finishLine.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        finishLine.SetActive(true);
        Time.timeScale = 0;
    }
}
