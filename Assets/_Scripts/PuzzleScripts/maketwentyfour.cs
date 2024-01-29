using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class maketwentyfour : MonoBehaviour
{
    public int[] num;
    public TMP_Text one, two, three, four, displayField, result;
    public Button b1, b2, b3, b4, reset, add, sub, mult, div, regenerate;
    public bool isSolved = false;
    public int rewardAmt = 10;

    public RoomGeneration roomGeneration;

    public TreasureManager tm;
    
    // Start is called before the first frame update
    void Start()
    {
        bool check = false;
        while (check != true)
        {
            num = GenerateProblem();
            if (CanReach24(num))
            {
                check = true;
            }
        }

        roomGeneration = FindAnyObjectByType<RoomGeneration>();

        one.text = num[0].ToString();
        two.text = num[1].ToString();
        three.text = num[2].ToString();
        four.text = num[3].ToString();
        displayField.text = "";
        b1.onClick.AddListener(ButtonOne);
        b2.onClick.AddListener(ButtonTwo);
        b3.onClick.AddListener(ButtonThree);
        b4.onClick.AddListener(ButtonFour);
        reset.onClick.AddListener(ButtonReset);
        add.onClick.AddListener(Add);
        sub.onClick.AddListener(Sub);
        mult.onClick.AddListener(Mult);
        div.onClick.AddListener(Div);
        regenerate.onClick.AddListener(Regenerate);
    }

    void Update()
    {
        try
        {
            float r = Evaluate(displayField.text);
            if (Mathf.Approximately(24f,r) && b1.interactable == false && b2.interactable == false && b3.interactable == false && b4.interactable == false)
            {
                isSolved = true;
                roomGeneration.keyRooms--;
                tm.AddTreasure(rewardAmt);
                this.transform.parent.gameObject.SetActive(false);
            }
            result.text = Math.Round(r,3).ToString();
        }
        catch
        {
            result.text = "#";
        }
    }


    private int[] GenerateProblem()
    {
        int[] problem = new int[20];
        Random randNum = new Random();

        for (int i = 0; i < problem.Length; i++)
        {
            problem[i] = randNum.Next(1, 11);
        }

        return problem;
    }

    static bool CanReach24(int[] nums)
    {
        // Base case: if there's only one number, check if it equals 24
        if (nums.Length == 1)
            return nums[0] == 24;

        // Try all possible combinations of two numbers and apply arithmetic operations
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = 0; j < nums.Length; j++)
            {
                if (i != j)
                {
                    // Create a new array without the two numbers
                    int[] newNums = new int[nums.Length - 1];
                    int idx = 0;
                    for (int k = 0; k < nums.Length; k++)
                    {
                        if (k != i && k != j)
                        {
                            newNums[idx] = nums[k];
                            idx++;
                        }
                    }

                    // Try all arithmetic operations
                    foreach (char op in new char[] { '+', '-', '*', '/' })
                    {
                        if ((op == '/' && nums[j] != 0) || op != '/')
                        {
                            switch (op)
                            {
                                case '+':
                                    newNums[idx] = nums[i] + nums[j];
                                    break;
                                case '-':
                                    newNums[idx] = nums[i] - nums[j];
                                    break;
                                case '*':
                                    newNums[idx] = nums[i] * nums[j];
                                    break;
                                case '/':
                                    newNums[idx] = nums[i] / nums[j];
                                    break;
                            }

                            // Recursively check the new array
                            if (CanReach24(newNums))
                                return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    private float Evaluate(string expression)
    {
        char[] operators = { '+', '-', '*', '/' };
        string[] tokens = expression.Split(operators, StringSplitOptions.RemoveEmptyEntries);
        char[] ops = expression.Where(c => IsOperator(c)).ToArray();

        float result = int.Parse(tokens[0]);
        for (int i = 0; i < ops.Length; i++)
        {
            result = ApplyOperation(result, int.Parse(tokens[i + 1]), ops[i]);
        }
        return result;

    }

    static bool IsOperator(char c)
    {
        return c == '+' || c == '-' || c == '*' || c == '/';
    }
    
    static float ApplyOperation(float a, int b, int op)
    {
        switch (op)
        {
            case '+':
                return a + b;
            case '-':
                return a - b;
            case '*':
                return a * b;
            case '/':
                return a / b;
            default:
                throw new ArgumentException("Invalid operator: " + op);
        }
    }
    
    void ButtonOne()
    {
        b1.interactable = false;
        displayField.text += num[0].ToString();
    }

    void ButtonTwo()
    {
        b2.interactable = false;
        displayField.text += num[1].ToString();
    }
    
    void ButtonThree()
    {
        b3.interactable = false;
        displayField.text += num[2].ToString();
    }

    void ButtonFour()
    {
        b4.interactable = false;
        displayField.text += num[3].ToString();
    }

    void ButtonReset()
    {
        b1.interactable = b2.interactable = b3.interactable = b4.interactable = true;
        displayField.text = "";
    }

    void Add()
    {
        displayField.text += "+";
    }
    void Sub()
    {
        displayField.text += "-";
    }
    void Mult()
    {
        displayField.text += "*";
    }
    void Div()
    {
        displayField.text += "/";
    }

    void Regenerate()
    {
        bool check = false;
        while (check != true)
        {
            num = GenerateProblem();
            if (CanReach24(num))
            {
                check = true;
            }
        }
        one.text = num[0].ToString();
        two.text = num[1].ToString();
        three.text = num[2].ToString();
        four.text = num[3].ToString();
        ButtonReset();
    }
    
}
