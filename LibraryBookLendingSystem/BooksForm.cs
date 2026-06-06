using System;
using System.Drawing;
using System.Windows.Forms;
using LibraryBookLendingSystem.Models;

namespace LibraryBookLendingSystem
{
    public partial class BooksForm : Form
    {
        private Library _library = MainDashboard.Library;
        private DataGridView dgvBooks;
        private TextBox txtTitle, txtAuthor, txtISBN, txtYear, txtGenre, txtCopies, txtSearch;
        private Button btnAdd, btnDelete, btnSearch, btnClear;

        public BooksForm()
        {
            InitializeComponent();
            SetupForm();
            LoadBooks();
        }

        private void SetupForm()
        {
            this.Text = "Manage Books";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 244, 248);

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "📖 Book Management";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 30, 60);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 15);
            this.Controls.Add(lblTitle);

            // Input Panel
            Panel inputPanel = new Panel();
            inputPanel.Size = new Size(900, 160);
            inputPanel.Location = new Point(20, 55);
            inputPanel.BackColor = Color.White;
            inputPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(inputPanel);

            // Input fields
            txtTitle = CreateInput(inputPanel, "Title:", 10, 10);
            txtAuthor = CreateInput(inputPanel, "Author:", 210, 10);
            txtISBN = CreateInput(inputPanel, "ISBN:", 410, 10);
            txtYear = CreateInput(inputPanel, "Year:", 610, 10);
            txtGenre = CreateInput(inputPanel, "Genre:", 10, 80);
            txtCopies = CreateInput(inputPanel, "Copies:", 210, 80);

            // Add Button
            btnAdd = new Button();
            btnAdd.Text = "➕ Add Book";
            btnAdd.Size = new Size(130, 40);
            btnAdd.Location = new Point(430, 100);
            btnAdd.BackColor = Color.FromArgb(52, 152, 219);
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
            btnDelete.Size = new Size(130, 40);
            btnDelete.Location = new Point(570, 100);
            btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Click += BtnDelete_Click;
            inputPanel.Controls.Add(btnDelete);

            // Search bar
            Label lblSearch = new Label();
            lblSearch.Text = "🔍 Search:";
            lblSearch.Font = new Font("Segoe UI", 10);
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(20, 230);
            this.Controls.Add(lblSearch);

            txtSearch = new TextBox();
            txtSearch.Size = new Size(300, 30);
            txtSearch.Location = new Point(100, 227);
            txtSearch.Font = new Font("Segoe UI", 10);
            this.Controls.Add(txtSearch);

            btnSearch = new Button();
            btnSearch.Text = "Search";
            btnSearch.Size = new Size(100, 30);
            btnSearch.Location = new Point(410, 227);
            btnSearch.BackColor = Color.FromArgb(52, 152, 219);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.Click += BtnSearch_Click;
            this.Controls.Add(btnSearch);

            btnClear = new Button();
            btnClear.Text = "Clear";
            btnClear.Size = new Size(100, 30);
            btnClear.Location = new Point(520, 227);
            btnClear.BackColor = Color.Gray;
            btnClear.ForeColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Cursor = Cursors.Hand;
            btnClear.Click += (s, e) => { txtSearch.Clear(); LoadBooks(); };
            this.Controls.Add(btnClear);

            // DataGridView
            dgvBooks = new DataGridView();
            dgvBooks.Size = new Size(900, 330);
            dgvBooks.Location = new Point(20, 270);
            dgvBooks.BackgroundColor = Color.White;
            dgvBooks.BorderStyle = BorderStyle.None;
            dgvBooks.RowHeadersVisible = false;
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.ReadOnly = true;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.Font = new Font("Segoe UI", 9);
            dgvBooks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 60);
            dgvBooks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBooks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvBooks.EnableHeadersVisualStyles = false;
            this.Controls.Add(dgvBooks);
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

        private void LoadBooks()
        {
            dgvBooks.Rows.Clear();
            dgvBooks.Columns.Clear();

            dgvBooks.Columns.Add("Id", "ID");
            dgvBooks.Columns.Add("Title", "Title");
            dgvBooks.Columns.Add("Author", "Author");
            dgvBooks.Columns.Add("ISBN", "ISBN");
            dgvBooks.Columns.Add("Year", "Year");
            dgvBooks.Columns.Add("Genre", "Genre");
            dgvBooks.Columns.Add("Copies", "Available/Total");
            dgvBooks.Columns.Add("Status", "Status");

            foreach (var item in _library.GetAllItems())
            {
                if (item is Book book)
                {
                    dgvBooks.Rows.Add(
                        book.Id, book.Title, book.Author, book.ISBN,
                        book.Year, book.Genre,
                        $"{book.AvailableCopies}/{book.TotalCopies}",
                        book.IsAvailable ? "Available" : "Unavailable"
                    );
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                    string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                    string.IsNullOrWhiteSpace(txtISBN.Text))
                    throw new Exception("Title, Author and ISBN are required.");

                int year = int.Parse(txtYear.Text);
                int copies = int.Parse(txtCopies.Text);

                Book book = new Book(0, txtTitle.Text, txtAuthor.Text,
                    txtISBN.Text, year, txtGenre.Text, copies);
                _library.AddItem(book);

                MessageBox.Show("Book added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtTitle.Clear(); txtAuthor.Clear(); txtISBN.Clear();
                txtYear.Clear(); txtGenre.Clear(); txtCopies.Clear();
                LoadBooks();
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
                if (dgvBooks.SelectedRows.Count == 0)
                    throw new Exception("Please select a book to delete.");

                int id = int.Parse(dgvBooks.SelectedRows[0].Cells["Id"].Value.ToString());
                var result = MessageBox.Show("Are you sure you want to delete this book?",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _library.RemoveItem(id);
                    LoadBooks();
                    MessageBox.Show("Book deleted!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                    throw new Exception("Please enter a search term.");

                var results = _library.SearchItems(txtSearch.Text);
                dgvBooks.Rows.Clear();
                dgvBooks.Columns.Clear();

                dgvBooks.Columns.Add("Id", "ID");
                dgvBooks.Columns.Add("Title", "Title");
                dgvBooks.Columns.Add("Author", "Author");
                dgvBooks.Columns.Add("ISBN", "ISBN");
                dgvBooks.Columns.Add("Year", "Year");
                dgvBooks.Columns.Add("Genre", "Genre");
                dgvBooks.Columns.Add("Copies", "Available/Total");
                dgvBooks.Columns.Add("Status", "Status");

                foreach (var item in results)
                {
                    if (item is Book book)
                    {
                        dgvBooks.Rows.Add(
                            book.Id, book.Title, book.Author, book.ISBN,
                            book.Year, book.Genre,
                            $"{book.AvailableCopies}/{book.TotalCopies}",
                            book.IsAvailable ? "Available" : "Unavailable"
                        );
                    }
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