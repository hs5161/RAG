#region techer
//using C___teacher;
////הפיכת מחסנית בעזרת תור
////static void ReverseStack<T>(MaHatForTest.Stack<T> s)
////{
////    MaHatForTest.Queue<T> q = new MaHatForTest.Queue<T>();
////    while (!s.IsEmpty())
////    {
////        q.Insert(s.Pop());
////    }
////    while (!q.IsEmpty())
////    {
////        s.Push(q.Remove());
////    }
////}
//////עמוד 14
////static Node<int> DiffrenceList(Node<int> head)
////{
////    if (head == null || !head.HasNext) return null;
////    Node<int> headNew, temp;
////    temp = headNew = new Node<int>(Math.Abs(head.GetValue - head.GetNext.GetValue));//יצירת איבר ראשון 
////    head = head.GetNext;
////    while(head .HasNext) {
////        temp.SetNext(new Node<int>((Math.Abs(head.GetValue - head.GetNext.GetValue))));
////        temp=temp.GetNext;  
////        head=head.GetNext;
////    }
////    return headNew;

////}

//static void Print<T>(Node<T> node)
//{
//    if (node == null) return;
//    Console.Write(node.GetValue + " ");
//    Print(node.GetNext);
//}


////Random random = new Random();
//////MaHatForTest.Stack<int> stack = new MaHatForTest.Stack<int>();
//////for (int i = 0; i < 10; i++)
//////    stack.Push(random.Next(1, 10));
//////Console.WriteLine(stack);
//////MaHatForTest.Queue<int> queue = new MaHatForTest.Queue<int>();
//////ReverseStack(stack);
//////Console.WriteLine(stack);
//////for (int i = 0;i < 10;i++)
//////    queue.Insert(random .Next(1,10));
//////Console.WriteLine(queue);
////Node<int> head = new Node<int>(10);
////for (int i = 0;i < 10;i++)  
////    head=new Node<int>(random.Next(1, 10),head);//הוספה לראש 
////while (head.HasNext)
////{ 
////    Print(head);
////    head=DiffrenceList(head);
////    Console.WriteLine(  );
////}
////Console.WriteLine(  head.GetValue);

////------------------tree----------------------
////
//#region tree
////פונקציה שבונה עץ חיפוש בינארי
//static void AddItem(BinNode<int> root, int item)
//{
//    if (root == null)
//        return;
//    if (item <= root.GetValue())
//    {
//        if (!root.HasLeft)
//        {
//            root.SetLeft(new BinNode<int>(item));
//            return;
//        }
//        else AddItem(root.GetLeft, item);
//    }
//    else if (!root.HasRight)
//    {
//        root.SetRight(new BinNode<int>(item));
//        return;
//    }
//    else AddItem(root.GetRight, item);


//}
////ריקה תוכיתס
//static void PrintIn(BinNode<int> root)
//{
//    if (root == null) return;
//    PrintIn(root.GetLeft);
//    Console.Write(root + ", ");
//    PrintIn(root.GetRight);
//}
//// פונקציה שמקבלת עץ חיפוש ומחזירה עץ חיפוש שלם/כמעט שלם
//static void ReturnFullBinaryTree(BinNode<int> node)
//{
//    Node<int> temp = new Node<int>(0);
//    Node<int> temp1 = temp;
//    int count = Func(node, temp, 0);
//    Print<int>(temp1.GetNext);
//}

//static int Func(BinNode<int> root, Node<int> temp, int count)
//{
//    if (root == null) return count;
//    Func(root.GetLeft, temp, count);
//    temp.SetNext(new Node<int>(root.GetValue()));
//    count++;
//    temp = temp.GetNext;
//    Func(root.GetRight, temp, count);
//    return count;
//}

