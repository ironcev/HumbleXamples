﻿using System;
using System.Linq;
using static System.Console;

namespace Snippets
{
    class LocalFunctionsThatDoNotUseLocalVariablesAndClassInstanceMembers
    {
        void Method()
        {
            int localVariable = 0;
            WriteLine(localVariable);

            LocalFunction();

            void LocalFunction()
            {
                WriteLine("I do not use local variables.");
                WriteLine("I do not use class instance members.");
            }
        }
    }

    class LocalFunctionsThatUseLocalVariables
    {
        void Method()
        {
            int localVariable = 0;
            string otherLocalVariable = localVariable.ToString();

            LocalFunction();
            LocalFunctionWithParameters(localVariable, otherLocalVariable);

            void LocalFunction()
            {
                WriteLine($"I use {localVariable} and {otherLocalVariable}.");
            }

            void LocalFunctionWithParameters(int number, string text)
            {
                WriteLine($"I use {localVariable} and {otherLocalVariable}.");
            }
        }
    }

    class LocalFunctionsThatUseOnlySomeLocalVariables
    {
        void Method()
        {
            int localVariable = 0;
            string otherLocalVariable = localVariable.ToString();
            WriteLine(otherLocalVariable);

            LocalFunction();

            void LocalFunction()
            {
                WriteLine($"I use only the local variable {localVariable}.");
            }
        }
    }

    class LocalFunctionsThatUseOnlyClassInstanceMembers
    {
        private int _instanceField = 0;
        public string InstanceProperty => "";
        int InstanceMethod() => 0;

        void Method()
        {
            LocalFunction();

            void LocalFunction()
            {
                WriteLine($"I use {_instanceField}.");
                WriteLine($"I use {InstanceProperty}.");
                WriteLine($"I call {InstanceMethod()}.");
            }
        }
    }

    class LocalFunctionsThatUseLocalVariablesAndClassInstanceMembers
    {
        private int _instanceField = 0;
        public string InstanceProperty => "";
        int InstanceMethod() => 0;

        void Method()
        {
            int localVariable = 0;
            string otherLocalVariable = localVariable.ToString();
            WriteLine(otherLocalVariable);

            LocalFunction();

            void LocalFunction()
            {
                WriteLine($"I use {localVariable} and {otherLocalVariable}.");
                WriteLine($"I use {_instanceField}.");
                WriteLine($"I use {InstanceProperty}.");
                WriteLine($"I call {InstanceMethod()}.");
            }
        }
    }

    class LocalFunctionsThatUseLocalVariablesAndAreUsedInLambdaExpressions
    {
        void Method()
        {
            int localVariable = 0;
            string otherLocalVariable = localVariable.ToString();
            WriteLine(otherLocalVariable);

            Enumerable.Range(0, 10).Where(i => !LocalFunction());

            bool LocalFunction()
            {
                WriteLine($"I use {localVariable} and {otherLocalVariable}.");
                return true;
            }
        }
    }

    class LocalFunctionsThatUseLocalVariablesDifferentThanThoseUsedInALambdaExpression
    {
        void Method()
        {
            int localVariable = 0;
            string otherLocalVariable = localVariable.ToString();

            Action lambdaThatUsesOtherLocalVariable = () => WriteLine(otherLocalVariable);

            LocalFunction();

            void LocalFunction()
            {
                WriteLine($"I use only the local variable {localVariable}.");
            }
        }
    }

    class SubtleMemoryLeak
    {
        private class ResourceDemandingClass { }

        Action Method()
        {
            var resourceDemandingObject = new ResourceDemandingClass();
            string someLocalVariable = "";

            Action lambdaThatUsesSomeLocalVariable = () => WriteLine(someLocalVariable);

            LocalFunctionThatUsesTheResourceDemandingObject();

            return lambdaThatUsesSomeLocalVariable;

            void LocalFunctionThatUsesTheResourceDemandingObject()
            {
                WriteLine($"I use the {resourceDemandingObject}.");
            }
        }
    }
}