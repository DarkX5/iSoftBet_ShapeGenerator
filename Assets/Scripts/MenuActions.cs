using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using MEC;

public class MenuActions : MonoBehaviour
{
    public static MenuActions Instance { get; private set; }

    [Header("Text References")]
    [SerializeField] private TMP_Text text_NoOfCurrentShapes;
    [SerializeField] private TMP_Text text_SurfaceAreaOccupiedByShapes;
    [SerializeField] private TMP_Text text_ShapesPerSecond;
    [SerializeField] private TMP_Text text_Gravity;
    [SerializeField] private TMP_Text text_Hits;
    [SerializeField] [Range(0f, 1f)]private float updateFrequencyInSeconds = 1f;
    public event Action onGravityIncreaseAction;
    public event Action onGravityDecreaseAction;
    // public event Action onUniqueShapesToggleAction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start() {
        UpdateNoOfCurrentShapeValueText();
        UpdateSurfaceAreaOccupiedByShapesText();
        UpdateHitsText();
        text_ShapesPerSecond.text = ShapeGenerator.Instance.ShapesNoPerSecond.ToString();
        text_Gravity.text = ShapeGenerator.Instance.GravityBase.ToString();
        
        Timing.RunCoroutine(UpdateUICo());
    }
    IEnumerator<float> UpdateUICo()
    {
        UpdateNoOfCurrentShapeValueText();
        UpdateSurfaceAreaOccupiedByShapesText();
        UpdateHitsText();

        yield return Timing.WaitForSeconds(updateFrequencyInSeconds);

        Timing.RunCoroutine(UpdateUICo());
    }

#region Text Updates
    private void UpdateNoOfCurrentShapeValueText()
    {
        text_NoOfCurrentShapes.text = ShapeGenerator.Instance.NoOfCurrentShapes.ToString();
    }
    private void UpdateSurfaceAreaOccupiedByShapesText()
    {
        text_SurfaceAreaOccupiedByShapes.text = ShapeGenerator.Instance.SurfaceAreaOccupiedByShapes.ToString();
    }
    private void UpdateHitsText()
    {
        text_Hits.text = PlayerControls.Instance.HitsNo.ToString();
    }
#endregion
#region [Public] Shapes Per Second Controls
    public void ShapesPerSecondIncrease() {
        ShapeGenerator.Instance.ShapesPerSecondIncrease();
        text_ShapesPerSecond.text = ShapeGenerator.Instance.ShapesNoPerSecond.ToString();
    }
    public void ShapesPerSecondDecrease() {
        ShapeGenerator.Instance.ShapesPerSecondDecrease();
        text_ShapesPerSecond.text = ShapeGenerator.Instance.ShapesNoPerSecond.ToString();
    }
#endregion
#region [Public] Gravity Controls
    public void GravityIncrease() {
        if(onGravityIncreaseAction != null) {
            onGravityIncreaseAction();
        }
        // ShapeGenerator.Instance.GravityIncrease();
        text_Gravity.text = ShapeGenerator.Instance.GravityBase.ToString();

    }
    public void GravityDecrease() {
        if (onGravityDecreaseAction != null)
        {
            onGravityDecreaseAction();
        }
        // ShapeGenerator.Instance.GravityDecrease();
        text_Gravity.text = ShapeGenerator.Instance.GravityBase.ToString();
    }
#endregion
}
