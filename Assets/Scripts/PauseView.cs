using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button continueButton;

    #endregion

    #region Events

    public static event Action OnClose;

    #endregion

    #region Unity lifecycle

    void Start()
    {
        continueButton.onClick.AddListener(ContinueClickHandler);
    }

    #endregion

    #region Event Handlers

    private void ContinueClickHandler()
    {
        OnClose?.Invoke();
    }

    #endregion
}
