using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TriviaGame : MonoBehaviour {

    //This is the struct that sets up what our question values are going to be.
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
    private int[] questionNumbersChosen = new int[5];
    private int questionsFinished;

    public GameObject[] TriviaPanels;
    public GameObject finalResultsPanel;
    public Text resultsText;
    private int numberOfCorrectAnswers;
    private bool allowSelection = true;
    public GameObject feedbackText;

	// Use this for initialization
	void Start () {

        for(int i = 0; i < questionNumbersChosen.Length; i++)
        {
            questionNumbersChosen[i] = -1;
        }

        questions[0] = new Question("What is the capital of Spain?", new string[] { "Topeka", "Amsterdam", "Madrid", "London", "Toledo" }, 2);
        questions[1] = new Question("Who was the second US President?", new string[] { "George Washington", "John Adams", "Thomas Jefferson", "James Madison", "Andrew Jackson" }, 1);
        questions[2] = new Question("What is the second planet in our solar system?", new string[] { "Earth", "Mars", "Pluto", "Mercury", "Venus" }, 4);
        questions[3] = new Question("What US state has the highest population?", new string[] { "California", "New York", "Texas", "Illinois", "Florida" }, 0);
        questions[4] = new Question("What is the largest continent?", new string[] { "North America", "Europe", "Australia", "Asia", "South America" }, 3);
        questions[5] = new Question("A Platypus is a ____", new string[] { "Bird", "Reptile", "Insect", "Amphibian", "Mammal" }, 4);
        questions[6] = new Question("What is the boiling point in Fahrenheit?", new string[] { "100 degrees", "190 degrees", "300 degrees", "312 degrees", "212 degrees" }, 4);
        questions[7] = new Question("How many degrees are in a circle?", new string[] { "720", "180", "360", "420", "100" }, 2);
        questions[8] = new Question("What is the name of a group of crows?", new string[] { "A bloat", "A murder", "A herd", "A pack", "A team" }, 1);
        questions[9] = new Question("Who created the painting Starry Night?", new string[] { "Van Gogh", "Monet", "Picasso", "da Vinci", "Warhol" }, 0);
        chooseQuestions();
        assignQuestion(questionNumbersChosen[0]);
    }
	// Update is called once per frame
	void Update () {
        quitGame();
    }

    //Setting up interface to show a question
    void assignQuestion (int questionNum){
        currentQuestion = questions[questionNum];
        questionText.text = currentQuestion.questionText;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
        }
    }

    //Give feedback for question answered
    //moving onto next question after a pause
    public void checkAnswer(int buttonNumber)
    {
        if (allowSelection)
        {
            if (buttonNumber == currentQuestion.correctAnswerIndex)
            {
                print("Correct");
                numberOfCorrectAnswers++;
                feedbackText.GetComponent<Text>().text = "Correct!";
                feedbackText.GetComponent<Text>().color = Color.green;
            }
            else
            {
                print("Incorrect");
                feedbackText.GetComponent<Text>().text = "Incorrect!";
                feedbackText.GetComponent<Text>().color = Color.red;
            }
            StartCoroutine("continueAfterFeedback");
        }


    }

    //Choosing the question numbers for the trivia game
    void chooseQuestions()
    {
        for (int i =0; i < questionNumbersChosen.Length; i++)
        {
            int questionNum = Random.Range(0, questions.Length);
            if(numberNotContained(questionNumbersChosen, questionNum))
            {
                questionNumbersChosen[i] = questionNum;
            }
            else
            {
                i--;
            }
        }

        currentQuestionIndex = Random.Range(0, questions.Length);
    }

    //Checking to see if the random number chosen has already been chosen
    bool numberNotContained(int[] numbers, int num)
    {
        for(int i = 0; i < numbers.Length; i++)
        {
            if(num == numbers[i])
            {
                return false;
            }
        }
        return true;
    }

    //Assigns new question using the next question number
    public void moveToNextQuestion()
    {
        assignQuestion(questionNumbersChosen[questionNumbersChosen.Length - 1] - questionsFinished);

    }

    //Setting the results text to the appropriate text depending on # of correct responses
    void displayResults()
    {
        switch (numberOfCorrectAnswers)
        {
            case 5:
                resultsText.text = "5 out of 5 correct. Perfect score!!";
                break;
            case 4:
                resultsText.text = "4 out of 5 correct. Almost had it!";
                break;
            case 3:
                resultsText.text = "3 out of 5 correct. Well done!";
                break;
            case 2:
                resultsText.text = "2 out of 5 correct. You can do better next time!";
                break;
            case 1:
                resultsText.text = "1 out of 5 correct. You need to practice more.";
                break;
            case 0:
                resultsText.text = "0 out of 5 correct. Are you even trying?";
                break;
            default:
                print("Not a correct number");
                break;
        }
    }

    //Restarts level
    public void restartLevel()
    {
        Application.LoadLevel(Application.loadedLevelName);

    }

    //Coroutine that pauses and then moves onto next question
    IEnumerator continueAfterFeedback()
    {
        allowSelection = false;
        feedbackText.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        if (questionsFinished < questionNumbersChosen.Length - 1)
        {
            moveToNextQuestion();
            questionsFinished++;
        }
        else
        {
            foreach (GameObject p in TriviaPanels)
            {
                p.SetActive(false);
            }
            finalResultsPanel.SetActive(true);
            displayResults();
        }
        allowSelection = true;
        feedbackText.SetActive(false);
    }

    //Checks for input to quit game
    void quitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
