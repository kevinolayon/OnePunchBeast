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

    public void OnPointerDown(PointerEventData eventData)
    {
        Show(eventData);
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Move(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Release();
    }

    void Show(PointerEventData eventData) // Show joystick image in the touched position
    {
        joyGroup.DOKill();
        joyGroup.alpha = 1;
        joyBg.position = eventData.position;
        fadeJoy = StartCoroutine(JoyHide());
    }

    void Move(PointerEventData eventData) // Move thumb image position
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

    void Release()
    {
        StopCoroutine(fadeJoy);
        joyGroup.DOKill();
        joyGroup.DOFade(0, .25f);
        inputVector = Vector3.zero;
        joyThumb.localPosition = Vector3.zero;
    }

    IEnumerator JoyHide()
    {
        yield return new WaitForSeconds(2);

        joyGroup.DOFade(.25f, .25f);
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
}