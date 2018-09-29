using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Math_Quiz
{
    public partial class Form1 : Form
    {
        //Create a random object for number selection later
        Random randomizer = new Random();

        //Create two variables to store the numbers later
        int addend1, addend2, minuend, subtrahend, multiplicand, multiplier, dividend, divisor;

        //Variable to control time
        int timeLeft;

        public void StartTheQuiz()
        {
            //Select our random numbers
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            //Replace the question marks in the form with the numbers
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            //Reset the value in the answer box to zero
            sum.Value = 0;

            //Fill in the subtraction problem
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            //Fill in the multiplication problem
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            //Fill in the division problem
            divisor = randomizer.Next(2, 11);
            dividend = randomizer.Next(2, 11) * divisor;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            //Start the timer
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                //If CheckTheAnswer returns true then the user succeeded
                timer1.Stop();
                MessageBox.Show("You got all the answers right!", "Congratulations!");
                timeLabel.BackColor = Color.White;
                startButton.Enabled = true;
            }
            else if(timeLeft > 0)
            {
                //Change label to new time left
                timeLeft--;
                timeLabel.Text = $"{timeLeft} seconds";
                if (timeLeft == 5)
                {
                    timeLabel.BackColor = Color.Red;
                }
            }
            else
            {
                //When user runs out of time display message and fill in correct answers
                timer1.Stop();
                timeLabel.Text = "Times Up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                timeLabel.BackColor = Color.White;
            }
        }

        private bool CheckTheAnswer()
        {
            return addend1 + addend2 == sum.Value 
                && minuend - subtrahend == difference.Value
                && multiplicand * multiplier == product.Value 
                && dividend / divisor == quotient.Value ? true : false;
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            //Select the whole answer
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        public Form1()
        {
            InitializeComponent();
            //Display the Date
            dateLabel.Text = DateTime.Now.ToString("dd MMMM yyyy");
        }
    }
}
