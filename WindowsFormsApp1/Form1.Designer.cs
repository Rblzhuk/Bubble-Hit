
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
            this.courseProject1 = new SDCourseProject.BubbleHit();
            this.SuspendLayout();
            // 
            // StartGameButton
            // 
            this.StartGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartGameButton.Location = new System.Drawing.Point(550, 148);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(129, 23);
            this.StartGameButton.TabIndex = 1;
            this.StartGameButton.Text = "Начать игру";
            this.StartGameButton.UseVisualStyleBackColor = true;
            this.StartGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(588, 106);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(33, 13);
            this.label.TabIndex = 2;
            this.label.Text = "Счёт:";
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Location = new System.Drawing.Point(625, 105);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(54, 13);
            this.ScoreLabel.TabIndex = 3;
            this.ScoreLabel.Text = "значение";
            // 
            // EndGameButton
            // 
            this.EndGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EndGameButton.Location = new System.Drawing.Point(550, 195);
            this.EndGameButton.Name = "EndGameButton";
            this.EndGameButton.Size = new System.Drawing.Size(129, 23);
            this.EndGameButton.TabIndex = 5;
            this.EndGameButton.Text = "Закончить игру";
            this.EndGameButton.UseVisualStyleBackColor = true;
            this.EndGameButton.Click += new System.EventHandler(this.EndGameButton_Click);
            // 
            // courseProject1
            // 
            this.courseProject1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.courseProject1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.courseProject1.ColorDefeatLine = System.Drawing.Color.Maroon;
            this.courseProject1.ColorFillPlayField = System.Drawing.Color.Transparent;
            this.courseProject1.ColorPlayFieldFrame = System.Drawing.Color.Maroon;
            this.courseProject1.FifthBallColor = System.Drawing.Color.OliveDrab;
            this.courseProject1.FirstBallColor = System.Drawing.Color.Red;
            this.courseProject1.FourthBallColor = System.Drawing.Color.Tomato;
            this.courseProject1.IsNotifyToGameComplete = false;
            this.courseProject1.Location = new System.Drawing.Point(227, 0);
            this.courseProject1.Name = "courseProject1";
            this.courseProject1.SecondBallColor = System.Drawing.Color.MediumOrchid;
            this.courseProject1.Size = new System.Drawing.Size(315, 549);
            this.courseProject1.TabIndex = 0;
            this.courseProject1.Text = "courseProject1";
            this.courseProject1.ThirdBallColor = System.Drawing.Color.Peru;
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SDCourseProject.BubbleHit courseProject1;
        private System.Windows.Forms.Button StartGameButton;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Button EndGameButton;
    }
}

