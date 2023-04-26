using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class SimulationMenu : MonoBehaviour
{
    [SerializeField] private Image ppImg; // reference to play-pause image
    [SerializeField] private Sprite playSprite, pauseSprite; // store play-pause sprites
    int switchPP; // switch for play|pause

    [SerializeField] private TMP_Text timeXText; // reference to time text
    private int timeX; // timescale state


    [Header("Events")] // add event in inspector
    public GameEvent conveyorBelt_event; // event for conveyorBelt broadcast

    public TMP_Text timeText; // holds the text object
    float timeStart; // stores "start time"
    float currentTime; // stores current sim time
    int restart_switch; // asdf
    int minuteCount; // count minutes
    int hourCount; // count h ours


    void Start()
    {
        ppImg.sprite = playSprite; // start in pause mode, showing play button
        switchPP = 0; // play-pause state, start in pause mode

        Time.timeScale = 0f; // start paused (timescale = 0)
        restart_switch = 1; // scene has been loaded, for distinction between reset and pause

        timeText.text = "00:00:00"; // initial time
        minuteCount = 0; // minute count
        hourCount = 0; // hour count

        timeX = 0; // start in 1x mode
        timeXText.text = "1x";

    }

    void FixedUpdate()
    {
        string simSeconds = "00"; // local time counts
        string simMinutes = "00:";
        string simHours = "00:";

        currentTime = Time.time - timeStart; // local time - local start
        
        Debug.Log("current time:" + Mathf.Round(currentTime));

        if (currentTime >= 60) // for minutes
        {
            currentTime = currentTime - 60;
            timeStart += 60;
            minuteCount += 1;
        }
        if (minuteCount >= 60) // for hours
        {
            minuteCount -= 60;
            hourCount += 1;
        }

        if (currentTime > 9) // double digits
        {
            simSeconds = Mathf.Round(currentTime).ToString();
        } 
        else
        {
            simSeconds = "0" + Mathf.Round(currentTime).ToString();
        }
        if (minuteCount > 9)
        {
            simMinutes = minuteCount.ToString() + ":";
        }
        else
        {
            simMinutes = "0" + minuteCount.ToString() + ":";
        }
        if (hourCount > 9)
        {
            simHours = hourCount.ToString() + ":";
        }
        else
        {
            simHours = "0" + hourCount.ToString() + ":";
        }

        timeText.text = simHours + simMinutes + simSeconds; // update current time text
    }

    public void Pause() // pause function (inactive)
    {
        //PauseMenuPanel.SetActive()
        ppImg.sprite = playSprite;
        Time.timeScale = 0f;
    }
    public void Play() // play function
    {
        if (timeX == 2) // keep track of current selected time scale
        {
            Time.timeScale = 3f;
        }
        else if (timeX == 1)
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        
        if (restart_switch == 1) // if restart, then play initializes local start time
        {
            timeStart = Time.time;
            restart_switch = 0;
        }
        ppImg.sprite = pauseSprite;
    }

    public void PlayPause() // play pause logic
    {
        if (switchPP == 0)
        {
            Play();
            switchPP = 1;
        } 
        else
        {
            Pause();
            switchPP = 0;
        }
    }

    public void changeTimeX() // change time scale (state-machine)
    {
        if (Time.timeScale != 0f)
        {
            // states
            if (timeX == 0)
            {
                Time.timeScale = 2f;
                timeX = 1;
                timeXText.text = "2x";
            } else if (timeX == 1)
            {
                Time.timeScale = 3f;
                timeX = 2;
                timeXText.text = "3x";

            }
            else
            {
                Time.timeScale = 1f;
                timeX = 0;
                timeXText.text = "1x";

            }
        }
    }

    public void Restart() // restart button
    {
        // set time scale; otherwise some unknwon bugs can occur in regards to current time text
        Time.timeScale = 0f; // void start triggers after .LoadScene so no need to set time scale
        SceneManager.LoadScene("SimulationEnvironment");
    }

    public void conveyorBelt_button() // raise conveyorBelt event for communication to conveyorBelts
    {
        conveyorBelt_event.Raise();
    }

    public void toMainMenu(string sceneName) // go back to main menu
    {
        SceneManager.LoadScene(sceneName);
    }
}
