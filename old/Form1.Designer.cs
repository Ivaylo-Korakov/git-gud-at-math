namespace Git_Gud_At_Math
{
    using System.Drawing;
    using System.Windows.Forms;
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CalculateLabel = new System.Windows.Forms.Label();
            this.FunctionInput = new System.Windows.Forms.TextBox();
            this.SolveBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CalculateLabel
            // 
            this.CalculateLabel.AutoSize = true;
            this.CalculateLabel.Font = new System.Drawing.Font("Arial", 25F, System.Drawing.FontStyle.Bold);
            this.CalculateLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.CalculateLabel.Location = new System.Drawing.Point(15, 22);
            this.CalculateLabel.Name = "CalculateLabel";
            this.CalculateLabel.Size = new System.Drawing.Size(178, 40);
            this.CalculateLabel.TabIndex = 1;
            this.CalculateLabel.Text = "Calculate:";
            // 
            // FunctionInput
            // 
            this.FunctionInput.BackColor = System.Drawing.SystemColors.Window;
            this.FunctionInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FunctionInput.Font = new System.Drawing.Font("Arial", 21F, System.Drawing.FontStyle.Bold);
            this.FunctionInput.ForeColor = System.Drawing.Color.DarkBlue;
            this.FunctionInput.Location = new System.Drawing.Point(199, 22);
            this.FunctionInput.Name = "FunctionInput";
            this.FunctionInput.Size = new System.Drawing.Size(827, 40);
            this.FunctionInput.TabIndex = 2;
            // 
            // SolveBtn
            // 
            this.SolveBtn.BackColor = System.Drawing.SystemColors.HotTrack;
            this.SolveBtn.FlatAppearance.BorderColor = System.Drawing.Color.DarkBlue;
            this.SolveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SolveBtn.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.SolveBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SolveBtn.Location = new System.Drawing.Point(1047, 22);
            this.SolveBtn.Name = "SolveBtn";
            this.SolveBtn.Size = new System.Drawing.Size(160, 40);
            this.SolveBtn.TabIndex = 3;
            this.SolveBtn.Text = "SOLVE";
            this.SolveBtn.UseVisualStyleBackColor = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1243, 811);
            this.Controls.Add(this.SolveBtn);
            this.Controls.Add(this.FunctionInput);
            this.Controls.Add(this.CalculateLabel);
            this.Name = "MainWindow";
            this.Text = "Git Gud At Math";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label CalculateLabel;
        private System.Windows.Forms.TextBox FunctionInput;
        private System.Windows.Forms.Button SolveBtn;
    }
}

