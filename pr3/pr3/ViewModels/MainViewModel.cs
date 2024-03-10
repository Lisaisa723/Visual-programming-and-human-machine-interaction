using System;
using System.Collections.Generic;
using System.Reactive;
using Microsoft.CodeAnalysis;
using pr3.Model;
using ReactiveUI;

namespace pr3.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string _resultatik;
    private double _firstDoulbe;
    private double _secondDoulbe;
    private bool hasTochka;
    private Operation _operation;
    private State _state;

    public string ShownValue
    {
        get => _resultatik;
        set => this.RaiseAndSetIfChanged(ref _resultatik, value);
    }

    public ReactiveCommand<char, Unit> AddNumberCommand { get; }
    public ReactiveCommand<Unit, Unit> RemoveLastNumberCommand { get; }
    public ReactiveCommand<Operation, Unit> ExecuteOperationCommand { get; }
    public ReactiveCommand<Function, Unit> ExecuteFunctionCommand { get; }
    public ReactiveCommand<Unit, Unit> ComputeCommand { get; }
    public MainViewModel()
    {
        AddNumberCommand = ReactiveCommand.Create<char>(AddNumber);
        RemoveLastNumberCommand = ReactiveCommand.Create(RemoveLastNumber);
        ExecuteOperationCommand = ReactiveCommand.Create<Operation>(ExecuteOperation);
        ExecuteFunctionCommand = ReactiveCommand.Create<Function>(ExecuteFunction);
        ComputeCommand = ReactiveCommand.Create(Compute);
        RxApp.DefaultExceptionHandler = Observer.Create<Exception>(
                ex => Console.Write("next"),
                ex => Console.Write("Unhandled rxui error"));
        ClearScreen();
    }

    public void ClearScreen()
    {
        ShownValue = "0";
        _firstDoulbe = 0;
        _secondDoulbe = 0;
        hasTochka = false;
        _operation = Operation.None;
        _state = State.Initial;
    }

    private void AddNumber(char digit)
    {
        if (_state == State.Value_computed)
        {
            ClearScreen();
        }
        else if (_state != State.Initial)
        {
            _state = State.Editing;
        }

        if (digit == '.' && hasTochka)
        {
            return;
        }

        if (digit == '.')
        {
            hasTochka = true;
            ShownValue += digit;
            return;
        }

        if (ShownValue == "0")
        {
            ShownValue = "";
        }

        ShownValue += digit;
    }

    public void Exit()
    {
        Environment.Exit(0);
    }
    public void RemoveLastNumber()
    {
        if (_state == State.Value_computed)
        {
            ClearScreen();
        } else if (_state != State.Initial)
        {
            _state = State.Editing;
        }

        if (ShownValue.Length == 0)
        {
            return;
        }
        if (ShownValue[ShownValue.Length - 1] == '.')
        {
            hasTochka = false;
        }
        ShownValue = ShownValue.Remove(ShownValue.Length - 1);

        if (ShownValue.Length == 0)
        {
            ShownValue += '0';
        }
    }

    private void MakeOperation(Operation operation)
    {
        switch (_operation)
        {
            case Operation.Add:
                {
                    _firstDoulbe += _secondDoulbe;
                    break;
                }
            case Operation.Subtract:
                {
                    _firstDoulbe -= _secondDoulbe;
                    break;
                }
            case Operation.Multiply:
                {
                    _firstDoulbe *= _secondDoulbe;
                    break;
                }
            case Operation.Divide:
                {
                    _firstDoulbe /= _secondDoulbe;
                    break;
                }
            case Operation.Modulus:
                {
                    _firstDoulbe %= _secondDoulbe;
                    break;
                }
            case Operation.Power:
                {
                    _firstDoulbe = Math.Pow(_firstDoulbe, _secondDoulbe);
                    break;
                }
        }
    }

    private void ExecuteOperation(Operation operation)
    {
        if (_state == State.Initial || _state == State.Editing)
        {
            _secondDoulbe = Convert.ToDouble(ShownValue);
        }

        if (_state == State.Initial || (_state == State.Function_computed && _operation == Operation.None))
        {
            _firstDoulbe = _secondDoulbe;
        } else if (_state == State.Editing || _state == State.Function_computed)
        {
            MakeOperation(operation);
        }
        _operation = operation;
        _state = State.Operation_changed;
        ShownValue = "0";
        hasTochka = false;
    }

    private void ExecuteFunction(Function function)
    {

        if (_state == State.Initial || _state == State.Editing)
        {
            _secondDoulbe = Convert.ToDouble(ShownValue);
        }

        switch (function)
        {
            case Function.Ln:
                {
                    _secondDoulbe = Math.Log(_secondDoulbe);
                    ShownValue = Convert.ToString(_secondDoulbe);
                    break;
                }
            case Function.Log:
                {
                    _secondDoulbe = Math.Log10(_secondDoulbe);
                    ShownValue = Convert.ToString(_secondDoulbe);
                    break;
                }
            case Function.Sin:
                {
                    _secondDoulbe = Math.Sin(_secondDoulbe);
                    ShownValue = Convert.ToString(_secondDoulbe);
                    break;
                }
            case Function.Cos:
                {
                    _secondDoulbe = Math.Cos(_secondDoulbe);
                    ShownValue = Convert.ToString(_secondDoulbe);
                    break;
                }
            case Function.Tg:
                {
                    _secondDoulbe = Math.Tan(_secondDoulbe);
                    ShownValue = Convert.ToString(_secondDoulbe);
                    break;
                }
            case Function.Factorial:
                {
                    _secondDoulbe = Factorial((long)Math.Round(_secondDoulbe));
                    ShownValue = Convert.ToString(_secondDoulbe);
                    break;
                }
            case Function.Floor:
                {
                    _secondDoulbe = Math.Floor(_secondDoulbe);
                    ShownValue = Convert.ToString(_secondDoulbe);
                    break;
                }
            case Function.Ceil:
                {
                    _secondDoulbe = Math.Ceiling(_secondDoulbe);
                    ShownValue = Convert.ToString(_secondDoulbe);
                    break;
                }
        }
        _state = State.Function_computed;
    }

    private void Compute()
    {
        if (_state == State.Initial || _state == State.Editing)
        {
            _secondDoulbe = Convert.ToDouble(ShownValue);
        }
        MakeOperation(_operation);
        ShownValue = Convert.ToString(_firstDoulbe);
        _state = State.Value_computed;
    }

    private long Factorial(long n)
    {
        if (n == 1)
        {
            return 1;
        }
        return n * Factorial(n - 1);
    }
}
