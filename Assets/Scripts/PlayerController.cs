using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public TextMesh answerA;
    public TextMesh answerB;
    public GameObject doorA;
    public GameObject doorB;
    public TextMesh infoText;

    private Rigidbody rb;
    private bool isOver;
    private GameObject obstacle;
    private GameObject ground;
    

    private int count;
    private float wallSpeed;

    void Start ()
    {   
        //Initial Question
        setQuestion("Are you ready?", "Yes", "No");
        setAnswer("A");

        wallSpeed = -0.075f;
        rb = GetComponent<Rigidbody>();
        count = 0;
        isOver = false;
        SetCountText();
        winText.text = "";
        obstacle = GameObject.Find("Obstacle");
        ground = GameObject.Find("Ground");
        // The below code is included so that the obstacle won't get stuck on the ground.
        Physics.IgnoreCollision(ground.GetComponent<Collider>(), obstacle.GetComponent<Collider>()); 
    }


    void FixedUpdate()
    {
        float ballMoveHorizontal = Input.GetAxis("Horizontal");
        // float ballMoveVertical = Input.GetAxis("Vertical");
    
        Vector3 ballMovement = new Vector3(ballMoveHorizontal, 0.0f, 0.0f);
        Vector3 wallMovement = new Vector3(0.0f, 0.0f, wallSpeed);

        // Player controls the movement
        rb.AddForce(ballMovement * speed);

        // Move the obstacle toward the player
        obstacle.transform.position += wallMovement;

        if (rb.transform.position.y < -1) // Check if player loses
        {
            winText.text = "You Lose!";
            isOver = true;
        }
        if (obstacle.transform.position.z < -13.7)
        {
            if (!isOver){ // Player did not lose
                obstacle.transform.position = new Vector3(0.0f, 2.6f, 19.4f);
                switch(count)
                {
                    case 0:
                        setQuestion("1 + 1 = ?", "1", "2");
                        setAnswer("B");
                        break;
                    case 1:
                        answerA.text = "289";
                        answerB.text = "306";
                        infoText.text = "17 X 17 = ?";
                        setAnswer("A");
                        break;
                    case 2:
                        answerA.text = "4950";
                        answerB.text = "5050";
                        infoText.text = "1 + ... + 100 = ?";
                        setAnswer("B");
                        break;
                    case 3:
                        setQuestion("d/dx ln(x^2) = ?", "2/x", "4/x");
                        setAnswer("A");
                        break;
                    case 4:
                        setQuestion("∫5x dx", "(1/6)x^6", "5x^6");
                        answerA.fontSize = 70;
                        setAnswer("A");
                        break;
                    case 5:
                        setQuestion("Limit of (x^2-4)/(x-2) at 0", "2", "4");
                        wallSpeed = -0.025f;
                        answerA.fontSize = 70;
                        setAnswer("B");
                        break;
                    case 6:
                        setQuestion("0, 1, 1, 2, 3, 5, 8, ?", "13", "11");
                        wallSpeed = -0.075f;
                        setAnswer("A");
                        break;
                    case 7:
                        setQuestion("Speed of light?", "3 X 10^8 m/s", "3 X 10^7 m/s");
                        wallSpeed = -0.075f;
                        answerA.fontSize = 50;
                        answerB.fontSize = 50;
                        setAnswer("A");
                        break;
                    case 8:
                        setQuestion("Do you love CS?", "No", "Yes");
                        wallSpeed = -0.075f;
                        answerA.fontSize = 160;
                        answerB.fontSize = 160;
                        setAnswer("B");
                        break;
                    default:
                        break;
                }
                count ++;
                if (count >= 8 + 2){
                    winText.text = "You Win!";
                    isOver = true;
                }
                SetCountText();
            }
        }
    }
    void setAnswer(string answer)
    {
        if (Equals(answer, "A")){
        doorA.GetComponent<Collider>().enabled = false;
        doorB.GetComponent<Collider>().enabled = true;
        }
        else if (Equals(answer, "B")){
        doorA.GetComponent<Collider>().enabled = true;
        doorB.GetComponent<Collider>().enabled = false;
        }
    }

    void setQuestion(string question, string a, string b)
    {
        answerA.text = a;
        answerB.text = b;
        infoText.text = question;
    }


    void OnTriggerEnter(Collider other)
    {
    }

    void SetCountText()
    {
        countText.text = "Level: " + count.ToString();

    }
}
