using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Serializable = System.SerializableAttribute;

public class JoyStickInput : MonoBehaviour
{
    public static JoyStickInput Instance = null;
    public static Vector2 axis;
    [Serializable]
    public struct JoystickTouch
    {
        public Vector2 offset;
        public Vector2 position;
        public int fingerId;
        public bool active;
    }
    [Serializable]
    public struct JoystickButton
    {
        public Image background;
        public Image thumbstick;
        public Rect area;
    }
    public JoystickTouch stickTouch = new JoystickTouch();
    public JoystickButton stickButton = new JoystickButton();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        stickButton.area = new Rect(stickButton.thumbstick.rectTransform.position.x, stickButton.thumbstick.rectTransform.position.y, stickButton.thumbstick.rectTransform.sizeDelta.x, stickButton.thumbstick.rectTransform.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                MobileButtonCheck(new Vector2(touch.position.x, Screen.height - touch.position.y), touch.fingerId);

            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (stickTouch.active)
                {
                    if (stickTouch.active && stickTouch.fingerId == touch.fingerId)
                    {
                        stickTouch.position = touch.position;
                    }
                }
            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                MobileButtonStop(touch.fingerId);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                MobileButtonCheck(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y), 0);

            }
            
            else
            {
                stickTouch.position = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MobileButtonStop(0);
        }
        if (stickTouch.active)
        {
            stickButton.thumbstick.rectTransform.position = new Vector3
                (
                stickTouch.position.x - stickTouch.offset.x,
                stickTouch.position.y - stickTouch.offset.y
                );
            axis.x = stickButton.thumbstick.rectTransform.position.x - stickButton.area.x;
            axis.y = stickButton.thumbstick.rectTransform.position.y - stickButton.area.y;
            if (Mathf.Abs(axis.x) < 19)
            {
                axis.x = 0;
            }
            else
            {
                axis.x = Mathf.Clamp(axis.x / 75f, -1f, 1f);
            }
            if (Mathf.Abs(axis.y) < 19)
            {
                axis.y = 0;
            }
            else
            {
                axis.y = Mathf.Clamp(axis.y / 75f, -1f, 1f);
            }
        }
        else
        {
            stickButton.thumbstick.rectTransform.position = new Vector3(stickButton.area.x, stickButton.area.y);
            axis = Vector2.zero;
        }
    }

    private void MobileButtonCheck(Vector2 touchPos, int touchId)
    {
        if (stickButton.area.Contains(new Vector2(touchPos.x, Screen.height - touchPos.y)) && !stickTouch.active)
        {
            stickTouch.active = true;
            stickTouch.fingerId = touchId;
            stickTouch.offset = new Vector2(touchPos.x - stickButton.area.x, Screen.height - touchPos.y - stickButton.area.y);
            stickTouch.position = new Vector2(touchPos.x, Screen.height - touchPos.y);
        }
    }

    private void MobileButtonStop(int touchId)
    {
        if (stickTouch.active && stickTouch.fingerId == touchId)
        {
            stickTouch.active = false;
            stickTouch.offset = Vector2.zero;
            stickTouch.fingerId = -1;
        }
    }
}
