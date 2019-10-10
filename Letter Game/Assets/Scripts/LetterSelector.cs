using System.Collections.Generic;
using System.Linq;
using TMPro;
using TritraNet;
using UnityEngine;
using UnityInput = UnityEngine.Input;
using UnityEngine.UI;


public class LetterSelector : MonoBehaviour
{
    private Recognizer recognizer;
    [SerializeField] private List<double> apexAngles;
    private LetterTextSelector textSelector;
    public string SelectedLetter;

    private void Awake()
    {
        var packageOptions = new RecognizerOptions()
        {
            maxAngleTolerance = 10,
            maxPointDistance = 400,
        };
        this.recognizer = new Recognizer(apexAngles, packageOptions);
    }

    void Update()
    {
        CalculateAngles(UnityInput.touches);
    }

    private void CalculateAngles(Touch[] touches)
    {
        var vecs = touches.Select(p => new Vector2d(p.position.x, p.position.y)).ToList();
        var foundMatches = this.recognizer.FindMatches(vecs);

        if (foundMatches.Count > 0)
        {
            var center = foundMatches[0].GetCenter();
            /* For each match, give the new object:
                - its corresponding position (which depends on the center of the triangle)
                - its corresponding rotation (which depends on the orientation of the triangle) */
            var pos = Camera.main.ScreenToWorldPoint(new Vector2((float)center.x, (float)center.y));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(pos.x, pos.y));

            //print(foundMatches[0].GetApexAngle().ToString());
            //print(foundMatches[0].GetOrientation());

            if (hit.collider != null)
            {
                if (hit.transform.tag == "vowels" && foundMatches[0].MatchedAngle == apexAngles[0])
                {
                    for (int i = 0; i < GetComponent<WordAlgorithm>().word.Length; i++)
                    {
                        textSelector = hit.transform.gameObject.GetComponent<LetterTextSelector>();
                        SelectedLetter = textSelector.ObjectLetter;
                    }
                }

                if (hit.transform.tag == "consonants" && foundMatches[0].MatchedAngle == apexAngles[1])
                {
                    textSelector = hit.transform.gameObject.GetComponent<LetterTextSelector>();
                    SelectedLetter = textSelector.ObjectLetter;
                }
            }
        }
    }
}