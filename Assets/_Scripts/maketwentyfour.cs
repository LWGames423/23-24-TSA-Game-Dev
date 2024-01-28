using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class maketwentyfour : MonoBehaviour
{
    public int[] num;
    public TMP_Text one, two, three, four, texter;
    public Button b1, b2, b3, b4;
    
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

        one.text = num[0].ToString();
        two.text = num[1].ToString();
        three.text = num[2].ToString();
        four.text = num[3].ToString();
    }

    void Update()
    {
        
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
}
