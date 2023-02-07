using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectivePan : MonoBehaviour
{
	private Vector3 touchStart;
	public float zoomMin = 1;
	public float zoomMax = 8;

	// Start is called before the first frame update
	void Start()
	{

	}

	PinchMode currentMode = PinchMode.none;
	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector3 direction = new Vector3();
			if (Input.touchCount == 2)
			{
				Touch touchFirst = Input.GetTouch(0);
				Touch touchSecond = Input.GetTouch(1);

				if (currentMode == PinchMode.pinch)
				{
					// Zoom
					Vector2 touchFirstPrev = touchFirst.position - touchFirst.deltaPosition;
					Vector2 touchSecondPrev = touchSecond.position - touchSecond.deltaPosition;
					float prevMagnitude = (touchFirstPrev - touchSecondPrev).magnitude;
					float currentMagnitude = (touchFirst.position - touchSecond.position).magnitude;
					float difference = currentMagnitude - prevMagnitude;
					zoom(difference * 0.01f);
					// Calculate pinch move
					Vector2 currentMid = (touchFirst.position + touchSecond.position) / 2;
					direction = touchStart - Camera.main.ScreenToWorldPoint(currentMid);
				}
				else
				{
					// Init pinch
					touchStart = Camera.main.ScreenToWorldPoint((touchFirst.position + touchSecond.position) / 2);
					currentMode = PinchMode.pinch;
				}
			}
			else if (Input.touchCount < 2)
			{
				if (currentMode == PinchMode.move)
				{
					// Calculate move
					direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
				}
				else
				{
					// Init move
					touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					currentMode = PinchMode.move;
				}
			}
			// Move cam
			Camera.main.transform.position += direction;
			// DrawPoint(touchStartRaw, center);
		}
		else
		{
			currentMode = PinchMode.none;
		}
	}

	void zoom(float zoom)
	{
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoom /* Mathf.Min(Mathf.Log10(Camera.main.orthographicSize), 0.1f)*/, zoomMin, zoomMax);
	}

	enum PinchMode
	{
		none,
		move,
		pinch
	}
}