using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler 
{
	private Image backGroundImg;
	private Image joyStickImg;
	private Vector2 inputVector;

	public string alternativeInputXAxis, alternativeInputYAxis;//These are used for keyboard input, and maybe also for console remotes

	public float deadzoneSize = 0.1f;

	private void Start()
	{
		backGroundImg = GetComponent<Image> ();
		joyStickImg = transform.GetChild (0).GetComponent<Image> ();
		//backGroundImg.rectTransform.anchoredPosition.x = Screen.width / 10;
		//backGroundImg.rectTransform.anchoredPosition.y = Screen.height / 10;
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (backGroundImg.rectTransform, 
			   ped.position, ped.pressEventCamera, out pos)) 
		{
			
			pos.x = (pos.x / 200 - 0.5f);
			pos.y = (pos.y / /*backGroundImg.rectTransform.sizeDelta.y these lines have been swapped for the actual size I set rect, as the size property was lost after applying anchors */ 200 + 0.5f);

			inputVector = new Vector2 (pos.x * 2 + 1, pos.y * 2 - 1);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			//Debug.Log (inputVector);

			//Move joytsick image
			joyStickImg.rectTransform.anchoredPosition = new Vector2(inputVector.x * 
				(200 /1.5f), inputVector.y * 
				(200 /1.5f)); //Changing values here seem to allow the stick to move more
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector2.zero;
		joyStickImg.rectTransform.anchoredPosition = Vector2.zero;
	}

	
	public float Horizontal()
	{
	if (inputVector.x != 0 && inputVector.magnitude > deadzoneSize)
			return inputVector.x;
		else
			return new Vector2 (Input.GetAxis (alternativeInputXAxis), Input.GetAxis (alternativeInputYAxis)).normalized.x;
	}

	public float Vertical()
	{
	if (inputVector.y != 0 && inputVector.magnitude > deadzoneSize)
			return inputVector.y;
		else
			return new Vector2 (Input.GetAxis (alternativeInputXAxis), Input.GetAxis (alternativeInputYAxis)).normalized.y;
	}
}
