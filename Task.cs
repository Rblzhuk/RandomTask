using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomTask
{
    public class Task
    {
        public double taskResult;
        public int typeOperationDigit, taskType;
        public string leftArg, rightArg, createdTask, typeOperationString, cleareTaskResultString;
        Random random = new Random();

        public Task(double lArg, double rtArg, int tOperation, int tTask)
        {
            leftArg = lArg.ToString();
            rightArg = rtArg.ToString();
            typeOperationDigit = tOperation;
            taskType = tTask;

            HandlerTask();
        }

        private void HandlerTask()
        {
            switch (typeOperationDigit)
            {
                case 0:
                    {
                        typeOperationString = "+";
                        taskResult = Double.Parse(leftArg) + Double.Parse(rightArg);

                        break;
                    }
                case 1:
                    {
                        typeOperationString = "-";
                        taskResult = Double.Parse(leftArg) - Double.Parse(rightArg);
                        break;
                    }
                case 2:
                    {
                        typeOperationString = "*";
                        taskResult = Double.Parse(leftArg) * Double.Parse(rightArg);
                        break;
                    }
                case 3:
                    {
                        typeOperationString = ":";
                        taskResult = Double.Parse(leftArg) / Double.Parse(rightArg);
                        break;
                    }
            }
            cleareTaskResultString = leftArg + typeOperationString + rightArg + "=" + taskResult.ToString();
            switch (taskType)
            {
                case 0:
                    {
                        createdTask = leftArg + typeOperationString + rightArg + "=" + "х";
                        break;
                    }
                case 1:
                    {
                        switch (random.Next(2))
                        {
                            case 0:
                                {
                                    createdTask = "х" + typeOperationString + rightArg + "=" + taskResult.ToString();
                                    break;
                                }
                            case 1:
                                {
                                    createdTask = leftArg + typeOperationString + "х" + "=" + taskResult.ToString();
                                    break;
                                }
                        }
                        break;
                    }
            }

        }
    }
}
