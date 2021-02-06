using UnityEngine;
using System.Collections;
using TMPro;

public class Challenge_Answer_Cue : Visual_Cue
{
    public Challenge_Question_Sign challenge_question_sign; // related question to answer 

    public TextMeshPro answer; // answer text component 

    // set answer 
    public void SetAnswer(string answer, Challenge_Question_Sign question_sign) 
    {
        this.answer.SetText(answer); // set text 
        challenge_question_sign = question_sign; // set related question to answer
    }

    public override void OnPointerEnter()
    {
        base.OnPointerEnter();
    }

    public override void OnPointerExit()
    {
        base.OnPointerExit();
    }

    // when click 
    public override void OnPointerClick()
    {
        base.OnPointerClick();
        challenge_question_sign.SubmitAnswer(answer.text); // submit answer when selected
    }

}
