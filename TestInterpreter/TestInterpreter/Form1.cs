//-----------------------------------------------------------------------
//  Interpreterパターンサンプル Project
//
//  逆ボーランド記法で、記述された文字列を解析し、
//  計算する。演算子は、+/-のみ。文字列の Numericチェックはしていない。
//  Interpreter を短めにまとめてあります。
//  
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestInterpreter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Interpreterパターン実験開始
        private void button1_Click(object sender, EventArgs e)
        {
            String expression = "52 4 2 - +";   // 解析対象文字列 (spaceが区切り)

            Parser p = new Parser(expression);  // Parserクラスに設定

            int r = p.evaluate();               // 解析実行。 r = 54 となる。
        }
    }

    // Number/Plus/Minus のための interface
    public interface Expression
    {
        void interpret(Stack<int> s);
    }

    // 終端 数値
    class TerminalExpression_Number : Expression
    {
        private int number;
        public TerminalExpression_Number(int number) { this.number = number; }
        // 数値を push
        public void interpret(Stack<int> s) { s.Push(number); }
    }

    // 終端 +
    class TerminalExpression_Plus : Expression
    {
        // 2個Stackから pop して足し算後、push
        public void interpret(Stack<int> s) { s.Push(s.Pop() + s.Pop()); }
    }
 
    // 終端 -
    class TerminalExpression_Minus : Expression
    {
        // 2個Stackから pop して引き算後、push
        public void interpret(Stack<int> s) { s.Push(-s.Pop() + s.Pop()); }
    }
  
    // 解析
    class Parser
    {
        private List<Expression> parseTree = new List<Expression>(); // only one NonTerminal Expression here

        // ソース expression "52 4 2 - +" を
        // token '52','4','2','-','+' に分解し、List型parseTree に保存
        public Parser(String s)
        {
            foreach (String token in s.Split(' '))
            {
                if (token.Equals("+")) parseTree.Add(new TerminalExpression_Plus());
                else if (token.Equals("-")) parseTree.Add(new TerminalExpression_Minus());
                // ...
                else parseTree.Add(new TerminalExpression_Number(Convert.ToInt32(token)));
            }
        }

        // 評価(処理)
        public int evaluate()
        {
            // 52,4,2 の順に push し、 - で、 2, 4 を popし、4-2=2 の 2
            // を push、+ で、2, 52 を pop し、 52+2=54 の 54 を push し、
            // 最後に 54 を pop して return 
            Stack<int> context = new Stack<int>();

            foreach(Expression e in parseTree)
                e.interpret(context);

            return context.Pop();
        }
    }


}
