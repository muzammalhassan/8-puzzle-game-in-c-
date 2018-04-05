using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_assignment1
{
    class NodeAStar
    {
        private Int16[,] PuzzleClass;
        private UInt64 NodeNumber;
        private UInt32 NodeDepth;
        private int Depth;
        private int fn;
        private string ActionClass;
        private NodeAStar Next;
        private NodeAStar Back;
        private NodeAStar Parent;
        public NodeAStar()//constructor
        {
            PuzzleClass = null;
            ActionClass = null;
            Next = null;
            Parent = null;
        }
        //********************* 
        public NodeAStar(Int16[,] element)//parametrized constructor
        {
            PuzzleClass = element;
            ActionClass = null;
            Next = null;
            Parent = null;
        }
        //********************
        public NodeAStar(Int16[,] element, string act, NodeAStar prnt)//another parameterized constructor
        {
            PuzzleClass = element;
            ActionClass = act;
            Next = null;
            Parent = prnt;
        }
        //********************
        public Int16[,] PuzzleCLASS//puzzle class having an array of int
        {
            get
            {
                return PuzzleClass;
            }
            set
            {
                PuzzleClass = value;
            }

        }

        public string ActionCLASS//action class
        {
            get
            {
                return ActionClass;
            }
            set
            {
                ActionClass = value;
            }

        }
        public NodeAStar NEXT//next class
        {
            get
            {
                return Next;
            }
            set
            {
                Next = value;
            }

        }
        public NodeAStar BACK//back class
        {
            get
            {
                return Back;
            }
            set
            {
                Back = value;
            }

        }
        public NodeAStar PARENT//parent class
        {
            get
            {
                return Parent;
            }
            set
            {
                Parent = value;
            }

        }
        public UInt64 NodeNUMBER//node number class
        {
            get
            {
                return NodeNumber;
            }
            set
            {
                NodeNumber = value;
            }

        }
        public UInt32 NodeDEPTH//node depth class
        {
            get
            {
                return NodeDepth;
            }
            set
            {
                NodeDepth = value;
            }

        }

        public int Fn//it is a function
        {
            get
            {
                return fn;
            }
            set
            {
                fn = value;
            }

        }
        public int DEPTH//it is a depth function
        {
            get
            {
                return Depth;
            }
            set
            {
                Depth = value;
            }

        }

    }//end of class NodeIds

    //*****************************************************
    class PQueue//it is for keeping record that what we have done to solve the puzzler
    {
        private static NodeAStar rear;
        private static NodeAStar front;
        public static UInt64 count = 0;

        public PQueue() { }//constructor

        public void Enqueue(Int16[,] pzl, string act, NodeAStar parent, int hn)//(array of puzzel values,action,parent,huristic value)
        {
            NodeAStar newNode = new NodeAStar(pzl, act, parent);
            if (parent != null)
            {
                newNode.DEPTH = parent.DEPTH + 1;//calculating depth of new node
                newNode.Fn = newNode.DEPTH + hn;//calculating huristic function value of newnode
            }


            if (rear == null)
            {
                newNode.DEPTH = 0;
                newNode.Fn = hn;
                front = newNode;
                newNode.NodeNUMBER = count;
                rear = front;

            }
            else
            {
                NodeAStar Current = front;

                while (newNode.Fn > Current.Fn && Current.NEXT != null)//traversing the pointer to current node starting from front node
                {
                    Current = Current.NEXT;
                }

                if (Current.BACK == null && Current.NEXT == null)//first node that is value of front pointer
                {
                    newNode.DEPTH = parent.DEPTH + 1;
                    newNode.Fn = newNode.DEPTH + hn;
                    front = newNode;
                    front.NEXT = Current;
                    Current.BACK = front;
                    rear = front.NEXT;//front and rear pointing to the same value
                }
                else if (Current.BACK == null && Current.NEXT != null)
                {
                    newNode.DEPTH = parent.DEPTH + 1;
                    front = newNode;
                    front.NEXT = Current;
                    Current.BACK = front;//if there is a space befor current node
                }
                else if (Current.NEXT == null && newNode.Fn > Current.Fn)
                {
                    newNode.DEPTH = parent.DEPTH + 1;
                    Current.NEXT = newNode;
                    newNode.BACK = Current;
                    rear = newNode;//placing nodes having high huristic values at the end of queue
                }
                else
                {
                    newNode.DEPTH = parent.DEPTH + 1;
                    newNode.NEXT = Current;
                    Current.BACK.NEXT = newNode;
                    newNode.BACK = Current.BACK;
                    Current.BACK = newNode;//making last values new node
                }
                newNode.NodeNUMBER = count;//counting the number of nodes in our state space

            }
            count++;
        }
        public NodeAStar Dequeue()//removing node having low huristic function value
        {
            if (front == null)
            {
                throw new Exception("Queue is Empty");
            }
            NodeAStar result = front;
            front = front.NEXT;
            return result;
        }

        public NodeAStar Peek()//getting node having low huristic function value
        {
            if (front == null)
            {
                throw new Exception("Queue is Empty");
            }
            NodeAStar result = front;
            return result;
        }
        void InsertionSort(NodeAStar Node)//after a deque operation we neet to perform a sorting operation to keep pq
        {
            NodeAStar Current = front;

            while (Node.Fn < Current.Fn || Current.NEXT != null)
            {
                Current = Current.NEXT;
            }

            if (Current.NEXT == null)
            {
                Current.NEXT = Node;
            }
            else
            {
                Node.NEXT = Current.NEXT;
                Current.NEXT = Node;
            }
        }
        public void Clear()//clearing queue
        {
            front = rear = null;

        }
        public NodeAStar Front//front class
        {
            get
            {
                return front;
            }
            set
            {
                front = value;
            }

        }
        public NodeAStar Rear//rear class
        {
            get
            {
                return rear;
            }
            set
            {
                rear = value;
            }

        }

    }
    //end of class PQueue
    class AStar
    {
        PQueue Pqueue = new PQueue();
        public Int16[,] puzzleIDS = new Int16[3, 3];
        public List<string> PathList = new List<string>();
        public List<UInt64> NodeNumber = new List<UInt64>();
        public int i = 0, j = 0;

        //*****************************************************

        public void PuzzleArrayFunc(Button btn1, Button btn2, Button btn3, Button btn4, Button btn5, Button btn6, Button btn7, Button btn8, Button btn9)
        {
            //filling the 3*3 integer array by getting the button values and change them to int
            if (btn1.Text == "")
            {
                puzzleIDS[0, 0] = 0;
            }
            else
            {
                puzzleIDS[0, 0] = Convert.ToInt16(btn1.Text);
            }

            if (btn2.Text == "")
            {
                puzzleIDS[0, 1] = 0;
            }
            else
            {
                puzzleIDS[0, 1] = Convert.ToInt16(btn2.Text);
            }

            if (btn3.Text == "")
            {
                puzzleIDS[0, 2] = 0;
            }
            else
            {
                puzzleIDS[0, 2] = Convert.ToInt16(btn3.Text);
            }

            if (btn4.Text == "")
            {
                puzzleIDS[1, 0] = 0;
            }
            else
            {
                puzzleIDS[1, 0] = Convert.ToInt16(btn4.Text);
            }

            if (btn5.Text == "")
            {
                puzzleIDS[1, 1] = 0;
            }
            else
            {
                puzzleIDS[1, 1] = Convert.ToInt16(btn5.Text);
            }

            if (btn6.Text == "")
            {
                puzzleIDS[1, 2] = 0;
            }
            else
            {
                puzzleIDS[1, 2] = Convert.ToInt16(btn6.Text);
            }

            if (btn7.Text == "")
            {
                puzzleIDS[2, 0] = 0;
            }
            else
            {
                puzzleIDS[2, 0] = Convert.ToInt16(btn7.Text);
            }

            if (btn8.Text == "")
            {
                puzzleIDS[2, 1] = 0;
            }
            else
            {
                puzzleIDS[2, 1] = Convert.ToInt16(btn8.Text);
            }

            if (btn9.Text == "")
            {
                puzzleIDS[2, 2] = 0;
            }
            else
            {
                puzzleIDS[2, 2] = Convert.ToInt16(btn9.Text);
            }

            int hn = Hn(puzzleIDS);
            Pqueue.Enqueue(puzzleIDS, null, null, hn);
        }

        //*****************************************************
        public bool Goal(Int16[,] puzzleArray)//compring our current state to goal if goal then finish searching
        {
            //i++; useless statement
            if (puzzleArray[0, 0] == 1 &&
                     puzzleArray[0, 1] == 2 &&
                     puzzleArray[0, 2] == 3 &&
                     puzzleArray[1, 0] == 4 &&
                     puzzleArray[1, 1] == 5 &&
                     puzzleArray[1, 2] == 6 &&
                     puzzleArray[2, 0] == 7 &&
                     puzzleArray[2, 1] == 8 &&
                     puzzleArray[2, 2] == 0)//goal discription and compring our current state to goal if goal then finish searching
            {
                return true;
            }

            return false;

        }

        //*****************************************************
        //Astar search untill solution not found
        public NodeAStar AstarSearch()
        {
            NodeAStar pz;
            pz = Pqueue.Peek();//getting node having low fn value
            while (Goal(pz.PuzzleCLASS) == false)//comparint node state with goal
            {
                addNode(pz);//calculating the huristic value and push the state of puzzle into the pq
                pz = Pqueue.Dequeue();//removing node having low huristic value from pq

            }

            return pz;//return the last node or goal node

        }

        //*****************************************************
        //addnode is function that gives us the possible moves that we can have 
        //according to the blank space and push all the possiblities into the pq
        public void addNode(NodeAStar puzzle)
        {
            int hn;//huristic value
            string id = SearchArray((Int16[,])puzzle.PuzzleCLASS);//getting the place of zero or blank value
            Int16 temp;//a temprary value
            switch (id)
            {
                case "00"://blank space at the start of array
                    {
                        //we can move the blank to down or right 
                        //so, down move and its hn value pushed into queue
                        Int16[,] pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);//making a copy of puzzle values
                        temp = pz1[1, 0];
                        pz1[1, 0] = pz1[0, 0];
                        pz1[0, 0] = temp;//performing down action
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Down", puzzle, hn);
                        }
                        RemoveVisited(puzzle);//after performing action on original node and pushing the resulting node(copy node) to queue, we have to remove the original from the queue


                        //right move and its hn value pushed into pq
                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[0, 1];
                        pz1[0, 1] = pz1[0, 0];
                        pz1[0, 0] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Right", puzzle, hn);
                        }


                        break;
                    }
                case "01":
                    {
                        Int16[,] pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[1, 1];
                        pz1[1, 1] = pz1[0, 1];
                        pz1[0, 1] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Down", puzzle, hn);
                        }
                        RemoveVisited(puzzle);

                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[0, 0];
                        pz1[0, 0] = pz1[0, 1];
                        pz1[0, 1] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Left", puzzle, hn);
                        }


                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[0, 2];
                        pz1[0, 2] = pz1[0, 1];
                        pz1[0, 1] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Right", puzzle, hn);
                        }


                        break;
                    }
                case "02":
                    {
                        Int16[,] pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[1, 2];
                        pz1[1, 2] = pz1[0, 2];
                        pz1[0, 2] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Down", puzzle, hn);
                        }
                        RemoveVisited(puzzle);

                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[0, 1];
                        pz1[0, 1] = pz1[0, 2];
                        pz1[0, 2] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Left", puzzle, hn);
                        }


                        break;
                    }
                case "10":
                    {
                        Int16[,] pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[2, 0];
                        pz1[2, 0] = pz1[1, 0];
                        pz1[1, 0] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Down", puzzle, hn);
                        }
                        RemoveVisited(puzzle);


                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[0, 0];
                        pz1[0, 0] = pz1[1, 0];
                        pz1[1, 0] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Up", puzzle, hn);
                        }



                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[1, 1];
                        pz1[1, 1] = pz1[1, 0];
                        pz1[1, 0] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Right", puzzle, hn);
                        }


                        break;
                    }
                case "11":
                    {
                        Int16[,] pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[2, 1];
                        pz1[2, 1] = pz1[1, 1];
                        pz1[1, 1] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Down", puzzle, hn);
                        }
                        RemoveVisited(puzzle);

                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[0, 1];
                        pz1[0, 1] = pz1[1, 1];
                        pz1[1, 1] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Up", puzzle, hn);
                        }



                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[1, 0];
                        pz1[1, 0] = pz1[1, 1];
                        pz1[1, 1] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Left", puzzle, hn);
                        }


                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[1, 2];
                        pz1[1, 2] = pz1[1, 1];
                        pz1[1, 1] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Right", puzzle, hn);
                        }




                        break;
                    }
                case "12":
                    {
                        Int16[,] pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[2, 2];
                        pz1[2, 2] = pz1[1, 2];
                        pz1[1, 2] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Down", puzzle, hn);
                        }
                        RemoveVisited(puzzle);

                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[0, 2];
                        pz1[0, 2] = pz1[1, 2];
                        pz1[1, 2] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Up", puzzle, hn);
                        }



                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[1, 1];
                        pz1[1, 1] = pz1[1, 2];
                        pz1[1, 2] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Left", puzzle, hn);
                        }


                        break;
                    }
                case "20":
                    {

                        Int16[,] pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[1, 0];
                        pz1[1, 0] = pz1[2, 0];
                        pz1[2, 0] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Up", puzzle, hn);
                        }
                        RemoveVisited(puzzle);

                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[2, 1];
                        pz1[2, 1] = pz1[2, 0];
                        pz1[2, 0] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Right", puzzle, hn);
                        }



                        break;
                    }
                case "21":
                    {
                        Int16[,] pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[1, 1];
                        pz1[1, 1] = pz1[2, 1];
                        pz1[2, 1] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Up", puzzle, hn);
                        }
                        RemoveVisited(puzzle);


                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[2, 0];
                        pz1[2, 0] = pz1[2, 1];
                        pz1[2, 1] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Left", puzzle, hn);

                        }


                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[2, 2];
                        pz1[2, 2] = pz1[2, 1];
                        pz1[2, 1] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Right", puzzle, hn);

                        }


                        break;
                    }
                case "22":
                    {
                        Int16[,] pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[1, 2];
                        pz1[1, 2] = pz1[2, 2];
                        pz1[2, 2] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Up", puzzle, hn);

                        }
                        RemoveVisited(puzzle);

                        pz1 = new Int16[3, 3];
                        pz1 = Clone(pz1, puzzle);
                        temp = pz1[2, 1];
                        pz1[2, 1] = pz1[2, 2];
                        pz1[2, 2] = temp;
                        if (SimilarFinder(pz1))
                        {
                            hn = Hn(pz1);
                            Pqueue.Enqueue(pz1, "Left", puzzle, hn);

                        }


                        break;
                    }
            }
        }

        //*****************************************************

        private Int16[,] Clone(Int16[,] pz1, NodeAStar puzzle)//makes a copy of given puzzle array values
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    pz1[i, j] = puzzle.PuzzleCLASS[i, j];
                }
            }
            return pz1;
        }

        //*****************************************************
        //checks is there a blank value or not if yes then return the position of that array
        public string SearchArray(Int16[,] puzzleSearch)//(3*3 puzzle array)
        {

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (puzzleSearch[i, j] == 0)
                        return i.ToString() + j.ToString();
                }
            }
            return "";
        }

        //*****************************************************
        //function that gives a list of action on a node and  node# and parent of node
        public List<string> PathFinder(NodeAStar node)
        {
            while (node.PARENT != null)
            {
                PathList.Add(node.ActionCLASS);
                NodeNumber.Add(node.NodeNUMBER);
                node = node.PARENT;
                j++;
            }

            return PathList;
        }

        //*****************************************************
        public int DepthFinder()//gives the depth of node
        {
            return j;
        }

        //*****************************************************

        int Hn(Int16[,] pzl)//calculating huristic values
        {
            int hn = 0;

            for (int i = 1; i < 9; i++)
            {
                switch (i)
                {

                    case 1://checking moves of 1 and calculating hvalue for tile moving having value "1"
                        {

                            if (pzl[0, 1] == 1)
                                hn += 1;
                            else if (pzl[0, 2] == 1)
                                hn += 2;
                            else if (pzl[1, 0] == 1)
                                hn += 1;
                            else if (pzl[1, 1] == 1)
                                hn += 2;
                            else if (pzl[1, 2] == 1)
                                hn += 3;
                            else if (pzl[2, 0] == 1)
                                hn += 2;
                            else if (pzl[2, 1] == 1)
                                hn += 3;
                            else if (pzl[2, 2] == 1)
                                hn += 4;

                            break;
                        }
                    case 2://checking moves of 2 and calculating hvalue for tile moving having value "2"
                        {
                            if (pzl[0, 0] == 2)
                                hn += 1;
                            else if (pzl[0, 2] == 2)
                                hn += 1;
                            else if (pzl[1, 0] == 2)
                                hn += 2;
                            else if (pzl[1, 1] == 2)
                                hn += 1;
                            else if (pzl[1, 2] == 2)
                                hn += 2;
                            else if (pzl[2, 0] == 2)
                                hn += 3;
                            else if (pzl[2, 1] == 2)
                                hn += 2;
                            else if (pzl[2, 2] == 2)
                                hn += 3;

                            break;
                        }
                    case 3://checking moves of 3 and calculating hvalue for tile moving having value "3"
                        {
                            if (pzl[0, 0] == 3)
                                hn += 2;
                            else if (pzl[0, 1] == 3)
                                hn += 1;
                            else if (pzl[1, 0] == 3)
                                hn += 3;
                            else if (pzl[1, 1] == 3)
                                hn += 2;
                            else if (pzl[1, 2] == 3)
                                hn += 1;
                            else if (pzl[2, 0] == 3)
                                hn += 4;
                            else if (pzl[2, 1] == 3)
                                hn += 3;
                            else if (pzl[2, 2] == 3)
                                hn += 2;

                            break;
                        }
                    case 4://checking moves of 4 and calculating hvalue for tile moving having value "4"
                        {
                            if (pzl[0, 0] == 4)
                                hn += 1;
                            else if (pzl[0, 1] == 4)
                                hn += 2;
                            else if (pzl[0, 2] == 4)
                                hn += 3;
                            else if (pzl[1, 1] == 4)
                                hn += 1;
                            else if (pzl[1, 2] == 4)
                                hn += 2;
                            else if (pzl[2, 0] == 4)
                                hn += 1;
                            else if (pzl[2, 1] == 4)
                                hn += 2;
                            else if (pzl[2, 2] == 4)
                                hn += 3;

                            break;
                        }
                    case 5://checking moves of 5 and calculating hvalue for tile moving having value "5"
                        {
                            if (pzl[0, 0] == 5)
                                hn += 2;
                            else if (pzl[0, 1] == 5)
                                hn += 1;
                            else if (pzl[0, 2] == 5)
                                hn += 2;
                            else if (pzl[1, 0] == 5)
                                hn += 1;
                            else if (pzl[1, 2] == 5)
                                hn += 1;
                            else if (pzl[2, 0] == 5)
                                hn += 2;
                            else if (pzl[2, 1] == 5)
                                hn += 1;
                            else if (pzl[2, 2] == 5)
                                hn += 2;

                            break;
                        }
                    case 6://checking moves of 6 and calculating hvalue for tile moving having value "6"
                        {
                            if (pzl[0, 0] == 6)
                                hn += 3;
                            else if (pzl[0, 1] == 6)
                                hn += 2;
                            else if (pzl[0, 2] == 6)
                                hn += 1;
                            else if (pzl[1, 0] == 6)
                                hn += 2;
                            else if (pzl[1, 1] == 6)
                                hn += 1;
                            else if (pzl[2, 0] == 6)
                                hn += 3;
                            else if (pzl[2, 1] == 6)
                                hn += 2;
                            else if (pzl[2, 2] == 6)
                                hn += 1;

                            break;
                        }
                    case 7://checking moves of 7 and calculating hvalue for tile moving having value "7"
                        {
                            if (pzl[0, 0] == 7)
                                hn += 2;
                            else if (pzl[0, 1] == 7)
                                hn += 3;
                            else if (pzl[0, 2] == 7)
                                hn += 4;
                            else if (pzl[1, 0] == 7)
                                hn += 1;
                            else if (pzl[1, 1] == 7)
                                hn += 2;
                            else if (pzl[1, 2] == 7)
                                hn += 3;
                            else if (pzl[2, 1] == 7)
                                hn += 1;
                            else if (pzl[2, 2] == 7)
                                hn += 2;

                            break;
                        }
                    case 8://checking moves of 8 and calculating hvalue for tile moving having value "8"
                        {
                            if (pzl[0, 0] == 8)
                                hn += 3;
                            else if (pzl[0, 1] == 8)
                                hn += 2;
                            else if (pzl[0, 2] == 8)
                                hn += 3;
                            else if (pzl[1, 0] == 8)
                                hn += 2;
                            else if (pzl[1, 1] == 8)
                                hn += 1;
                            else if (pzl[1, 2] == 8)
                                hn += 2;
                            else if (pzl[2, 0] == 8)
                                hn += 1;
                            else if (pzl[2, 2] == 8)
                                hn += 1;

                            break;
                        }
                }
            }

            return hn;
        }

        //*****************************************************
        void RemoveVisited(NodeAStar Visited)//removing visited node from pq
        {
            if (Visited.BACK == null && Visited.NEXT == null)
            {
                //do nothing
            }
            else if (Visited.BACK == null && Visited.NEXT != null)
            {
                Pqueue.Front = Visited.NEXT;
                Visited.NEXT.BACK = null;
                Visited.BACK = Visited.NEXT = null;//????
            }
            else if (Visited.NEXT == null)
            {
                Pqueue.Rear = Visited.BACK;
                Visited.BACK.NEXT = null;
                Visited.BACK = Visited.NEXT = null;//removing dangling pointer
            }
            else
            {
                Visited.BACK.NEXT = Visited.NEXT;
                Visited.NEXT.BACK = Visited.BACK;
                Visited.BACK = Visited.NEXT = null;
            }

        }

        //*****************************************************
        bool SimilarFinder(Int16[,] pzl)
        {
            //checking is the specific action already performed or not from the pq
            NodeAStar current = Pqueue.Front;
            while (current != null)
            {
                if (SimilarArray(pzl, current.PuzzleCLASS))
                {
                    return false;
                }
                current = current.NEXT;
            }
            return true;
        }

        //*****************************************************
        bool SimilarArray(Int16[,] pz1, Int16[,] pz2)
        {//checking the equality of two puzzles
            int Count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (pz1[i, j] == pz2[i, j])
                        Count++;
                }
            }

            if (Count == 9)
                return true;

            return false;
        }
    }
}
