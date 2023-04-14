using UnityEngine;
using System.Collections;


//Script populates and animates red blood cell prefabs on the main menu (home screen)
public class HomeScreen : MonoBehaviour
{

    private bool spawningCells;
    private int delay;
    public Transform cell1;
    public Transform cell2;
    public Transform cell3;
    public Transform cell4;
  
    public Transform cell6;
    public Vector3 cellPos = new Vector3(0, 0, 0);
    public GameObject infoBtn;
    public GameObject playBtn;

    void Start()
    {
        Time.timeScale = 1;
        delay = 0;
        spawningCells = true;
        playBtn.SetActive(false);
        infoBtn.SetActive(false);
    }

    void FixedUpdate()
    {
        //The followin block of code instantiates cell prefabs approximately 1 every 0.5 seconds
        //A 0.5 second delay is added to give time for the Unity screen to execute at startup
        //Without the delay, the cells spawn prior to displaying the home screen;
        //Aleksandr Gryzlov: deleted cell 5 spawn and blink because main menu is overloaded and some spries overlaps on each other
        //moved cell 2,3,4 to stop them from overlapsing
        if(delay++ < 128 && spawningCells)
        {
            switch(delay)
            {
                case 38:
                    PopupCell(cell2, 9.0f, -33.0f, 44f);
                    break;
                case 51:
                    PopupCell(cell3, 54.0f, 28.8f, 46f);
                    break;
                case 64:
                    PopupCell(cell4, 18.0f, 8.50f, 48f);
                    break;
             
                case 128:
                    playBtn.SetActive(true);
                    infoBtn.SetActive(true);
                    break;
            }
        }
        else
        {   //set spawning to false and begin animation
            spawningCells = false;
            if(delay++ > 100)//Blink every two seconds
            {
                delay = 0;
                Blink();
            }
        }
    }

    public void PopupCell(Transform cell, float x, float y, float z)
    {
        AudioSource pop = GetComponent<AudioSource>();

        Instantiate(cell, new Vector3(x, y, z), transform.rotation = Quaternion.identity);
        pop.Play();
    }

    public void Blink()
    {   //If understood correctly, the random number generator generates a float
        //number between a min and max inclusive.  Typecasting the float to int
        //results in a less than desirable probability the random value will equal
        // the maximum value.  Therefore, maximum of 4, therefore a random value
        // between 1 and 5 (inclusive) is used.

        int cell = (int)Random.Range(1f, 5f);

        cell6.localScale = new Vector3(5, 5, 0);
        cellPos = new Vector3(0, 0, 0);

        switch(cell)
        {
            case 1: //largest cell in the lower right corner of the home screen
                cellPos = new Vector3(52.1f, -26.6f, 39);
                cell6.localScale = new Vector3(11.57F, 11.57f, 0);//scale blinking cell to match the larger cell1
                break;
            case 2:
                cellPos = new Vector3(9.0f, -33.0f, 43f);
                break;
            case 3:
                cellPos = new Vector3(54.0f, 28.8f, 45f);
                break;
            case 4:
                cellPos = new Vector3(18.0f, 8.50f, 47f);
                break;
            default:                
                break;
        }
        Transform cloneCell = Instantiate(cell6, cellPos, cell6.rotation = Quaternion.identity) as Transform;
        StartCoroutine(destroyBlinkCell(cloneCell));
    }

    public IEnumerator destroyBlinkCell(Transform cloneCell)
    {
        //Destroys the instantiated blinking cell giving the cells
        //the illusion of blinking.
        yield return new WaitForSeconds(0.1f);
        if(cloneCell != null)
            Destroy(cloneCell.gameObject);
    }
}
