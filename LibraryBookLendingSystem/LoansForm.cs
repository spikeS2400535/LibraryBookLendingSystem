using System;
using System.Drawing;
using System.Windows.Forms;
using LibraryBookLendingSystem.Models;

namespace LibraryBookLendingSystem
{
    public partial class LoansForm : Form
    {
        private Library _library = MainDashboard.Library;
        private DataGridView dgvLoans;
        private TextBox txtMemberId, txtItemId;
        private Button btnBorrow, btnReturn;

        public LoansForm()
        {
            InitializeComponent();
            SetupForm();
            LoadLoans();
        }

        private void SetupForm()
        {
            this.Text = "Manage Loans";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "🔖 Loan Management";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 30, 60);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 15);
            this.Controls.Add(lblTitle);

            // Input Panel
            Panel inputPanel = new Panel();
            inputPanel.Size = new Size(900, 110);
            inputPanel.Location = new Point(20, 55);
            inputPanel.BackColor = Color.White;
            inputPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(inputPanel);

            // Borrow section
            Label lblBorrow = new Label();
            lblBorrow.Text = "── Borrow a Book ──";
            lblBorrow.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblBorrow.ForeColor = Color.FromArgb(155, 89, 182);
            lblBorrow.AutoSize = true;
            lblBorrow.Location = new Point(10, 8);
            inputPanel.Controls.Add(lblBorrow);

            txtMemberId = CreateInput(inputPanel, "Member ID:", 10, 28);
            txtItemId = CreateInput(inputPanel, "Book ID:", 210, 28);

            btnBorrow = new Button();
            btnBorrow.Text = "📤 Borrow";
            btnBorrow.Size = new Size(130, 40);
            btnBorrow.Location = new Point(410, 48);
            btnBorrow.BackColor = Color.FromArgb(155, 89, 182);
            btnBorrow.ForeColor = Color.White;
            btnBorrow.FlatStyle = FlatStyle.Flat;
            btnBorrow.FlatAppearance.BorderSize = 0;
            btnBorrow.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnBorrow.Cursor = Cursors.Hand;
            btnBorrow.Click += BtnBorrow_Click;
            inputPanel.Controls.Add(btnBorrow);

            // Return section
            Label lblReturn = new Label();
            lblReturn.Text = "── Return a Book (select row below) ──";
            lblReturn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblReturn.ForeColor = Color.FromArgb(46, 204, 113);
            lblReturn.AutoSize = true;
            lblReturn.Location = new Point(560, 8);
            inputPanel.Controls.Add(lblReturn);

            btnReturn = new Button();
            btnReturn.Text = "📥 Return Selected";
            btnReturn.Size = new Size(180, 40);
            btnReturn.Location = new Point(700, 48);
            btnReturn.BackColor = Color.FromArgb(46, 204, 113);
            btnReturn.ForeColor = Color.White;
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.FlatAppearance.BorderSize = 0;
            btnReturn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnReturn.Cursor = Cursors.Hand;
            btnReturn.Click += BtnReturn_Click;
            inputPanel.Controls.Add(btnReturn);

            // Info label
            Label lblInfo = new Label();
            lblInfo.Text = "ℹ️ To borrow: enter Member ID and Book ID. To return: select a loan row and click Return.";
            lblInfo.Font = new Font("Segoe UI", 9);
            lblInfo.ForeColor = Color.Gray;
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(20, 175);
            this.Controls.Add(lblInfo);

            // DataGridView
            dgvLoans = new DataGridView();
            dgvLoans.Size = new Size(900, 400);
            dgvLoans.Location = new Point(20, 200);
            dgvLoans.BackgroundColor = Color.White;
            dgvLoans.BorderStyle = BorderStyle.None;
            dgvLoans.RowHeadersVisible = false;
            dgvLoans.AllowUserToAddRows = false;
            dgvLoans.ReadOnly = true;
            dgvLoans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLoans.Font = new Font("Segoe UI", 9);
            dgvLoans.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 60);
            dgvLoans.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLoans.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvLoans.EnableHeadersVisualStyles = false;
            this.Controls.Add(dgvLoans);
        }

        private TextBox CreateInput(Panel panel, string label, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = label;
            lbl.Font = new Font("Segoe UI", 9);
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            panel.Controls.Add(lbl);

            TextBox txt = new TextBox();
            txt.Size = new Size(170, 25);
            txt.Location = new Point(x, y + 20);
            txt.Font = new Font("Segoe UI", 10);
            panel.Controls.Add(txt);
            return txt;
        }

        private void LoadLoans()
        {
            dgvLoans.Rows.Clear();
            dgvLoans.Columns.Clear();

            dgvLoans.Columns.Add("LoanId", "Loan ID");
            dgvLoans.Columns.Add("MemberName", "Member");
            dgvLoans.Columns.Add("BookTitle", "Book Title");
            dgvLoans.Columns.Add("BorrowDate", "Borrowed On");
            dgvLoans.Columns.Add("DueDate", "Due Date");
            dgvLoans.Columns.Add("Status", "Status");

            foreach (var loan in _library.GetAllLoans())
            {
                string status = loan.IsReturned ? "✅ Returned" :
                                loan.IsOverdue() ? $"⚠️ Overdue ({loan.DaysOverdue()} days)" :
                                "📖 Active";

                var row = dgvLoans.Rows.Add(
                    loan.Id,
                    loan.Member.Name,
                    loan.Item.Title,
                    loan.BorrowDate.ToShortDateString(),
                    loan.DueDate.ToShortDateString(),
                    status
                );

                // Color overdue rows red
                if (loan.IsOverdue())
                    dgvLoans.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                else if (loan.IsReturned)
                    dgvLoans.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
            }
        }

        private void BtnBorrow_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMemberId.Text) ||
                    string.IsNullOrWhiteSpace(txtItemId.Text))
                    throw new Exception("Please enter both Member ID and Book ID.");

                int memberId = int.Parse(txtMemberId.Text);
                int itemId = int.Parse(txtItemId.Text);

                string result = _library.BorrowItem(memberId, itemId);

                if (result == "Success")
                {
                    MessageBox.Show("Book borrowed successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMemberId.Clear();
                    txtItemId.Clear();
                    LoadLoans();
                }
                else
                {
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLoans.SelectedRows.Count == 0)
                    throw new Exception("Please select a loan to return.");

                int loanId = int.Parse(dgvLoans.SelectedRows[0].Cells["LoanId"].Value.ToString());
                string result = _library.ReturnItem(loanId);

                if (result == "Success")
                {
                    MessageBox.Show("Book returned successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLoans();
                }
                else
                {
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}