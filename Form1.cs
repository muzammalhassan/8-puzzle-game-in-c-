using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace AI_assignment1
{
    public partial class Form1 : Form
    {
        AStar astar = new AStar();
        PQueue Pqueue = new PQueue();
        IDS ids = new IDS();
        Stack stack = new Stack();
        Thread  trFIRST, trPbfs, trIDS, trAStar, trReset;
        Queue<int> queue = new Queue<int>();
        Stopwatch sw = new Stopwatch();
        bool ResetRunner = false, BfsRunCheck = false, PbfsRunCheck = false, IdsRunCheck = false, AstarRunCheck = false, ResetRunCheck = false;

        //*****************************************************

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            trFIRST = new Thread(new ThreadStart(myThread2));
            trFIRST.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            UserMoveBtn.MoveBTN(button1, button2, button4, panel1.BackColor);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            UserMoveBtn.MoveBTN(button2, button1, button3, button5, panel1.BackColor);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserMoveBtn.MoveBTN(button3, button2, button6, panel1.BackColor);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UserMoveBtn.MoveBTN(button4, button7, button5, button1, panel1.BackColor);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UserMoveBtn.MoveBTN(button5, button8, button6, button4, button2, panel1.BackColor);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserMoveBtn.MoveBTN(button6, button9, button5, button3, panel1.BackColor);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UserMoveBtn.MoveBTN(button7, button8, button4, panel1.BackColor);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UserMoveBtn.MoveBTN(button8, button9, button7, button5, panel1.BackColor);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            UserMoveBtn.MoveBTN(button9, button8, button6, panel1.BackColor);
        }
        //////////////////////////////////////////////////////
        private void btn_AStar_Click(object sender, EventArgs e)
        {
            if (!BfsRunCheck && !PbfsRunCheck && !IdsRunCheck && !AstarRunCheck)
            {
                ResetRunner = true;

                trFIRST.Abort();
                label3_Info.Font = new Font("Century Gothic", 16);
                lbl_depth_ans.Text = 0.ToString();
                lbl_Gnodes_ans.Text = 0.ToString();
               // lbl_Snodes_ans.Text = 0.ToString();
               astar.i = 0;
                astar.j = 0;


                try
                {
                    astar.PuzzleArrayFunc(button1, button2, button3, button4, button5, button6, button7, button8, button9);
                    if (astar.Goal(Pqueue.Peek().PuzzleCLASS))
                    {
                        label3_Info.ForeColor = Color.LightSkyBlue;
                        label3_Info.Text = "Goal state !";
                        Pqueue.Clear();

                    }
                    else
                    {
                        trAStar = new Thread(new ThreadStart(ThreadAStar));
                        trAStar.Start();
                        AstarRunCheck = true;

                        btn_Reset.ForeColor = Color.DarkRed;
                        btn_Reset.BackColor = Color.DarkGray;
                        btn_Reset.FlatAppearance.BorderColor = Color.Silver;
                        btn_Reset.Text = "Stop";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        void ThreadAStar()
        {
            List<string> temp;
            NodeAStar nodeResult;

            label3_Info.ForeColor = Color.DarkTurquoise;
            label3_Info.Text = "Searching ...";
            sw.Start();
            nodeResult = astar.AstarSearch();
            label3_Info.Text = "Puzzle Solved!";
            sw.Stop();

            lbl_time_ans.Text = sw.Elapsed.TotalMilliseconds.ToString() + "  ms";
            sw.Reset();
            int moves = 0;
            temp = astar.PathFinder(nodeResult);
            foreach (string item in temp)
            {
                moves++;
            }

           // lbl_Snodes_ans.Text = astar.i.ToString();
            lbl_Gnodes_ans.Text = PQueue.count.ToString();
            lbl_depth_ans.Text = temp.Count.ToString();
            lbmoves_ans.Text = moves.ToString();

            MachineMove.path = temp.ToArray();
            temp.Clear();

            label3_Info.ForeColor = Color.LightPink;
            label3_Info.Text = "Puzzle Solved!";
            Thread.Sleep(300);
            label3_Info.Text = "";
            Thread.Sleep(300);
            label3_Info.Text = "Puzzle Solved!";
            Thread.Sleep(300);
            label3_Info.Text = "";
            Thread.Sleep(300);
            label3_Info.Text = "Puzzle Solved!";

            label3_Info.ForeColor = Color.Cyan;
            label3_Info.Text = "Showing Solution .";
            Thread.Sleep(300);
            label3_Info.Text = "Showing Solution ..";
            Thread.Sleep(300);
            label3_Info.Text = "Showing Solution ...";

            MachineMove.Action(button1, button2, button3, button4, button5, button6, button7, button8, button9, panel1.BackColor);
            label3_Info.ForeColor = Color.YellowGreen;
            label3_Info.Text = "Machine Win !";
            astar.NodeNumber.Clear();

            Pqueue.Clear();
            astar.i = 0;
            astar.j = 0;
            PQueue.count = 0;
            trReset = new Thread(new ThreadStart(ThreadReset));
            trReset.Start();

        }
        
//////////////////////////////////////////////////////////////////////

        void myThread2()
        {
            label3_Info.ForeColor = Color.SkyBlue;
            label3_Info.Font = new Font("Century Gothic", 14);
            while (true)
            {
                label3_Info.ForeColor = Color.SkyBlue;
                label3_Info.Font = new Font("Century Gothic", 14);
                label3_Info.Text = "Set puzzle initial state !";
                Thread.Sleep(1000);
                label3_Info.Text = "";
                Thread.Sleep(200);

            }
        }

        private void btn_IDS_Click(object sender, EventArgs e)
        {
            if (!BfsRunCheck && !PbfsRunCheck && !IdsRunCheck && !AstarRunCheck)
            {
                ResetRunner = true;

                trFIRST.Abort();
                label3_Info.Font = new Font("Century Gothic", 16);
                lbl_depth_ans.Text = 0.ToString();
                lbl_Gnodes_ans.Text = 0.ToString();
                //lbl_Snodes_ans.Text = 0.ToString();
                lbmoves_ans.Text = 0.ToString();
                ids.i = 0;
                ids.j = 0;

                try
                {
                    ids.PuzzleArrayFunc(button1, button2, button3, button4, button5, button6, button7, button8, button9);
                    if (ids.Goal(stack.Peek().PuzzleCLASS))
                    {
                        label3_Info.ForeColor = Color.LightSkyBlue;
                        label3_Info.Text = "Goal state !";
                        stack.Clear();

                    }
                    else
                    {
                        trIDS = new Thread(new ThreadStart(ThreadIDS));
                        trIDS.Start();
                        IdsRunCheck = true;

                        btn_Reset.ForeColor = Color.DarkRed;
                        btn_Reset.BackColor = Color.DarkGray;
                        btn_Reset.FlatAppearance.BorderColor = Color.Silver;
                        btn_Reset.Text = "Stop";


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
    
        }
        void ThreadIDS()
        {
            List<string> temp;
            NodeIds nodeResult;

            label3_Info.ForeColor = Color.DarkTurquoise;
            label3_Info.Text = "Searching ...";
            sw.Start();
            nodeResult = ids.IdsSearchFunction();
            label3_Info.Text = "Puzzle Solved!";
            sw.Stop();

            lbl_time_ans.Text = sw.Elapsed.TotalMilliseconds.ToString() + "  ms";
            sw.Reset();

            temp = ids.PathFinder(nodeResult);
            int moves = 0;
            foreach (string item in temp)
            {
                moves++;
            }

           // lbl_Snodes_ans.Text = ids.i.ToString();
            lbl_Gnodes_ans.Text = Stack.count.ToString();
            lbl_depth_ans.Text = temp.Count.ToString();
            lbmoves_ans.Text = moves.ToString();

            MachineMove.path = temp.ToArray();
            temp.Clear();

            label3_Info.ForeColor = Color.LightPink;
            label3_Info.Text = "Puzzle Solved!";
            Thread.Sleep(300);
            label3_Info.Text = "";
            Thread.Sleep(300);
            label3_Info.Text = "Puzzle Solved!";
            Thread.Sleep(300);
            label3_Info.Text = "";
            Thread.Sleep(300);
            label3_Info.Text = "Puzzle Solved!";

            label3_Info.ForeColor = Color.Cyan;
            label3_Info.Text = "Showing Solution .";
            Thread.Sleep(300);
            label3_Info.Text = "Showing Solution ..";
            Thread.Sleep(300);
            label3_Info.Text = "Showing Solution ...";

            MachineMove.Action(button1, button2, button3, button4, button5, button6, button7, button8, button9, panel1.BackColor);
            label3_Info.ForeColor = Color.YellowGreen;
            label3_Info.Text = "Machine Win !";
            ids.NodeNumber.Clear();
            Stack.count = 0;

            stack.Clear();
            ids.i = 0;
            ids.j = 0;

            trReset = new Thread(new ThreadStart(ThreadReset));
            trReset.Start();


        }
        //////////////////////////////////////////////////////////////////////////////
        private void btn_Reset_Click(object sender, EventArgs e)
        {
            if (ResetRunner)
            {
                if (IdsRunCheck) { trIDS.Abort(); IdsRunCheck = false; }
                if (AstarRunCheck) { trAStar.Abort(); AstarRunCheck = false; }
                if (ResetRunCheck) { trReset.Abort(); ResetRunCheck = false; }

                btn_Reset.Text = "Reset";
                btn_Reset.BackColor = Color.RoyalBlue;
                //btn_Reset.ForeColor = btn_Start.ForeColor;
                trFIRST = new Thread(new ThreadStart(myThread2));
                trFIRST.Start();
                lbl_depth_ans.Text = 0.ToString();
                lbl_Gnodes_ans.Text = 0.ToString();
               // lbl_Snodes_ans.Text = 0.ToString();
                lbl_time_ans.Text = 0.ToString();
                lbmoves_ans.Text = 0.ToString();
       
                stack.Clear();
                ids.i = 0;
                ids.j = 0;
                ids.NodeNumber.Clear();
                Stack.count = 0;

                queue.Clear();
       
                Pqueue.Clear();
                astar.i = 0;
                astar.j = 0;
                astar.NodeNumber.Clear();
                PQueue.count = 0;

                GC.Collect();

                ResetRunner = false;
            }

        }
        void ThreadReset()
        {
            ResetRunCheck = true;
            btn_Reset.Text = "Reset";
            while (true)
            {
                btn_Reset.FlatAppearance.BorderColor = Color.Silver;
                btn_Reset.BackColor = Color.RoyalBlue;
                btn_Reset.ForeColor = Color.DarkGreen;
                Thread.Sleep(500);
                btn_Reset.FlatAppearance.BorderColor = Color.DarkGray;
                btn_Reset.BackColor = Color.RoyalBlue;
                // btn_Reset.ForeColor = btn_Start.ForeColor;
                Thread.Sleep(500);
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }


/////////////////////////////////////////////////////////////////////////////////////
        

////////////////////////////////////////

    }
}
