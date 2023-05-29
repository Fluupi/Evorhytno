using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenUIController : MonoBehaviour
{
    #region Singleton
    private static EndScreenUIController instance;
    public static EndScreenUIController Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }
    #endregion

    private void Start()
    {
        transform.Find(MySceneManager.Instance.IsWin ? "Win" : "Lose")?.gameObject.SetActive(true);
    }
}
