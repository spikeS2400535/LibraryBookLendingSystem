using System;
using System.Drawing;
using System.Windows.Forms;
using LibraryBookLendingSystem.Models;

namespace LibraryBookLendingSystem
{
    public partial class MainDashboard : Form
    {
        // Shared library instance passed between forms
        public static Library Library = new Library();

        public MainDashboard()
        {
            InitializeComponent();
            SetupDashboard();
        }

        private void SetupDashboard()
        {
            // Form settings
            this.Text = "Library Book Lending System";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 30, 60);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "📚 Library Book Lending System";
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(180, 50);
            this.Controls.Add(lblTitle);

            // Subtitle
            Label lblSub = new Label();
            lblSub.Text = "Welcome! Choose an option below to get started.";
            lblSub.Font = new Font("Segoe UI", 11);
            lblSub.ForeColor = Color.LightSteelBlue;
            lblSub.AutoSize = true;
            lblSub.Location = new Point(250, 100);
            this.Controls.Add(lblSub);

            // Books Button
            Button btnBooks = CreateButton("📖  Manage Books", 150, 180, Color.FromArgb(52, 152, 219));
            btnBooks.Click += (s, e) => { new BooksForm().ShowDialog(); };
            this.Controls.Add(btnBooks);

            // Members Button
            Button btnMembers = CreateButton("👤  Manage Members", 450, 180, Color.FromArgb(46, 204, 113));
            btnMembers.Click += (s, e) => { new MembersForm().ShowDialog(); };
            this.Controls.Add(btnMembers);

            // Loans Button
            Button btnLoans = CreateButton("🔖  Manage Loans", 150, 320, Color.FromArgb(155, 89, 182));
            btnLoans.Click += (s, e) => { new LoansForm().ShowDialog(); };
            this.Controls.Add(btnLoans);

            // Overdue Button
            Button btnOverdue = CreateButton("⚠️  Overdue Books", 450, 320, Color.FromArgb(231, 76, 60));
            btnOverdue.Click += (s, e) => { new OverdueForm().ShowDialog(); };
            this.Controls.Add(btnOverdue);

            // Report Label
            Label lblReport = new Label();
            lblReport.Name = "lblReport";
            lblReport.Text = Library.GenerateReport();
            lblReport.Font = new Font("Segoe UI", 10);
            lblReport.ForeColor = Color.LightGray;
            lblReport.AutoSize = true;
            lblReport.Location = new Point(300, 460);
            this.Controls.Add(lblReport);
        }

        private Button CreateButton(string text, int x, int y, Color color)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(250, 100);
            btn.Location = new Point(x, y);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            return btn;
        }
    }
}