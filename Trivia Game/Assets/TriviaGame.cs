using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TriviaGame : MonoBehaviour {

    public struct Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;

        public Question(string questionText, string[] answers, int correctAnswerIndex)
        {
            this.questionText = questionText;
            this.answers = answers;
            this.correctAnswerIndex = correctAnswerIndex;
        }
    }

    private Question currentQuestion = new Question("What is your favorite color?", new string[] {"Blue", "Red", "Yellow", "White", "Black"}, 0);
    public Button[] answerButtons;
    public Text questionText;

    private Question[] questions = new Question[10];
    private int currentQuestionIndex;
	// Use this for initialization
	void Start () {

        questions[0] = new Question("What is the capital of Spain?", new string[] { "Topeka", "Amsterdam", "Madrid", "London", "Toledo" }, 2);
        questions[1] = new Question("Who was the second US President?", new string[] { "George Washington", "John Adams", "Thomas Jefferson", "James Madison", "Andrew Jackson" }, 1);
        questions[2] = new Question("What is the second planet in our solar system?", new string[] { "Earth", "Mars", "Pluto", "Mercury", "Venus" }, 3);
        questions[3] = new Question("What US state has the highest population?", new string[] { "California", "New York", "Texas", "Illinois", "Florida" }, 0);
        questions[4] = new Question("What is the largest continent?", new string[] { "North America", "Europe", "Australia", "Asia", "South America" }, 3);
        questions[5] = new Question("A Platypus is a ____", new string[] { "Bird", "Reptile", "Insect", "Amphibian", "Mammal" }, 5);
        questions[6] = new Question("What is the boiling point in Fahrenheit?", new string[] { "100 degrees", "190 degrees", "300 degrees", "312 degrees", "212 degrees" }, 5);
        questions[7] = new Question("How many degrees are in a circle?", new string[] { "720", "180", "360", "420", "100" }, 2);
        questions[8] = new Question("What is the name of a group of crows?", new string[] { "A bloat", "A murder", "A herd", "A pack", "A team" }, 1);
        questions[9] = new Question("Who created the painting Starry Night?", new string[] { "Van Gogh", "Monet", "Picasso", "da Vinci", "Warhol" }, 0);
        chooseQuestions();
        currentQuestion = questions[currentQuestionIndex];
        assignQuestion();
    }
	// Update is called once per frame
	void Update () {
		
	}
    void assignQuestion (){
         questionText.text = currentQuestion.questionText;
         for (int i = 0; i < answerButtons.Length; i++)
         {
             answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
         }
    }
    public void checkAnswer(int buttonNumber)
    {
        if(buttonNumber == currentQuestion.correctAnswerIndex)
        {
            print("Correct");
        }
        else
        {
            print("Incorrect");
        }
    }
    void chooseQuestions()
    {
        currentQuestionIndex = Random.Range(0, questions.Length);
    }
}
