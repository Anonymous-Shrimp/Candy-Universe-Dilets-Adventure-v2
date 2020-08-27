using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBar : MonoBehaviour
{
    public Transform targetTransform;
    public Transform bar;
    private float target;

    private void Start()
    {
        targetTransform = FindObjectOfType<Ouch>().transform;
    }
    void Update()
    {
        Vector3 targetScale = bar.localScale;
        transform.LookAt(targetTransform.transform);
        if (targetScale.x < target)
        {
            targetScale.x += Time.deltaTime * Mathf.Abs(targetScale.x - target) * 2;
        }
        else if (targetScale.x > target)
        {
            targetScale.x -= Time.deltaTime * Mathf.Abs(targetScale.x - target) * 2;
        }
        bar.GetComponentInChildren<SpriteRenderer>().color = new Color(1, Mathf.Abs(targetScale.x - target) * 5, Mathf.Abs(targetScale.x - target) * 5, 0.7f);
        gameObject.transform.localScale = new Vector3(0.5f + (targetScale.x - target) * 0.6f, 0.5f + (targetScale.x - target) * 0.6f, 1);
        bar.localScale = targetScale;
    }
    public void EnemySize(float sizeNormalized)
    {
        target = sizeNormalized;
    }
}