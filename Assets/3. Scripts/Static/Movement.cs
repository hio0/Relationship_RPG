using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class Movement
{
    // Made'n
    public static IEnumerator MoveAnimation(RectTransform what, Vector2 target, float speed)
    {
        while ((what.anchoredPosition - target).sqrMagnitude > 0.001f)
        {
            float x = Mathf.Lerp(what.anchoredPosition.x, target.x, Time.deltaTime * speed);
            float y = Mathf.Lerp(what.anchoredPosition.y, target.y, Time.deltaTime * speed);

            what.anchoredPosition = new Vector2(x, y);
            yield return null;
        }
    }

    public static IEnumerator LerpFade(RectTransform what, CanvasGroup can, Vector2 target)
    {
        Vector2 startPos = what.anchoredPosition;
        float totalDistance = Vector2.Distance(startPos, target);

        if (totalDistance <= 0f)
        {
            can.alpha = 0f;
            yield break;
        }

        while (true)
        {
            float currentDistance = Vector2.Distance(startPos, what.anchoredPosition);

            float progress = currentDistance / totalDistance;

            can.alpha = 1f - Mathf.Clamp01(progress);

            if (progress >= 1f)
                break;

            yield return null;
        }

        can.alpha = 0f;
    }

    public static IEnumerator SizeSetAnimation(RectTransform what, Vector2 target, float speed)
    {
        while ((what.sizeDelta - target).sqrMagnitude > 0.001f)
        {
            float x = Mathf.Lerp(what.sizeDelta.x, target.x, Time.deltaTime * speed);
            float y = Mathf.Lerp(what.sizeDelta.y, target.y, Time.deltaTime * speed);

            what.sizeDelta = new Vector2(x, y);
            yield return null;
        }
    }

    public static IEnumerator Typing(TMP_Text text, string message, float duration)
    {
        text.text = message;
        text.maxVisibleCharacters = 0;

        for (int i = 0; i <= message.Length; i++)
        {
            text.maxVisibleCharacters = i;
            yield return new WaitForSeconds(duration);
        }
    }

    public static IEnumerator LerpValue(float target, float targetValue, float duration)
    {
        float time = 0f;
        float startValue = target;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;
            target = Mathf.Lerp(startValue, targetValue, t);

            yield return null;
        }

        target = targetValue;
    }

    // DOTWeen
    public static void DoAnchorMove(RectTransform rect, Vector2 targetPos, float time)
    {
        rect.DOAnchorPos(targetPos, time);
    }

    public static void DoAnchorMoveX(RectTransform rect, float targetPos, float time)
    {
        rect.DOAnchorPosX(targetPos, time);
    }

    public static void DoAnchorMoveY(RectTransform rect, float targetPos, float time)
    {
        rect.DOAnchorPosY(targetPos, time);
    }

    public static void DoSizeMove(RectTransform rect, Vector2 targetSize, float time)
    {
        rect.DOSizeDelta(targetSize, time);
    }

    public static void DoRotation(RectTransform rect, Vector3 spinPos, float time)
    {
        rect.DORotate(spinPos, time);
    }

    public static void DoScale(RectTransform rect, Vector3 targetPos, float time)
    {
        rect.DOScale(targetPos, time);
    }

    public static void DOFade(CanvasGroup what, float howmuch, float time)
    {
        what.DOFade(howmuch, time);
    }

    public static void DoPunchScale(RectTransform rect, Vector2 targetSize, float time)
    {
        rect.sizeDelta = Vector3.zero;

        rect.DOSizeDelta(targetSize, time).SetEase(Ease.OutBack);
    }

    public static void DoOutEaseMove(RectTransform rect, Vector2 targetPos, float time)
    {
        rect.DOAnchorPos(targetPos, time).SetEase((time, duration, overshoot, period) =>
        {
            float t = time / duration;

            return 1f - Mathf.Pow(1f - t, 3f);
        });
    }

    public static void DoFillAmount_Cubic(Image image, float target, float time)
    {
        image.DOFillAmount(target, time).SetEase(Ease.OutCubic);
    }
}
