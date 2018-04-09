using newapp.econtactClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace newapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lblLastName_Click(object sender, EventArgs e)
        {

        }

       
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get values from input fields 
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNo.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;

            //Inseting data into database using the method we created 
            bool success = c.Insert(c);
            if (success == true)
            {
                //Successfully inserted
                MessageBox.Show("New contact Inserted Successfully!");
                //call the method for clearing the fields
                Clear();
            }
            else
            {
                //Failed to add contact
                MessageBox.Show("Failed to insert record. Try Again!");
            }

            //Load data on Grid View
            System.Data.DataTable dt = c.Select(); //selects data from database
            dgvContactList.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Data.DataTable dt = c.Select(); //selects data from database
            dgvContactList.DataSource = dt;
        }

        private void dgvContactList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //method to clear fields data
        public void Clear() {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxContactNo.Text = "";
            textBoxAddress.Text = "";
            comboBoxGender.Text = "";
            textboxContactID.Text = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the data from text boxes
            c.ContactID = int.Parse(textboxContactID.Text);
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNo.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;
            //Update data in database
            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("Contact has been updated");
                System.Data.DataTable dt = c.Select(); //selects data from database
                dgvContactList.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update contact");
            }
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the data from data grid view and load it into the text boxes respectively
            //Identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            textboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxContactNo.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            comboBoxGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //data from textbox
            c.ContactID = Convert.ToInt32(textboxContactID.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                MessageBox.Show("Data deleted successfully");
                //refresh datagrid view
                System.Data.DataTable dt = c.Select(); //selects data from database
                dgvContactList.DataSource = dt;
                Clear();
            }
            else {
                MessageBox.Show("Failed to delete data");
            }
        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //get the value from text box 
            string keyword = textBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%"+keyword+"%' OR LastName LIKE '%"+keyword+ "%' OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