////BinNode<int> tree = new BinNode<int>(10);
////Random random = new Random();
////for (int i = 0; i < 10; i++)
////{
////    AddItem(tree, random.Next(15));
////}
////PrintIn(tree);
////Console.WriteLine();
////ReturnFullBinaryTree(tree);
//#endregion
////תרגיל חדש לבנות עץ להסתכל אצלי עד איפו הגענו 
////תרגיל 7  2023 מועד ב
////  פונקציה שמקבלת צומת   ומספר ומחזירה האם המספר קיים בעץ
//static bool IsExist(BinNode<int> root, int num)//n
//{
//    if (root == null)
//        return true;
//    if (root.GetValue() == num) return false;
//    return IsExist(root.GetLeft, num) && IsExist(root.GetRight, num);
//}
//static bool IsExist1(BinNode<int> root, BinNode<int> headRoot)//עוברת על כל תת עץ שמאלי לעומת  ראש על ימני קבוע  n^2
//{
//    if (root == null) return true;
//    return IsExist(headRoot, root.GetValue()) && IsExist1(root.GetLeft, headRoot)
//                                             && IsExist1(root.GetRight, headRoot);
//    ;
//}
//static bool IsSpeicalTree(BinNode<int> root)//מעבר על כל צמתי העץ  ושליחה לפונקציה קודמת ךמצוא גם תתי עץ שגויים בצד אחד  N^3 
//{
//    if (root == null) return true;
//    return IsExist1(root.GetLeft, root.GetRight) && IsSpeicalTree(root.GetLeft) && IsSpeicalTree(root.GetRight); ;
//}

////בחינה לדוגמא
//static void print(Node<NodeTree> LT)
//{
//    Node<NodeTree> temp = LT;
//    while (temp != null)
//    {
//        PrintIn(temp.GetValue.GetTree());
//        temp = temp.GetNext;
//    }
//}
////בניה
//static void BuildListNodeTtee(int x, Node<NodeTree> LT)
//{

//    Node<NodeTree> prev = LT;
//    while (LT!=null&&LT.GetValue != null && LT.GetValue.getRange() < (x / 100 * 100+100))
//    {
//        prev = LT;
//        LT = LT.GetNext;
//    }
//    if (LT!=null&&LT.GetValue == null)//אם אין שום אוביקט מטיפוס המחלקה,או אם אין אוביקט מתאים 
//    {
//        LT.SetValue(new NodeTree(new BinNode<int>(x), x / 100 * 100 + 100));
//        return;
//    }
//    //בעצמו
//    if (LT!=null&&LT.GetValue.getRange() == (x / 100 * 100+100))
//    {
//        AddItem(LT.GetValue.GetTree(), x);
//    }
//    else
//    {
//         prev.SetNext(new Node<NodeTree>(new NodeTree(new BinNode<int>(x), x / 100 * 100 + 100), LT));
//    }

//}

//Node<NodeTree> LT = new Node<NodeTree>(new NodeTree(new BinNode<int>(0), 100));
//BuildListNodeTtee(145,LT);
//BuildListNodeTtee(160,LT);
//BuildListNodeTtee(155,LT);
//BuildListNodeTtee(205,LT);
//BuildListNodeTtee(405,LT);
//BuildListNodeTtee(210,LT);
//BuildListNodeTtee(50,LT);
//print(LT);  
#endregion


void aad(C___teacher.Stack<string> s, char c)
{
    C___teacher.Stack<string> temp = new C___teacher.Stack<string>();
    while (!s.IsEmpty())
    {
        temp.Push(s.Pop());
    }
    bool flag = false;
    while (!temp.IsEmpty())
    {
        if (!flag && temp.Top().Contains(c))
        {
            s.Push(temp.Pop() + c);
            flag = true;
        }
        else
            s.Push(temp.Pop());
    }
    if(!flag)
        s.Push(c.ToString());
}

bool check(C___teacher.Node<int> list, int[] arr)
{
    return false;
}
C___teacher.Node<int> copy(C___teacher.Node<int> list, int[] arr)
{
    return null;
}
C___teacher.Node<int> ex1(C___teacher.Node<int> list, int[] arr)
{
    while (list.GetNext.HasNext)
    {
        if (list.GetNext.GetValue == arr[0])
        {
            if(check(list,arr))
            {

            }

        }
    }
}