using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandomTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int leftArg, rightArg, operation;
        StringBuilder errorList = new StringBuilder();
        Regex regex;
        Random random;
        Task task;

        private void typeWriteDataComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (numericArgumentsDP != null && rangeArgumentsDP != null)
            {
                switch (typeWriteDataComboBox.SelectedIndex)
                {
                    //Вручную
                    case 0:
                        {
                            numericArgumentsDP.Height = 26;
                            rangeArgumentsDP.Height = 0;
                            leftMinArg.Text = string.Empty;
                            leftMaxArg.Text = string.Empty;
                            rightMinArg.Text = string.Empty;
                            rightMaxArg.Text = string.Empty;
                            break;
                        }
                    //Диапазон
                    case 1:
                        {
                            numericArgumentsDP.Height = 0;
                            rangeArgumentsDP.Height = 26;
                            leftHandArg.Text = string.Empty;
                            rightHandArg.Text = string.Empty;
                            break;
                        }
                }
            }
        }

        private void createTaskB_Click(object sender, RoutedEventArgs e)
        {
            switch (typeWriteDataComboBox.SelectedIndex)
            {
                //Вручную
                case 0:
                    {
                        if (CheckOnCorrectHandArguments())
                        {
                            leftArg = int.Parse(leftHandArg.Text);
                            rightArg = int.Parse(rightHandArg.Text);
                        }
                        break;
                    }
                //Диапазон
                case 1:
                    {
                        if (CheckOnCorrectHandArguments())
                        {
                            random = new Random();

                            leftArg = random.Next(int.Parse(leftMinArg.Text), int.Parse(leftMaxArg.Text) + 1);
                            if (typeRangeOperationC.SelectedIndex == 3)
                            {
                                do
                                {
                                    rightArg = random.Next(int.Parse(rightMinArg.Text), int.Parse(rightMaxArg.Text) + 1);
                                } while (rightArg == 0);
                            }
                            else
                            {
                                rightArg = random.Next(int.Parse(rightMinArg.Text), int.Parse(rightMaxArg.Text) + 1);
                            }
                        }
                        break;
                    }
            }
            HandlerTaskOperation();

            if (errorList.Length > 0)
            {
                MessageBox.Show(errorList.ToString());
            }
            else
            {
                task = new Task(leftArg, rightArg, operation, taskTypeC.SelectedIndex);
                taskResultL.Content = task.createdTask;
                taskResultL.Height = 53;
            }
            errorList.Clear();
        }

        private void HandlerTaskOperation()
        {
            switch (typeWriteDataComboBox.SelectedIndex)
            {
                //Вручную
                case 0:
                    {
                        operation = typeHandOperationC.SelectedIndex;
                        break;
                    }
                //Диапазон
                case 1:
                    {
                        operation = typeRangeOperationC.SelectedIndex;
                        break;
                    }
            }
        }

        //Все проверки введённых аргументов
        private bool CheckOnCorrectHandArguments()
        {
            bool result = true;

            switch (typeWriteDataComboBox.SelectedIndex)
            {
                //Вручную
                case 0:
                    {
                        if (!CheckOnHandArgIsNotEmpty())
                        {
                            return false;
                        }
                        else
                        {
                            if (CheckOnNotNumericTypeArguments(leftHandArg.Text))
                            {
                                result = false;
                                errorList.AppendLine("Левый аргумент содержит не корректные символы");
                            }
                            if (CheckOnNotNumericTypeArguments(rightHandArg.Text))
                            {
                                result = false;
                                errorList.AppendLine("Правый аргумент содержит не корректные символы");
                            }
                            else if (CheckOnZero(int.Parse(rightHandArg.Text))&&typeHandOperationC.SelectedIndex==3)
                            {
                                result = false;
                                errorList.AppendLine("Деление на 0 не допустимо");
                            }
                        }
                        break;
                    }
                //Диапазон
                case 1:
                    {
                        if (!CheckOnRangeArgIsNotEmpty())
                        {
                            return false;
                        }
                        else
                        {
                            if (CheckOnNotNumericTypeArguments(leftMinArg.Text) && typeWriteDataComboBox.SelectedIndex == 1)
                            {
                                result = false;
                                errorList.AppendLine("Левый минимальный аргумент содержит не корректные символы");
                            }
                            if (CheckOnNotNumericTypeArguments(leftMaxArg.Text))
                            {
                                result = false;
                                errorList.AppendLine("Левый максимальный аргумент содержит не корректные символы");
                            }
                            if (CheckOnNotNumericTypeArguments(rightMinArg.Text))
                            {
                                result = false;
                                errorList.AppendLine("Правый минимальный аргумент содержит не корректные символы");
                            }
                            if (CheckOnNotNumericTypeArguments(rightMaxArg.Text))
                            {
                                result = false;
                                errorList.AppendLine("Правый максимальный аргумент содержит не корректные символы");
                            }
                        }
                        break;
                    }
            }

            return result;
        }

        private bool CheckOnHandArgIsNotEmpty()
        {
            bool result = true;
            if (String.IsNullOrEmpty(leftHandArg.Text))
            {
                result = false;
                errorList.AppendLine("Левый аргумент не указан");
            }
            if (String.IsNullOrEmpty(rightHandArg.Text))
            {
                result = false;
                errorList.AppendLine("Правый аргумент не указан");
            }
            //if (errorList.Length > 0)
            //{
            //    MessageBox.Show(errorList.ToString());
            //}
            //errorList.Clear();
            return result;
        }

        private bool CheckOnRangeArgIsNotEmpty()
        {
            bool result = true;
            if (String.IsNullOrEmpty(leftMinArg.Text))
            {
                result = false;
                errorList.AppendLine("Минимальное значение левого аргумента не указано");
            }
            if (String.IsNullOrEmpty(leftMaxArg.Text))
            {
                result = false;
                errorList.AppendLine("Максимальное значение левого аргумента не указано");
            }
            if (String.IsNullOrEmpty(rightMinArg.Text))
            {
                result = false;
                errorList.AppendLine("Минимальное значение правого аргумента не указано");
            }
            if (String.IsNullOrEmpty(rightMaxArg.Text))
            {
                result = false;
                errorList.AppendLine("Максимальное значение правого аргумента не указано");
            }
            //if (errorList.Length> 0)
            //{
            //    MessageBox.Show(errorList.ToString());
            //}
            //errorList.Clear();
            return result;
        }

        private void getFullTaskB_Click(object sender, RoutedEventArgs e)
        {
            if (task != null)
            {
                fullTaskL.Content = task.cleareTaskResultString;
            }
            else
            {
                MessageBox.Show("Задача ещё не создана");
            }
        }

        private void cleareAllTextFieldsB_Click(object sender, RoutedEventArgs e)
        {
            leftHandArg.Text = string.Empty;
            rightHandArg.Text = string.Empty;

            leftMinArg.Text = string.Empty;
            leftMaxArg.Text = string.Empty;
            rightMinArg.Text = string.Empty;
            rightMaxArg.Text = string.Empty;

            taskResultL.Content = string.Empty;
            taskResultL.Height = 0;
            fullTaskL.Content = string.Empty;

        }

        private void copyResultB_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(taskResultL.Content.ToString());
            MessageBox.Show("Скопировано");
        }

        private bool CheckOnNotNumericTypeArguments(string argument)
        {
            string expression = @"^\d{1,10}$";
            regex = new Regex(expression);
            return !regex.IsMatch(argument);
        }

        public bool CheckOnZero(int argument)
        {
            return argument == 0;
        }
    }
}
