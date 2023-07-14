using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;

[RequireComponent(typeof(Image))]
public class BlackoutController : MonoBehaviour
{
    #region Static Reference

    public static BlackoutController instance;

    void InitializeStaticReference()
    {
        // Setting up the static reference to this script.
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogError("You can't have 2 BlackoutController scripts. BlackoutController contains a static reference.");
            Destroy(this);
        }
    }

    #endregion


    Image _myImage;
    Image MyImage
    {
        get
        {
            if (_myImage == null)
            {
                if (TryGetComponent<Image>(out Image i))
                    _myImage = i;
                else
                    Debug.LogError("BlackoutController needs to be attached to an object with an Image component in it.");
            }

            return _myImage;
        }
    }


    private void Awake()
    {
        InitializeStaticReference();
    }


    Coroutine activeCoroutine;

    IEnumerator BlackOutTransition
        (float fadeInAnimDuration, float middleWaitDuration, float fadeOutAnimDuration)
    {
        // SECURITY CHECKS
        if (fadeInAnimDuration <= 0)
        {
            Debug.LogError("Blackout Fade In duration (" + fadeInAnimDuration + ") must be bigger than 0.");
            fadeInAnimDuration = 1;
        }

        if (middleWaitDuration < 0)
        {
            Debug.LogError("Blackout Middle Wait duration (" + middleWaitDuration 
                + ") must be equal or higher than 0.");
            middleWaitDuration = 0;
        }

        if (fadeOutAnimDuration <= 0)
        {
            Debug.LogError("Blackout Fade Out duration (" + fadeOutAnimDuration + ") must be bigger than 0.");
            fadeOutAnimDuration = 1;
        }


        // FADE IN

        Timer fadeInTimer = new Timer(fadeInAnimDuration);

        while (!fadeInTimer.IsComplete)
        {
            fadeInTimer.Update();

            SetBlackoutTransparency(fadeInTimer.PercentageComplete);

            yield return null;
        }


        // MIDDLE WAIT
        if(middleWaitDuration != 0)
        {
            Timer middleWaitTimer = new Timer(middleWaitDuration);

            while (!middleWaitTimer.IsComplete)
            {
                middleWaitTimer.Update();

                yield return null;
            }
        }


        // FADE OUT

        Timer fadeOutTimer = new Timer(fadeOutAnimDuration, fadeOutAnimDuration);

        while (fadeInTimer.Time != 0)
        {
            fadeInTimer.NegativeUpdate();

            SetBlackoutTransparency(fadeInTimer.PercentageComplete);

            yield return null;
        }

        activeCoroutine = null;
    }


    private void SetBlackoutTransparency(float alpha)
    {
        if (alpha < 0 || alpha > 1)
        {
            Debug.LogError("Blackout alpha value needs to be in the Range [0,1].");
            return;
        }

        MyImage.color = new Color(0, 0, 0, alpha);
    }


    #region Public methods

    /// <summary>
    /// Sets the alpha of the Blackout Image. This method overrides and disables active Blackout Animations.
    /// </summary>
    /// <param name="alpha"></param>
    public void ChangeTransparency(float alpha)
    {
        StopBlackoutAnimation();

        SetBlackoutTransparency(alpha);
    }

    /// <summary>
    /// Initiates an animation that fades to black and then fades out.
    /// </summary>
    /// <param name="fadeAnimDuration">Duration of the fade in and fade out animations.</param>
    /// <param name="middleWaitDuration">Duration of the middle wait between fades.</param>
    public void ActivateBlackoutAnimation(float fadeAnimDuration, float middleWaitDuration)
    {
        StopBlackoutAnimation();

        activeCoroutine = 
            StartCoroutine(BlackOutTransition(fadeAnimDuration, middleWaitDuration, fadeAnimDuration));
    }

    /// <summary>
    /// Initiates an animation that fades to black and then fades out.
    /// </summary>
    /// <param name="fadeInAnimDuration">Duration of the fade in animation.</param>
    /// <param name="middleWaitDuration">Duration of the middle wait between fades.</param>
    /// <param name="fadeOutAnimDuration">Duration of the fade out animation.</param>
    public void ActivateBlackoutAnimation
        (float fadeInAnimDuration, float middleWaitDuration, float fadeOutAnimDuration)
    {
        StopBlackoutAnimation();

        activeCoroutine =
            StartCoroutine(BlackOutTransition(fadeInAnimDuration, middleWaitDuration, fadeOutAnimDuration));
    }

    /// <summary>
    /// Stops any blackout animation currently happening and returns the Blackout image to a 0 transparency.
    /// </summary>
    public void StopBlackoutAnimation()
    {
        if (activeCoroutine == null)
            return;

        StopCoroutine(activeCoroutine);
        activeCoroutine = null;
        ChangeTransparency(0);
    }

    #endregion
}
