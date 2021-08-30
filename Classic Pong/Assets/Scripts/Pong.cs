using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pong : MonoBehaviour
{

	public Rigidbody2D player;
	public Rigidbody2D player2;
	public Transform enemy;
	public Transform ball;
	public Transform pause;
	public Transform musicPause;
	public Transform mainCamera;

	public GameObject menuSettings;
	public GameObject ballSetActive;

	public GameObject[] choiceMassive;

	public AudioSource[] musicManager;
	private int trackNum = 0;

	public int startBallSpeed;
	public float enemySpeed;

	public Text scoresText;
	public Text musicText; 

	private int playerScore = 0;
	private int enemyScore = 0;

	bool begin = false;
	bool isPaused = false;
	bool menuOn = false;

	private float VelY;
	private float VelX;

	public Sprite[] spritePause;
	public Sprite[] spriteMusic; 

	private bool ispvp;
	private bool isMusicPause; 

	bool firstSensor = false;
	bool secondSensor = false;

	bool thirdSensor = false;
	bool forthSensor = false;

	public GameObject thirdSensorSetActive;
	public GameObject forthSensorSetActive;
	public GameObject start;

	Vector2 freezPlayer;
	Vector2 freezEnemy;

	float enemySpeed_Difficult_1 = 2.8f;
	float enemySpeed_Difficult_2 = 4.325f;
	float enemySpeed_Difficult_3 = 6.05f;

	float randomSpeed;

	int choice_index;

	public float playerSpeed;

	void Start()
	{
		choice_index = 0;
		randomSpeed = 0;

		enemySpeed = enemySpeed_Difficult_1;
		playerSpeed = enemySpeed;
		startBallSpeed = 500;
		ispvp = false;
		isMusicPause = false;

		menuSettings.SetActive(false);

		thirdSensorSetActive.SetActive(false);
		forthSensorSetActive.SetActive(false);

		foreach(GameObject choice in choiceMassive)
        {
			choice.SetActive(false);
		}

		choiceMassive[choice_index].SetActive(true);

		Reset(0);
		musicManager[trackNum].Play();

		freezPlayer = player.position;
		freezEnemy = player2.position;
		musicText.text = "Smooth Bell";
		scoresText.text = playerScore.ToString() + ":" + enemyScore.ToString();
	}

	void Update()
	{
		enemySpeed -= randomSpeed;
		playerSpeed = enemySpeed;

		enemyReaction();

		scoresText.text = playerScore.ToString() + ":" + enemyScore.ToString();

		if (firstSensor)
		{
			player_Position(1);
			freezPlayer = player.position;
		}
		else
        {
			player.position = freezPlayer;
		}

		if (secondSensor)
		{
			player_Position(-1);
			freezPlayer = player.position;
		}
		else
		{
			player.position = freezPlayer;
		}

		if (thirdSensor)
		{
			player2_Position(1);
			freezEnemy = player2.position;
		}
		else
		{
			player2.position = freezEnemy;
		}

		if (forthSensor)
		{
			player2_Position(-1);
			freezEnemy = player2.position;
		}
		else
		{
			player2.position = freezEnemy;
		}

		if(!menuOn && !begin)
        {
			start.SetActive(true);
		}

		if(menuOn)
        {
			start.SetActive(false);
		}

		if (!ispvp)
		{
			randomSpeed = Random.Range(-0.25f, 0.25f);
			enemySpeed += randomSpeed;
		}
		else
        {
			randomSpeed = 0;
        }
	}

	private void enemyReaction()
	{
		if ((ball.position.x > 0) && !ispvp)
		{
			float Y = Mathf.Lerp(enemy.position.y, ball.position.y, enemySpeed * Time.deltaTime);

			if (Y > 7.3f)
			{
				Y = 7.3f;
			}
			else if (Y < -7.3f)
			{
				Y = -7.3f;
			}

			enemy.position = new Vector2(enemy.position.x, Y);
			freezEnemy = enemy.position;
		}
		else if((ball.position.x < 0) && !ispvp)
        {
			float Y = 0f;

			if(enemy.position.y > 0.1f)
            {
				Y = Mathf.Lerp(enemy.position.y, enemy.position.y - 1.5f, enemySpeed * Time.deltaTime);
			}
			else if (enemy.position.y < -0.1f)
			{
				Y = Mathf.Lerp(enemy.position.y, enemy.position.y + 1.5f, enemySpeed * Time.deltaTime);
			}
			else
            {
				Y = 0;
			}


			enemy.position = new Vector2(enemy.position.x, Y);
			freezEnemy = enemy.position;
		}

		if(!ispvp && !begin)
        {
			enemy.position = new Vector2(enemy.position.x, 0);
			freezEnemy = enemy.position;
			Reset(0);
		}
	}
	

	public void Reset(float x)
	{
		ball.GetComponent<Rigidbody2D>().Sleep();
		enemy.position = new Vector2(enemy.position.x, 0f);
		player.position = new Vector2(player.position.x, 0f);
		freezPlayer = player.position;
		freezEnemy = enemy.position;
		ball.position = new Vector2(0, 0);
		begin = false;

		if (x > 0)
		{
			playerScore++;
		}

		else if (x < 0)
		{
			enemyScore++;
		}
	}

	public void Reset_OnClick()
	{
		Reset(0);
		playerScore = 0;
		enemyScore = 0;
	}

	public void Pause_OnClick()
	{
		if (!isPaused)
		{
			Time.timeScale = 0f;
			isPaused = true;
			pause.GetComponent<Image>().sprite = spritePause[1];
		}
		else if (isPaused)
		{
			Time.timeScale = 1f;
			isPaused = false;
			pause.GetComponent<Image>().sprite = spritePause[0];
		}
	}

	public void Menu_OnClick()
	{
		if (!menuOn)
		{
			menuOn = true;
			isPaused = false;
			Pause_OnClick();
			menuSettings.SetActive(true);
		}
		else if (menuOn)
		{
			menuOn = false;
			isPaused = true;
			Pause_OnClick();
			menuSettings.SetActive(false);
		}
	}

	public void Music_Forward_OnClick() 
	{
		if (isMusicPause)
		{
			Music_Pause_OnClick();
		}

		musicManager[trackNum].Stop();
		trackNum++;

		if (trackNum < 4 && trackNum != 0)
		{
			musicManager[trackNum].Play();
		}
		else
		{
			trackNum = 0;
			musicManager[trackNum].Play();

		}

		switch (trackNum)
		{
			case 0:
				musicText.text = "Smooth Bell";
				break;
			case 1:
				musicText.text = "Acid Smooth";
				break;
			case 2:
				musicText.text = "Baby Smooth";
				break;
			case 3:
				musicText.text = "Synth Mellow";
				break;
			default:
				musicText.text = "?";
				break;
		}
	}

	public void Music_Back_OnClick()
	{
		if (isMusicPause)
		{
			Music_Pause_OnClick();
		}

		musicManager[trackNum].Stop();
		trackNum--;

		if (trackNum > -1 && trackNum != 4)
		{
			musicManager[trackNum].Play();
		}
		else
		{
			trackNum = 3;
			musicManager[trackNum].Play();

		}

		switch (trackNum)
		{
			case 0:
				musicText.text = "Smooth Bell";
				break;
			case 1:
				musicText.text = "Acid Smooth";
				break;
			case 2:
				musicText.text = "Baby Smooth";
				break;
			case 3:
				musicText.text = "Synth Mellow";
				break;
			default:
				musicText.text = "?";
				break;
		}
	}

	public void Music_Pause_OnClick()
    {
		if (isMusicPause)
		{
			musicManager[trackNum].Play();
			isMusicPause = !isMusicPause;
			musicPause.GetComponent<Image>().sprite = spriteMusic[0];
		}
        else
        {
			musicManager[trackNum].Pause();
			isMusicPause = !isMusicPause;
			musicPause.GetComponent<Image>().sprite = spriteMusic[1];
		}
	}

	public void Difficult_1_OnClick()
	{
		enemySpeed = enemySpeed_Difficult_1;
		startBallSpeed = 500;
		
		ispvp = false;
		Reset_OnClick();

		thirdSensorSetActive.SetActive(false);
		forthSensorSetActive.SetActive(false);

		choiceMassive[choice_index].SetActive(false);
		choice_index = 0;
		choiceMassive[choice_index].SetActive(true);
	}

	public void Difficult_2_OnClick()
	{
		enemySpeed = enemySpeed_Difficult_2;
		startBallSpeed = 750;
		
		ispvp = false;
		Reset_OnClick();

		thirdSensorSetActive.SetActive(false);
		forthSensorSetActive.SetActive(false);

		choiceMassive[choice_index].SetActive(false);
		choice_index = 1;
		choiceMassive[choice_index].SetActive(true);
	}

	public void Difficult_3_OnClick()
	{
		enemySpeed = enemySpeed_Difficult_3;
		startBallSpeed = 1125;
		
		ispvp = false;
		Reset_OnClick();

		thirdSensorSetActive.SetActive(false);
		forthSensorSetActive.SetActive(false);

		choiceMassive[choice_index].SetActive(false);
		choice_index = 2;
		choiceMassive[choice_index].SetActive(true);
	}

	public void PvP_OnClick()
	{
		enemySpeed = enemySpeed_Difficult_2;
		startBallSpeed = 1250;
		
		playerScore = 0;
		enemyScore = 0;
		ispvp = true;
		Reset_OnClick();

		thirdSensorSetActive.SetActive(true);
		forthSensorSetActive.SetActive(true);

		choiceMassive[choice_index].SetActive(false);
		choice_index = 3;
		choiceMassive[choice_index].SetActive(true);
	}

	public void Menu_Return_OnClick()
	{
		if (menuOn)
		{
			menuOn = false;
			Pause_OnClick();
			menuSettings.SetActive(false);
		}
	}

	public void Start_OnClick()
    {
		if(!begin)
		{
			ball.GetComponent<Rigidbody2D>().WakeUp();

			float r = Random.Range(-0.2f, 0.2f); ;

			VelY = Random.Range(-1f, 1f);
			if (VelY > 0)
			{
				VelY = 1;
			}
            else
            {
				VelY = -1;
			}

			VelX = Random.Range(-1f, 1f);
			if (VelX > 0)
			{
				VelX = -1;
			}
			else
			{
				VelX = 1;
			}

			Vector2 direction = new Vector2(VelX, VelY - r);

			ball.GetComponent<Rigidbody2D>().AddForce(direction * startBallSpeed);
			begin = true;

			start.SetActive(false);
		}
	}

	public void firstSensorUp() 
	{
		firstSensor = false;
	}

	public void firstSensorDown() 
	{
		firstSensor = true;
	}

	public void secondSensorUp()
	{
		secondSensor = false;
	}

	public void secondSensorDown()
	{
		secondSensor = true;
	}
	
	public void thirdSensorUp()
	{
		thirdSensor = false;
	}

	public void thirdSensorDown()
	{
		thirdSensor = true;
	}

	public void forthSensorUp()
	{
		forthSensor = false;
	}

	public void forthSensorDown()
	{
		forthSensor = true;
	}

	public void player_Position(float i)
    {
		i *= 0.5f;
		float Y = Mathf.Lerp(player.position.y, player.position.y + i, playerSpeed * Time.deltaTime * 10f);

		if (Y > 7.3f)
		{
			Y = 7.3f;
		}
		else if (Y < -7.3f)
		{
			Y = -7.3f;
		}

		player.position = new Vector2(player.position.x, Y);
		freezPlayer = player.position;
	}

	public void player2_Position(float i)
	{
		i *= 0.5f;
		float Y = Mathf.Lerp(player2.position.y, player2.position.y + i, playerSpeed * Time.deltaTime * 10f);

		if (Y > 7.3f)
		{
			Y = 7.3f;
		}
		else if (Y < -7.3f)
		{
			Y = -7.3f;
		}

		player2.position = new Vector2(player2.position.x, Y);
		freezEnemy = player2.position;
	}
}
