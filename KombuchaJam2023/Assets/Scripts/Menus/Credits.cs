using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    #region Self-Initiated References

    RectTransform _myRectTransform;
    RectTransform MyRectTransform
    {
        get
        {
            if (_myRectTransform == null)
                _myRectTransform = GetComponent<RectTransform>();

            return _myRectTransform;
        }
    }

    #endregion

    [SerializeField][Range(0, 3000)] float ammountToLower = 2000;
    [SerializeField][Range(0.1f, 30)] float durationOfCredits = 15f;

    float initialYPosition;
    Timer creditsTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        initialYPosition = MyRectTransform.localPosition.y;
        creditsTimer = new Timer(durationOfCredits);
    }

    private void Update()
    {
        if (creditsTimer.IsComplete)
            SceneManager.LoadScene(0);  // Loading the main menu if the credits are finished.

        creditsTimer.Update();

        // If players are pressing the Space key, credits roll 3 TIMES as fast.
        if (Input.GetKey(KeyCode.Space))
            creditsTimer.Update(Time.deltaTime * 2);

        float newYPosition = Mathf.Lerp(initialYPosition, initialYPosition - ammountToLower,
            creditsTimer.PercentageComplete);

        MyRectTransform.localPosition = new Vector3(MyRectTransform.localPosition.x, newYPosition, 0);
    }
}
