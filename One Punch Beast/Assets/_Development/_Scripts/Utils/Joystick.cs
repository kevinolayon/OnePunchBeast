using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : Singleton<Joystick>, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] RectTransform joyBg;
    [SerializeField] RectTransform joyThumb;
    [SerializeField] CanvasGroup joyGroup;
    Vector3 inputVector;
    Coroutine fadeJoy;

    /* Example
     * // Get joystick direction
        float horizontal = Joystick.Instance.Horizontal();
        float vertical = Joystick.Instance.Vertical();

        // Set direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
    */

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joyBg, eventData.position, eventData.pressEventCamera, out Vector2 pos))
        {
            pos.x /= joyBg.sizeDelta.x;
            pos.y /= joyBg.sizeDelta.y;

            inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joyThumb.anchoredPosition = new Vector3(inputVector.x * (joyBg.sizeDelta.x / 3), inputVector.z * (joyBg.sizeDelta.y / 3));
        }
    }

    public Vector2 GetThumbPosition()
    {
        return joyThumb.anchoredPosition / joyBg.sizeDelta;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ShowJoystick(eventData);
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //joyGroup.interactable = false;
        //joyGroup.blocksRaycasts = false;
        StopCoroutine(fadeJoy);
        joyGroup.alpha = 1;
        inputVector = Vector3.zero;
        joyThumb.localPosition = Vector3.zero;
    }

    public void ShowJoystick(PointerEventData eventData)
    {
        joyBg.position = eventData.position;
        //joyGroup.interactable = true;
        //joyGroup.blocksRaycasts = true;
        fadeJoy = StartCoroutine(JoyHide());
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }

    IEnumerator JoyHide()
    {
        yield return new WaitForSeconds(2);

        joyGroup.DOFade(.25f, .25f);
    }
}
