using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace What_the_Brainfuck
{
    public class BrainfuckInterpreter
    {
        public int lines;
        public int memorySize;
        public Queue<int> inputs;
        public List<int> memory;
        public Stack<int> stack;
        public int programCounter;
        public int memoryPointer;

        public BrainfuckInterpreter()
        {

        }

        public Boolean CheckSyntax(string program)
        {
            Stack<char> counterSquareBracket = new Stack<char>();
            foreach (char c in program)
            {
                if (c == '[')
                {
                    counterSquareBracket.Push('[');
                }
                else if (c == ']')
                {
                    if (counterSquareBracket.Count == 0)
                    {
                        return false;
                    }
                    counterSquareBracket.Pop();
                }
            }
            return (counterSquareBracket.Count == 0) ? true : false;
        }

        public BrainfuckInterpreter(int lines, int memorySize, Queue<int> inputs)
        {
            this.lines = lines;
            this.memorySize = memorySize;
            this.inputs = inputs;
            this.memory = new List<int>();
            for (int i = 0; i < memorySize; i++)
            {
                memory.Add(0);
            }
            this.stack = new Stack<int>();
            programCounter = 0;
            memoryPointer = 0;
        }

        public string PreprocessingText(List<string> text)
        {
            List<char> validChars = new List<char> { '>', '<', '+', '-', '.', ',', '[', ']' };
            StringBuilder sb = new StringBuilder();
            foreach (string line in text)
            {
                sb.Append(line.Where(c => validChars.Contains(c)).ToArray());
            }
            return sb.ToString();
        }

        public string CheckErrorStatus()
        {
            foreach (int v in memory)
            {
                if (0 > v || v > 255)
                {
                    return "INCORRECT VALUE";
                }
            }
            if (memoryPointer >= this.memorySize || 0 > memoryPointer)
            {
                return "POINTER OUT OF BOUNDS";
            }
            return "OK";
        }

        public string Run(List<string> text)
        {
            string program = PreprocessingText(text);
            if (!CheckSyntax(program))
            {
                return "SYNTAX ERROR";
            }
            StringBuilder output = new StringBuilder();
            while (programCounter < program.Length)
            {
                char instruction = program[programCounter];
                programCounter += 1;
                switch (instruction)
                {
                    case '>':
                        memoryPointer += 1;
                        break;
                    case '<':
                        memoryPointer -= 1;
                        break;
                    case '+':
                        memory[memoryPointer] += 1;
                        break;
                    case '-':
                        memory[memoryPointer] -= 1;
                        break;
                    case '.':
                        output.Append(Convert.ToChar(memory[memoryPointer]));
                        break;
                    case ',':
                        memory[memoryPointer] = inputs.Dequeue();
                        break;
                    case '[':
                        if (0 == memory[memoryPointer])
                        {
                            while (']' != program[programCounter])
                            {
                                programCounter++;
                            }
                            programCounter++;
                        }
                        else
                        {
                            stack.Push(programCounter - 1);
                        }
                        break;
                    case ']':
                        int tmp = stack.Pop();
                        if (0 != memory[memoryPointer])
                        {
                            programCounter = tmp;
                        }
                        break;
                }
                string statusText = CheckErrorStatus();
                if ("OK" != statusText)
                {
                    return statusText;
                }
            }
            return output.ToString();
        }
    }

    public class Solution
    {
        static void Main(string[] args)
        {
            List<string> data = new List<string>();
            Queue<int> queue = new Queue<int>();
            string[] inputs = Console.ReadLine().Split(' ');
            int L = int.Parse(inputs[0]);
            int S = int.Parse(inputs[1]);
            int N = int.Parse(inputs[2]);
            for (int i = 0; i < L; i++)
            {
                data.Add(Console.ReadLine());
            }
            for (int i = 0; i < N; i++)
            {
                queue.Enqueue(int.Parse(Console.ReadLine()));
            }
            BrainfuckInterpreter brainfuckInterpreter = new BrainfuckInterpreter(L, S, queue);
            string result = brainfuckInterpreter.Run(data);
            Console.WriteLine(result);
        }
    }

}
