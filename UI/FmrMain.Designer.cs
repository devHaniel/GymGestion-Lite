namespace UI
{
    partial class FmrMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelVistas = new System.Windows.Forms.Panel();
            this.panelNavbar = new System.Windows.Forms.Panel();
            this.btnIniciarSesion = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelNavbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelNavbar);
            this.panel1.Controls.Add(this.panelVistas);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1252, 725);
            this.panel1.TabIndex = 0;
            // 
            // panelVistas
            // 
            this.panelVistas.BackColor = System.Drawing.SystemColors.Control;
            this.panelVistas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelVistas.Location = new System.Drawing.Point(0, 112);
            this.panelVistas.Name = "panelVistas";
            this.panelVistas.Size = new System.Drawing.Size(1252, 613);
            this.panelVistas.TabIndex = 1;
            // 
            // panelNavbar
            // 
            this.panelNavbar.Controls.Add(this.btnIniciarSesion);
            this.panelNavbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNavbar.Location = new System.Drawing.Point(0, 0);
            this.panelNavbar.Name = "panelNavbar";
            this.panelNavbar.Size = new System.Drawing.Size(1252, 112);
            this.panelNavbar.TabIndex = 2;
            // 
            // btnIniciarSesion
            // 
            this.btnIniciarSesion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.btnIniciarSesion.FlatAppearance.BorderSize = 0;
            this.btnIniciarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciarSesion.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btnIniciarSesion.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnIniciarSesion.Location = new System.Drawing.Point(1050, 32);
            this.btnIniciarSesion.Name = "btnIniciarSesion";
            this.btnIniciarSesion.Size = new System.Drawing.Size(179, 51);
            this.btnIniciarSesion.TabIndex = 43;
            this.btnIniciarSesion.Text = "Cortes";
            this.btnIniciarSesion.UseVisualStyleBackColor = false;
            this.btnIniciarSesion.Click += new System.EventHandler(this.btnIniciarSesion_Click);
            // 
            // FmrMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 725);
            this.Controls.Add(this.panel1);
            this.Name = "FmrMain";
            this.Text = "FmrMain";
            this.panel1.ResumeLayout(false);
            this.panelNavbar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelVistas;
        private System.Windows.Forms.Panel panelNavbar;
        private System.Windows.Forms.Button btnIniciarSesion;
    }
}