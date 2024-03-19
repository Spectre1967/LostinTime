using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClockPuzzle : MonoBehaviour, IPointerClickHandler
{
    public int targetTimeHours, targetTimeMinutes;
    public RectTransform hourHand, minuteHand;

    private double currentHour, currentMinute;
    const float minuteRotation = 6f, hourRotation = 30f;

    public static bool isSolved;
    public static bool clockVisible = true;

    private void Update()
    {
        if (clockVisible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        hourHand.rotation = Quaternion.Lerp(hourHand.rotation, Quaternion.Euler(0, 0, (float)(0 - (hourRotation * currentHour))), Time.deltaTime * 5);
        minuteHand.rotation = Quaternion.Lerp(minuteHand.rotation, Quaternion.Euler(0, 0, (float)(0 - (minuteRotation * currentMinute))), Time.deltaTime * 5);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            currentMinute += 5;
            currentHour += 1d/12d;

            if (currentMinute >= 60)
            {
                currentMinute -= 60;
                currentHour = Mathf.Round((float)currentHour);
            }
            if (currentHour >= 12)
            {
                currentHour -= 12;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            currentMinute -= 5;
            currentHour -= 1d/12d;

            if (currentMinute <= 0)
            {
                currentMinute += 60;
                currentHour = Mathf.Round((float)currentHour);
            }
            if (currentHour <= 0)
            {
                currentHour += 12;
            }
        }
    }
}
