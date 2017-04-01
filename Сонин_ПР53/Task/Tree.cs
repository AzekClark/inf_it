using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    /// <summary>
    /// Tree definetion
    /// </summary>
    public class Tree
    {
        public string Data;
        public Tree Left { get; set; }
        public Tree Right { get; set; }
    }


    public static class TreeWorker
    {
        public static Tree MakeTree(string data)
        {
            Tree tree = new Tree();

            if (data.IndexOf(")*") > 0)
            {
                tree.Data = "*";
                tree.Left = MakeTree(data.Substring(1, data.IndexOf(")*")-1));
                tree.Right = MakeTree(data.Substring(data.IndexOf(")*") + 2));
            }
            else if (data.IndexOf(")/") > 0)
            {
                tree.Data = "/";
                tree.Left = MakeTree(data.Substring(1, data.IndexOf(")*")));
                tree.Right = MakeTree(data.Substring(data.IndexOf(")*") + 2));
            }
            else if (data.IndexOf(")+") > 0)
            {
                tree.Data = "+";
                tree.Left = MakeTree(data.Substring(1, data.IndexOf(")*")));
                tree.Right = MakeTree(data.Substring(data.IndexOf(")*") + 2));
            }
            else if ( data.IndexOf(")-") > 0)
            {
                tree.Data = "-";
                tree.Left = MakeTree(data.Substring(1, data.IndexOf(")*")));
                tree.Right = MakeTree(data.Substring(data.IndexOf(")*") + 2));
            }
            else
            {

                int k = LastOp(data);
                if (k == 0)
                {
                    if (!Char.IsDigit(data[0]))
                        data = data.Remove(0, 1);
                    if (!Char.IsDigit(data[data.Length - 1]))
                        data = data.Remove(data.Length - 1);
                    tree.Data = data;
                    tree.Left = null;
                    tree.Right = null;
                }
                else
                {
                    tree.Data = data[k].ToString();
                    tree.Left = MakeTree(data.Substring(0, k));
                    tree.Right = MakeTree(data.Substring(k + 1));
                }
            }

            return tree;
        }

        static string Copy(string s, int start, int lenght)
        {
            string data = "";
            for (; start <= lenght; start++)
                data += s[start].ToString();
            return data;
        }

        private static int Priority(char op)
        {
            switch (op)
            {
                case '+':
                case '-': return 1;
                case '*':
                case '/': return 2;
            }
            return 100;
        }

        private static int LastOp(string s)
        {
            int i, minPrt, res;
            minPrt = 50; // любое между 2 и 100
            res = 0;
            for (i = 0; i < s.Length; i++)
                if (Priority(s[i]) <= minPrt)
                {
                    minPrt = Priority(s[i]);
                    res = i;
                }
            return res;
        }

        public static int Calculate(Tree tree)
        {
            
            int  res = 0;
            if (tree.Left == null)
                return Convert.ToInt32(((string)tree.Data));
            else
            {

                int n1 = Calculate(tree.Left); // левая
                int n2 = Calculate(tree.Right);  // правая
                switch (tree.Data)
                {
                    case "+": res = n1 + n2; break;
                    case "-": res = n1 - n2; break;
                    case "*": res = n1 * n2; break;
                    case "/": res = n1 / n2; break;
                }
            }
            return res;
        }

        public static string ToPreficsForm(Tree tree)
        {
            string data = "";
            data += $"{tree.Data} ";
            if ( tree.Left != null)
            {
                data += $"{ToPreficsForm(tree.Left)} ";
                data += $"{ToPreficsForm(tree.Right)} ";
            }
            return data;
        }

        public static string ToPostficsForm(Tree tree)
        {
            string data = "";
            if (tree.Left != null)
                data += ToPostficsForm(tree.Left) + " ";
            if (tree.Right != null)
                data += ToPostficsForm(tree.Right)+ " ";
            data += tree.Data ;
            return data;
        }

        public static string ToWidhtForm(Tree tree, bool head = true)
        {
            string data = "";
            if (head)
                data += tree.Data + " ";
            if ( tree.Left != null)
            {
                data += tree.Left.Data + " ";
                data += tree.Right.Data + " ";
                data += ToWidhtForm(tree.Left, false);
                data += ToWidhtForm(tree.Right, false);
            }

            return data;
        }




    }

}
