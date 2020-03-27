using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Act_1_CRUD_StudentInfo
{
    public partial class StudentInfo : Form
    {
        private static string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\donilfiles\AIS\SutudentInfo.mdb";
        OleDbConnection con = new OleDbConnection(connectionString);
        public StudentInfo()
        {
            InitializeComponent();
        }

        private void Createbutton_Click(object sender, EventArgs e)
        {
            if (!AllTextEmpty(this))
            {
                if (ifValidNumber(AgetextBox.Text) && ifValidNumber(YeartextBox.Text) && ifValidNumber(IDtextBox.Text))
                {
                    string sql = "Select * From StudentInfo";
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sql, con);
                    OleDbCommandBuilder cmdbuilder = new OleDbCommandBuilder(dataAdapter);
                    DataSet dataset = new DataSet();
                    dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    dataAdapter.Fill(dataset, "StudentInfo");

                    DataRow findrow = dataset.Tables["StudentInfo"].Rows.Find(IDtextBox.Text);
                    if (findrow == null)
                    {
                        DataRow datarow = dataset.Tables["StudentInfo"].NewRow();
                        datarow[0] = Convert.ToInt32(IDtextBox.Text);
                        datarow[1] = FNametextBox.Text;
                        datarow[2] = MNametextBox.Text;
                        datarow[3] = LNametextBox.Text;
                        datarow[4] = AddresstextBox.Text;
                        datarow[5] = Convert.ToDateTime(BDate.Text);
                        datarow[6] = GendertextBox.Text;
                        datarow[7] = Convert.ToInt32(AgetextBox.Text);
                        datarow[8] = CoursetextBox.Text;
                        datarow[9] = Convert.ToInt32(YeartextBox.Text);

                        dataset.Tables["StudentInfo"].Rows.Add(datarow);
                        dataAdapter.Update(dataset, "StudentInfo");
                        MessageBox.Show("Successfully Saved");
                    }
                    else
                    {
                        MessageBox.Show("Duplicate Entry");
                    }
                    ClearAllText(this);
                }
                else
                {
                    MessageBox.Show("Invalid Input");
                }
            }
            else
            {
                MessageBox.Show("Pls. Complete All Details");
            }
        }

        //Method Clear All Textbox
        public void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
        }

        public bool ifValidNumber(String Value)
        {
            try
            {
                double val = Convert.ToInt32(Value);
                return true;
            }
            catch
            {            
                return false;
            }
        }
        //All textbox is Empty
        public bool AllTextEmpty(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                    if (((TextBox)c).Text == "")
                        return true;
            }
            return false;
        }

        private void Retrievebutton_Click(object sender, EventArgs e)
        {
            bool found = false;
            con.Open();
            OleDbCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM StudentInfo";
            OleDbDataReader thisreader = command.ExecuteReader();
            while (thisreader.Read())
            {
                if (thisreader["ID"].ToString() == IDtextBox.Text)
                {
                    found = true;
                    FNametextBox.Text = thisreader["FName"].ToString();
                    MNametextBox.Text = thisreader["MName"].ToString();
                    LNametextBox.Text = thisreader["LName"].ToString();
                    AddresstextBox.Text = thisreader["Address"].ToString();
                    BDate.Text =thisreader["BDate"].ToString();
                    GendertextBox.Text = thisreader["Gender"].ToString();
                    AgetextBox.Text = thisreader["Age"].ToString();
                    CoursetextBox.Text = thisreader["Course"].ToString();
                    YeartextBox.Text = thisreader["yr"].ToString();
                    break;
                }
            }
            thisreader.Close();
            con.Close();
            if (!found)
            {              
                MessageBox.Show("Student ID Not found");
                IDtextBox.Clear();
            }
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            string sql = "Select * From StudentInfo";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sql, con);
            OleDbCommandBuilder cmdbuilder = new OleDbCommandBuilder(dataAdapter);
            DataSet dataset = new DataSet();
            dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            dataAdapter.Fill(dataset, "StudentInfo");

            DataRow findrow = dataset.Tables["StudentInfo"].Rows.Find(IDtextBox.Text);
            if (findrow != null)
            {
               // DataRow datarow = dataset.Tables["StudentInfo"].NewRow();
                findrow[0] = Convert.ToInt32(IDtextBox.Text);
                findrow[1] = FNametextBox.Text;
                findrow[2] = MNametextBox.Text;
                findrow[3] = LNametextBox.Text;
                findrow[4] = AddresstextBox.Text;
                findrow[5] = Convert.ToDateTime(BDate.Text);
                findrow[6] = GendertextBox.Text;
               // findrow[7] = Convert.ToInt32(AgetextBox.Text);
                findrow[8] = CoursetextBox.Text;
                //findrow[9] = Convert.ToInt32(YeartextBox.Text);
                
                dataAdapter.Update(dataset, "StudentInfo");
                MessageBox.Show("Successfully Updated");
            }
            else
            {
                MessageBox.Show("Duplicate Entry");
            }         
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
              string sql = "Select * From StudentInfo";
              OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sql, con);
              OleDbCommandBuilder cmdbuilder = new OleDbCommandBuilder(dataAdapter);
              DataSet dataset = new DataSet();
              dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
              dataAdapter.Fill(dataset, "StudentInfo");

              DataRow findrow = dataset.Tables["StudentInfo"].Rows.Find(IDtextBox.Text);
              if (findrow != null)
              {
                  findrow.Delete();
                  dataAdapter.Update(dataset, "StudentInfo");
                  MessageBox.Show("Student ID Successfully Deleted");
              }
              else
              {
                  MessageBox.Show("Student ID Not found");
              }
             

      
        }          

    }
}
