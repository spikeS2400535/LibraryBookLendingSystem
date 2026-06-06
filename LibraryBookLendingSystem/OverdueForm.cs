using System;
using System.Drawing;
using System.Windows.Forms;
using LibraryBookLendingSystem.Models;

namespace LibraryBookLendingSystem
{
    public partial class OverdueForm : Form
    {
        private Library _library = MainDashboard.Library;
        private DataGridView dgvOverdue;

        public OverdueForm()
        {
            InitializeComponent();
            SetupForm();
            LoadOverdueLoans();
        }

        private void SetupForm()
        {
            this.Text = "Overdue Books";
            this.Size = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "⚠️ Overdue Books";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(231, 76, 60);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 15);
            this.Controls.Add(lblTitle);

            // Info panel
            Panel infoPanel = new Panel();
            infoPanel.Size = new Size(900, 60);
            infoPanel.Location = new Point(20, 55);
            infoPanel.BackColor = Color.FromArgb(255, 235, 235);
            infoPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(infoPanel);

            Label lblInfo = new Label();
            lblInfo.Name = "lblInfo";
            lblInfo.Text = "Loading...";
            lblInfo.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblInfo.ForeColor = Color.FromArgb(231, 76, 60);
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(15, 18);
            infoPanel.Controls.Add(lblInfo);

            // Refresh button
            Button btnRefresh = new Button();
            btnRefresh.Text = "🔄 Refresh";
            btnRefresh.Size = new Size(120, 35);
            btnRefresh.Location = new Point(20, 130);
            btnRefresh.BackColor = Color.FromArgb(231, 76, 60);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.Click += (s, e) => LoadOverdueLoans();
            this.Controls.Add(btnRefresh);

            // DataGridView
            dgvOverdue = new DataGridView();
            dgvOverdue.Size = new Size(900, 370);
            dgvOverdue.Location = new Point(20, 175);
            dgvOverdue.BackgroundColor = Color.White;
            dgvOverdue.BorderStyle = BorderStyle.None;
            dgvOverdue.RowHeadersVisible = false;
            dgvOverdue.AllowUserToAddRows = false;
            dgvOverdue.ReadOnly = true;
            dgvOverdue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOverdue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOverdue.Font = new Font("Segoe UI", 9);
            dgvOverdue.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(231, 76, 60);
            dgvOverdue.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOverdue.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvOverdue.EnableHeadersVisualStyles = false;
            this.Controls.Add(dgvOverdue);
        }

        private void LoadOverdueLoans()
        {
            dgvOverdue.Rows.Clear();
            dgvOverdue.Columns.Clear();

            dgvOverdue.Columns.Add("LoanId", "Loan ID");
            dgvOverdue.Columns.Add("MemberName", "Member Name");
            dgvOverdue.Columns.Add("MemberEmail", "Email");
            dgvOverdue.Columns.Add("BookTitle", "Book Title");
            dgvOverdue.Columns.Add("BorrowDate", "Borrowed On");
            dgvOverdue.Columns.Add("DueDate", "Due Date");
            dgvOverdue.Columns.Add("DaysOverdue", "Days Overdue");

            var overdueLoans = _library.GetOverdueLoans();

            foreach (var loan in overdueLoans)
            {
                var row = dgvOverdue.Rows.Add(
                    loan.Id,
                    loan.Member.Name,
                    loan.Member.Email,
                    loan.Item.Title,
                    loan.BorrowDate.ToShortDateString(),
                    loan.DueDate.ToShortDateString(),
                    loan.DaysOverdue()
                );
                dgvOverdue.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
            }

            // Update info label
            foreach (Control c in this.Controls)
            {
                if (c is Panel panel)
                {
                    foreach (Control pc in panel.Controls)
                    {
                        if (pc.Name == "lblInfo")
                        {
                            pc.Text = overdueLoans.Count == 0
                                ? "✅ No overdue books at the moment!"
                                : $"⚠️ There are {overdueLoans.Count} overdue book(s) that need attention!";
                        }
                    }
                }
            }
        }
    }
}