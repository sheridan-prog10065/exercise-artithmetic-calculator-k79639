using System.Collections.ObjectModel;
using Debug = System.Diagnostics.Debug;


namespace MathOperators;

public partial class MainPage
{
    private ObservableCollection<string> _expList;
    
    public MainPage()
    {
        InitializeComponent();
        _expList = new ObservableCollection<string>();
        _lstExpHistory.ItemsSource = _expList;
    }

    private async void OnCalculate(object sender, EventArgs e)
    {
        try
        {
            //Get the number user inputted 
            double leftOperand = double.Parse(_txtLeftOp.Text); // this is how we convert a string to a number
            double rightOperand = double.Parse(_txtRightOp.Text);

            //Get what user chose to do with those numbers
            char opEntry =
                ((string)_pckOperand.SelectedItem)[0]; //cast to string is possible because selected item is an object.

            //Do the arithmetic operation
            double result = PerformArithmeticOperation(opEntry, leftOperand, rightOperand);

            //Remember the operation performed
            string expression = $"{leftOperand} {opEntry} {rightOperand} = {result}";

            //Add the expression to the list of expressions
            _expList.Add(expression);

            //Show the result to the user
            _txtMathExp.Text = expression;
        }
        catch (ArgumentNullException ex)
        {
            //The user did not provide an argument
            await DisplayAlert("Error", "Please provide the required input", "OK");
        }
        catch (FormatException ex)
        {
            //The user did not provide the correct input
            await DisplayAlert("Error", "Please provide the correct input", "OK");
        }
        catch (DivideByZeroException ex)
        {
            //The user did the division by 0
            await DisplayAlert("Error", "Please provide the correct input", "OK");
        }
    }
    
    private double PerformArithmeticOperation(char opEntry, double leftOperand, double rightOperand)
    {
        switch (opEntry)
        {
            case '+' :
                return PerformAddition(leftOperand, rightOperand);
            case '-':
                return PerformSubtraction(leftOperand, rightOperand);
            case '*' :
                return PerformMultiplication(leftOperand, rightOperand);
            case '/' :
                return PerformDivision(leftOperand, rightOperand);
            case '%' :
                return PerformDivRemainder(leftOperand, rightOperand);
            default:
                Debug.Assert(false, "Unknown operator. Cannot calculate operand.");
                return 0;
        }
    }

    private double PerformDivRemainder(double leftOperand, double rightOperand)
    {
        return leftOperand % rightOperand;
    }

    private double PerformDivision(double leftOperand, double rightOperand)
    {
        string divOp = _pckOperand.SelectedItem as string;
        if (divOp.Contains("Int", StringComparison.OrdinalIgnoreCase))
        {
            //If the user chose to divide by an integer, cast to integer.
            return (int)leftOperand / (int)rightOperand;
        }
        else
        {
            //Real division
            return leftOperand / rightOperand;
        }
    }

    private double PerformMultiplication(double leftOperand, double rightOperand)
    {
        return leftOperand * rightOperand;
    }

    private double PerformSubtraction(double leftOperand, double rightOperand)
    {
        return leftOperand - rightOperand;
    }

    private double PerformAddition(double leftOperand, double rightOperand)
    {
        return leftOperand + rightOperand;
    }

    private void OnClear(object sender, EventArgs e)
    {
        _txtLeftOp.Text = "";
        _txtRightOp.Text = "";
        _pckOperand.SelectedIndex = -1;
        _pckOperand.Title = "";
        _txtMathExp.Text = ""; 
        _expList.Clear();
    }
}