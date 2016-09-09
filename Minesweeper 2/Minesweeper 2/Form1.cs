using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Minesweeper_2
{
    public partial class Form1 : Form
    {
        #region variables
            //
            //Temp variables
            //
            int temp;
            bool response;
            //
            //Layout Variables
            //
            int mov_left,mov_right;
            //
            //Game Variables
            //
            Button [] mine_button;
            
            int[] mines;
        #endregion

        public Form1()
        {
            InitializeComponent();
            //
            //Initial Initialization
            //
            mine_button = new Button[81];
            mines = new int[10];
            mov_left = 0;
            mov_right = 0;
            mine_generator();
            //
            //Button Adds
            //
            for (int j = 0; j < 9; ++j)
            {
                for (int i = 0; i < 9; ++i)
                {
                    mine_button[i+(j*9)] = new Button();
                    //
                    //Group box
                    this.mine_button_box.Controls.Add(this.mine_button[i + (j * 9)]);
                    //
                    //Location
                    this.mine_button[i + (j * 9)].Location = new System.Drawing.Point(mov_left + 1, 9 + mov_right);
                    //
                    //Name to identify in logic
                    this.mine_button[i + (j * 9)].Name = ((j * 9) + (i + 1)).ToString();
                    this.mine_button[i + (j * 9)].Size = new System.Drawing.Size(23, 23);
                    //
                    //No Tab
                    this.mine_button[i + (j * 9)].TabStop = false;
                    //
                    //Testing Text
                    //this.mine_button[i].Text = ((i + 1) % 9).ToString();
                    this.mine_button[i + (j * 9)].Text = " ";
                    this.mine_button[i + (j * 9)].UseVisualStyleBackColor = true;
                    //
                    //Delegate
                    this.mine_button[i + (j * 9)].MouseClick += new MouseEventHandler(mine_button_click);
                    mov_left += 22;
                }
                mov_left = 0;
                mov_right = 22 * (j + 1);
            }
        }
        private void mine_button_click(Object oj,MouseEventArgs maj)
        {
            response=check_a_mine(oj,0);
            if (response)
            {
                game_over();
            }
            open_blocks(int.Parse(((Button)oj).Name),-1);
            //
            //Testing
            //Button use = (Button)oj;
            //use.Enabled = false;
            //MessageBox.Show(((Button)oj).Name + "\n" + maj.ToString() + "," + maj.Clicks.ToString());
        }
        private void mine_generator()
        {
            Random mine = new Random();
            for(int i=0;i<10;++i)
            {
                 mines[i] = mine.Next(1, 82);
                 for (int j = 0; j < i; ++j)
                 {
                     if ((mines[i] == mines[j]))
                     {
                         mines[i] = mine.Next(1, 82);
                         j = 0;
                     }
                 }
                   
            }
            Array.Sort(mines);
            String sos = "";
            for (int i = 0; i < 10;++i )
            {
                sos = sos+mines[i].ToString() + ",";
            }
            MessageBox.Show(sos);

            //Test Cases
            //mines[0] = 4;
            //mines[1] = 13;
            //mines[2] = 22;
            //mines[3] = 28;
            //mines[4] = 29;
            //mines[5] = 30;
            //mines[6] = 31;
            //mines[7] = 45;
            //mines[8] = 54;
            //mines[9] = 81;
           
        }
        private bool check_a_mine(Object oj,int value)
        {
            temp = 0;
            int first = 0, last = 9,middle;
            middle = (first + last) / 2;
            if (value == 0)
            {
                Button use = (Button)oj;
                temp = int.Parse(use.Name);
            }
            else
            {
                temp = value;
            }
                while (first <= last)
                {
                    if (mines[middle] < temp)
                        first = middle + 1;
                    else if (mines[middle] == temp)
                    {
                        break;
                    }
                    else
                    {
                        last = middle - 1;
                    }
                    middle = (first + last) / 2;
                }
                if (first > last)
                {
                    return false;
                    //Testing
                    //use.Text = (temp % 9).ToString();
                }
                else
                {
                    return true;
                    //
                    //Testing
                    //MessageBox.Show("Game Over");
                }
        }
        private void game_over()
        {
            for (int i = 0; i < 81; ++i)
            {
                mine_button[i].Enabled = false;
            }
            MessageBox.Show("Game Over");
        }
        private void open_blocks(int no,int leave)
        {
            int[,] mine_proximity = new int[8, 2];
            temp = no;
            int number_of_mines = 0;
            mine_proximity[0, 0] = temp - 10;
            mine_proximity[1, 0] = temp - 9;
            mine_proximity[2, 0] = temp - 8;
            mine_proximity[3, 0] = temp + 1;
            mine_proximity[4, 0] = temp + 10;
            mine_proximity[5, 0] = temp + 9;
            mine_proximity[6, 0] = temp + 8;
            mine_proximity[7, 0] = temp - 1;
            if ((temp >= 10) && (temp <= 72) && (temp % 9 != 0) && (temp % 9 != 1))
            {
                mine_proximity[0, 1] = 1;
                mine_proximity[1, 1] = 1;
                mine_proximity[2, 1] = 1;
                mine_proximity[3, 1] = 1;
                mine_proximity[4, 1] = 1;
                mine_proximity[5, 1] = 1;
                mine_proximity[6, 1] = 1;
                mine_proximity[7, 1] = 1;
            }
            else if (temp == 1)
            {
                mine_proximity[0, 1] = 0;
                mine_proximity[1, 1] = 0;
                mine_proximity[2, 1] = 0;
                mine_proximity[3, 1] = 1;
                mine_proximity[4, 1] = 1;
                mine_proximity[5, 1] = 1;
                mine_proximity[6, 1] = 0;
                mine_proximity[7, 1] = 0;
            }
            else if (temp == 9)
            {
                mine_proximity[0, 1] = 0;
                mine_proximity[1, 1] = 0;
                mine_proximity[2, 1] = 0;
                mine_proximity[3, 1] = 0;
                mine_proximity[4, 1] = 0;
                mine_proximity[5, 1] = 1;
                mine_proximity[6, 1] = 1;
                mine_proximity[7, 1] = 1;
            }
            else if (temp == 73)
            {
                mine_proximity[0, 1] = 0;
                mine_proximity[1, 1] = 1;
                mine_proximity[2, 1] = 1;
                mine_proximity[3, 1] = 1;
                mine_proximity[4, 1] = 0;
                mine_proximity[5, 1] = 0;
                mine_proximity[6, 1] = 0;
                mine_proximity[7, 1] = 0;
            }
            else if (temp == 81)
            {
                mine_proximity[0, 1] = 1;
                mine_proximity[1, 1] = 1;
                mine_proximity[2, 1] = 0;
                mine_proximity[3, 1] = 0;
                mine_proximity[4, 1] = 0;
                mine_proximity[5, 1] = 0;
                mine_proximity[6, 1] = 0;
                mine_proximity[7, 1] = 1;
            }
            else if ((temp <= 8) && (temp >= 2))
            {
                mine_proximity[0, 1] = 0;
                mine_proximity[1, 1] = 0;
                mine_proximity[2, 1] = 0;
                mine_proximity[3, 1] = 1;
                mine_proximity[4, 1] = 1;
                mine_proximity[5, 1] = 1;
                mine_proximity[6, 1] = 1;
                mine_proximity[7, 1] = 1;
            }
            else if ((temp <= 80) && (temp >= 74))
            {
                mine_proximity[0, 1] = 1;
                mine_proximity[1, 1] = 1;
                mine_proximity[2, 1] = 1;
                mine_proximity[3, 1] = 1;
                mine_proximity[4, 1] = 0;
                mine_proximity[5, 1] = 0;
                mine_proximity[6, 1] = 0;
                mine_proximity[7, 1] = 1;
            }
            else if (temp % 9 == 1)
            {
                mine_proximity[0, 1] = 0;
                mine_proximity[1, 1] = 1;
                mine_proximity[2, 1] = 1;
                mine_proximity[3, 1] = 1;
                mine_proximity[4, 1] = 1;
                mine_proximity[5, 1] = 1;
                mine_proximity[6, 1] = 0;
                mine_proximity[7, 1] = 0;
            }
            else if (temp % 9 == 0)
            {
                mine_proximity[0, 1] = 1;
                mine_proximity[1, 1] = 1;
                mine_proximity[2, 1] = 0;
                mine_proximity[3, 1] = 0;
                mine_proximity[4, 1] = 0;
                mine_proximity[5, 1] = 1;
                mine_proximity[6, 1] = 1;
                mine_proximity[7, 1] = 1;
            }
            if (leave >= 0)
            {
                mine_proximity[leave, 1] = 0;
            }
            
            for (int i = 0; i < 8; ++i)
            {
                if (mine_proximity[i, 1] == 1)
                { 
                    if(check_a_mine(null,mine_proximity[i,0]))
                    {
                        ++number_of_mines;
                    }
                }
            }
            
            if (number_of_mines > 0)
            {
                mine_button[no - 1].Text = number_of_mines.ToString();
                mine_button[no - 1].Enabled = false;
                //Test Case
                //MessageBox.Show(number_of_mines.ToString() + "," + no.ToString());
            }
            else
            {
                mine_button[no - 1].Enabled = false;
                //Test Case
                //MessageBox.Show(number_of_mines.ToString() + "," + no.ToString());
                //if ((no >= 10) && (no <= 72) && (no % 9 != 0) && (no % 9 != 1))
               // {
                    for (int i = 0; i < 8; ++i)
                    {
                        if ((mine_proximity[i, 1] == 1))
                        {
                            if (mine_button[mine_proximity[i, 0] - 1].Enabled)
                            {
                                open_blocks(mine_proximity[i, 0], (i + 4) % 8);
                            }
                        }
                    }
                //}
            }
            
        }



    }


}
