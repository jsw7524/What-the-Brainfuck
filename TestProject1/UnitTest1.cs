using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using What_the_Brainfuck;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> data = new List<string>
            {
                "++++++++++          Set the first cell (0) to 10",
                "[                   Start of the initialization loop ",
                "   >                Go to cell 1",
                "   +++++++          Set it to 7",
                "   >                Go to cell 2",
                "++++++++++       Set it to 10",
                ">                Go to cell 3",
                "+++              Set it to 3",
                "   <<<              Go back to cell 0",
                "   -",
                "]      "
            };

            BrainfuckInterpreter brainfuckInterpreter = new BrainfuckInterpreter();
            string result = brainfuckInterpreter.PreprocessingText(data);
            Assert.IsTrue(result == "++++++++++[>+++++++>++++++++++>+++<<<-]");
        }


        [TestMethod]
        public void TestMethod2()
        {
            List<string> data = new List<string>
            {
                "++++++++++          Set the first cell (0) to 10",
                "[                   Start of the initialization loop ",
                "                   ",
                "++++++++++       Set it to 10",
                "   ",
                "]      "
            };

            BrainfuckInterpreter brainfuckInterpreter = new BrainfuckInterpreter();
            string text = brainfuckInterpreter.PreprocessingText(data);
            Assert.IsTrue(brainfuckInterpreter.CheckSyntax(text));
        }

        [TestMethod]
        public void TestMethod3()
        {
            List<string> data = new List<string>
            {
                "++++++++++          Set the first cell (0) to 10",
                "[                   Start of the initialization loop ",
                "                   ",
                "++++++++++       Set it to 10",
                "   ",
                "[      "
            };

            BrainfuckInterpreter brainfuckInterpreter = new BrainfuckInterpreter();
            string text = brainfuckInterpreter.PreprocessingText(data);
            Assert.IsFalse(brainfuckInterpreter.CheckSyntax(text));
        }

        [TestMethod]
        public void TestMethod4()
        {
            List<string> data = new List<string>
            {
                "++++++++++          Set the first cell (0) to 10",
                "[                   Start of the initialization loop ",
                "[                   ",
                "++++++++++       Set it to 10",
                " ]  ",
                "]      "
            };

            BrainfuckInterpreter brainfuckInterpreter = new BrainfuckInterpreter();
            string text = brainfuckInterpreter.PreprocessingText(data);
            Assert.IsTrue(brainfuckInterpreter.CheckSyntax(text));
        }

        [TestMethod]
        public void TestMethod5()
        {
            List<string> data = new List<string>
            {
                "++++++++++          Set the first cell (0) to 10",
                "[                   Start of the initialization loop ",
                "]                   ",
                "++++++++++       Set it to 10",
                " ]  ",
                "]      "
            };

            BrainfuckInterpreter brainfuckInterpreter = new BrainfuckInterpreter();
            string text = brainfuckInterpreter.PreprocessingText(data);
            Assert.IsFalse(brainfuckInterpreter.CheckSyntax(text));
        }

        [TestMethod]
        public void TestMethod6()
        {
            List<string> data = new List<string>
            {
                ">>>",
            };
            BrainfuckInterpreter brainfuckInterpreter = new BrainfuckInterpreter(1, 1, null);
            brainfuckInterpreter.Run(data);
            Assert.IsTrue("POINTER OUT OF BOUNDS" == brainfuckInterpreter.CheckErrorStatus());
        }

        [TestMethod]
        public void TestMethod7()
        {
            List<string> data = new List<string>
            {
                "--",
            };
            BrainfuckInterpreter brainfuckInterpreter = new BrainfuckInterpreter(1, 3, null);
            brainfuckInterpreter.Run(data);
            Assert.IsTrue("INCORRECT VALUE" == brainfuckInterpreter.CheckErrorStatus());
        }

        [TestMethod]
        public void TestMethod8()
        {
            List<string> data = new List<string>
            {
                "++++++++++          Set the first cell (0) to 10",
                "[                   Start of the initialization loop ",
                "   >                Go to cell 1",
                "   +++++++          Set it to 7",
                "   >                Go to cell 2",
                "++++++++++       Set it to 10",
                ">                Go to cell 3",
                "+++              Set it to 3",
                "   <<<              Go back to cell 0",
                "   -",
                "]      "
            };

            BrainfuckInterpreter brainfuckInterpreter = new BrainfuckInterpreter(data.Count, 16, null);
            brainfuckInterpreter.Run(data);
            Assert.AreEqual(0, brainfuckInterpreter.memory[0]);
            Assert.AreEqual(70, brainfuckInterpreter.memory[1]);
            Assert.AreEqual(100, brainfuckInterpreter.memory[2]);
            Assert.AreEqual(30, brainfuckInterpreter.memory[3]);
        }

        [TestMethod]
        public void TestMethod9()
        {
            List<string> data = new List<string>
            {
                ">",
                "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++          Set the first cell (1) to 70",
                "<",
                "++++++++++          Set the first cell (0) to 10",
                "[                   Start of the initialization loop ",
                ">+.",
                "   <              Go back to cell 0",
                "   -",
                "]      "
            };

            BrainfuckInterpreter brainfuckInterpreter = new BrainfuckInterpreter(data.Count, 16, null);
            string result=brainfuckInterpreter.Run(data);
            Assert.AreEqual("ABCDEFGHIJ", result);
        }


        [TestMethod]
        public void TestMethod10()
        {
            Solution sln = new Solution();
            using (var reader = new StreamReader("Test10.txt"))
            {
                Console.SetIn(reader);
                List<string> data = new List<string>();
                Queue<int> queue = new Queue<int>();
                string[] inputs = Console.ReadLine().Split(' ');
                int L = int.Parse(inputs[0]);
                int S = int.Parse(inputs[1]);
                int N = int.Parse(inputs[2]);
                for (int i = 0; i < L; i++)
                {
                    string r = Console.ReadLine();
                    data.Add(r);
                }
                for (int i = 0; i < N; i++)
                {
                    int c = int.Parse(Console.ReadLine());
                    queue.Enqueue(c);
                }
                BrainfuckInterpreter brainfuckInterpreter = new BrainfuckInterpreter(L, S, queue);
                string result = brainfuckInterpreter.Run(data);
                Assert.AreEqual("$", result);
            }
        }
    }


}



