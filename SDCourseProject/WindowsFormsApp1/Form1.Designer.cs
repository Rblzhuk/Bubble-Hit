
namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartGameButton = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.EndGameButton = new System.Windows.Forms.Button();
            this.courseProject1 = new SDCourseProject.CourseProject();
            this.SuspendLayout();
            // 
            // StartGameButton
            // 
            this.StartGameButton.Location = new System.Drawing.Point(22, 106);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(129, 23);
            this.StartGameButton.TabIndex = 1;
            this.StartGameButton.Text = "Начать игру";
            this.StartGameButton.UseVisualStyleBackColor = true;
            this.StartGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(588, 106);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(33, 13);
            this.label.TabIndex = 2;
            this.label.Text = "Счёт:";
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Location = new System.Drawing.Point(625, 105);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(54, 13);
            this.ScoreLabel.TabIndex = 3;
            this.ScoreLabel.Text = "значение";
            // 
            // EndGameButton
            // 
            this.EndGameButton.Location = new System.Drawing.Point(22, 176);
            this.EndGameButton.Name = "EndGameButton";
            this.EndGameButton.Size = new System.Drawing.Size(129, 23);
            this.EndGameButton.TabIndex = 5;
            this.EndGameButton.Text = "Закончить игру";
            this.EndGameButton.UseVisualStyleBackColor = true;
            this.EndGameButton.Click += new System.EventHandler(this.EndGameButton_Click);
            // 
            // courseProject1
            // 
            this.courseProject1.ColorDefeatLine = System.Drawing.Color.Empty;
            this.courseProject1.ColorPlayFieldFrame = System.Drawing.Color.Maroon;
            this.courseProject1.ColPlayBall = 0;
            this.courseProject1.FifthBallColor = System.Drawing.Color.Black;
            this.courseProject1.FirstBallColor = System.Drawing.Color.Transparent;
            this.courseProject1.FourthBallColor = System.Drawing.Color.LightSkyBlue;
            this.courseProject1.HeightPlayField = 500;
            this.courseProject1.IsAskToGameComplete = false;
            this.courseProject1.Location = new System.Drawing.Point(2, 2);
            this.courseProject1.Name = "courseProject1";
            this.courseProject1.QuantityColors = 5;
            this.courseProject1.Score = 0;
            this.courseProject1.SecondBallColor = System.Drawing.Color.OrangeRed;
            this.courseProject1.Size = new System.Drawing.Size(682, 547);
            this.courseProject1.TabIndex = 0;
            this.courseProject1.Text = "courseProject1";
            this.courseProject1.ThirdBallColor = System.Drawing.Color.Transparent;
            this.courseProject1.WidthPlayField = 200;
            this.courseProject1.XDefeatLine = 241;
            this.courseProject1.XPlayBall = 241;
            this.courseProject1.XPlayField = 241;
            this.courseProject1.YDefeatLine = 423;
            this.courseProject1.YPlayBall = 0;
            this.courseProject1.YPlayFiled = 23;
            this.courseProject1.OnStartGame += new System.EventHandler(this.courseProject1_OnStartGame);
            this.courseProject1.OnScoreChanged += new System.EventHandler(this.courseProject1_OnScoreChanged);
            this.courseProject1.OnEndGame += new System.EventHandler(this.courseProject1_OnEndGame);
            this.courseProject1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.courseProject1_KeyDown);
            this.courseProject1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.courseProject1_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.EndGameButton);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.label);
            this.Controls.Add(this.StartGameButton);
            this.Controls.Add(this.courseProject1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SDCourseProject.CourseProject courseProject1;
        private System.Windows.Forms.Button StartGameButton;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Button EndGameButton;
    }
}

