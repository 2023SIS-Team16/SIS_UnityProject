using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToggle : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToSet;
    // Start is called before the first frame update

    public void SetObjectActive()
    {
        gameObjectToSet.SetActive(!gameObjectToSet.activeSelf);
    }
}
