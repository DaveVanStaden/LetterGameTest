using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class WordAlgorithm : MonoBehaviour
{
    [SerializeField] private List<string> wordList;
    public char[] WordCharacter;
    public string word;
    [SerializeField] private GameObject letterBox;

    [SerializeField] private Text WordText;

    [SerializeField] private Text letterText;

    [SerializeField] private Vector2[] SpawnPoints;
    [SerializeField] private List<GameObject> letterObjects;
    public List<GameObject> letterGuesser;
    private GameObject tempGameObject;
    private Rigidbody2D letterRigidbody;
    private float spawnTimerLimit = 1f;
    private float spawnTimer;
    private Transform LetterInObject;
    private CharacterScript charScript;
    // Start is called before the first frame update
    void Start()
    {
        GenerateAWord();
        SpawnLetter();
        
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine("timer");
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTimerLimit)
        {
            SpawnPlayletter();
            spawnTimer = 0;
        }
        
        //WordText.text = word;
        //WordText.text = tasd.ToString();


        print(word.Length);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateAWord();
        }

    }
    private void GenerateAWord()
    {
        int wordNumber = Random.Range(0, wordList.Count);
        word = wordList[wordNumber];
        print(word);

        WordText.text = "";
        BlankLetters();

    }
    private void BlankLetters()
    {
        WordCharacter = new char[word.Length];

        for (int i = 0; i < WordCharacter.Length; i++)
        {
            //print("Dit is dus de Incriment " + i);
            //print("Dit is het ding voordat de dingus gebeurt " + WordCharacter[i]);
            WordCharacter[i] = '_';
            //print("Dit is het ding nadat de dingus gebeurt " + WordCharacter[i]);
            //print(WordCharacter[i] + " ");
            WordText.text += WordCharacter[i].ToString() + " ";
        }
    }

    private void SpawnLetter()
    {
        for (int i = 0; i < word.Length; i++)
        {
            var letterbox = Instantiate(letterBox, new Vector2(-3+i,4), Quaternion.identity);
            letterGuesser.Add(letterbox);
            var letterFromWord = Instantiate(letterText, letterbox.transform.position, Quaternion.identity);
            letterFromWord.text = word[i].ToString();
            letterFromWord.transform.SetParent(letterbox.transform);
            letterFromWord.transform.gameObject.SetActive(false); 

            letterbox.transform.SetParent(gameObject.transform);

            letterbox.GetComponent<CharacterScript>().Character = word[i].ToString();
        }
        
    }
    
    private void SpawnPlayletter()
    {
        int speed = 4;
        int range = Random.Range(0, SpawnPoints.Length);
        
        //var letterFromWord = Instantiate(letterText, playletter.transform.position, Quaternion.identity);
        int wordletterorfake = Random.Range(0, 9);
        if(wordletterorfake <= 2)
        {
            tempGameObject = Instantiate(letterObjects[Random.Range(0, letterObjects.Count)], SpawnPoints[range], Quaternion.identity);
            //GameObject temp = letterObjects.Where(obj => obj.name == "A").SingleOrDefault();
                if (word.ToUpper().Contains(tempGameObject.name))
                {
                    for (int j = 0; j < letterGuesser.Count; j++)
                    {
                        charScript = letterGuesser[j].GetComponent<CharacterScript>();
                        if(charScript.Character.ToUpper() == tempGameObject.name)
                        {
                            LetterInObject = letterGuesser[j].transform.GetChild(0);
                            LetterInObject.transform.gameObject.SetActive(true);
                        }
                    }

                    print("inWord");
                }
        }

        if (wordletterorfake > 2)
        {
            int letternumber = Random.Range(0, letterObjects.Count);
            tempGameObject = Instantiate(letterObjects[letternumber], SpawnPoints[range], Quaternion.identity);
            print("outWord");
        }
            
        //letterFromWord.text = word[].ToString();
        
        /*letterRigidbody = tempGameObject.GetComponent<Rigidbody2D>();
        if (range == 0)
        {
            letterRigidbody.velocity = new Vector2(-1, 0) * speed;
            //tempGameObject.GetComponent<Rigidbody2D>().velocity = transform.GetComponent<Rigidbody2D>().velocity;
        }
        else if (range == 1)
        {
            letterRigidbody.velocity = new Vector2(0, 1) * -speed;
        }
        else if (range == 2)
        {
            letterRigidbody.velocity = new Vector2(1, 0) * -speed;
        }*/
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(10);
        SpawnPlayletter();
    }
}   