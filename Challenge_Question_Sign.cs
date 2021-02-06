using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Challenge_Question_Sign : MonoBehaviour
{
    public TextMeshPro question_text; // questions text 
    public GameObject answer_prefab; // answer prefab
    public GameObject anchor; // place to spawn answers to 

    public Challenge_Cue associated_object; // cue it relates to 
    private List<Challenge_Questions> challenge_questions; // list of questions 
    private int score = 0; // grade or score 
    private int current_index = 0; // current question 

    // fires at start 
    public void Initialize(Challenge_Cue associated, List<Challenge_Questions> questions) 
    {
        associated_object = associated; // link related cue 
        challenge_questions = questions; // set question list 
    
        SetQuestion(); // setup question
    }

    // sets question up for player 
    private void SetQuestion() 
    {
        question_text.SetText(challenge_questions[current_index].question); // set current question

        // checks of there is any available questions 
        if (challenge_questions[current_index].answers.Count > 0)
        {
            // if no answers are currently displayed
            if (anchor.transform.childCount == 0)
            {
                SpawnAnswers(); // spawn answers 
            }

            // if ansers are currently displayed 
            else
            {
                // cycle through displayed answers 
                for (int i = 0; i < anchor.transform.childCount; i++)
                {
                    //Destroy(anchor.transform.GetChild(i).gameObject); // destory or remove answers 
                    anchor.transform.GetChild(i).gameObject.SetActive(false);
                }

                SpawnAnswers(); // spawn new answers 
            }
        }

        // if no answers are available for this question 
        else 
        {
            GameObject current_answer_prefab;
            current_answer_prefab = Instantiate(answer_prefab, anchor.transform); // clone prefab
            current_answer_prefab.transform.parent = anchor.transform; // parent up with anchor 
            current_answer_prefab.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);// set location to anchor with offset, x,y,z are relevant to parent.
            current_answer_prefab.GetComponent<Challenge_Answer_Cue>().SetAnswer("Ok", this); // create OK button
        }

    }

    // spawns answers 
    private void SpawnAnswers() 
    {
        
        float offset = 0f; // offsets answers 
        GameObject current_answer_prefab; // current answer 
        
        // cycler through answers 
        foreach (string answer in challenge_questions[current_index].answers)
        {
            current_answer_prefab = Instantiate(answer_prefab, anchor.transform); // clone prefab 
            current_answer_prefab.transform.parent = anchor.transform; // parent up with anchor 
            current_answer_prefab.transform.localPosition = new Vector3(0.0f,0.0f,offset);// set location to anchor with offset, x,y,z are relevant to parent. 
            current_answer_prefab.GetComponent<Challenge_Answer_Cue>().SetAnswer(answer, this); // set current answers 
            offset += -12f; // adjust offset 
        }
    }
    // submit answer 
    public void SubmitAnswer(string answer) 
    {
        // if there is a correct answer available 
        if (challenge_questions[current_index].correct_answer != "")
        {
            // if answer is correct 
            if (challenge_questions[current_index].correct_answer == answer)
            {
                this.score += 1; // score point 
            }

            // of all questions have been answers 
            if (challenge_questions.Count == current_index + 1)
            {
                float score = this.score / challenge_questions.Count; // grade 
                associated_object.SubmitScore(score); // submit score 
                Trash();
                //Destroy(this.transform.parent.gameObject); // destory self 
            }

            // if more questions available 
            else
            {
                this.current_index += 1; // point to next question
                SetQuestion(); // setup new question 
            }
        }

        // if no correct answer is available 
        else 
        {
            associated_object.EndTask(); // end task 
            Trash();
            //Destroy(this.transform.parent.gameObject); // destroy self
        }
    }

    private void Trash() 
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject;
        this.transform.parent.position = gameManager.transform.position;
        this.transform.parent.gameObject.transform.parent = gameManager.transform;
    }
}
