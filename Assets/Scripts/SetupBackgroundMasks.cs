using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBackgroundMasks : MonoBehaviour
{
    [SerializeField] private RectTransform topMask;
    [SerializeField] private RectTransform leftMask;
    [SerializeField] private RectTransform bottomMask;
    [SerializeField] private RectTransform rightMask;

    [SerializeField] private float updateDuration = 0.25f;
    [SerializeField] [Range(0f, 1f)] private float widthSizeAdjuster = 0.125f;
    [SerializeField] [Range(0f, 1f)] private float heightSizeAdjuster = 4f;

    // Vector2 heightSize;
    // Vector2 widthSize;
    private float sWidth = 0f;
    private float sHeight = 0f;
    Vector2 auxSize;
    bool portrait = false;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(CheckScreenResolutionCo());
    }

    IEnumerator CheckScreenResolutionCo() {
        SetupMasksByResolution();

        yield return new WaitForSeconds(updateDuration);

        StartCoroutine(CheckScreenResolutionCo());
    }

    // Update is called once per frame
    void Update()
    {
        SetupMasksByResolution();
    }

    private void SetupMasksByResolution()
    {
        // if (sWidth != Screen.width || sHeight != Screen.height)
        // {
            sWidth = Screen.width;
            sHeight = Screen.height;

            // widthSize = new Vector2(sWidth * widthSizeAdjuster, sHeight); // 10% width
            // heightSize = new Vector2(sWidth, sHeight * heightSizeAdjuster); //25% height

            auxSize = new Vector2(sWidth * widthSizeAdjuster, sHeight);
            leftMask.sizeDelta = auxSize;
            leftMask.position = new Vector3(-(sWidth - auxSize.x), 0f, 0f);

            rightMask.sizeDelta = auxSize;
            rightMask.position = new Vector3((sWidth - auxSize.x), 0f, 0f);

            auxSize = new Vector2(sWidth, sHeight * heightSizeAdjuster / 2);// 50% height compared to bottom mask
            topMask.sizeDelta = auxSize;
            topMask.position = new Vector3(0f, (sHeight - auxSize.y), 0f);
            
            auxSize = new Vector2(sWidth, sHeight * heightSizeAdjuster); //25% height
            bottomMask.sizeDelta = auxSize;
            bottomMask.position = new Vector3(0f, -(sHeight - auxSize.y), 0f);

            Debug.Log($"Width: {sWidth} || Height: {sHeight}");
        // }
    }
}
