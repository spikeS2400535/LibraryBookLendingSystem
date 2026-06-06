using System;
using System.Drawing;
using System.Windows.Forms;
using LibraryBookLendingSystem.Models;

namespace LibraryBookLendingSystem
{
    public partial class MembersForm : Form
    {
        private Library _library = MainDashboard.Library;
        private DataGridView dgvMembers;
        private TextBox txtName, txtEmail, txtPhone;
        private Button btnAdd, btnDelete;

        public MembersForm()
        {
            InitializeComponent();
            SetupForm();
            LoadMembers();
        }

        private void SetupForm()
        {
            this.Text = "Manage Members";
            this.Size = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "👤 Member Management";
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

            // Input fields
            txtName = CreateInput(inputPanel, "Full Name:", 10, 10);
            txtEmail = CreateInput(inputPanel, "Email:", 210, 10);
            txtPhone = CreateInput(inputPanel, "Phone:", 410, 10);

            // Add Button
            btnAdd = new Button();
            btnAdd.Text = "➕ Add Member";
            btnAdd.Size = new Size(140, 40);
            btnAdd.Location = new Point(620, 30);
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            btnAdd.ForeColor = Color.White;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Click += BtnAdd_Click;
            inputPanel.Controls.Add(btnAdd);

            // Delete Button
            btnDelete = new Button();
            btnDelete.Text = "🗑 Delete";
            btnDelete.Size = new Size(120, 40);
            btnDelete.Location = new Point(770, 30);
            btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Click += BtnDelete_Click;
            inputPanel.Controls.Add(btnDelete);

            // DataGridView
            dgvMembers = new DataGridView();
            dgvMembers.Size = new Size(900, 390);
            dgvMembers.Location = new Point(20, 180);
            dgvMembers.BackgroundColor = Color.White;
            dgvMembers.BorderStyle = BorderStyle.None;
            dgvMembers.RowHeadersVisible = false;
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.ReadOnly = true;
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMembers.Font = new Font("Segoe UI", 9);
            dgvMembers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 60);
            dgvMembers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMembers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvMembers.EnableHeadersVisualStyles = false;
            this.Controls.Add(dgvMembers);

            // Members count label
            Label lblCount = new Label();
            lblCount.Name = "lblCount";
            lblCount.Text = "Total Members: 0";
            lblCount.Font = new Font("Segoe UI", 10);
            lblCount.ForeColor = Color.FromArgb(30, 30, 60);
            lblCount.AutoSize = true;
            lblCount.Location = new Point(20, 175);
            this.Controls.Add(lblCount);
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

        private void LoadMembers()
        {
            dgvMembers.Rows.Clear();
            dgvMembers.Columns.Clear();

            dgvMembers.Columns.Add("Id", "ID");
            dgvMembers.Columns.Add("Name", "Full Name");
            dgvMembers.Columns.Add("Email", "Email");
            dgvMembers.Columns.Add("Phone", "Phone");
            dgvMembers.Columns.Add("Joined", "Member Since");
            dgvMembers.Columns.Add("Status", "Status");

            foreach (var member in _library.GetAllMembers())
            {
                dgvMembers.Rows.Add(
                    member.Id,
                    member.Name,
                    member.Email,
                    member.Phone,
                    member.MembershipDate.ToShortDateString(),
                    member.IsActive ? "Active" : "Inactive"
                );
            }

            // Update count label
            foreach (Control c in this.Controls)
            {
                if (c.Name == "lblCount")
                    c.Text = $"Total Members: {_library.GetAllMembers().Count}";
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                    throw new Exception("Name and Email are required.");

                Member member = new Member(0, txtName.Text, txtEmail.Text, txtPhone.Text);
                _library.AddMember(member);

                MessageBox.Show("Member added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtName.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
                LoadMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMembers.SelectedRows.Count == 0)
                    throw new Exception("Please select a member to delete.");

                int id = int.Parse(dgvMembers.SelectedRows[0].Cells["Id"].Value.ToString());
                var result = MessageBox.Show("Are you sure you want to delete this member?",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _library.RemoveMember(id);
                    LoadMembers();
                    MessageBox.Show("Member deleted!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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