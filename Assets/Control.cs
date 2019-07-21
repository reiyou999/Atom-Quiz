using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control:MonoBehaviour {
    Text quizText;
    Button answerButton;
    GameObject answer;
    Button nextButton;
    //마지막 배열칸들은 모두 새로 넣을 용도

    private Vector2Int randNum = Vector2Int.zero;
    private Vector2Int[] record = new Vector2Int[4];

    public string[] quizType = new string[] {
        "의 원자반지름은 얼마인가",
        "의 이온반지름은 얼마인가",
        "의 제1이온화에너지는 얼마인가"
    };

    public enum atomNames {
        Li, Be, B, O, F,
        Na, Mg, Al, S, Cl
    }

    public string[] ionNames = new string[] {
        "Li⁺", "Be²⁺", "B³⁺", "O²⁻", "F⁻",
        "Na⁺", "Mg²⁺", "Al³⁺", "S²⁻", "Cl⁻",
    };

    public enum specialAtoms {
        Na, Mg, Al, F, O,
    }

    public Dictionary<int, int> atomAnswer = new Dictionary<int, int>();
    public Dictionary<string, int> ionAnswer = new Dictionary<string, int>();
    public Dictionary<int, int> ionizeAnswer = new Dictionary<int, int>();


    // Start is called before the first frame update
    void Start()
    {
        quizText = GameObject.Find("QuizText").GetComponent<Text>();
        answerButton = GameObject.Find("AnswerButton").GetComponent<Button>();
        answer = GameObject.Find("Answer");
        nextButton = GameObject.Find("NextButton").GetComponent<Button>();

        answerButton.onClick.AddListener(ShowAnswer);
        nextButton.onClick.AddListener(NewLevel);

        record[0] = Vector2Int.zero;
        record[1] = Vector2Int.zero;
        record[2] = Vector2Int.zero;
        record[3] = Vector2Int.zero;

        AddDictonaryContent();
        NewLevel();
    }

    private void ShowAnswer() {
        answer.SetActive(true);
    }

    private void AddDictonaryContent() {
        atomAnswer.Add((int)atomNames.Li, 152);
        atomAnswer.Add((int)atomNames.Be, 112);
        atomAnswer.Add((int)atomNames.B, 87);
        atomAnswer.Add((int)atomNames.O, 73);
        atomAnswer.Add((int)atomNames.F, 71);
        atomAnswer.Add((int)atomNames.Na, 186);
        atomAnswer.Add((int)atomNames.Mg, 160);
        atomAnswer.Add((int)atomNames.Al, 143);
        atomAnswer.Add((int)atomNames.S, 103);
        atomAnswer.Add((int)atomNames.Cl, 99);

        ionAnswer.Add(ionNames[0], 60);
        ionAnswer.Add(ionNames[1], 31);
        ionAnswer.Add(ionNames[2], 20);
        ionAnswer.Add(ionNames[3], 140);
        ionAnswer.Add(ionNames[4], 136);
        ionAnswer.Add(ionNames[5], 95);
        ionAnswer.Add(ionNames[6], 65);
        ionAnswer.Add(ionNames[7], 50);
        ionAnswer.Add(ionNames[8], 184);
        ionAnswer.Add(ionNames[9], 181);

        ionizeAnswer.Add((int)specialAtoms.O, 1314);
        ionizeAnswer.Add((int)specialAtoms.F, 1681);
        ionizeAnswer.Add((int)specialAtoms.Na, 496);
        ionizeAnswer.Add((int)specialAtoms.Mg, 738);
        ionizeAnswer.Add((int)specialAtoms.Al, 578);
    }

    private void NewLevel() {
        bool flag = true;
        int i;

        record[0] = record[1];
        record[1] = record[2];
        record[2] = record[3];
        record[3] = randNum;

        Vector2Int newRandom = new Vector2Int();
        while (flag) {
            flag = false;
            newRandom.x = Random.Range(0, 3);
            int max;
            switch (newRandom.x) {
                case 0:
                    max = atomAnswer.Count;
                    break;
                case 1:
                    max = ionAnswer.Count;
                    break;
                case 2:
                    max = ionizeAnswer.Count;
                    break;
                default:
                    max = 1;
                    break;
            }
            newRandom.y = Random.Range(0, max);
            if (record[0] == newRandom || record[1] == newRandom || record[2] == newRandom || record[3] == newRandom) flag = true;
        }

        answer.SetActive(false);
        int value;
        switch (newRandom.x) {
            case 0:
                quizText.text = ((atomNames)newRandom.y).ToString() + quizType[newRandom.x];
                atomAnswer.TryGetValue(newRandom.y, out value);
                answer.GetComponent<Text>().text = value + "pm";
                break;
            case 1:
                quizText.text = (ionNames[newRandom.y]) + quizType[newRandom.x];
                ionAnswer.TryGetValue(ionNames[newRandom.y], out value);
                answer.GetComponent<Text>().text = value + "pm";
                break;
            case 2:
                quizText.text = ((specialAtoms)newRandom.y).ToString() + quizType[newRandom.x];
                ionizeAnswer.TryGetValue(newRandom.y, out value);
                answer.GetComponent<Text>().text = value + "kJ/mol";
                break;
        }

    }
}
