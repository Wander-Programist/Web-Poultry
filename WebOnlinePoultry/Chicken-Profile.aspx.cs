﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace WebOnlinePoultry
{
    public partial class Chicken_Profile : System.Web.UI.Page
    {
        SqlConnection cpc = new SqlConnection(ConfigurationManager.ConnectionStrings["someeDB"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (cpc.State == ConnectionState.Open)
            {
                cpc.Close();
            }
            cpc.Open();
            if (!IsPostBack)
            {
                dbUpdater.Text = "Connected!";
                dbUpdater.ForeColor = System.Drawing.Color.Green;
                GridView1.DataBind();
            }
        }

        protected void CType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mcdReqType.Enabled = false;
            mcdReqBirth.Enabled = false;
            mcdReqWeight.Enabled = false;
            mcdReqPType.Enabled = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("poultryInsert", cpc);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@iCType", CType.SelectedValue);
            cmd.Parameters.AddWithValue("@iCBirthD", CBirthD.Text);
            cmd.Parameters.AddWithValue("@iCBirthW", CBirthW.Text);
            cmd.Parameters.AddWithValue("@iPType", ProductType.SelectedValue);

            int k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                CType.SelectedIndex = -1;
                CBirthD.Text = "";
                CBirthW.Text = "";
                ProductType.SelectedIndex = -1;
                GridView1.DataBind();
            }
            cpc.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('New data added!')", true);
        }

        protected void srcBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (srcBy.SelectedValue)
            {
                case "srcID":
                    if (srcRadio.Enabled && srcRadio.Visible && !srcBox.Visible)
                    {
                        srcRadio.Enabled = false;
                        srcRadio.Visible = false;
                        srcRadio.Items.Clear();
                    }

                    srcBox.Disabled = false;
                    srcBox.Visible = true;
                    srcBox.Attributes.Add("type", "number");
                    srcBox.Attributes.Add("placeholder", "ID");

                    srcButton.Enabled = true;
                    srcBoxRange.Enabled = true;
                    srcBoxValidator.Enabled = true;
                    srcRadioValidator.Enabled = false;

                    mcdReqType.Enabled = true;
                    mcdReqBirth.Enabled = true;
                    mcdReqWeight.Enabled = true;
                    mcdRanWeight.Enabled = true;
                    mcdReqPType.Enabled = true;
                    break;

                case "srcTYPE":
                    if (srcRadio.Enabled && !srcBox.Disabled || srcBox.Visible)
                    {
                        srcBox.Visible = false;
                        srcBox.Disabled = true;
                    }

                    srcRadio.Enabled = true;
                    srcRadio.Visible = true;
                    srcButton.Enabled = true;
                    srcRadioValidator.Enabled = true;
                    srcRadio.Items.Clear();
                    srcRadio.Items.Insert(0, new ListItem("Layer", "Layer"));
                    srcRadio.Items.Insert(1, new ListItem("Broiler", "Broiler"));
                    srcRadio.Items.Insert(2, new ListItem("Native", "Native"));
                    srcRadio.Attributes.Add("CssClass", "form-control-custome radio-custome");

                    srcBoxRange.Enabled = false;
                    srcBoxValidator.Enabled = false;

                    mcdReqType.Enabled = true;
                    mcdReqBirth.Enabled = true;
                    mcdReqWeight.Enabled = true;
                    mcdRanWeight.Enabled = true;
                    mcdReqPType.Enabled = true;
                    break;

                case "srcBIRTHD":
                    if (srcRadio.Enabled && srcRadio.Visible && srcBox.Disabled)
                    {
                        srcRadio.Items.Clear();
                        srcRadio.Enabled = false;
                        srcRadio.Visible = false;
                    }

                    srcBox.Disabled = false;
                    srcBox.Visible = true;
                    srcBox.Attributes.Add("type", "date");
                    srcBox.Attributes.Add("placeholder", "ID");

                    srcButton.Enabled = true;
                    srcBoxValidator.Enabled = true;
                    srcBoxRange.Enabled = false;
                    srcRadioValidator.Enabled = false;

                    mcdReqType.Enabled = true;
                    mcdReqBirth.Enabled = true;
                    mcdReqWeight.Enabled = true;
                    mcdRanWeight.Enabled = true;
                    mcdReqPType.Enabled = true;
                    break;

                case "srcBIRTHW":
                    if (srcRadio.Enabled && srcRadio.Visible && !srcBox.Visible || srcBox.Disabled)
                    {
                        srcRadio.Items.Clear();
                        srcRadio.Enabled = false;
                        srcRadio.Visible = false;
                    }

                    srcBox.Disabled = false;
                    srcBox.Visible = true;
                    srcBox.Attributes.Add("type", "number");
                    srcBox.Attributes.Add("placeholder", "Weight");

                    srcButton.Enabled = true;
                    srcBoxValidator.Enabled = true;
                    srcBoxRange.Enabled = true;
                    srcRadioValidator.Enabled = false;

                    mcdReqType.Enabled = true;
                    mcdReqBirth.Enabled = true;
                    mcdReqWeight.Enabled = true;
                    mcdRanWeight.Enabled = true;
                    mcdReqPType.Enabled = true;
                    break;

                case "srcPTYPE":
                    if (srcRadio.Enabled && !srcBox.Disabled || srcBox.Visible)
                    {
                        srcBox.Visible = false;
                        srcBox.Disabled = true;
                    }

                    srcRadio.Items.Clear();
                    srcRadio.Enabled = true;
                    srcRadio.Visible = true;
                    srcRadio.Items.Insert(0, new ListItem("45 Days", "45 Days"));
                    srcRadio.Items.Insert(1, new ListItem("9 Days", "9 Days"));

                    srcButton.Enabled = true;
                    srcRadioValidator.Enabled = true;
                    srcBoxRange.Enabled = false;
                    srcBoxValidator.Enabled = false;

                    mcdReqType.Enabled = true;
                    mcdReqBirth.Enabled = true;
                    mcdReqWeight.Enabled = true;
                    mcdRanWeight.Enabled = true;
                    mcdReqPType.Enabled = true;
                    break;

                default:
                    srcBox.Disabled = true;
                    srcBox.Visible = true;
                    srcBox.Value = "(None)";
                    srcRadio.Items.Clear();
                    srcRadio.Visible = false;
                    srcRadio.Enabled = false;
                    srcButton.Enabled = false;

                    srcBoxValidator.Enabled = false;
                    srcBoxRange.Enabled = false;
                    srcRadioValidator.Enabled = false;

                    mcdReqType.Enabled = false;
                    mcdReqBirth.Enabled = false;
                    mcdReqWeight.Enabled = false;
                    mcdRanWeight.Enabled = false;
                    mcdReqPType.Enabled = false;
                    break;
            }
            mcdType.SelectedIndex = -1;
            mcdBirthD.Text = "";
            mcdBirthW.Text = "";
            mcdProductType.SelectedIndex = -1;
        }

        protected void srcButton_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("poultrySearchBy", cpc);
            cmd.CommandType = CommandType.StoredProcedure;

            if (srcRadio.Enabled && srcBox.Disabled)
            {
                if (srcBy.SelectedValue.Equals("srcTYPE"))
                {
                    cmd.Parameters.AddWithValue("@CType", Convert.ToString(srcRadio.SelectedValue));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CPType", Convert.ToString(srcRadio.SelectedValue));
                }
            }
            if (!srcBox.Disabled && srcBox.Type.Equals("number"))
            {
                if (srcBy.SelectedValue.Equals("srcID"))
                {
                    cmd.Parameters.AddWithValue("@Cid", srcBox.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CWeight", srcBox.Value);
                }
            }
            if (!srcBox.Disabled && srcBox.Type.Equals("date"))
            {
                cmd.Parameters.AddWithValue("@CBirth", Convert.ToString(srcBox.Value));
            }
            mcdGridOut.DataSource = cmd.ExecuteReader();
            mcdGridOut.DataBind();
            cpc.Close();
        }

        protected void mcdRowSelect_Click(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;

            mcdSelectedID.Value = mcdGridOut.Rows[rowIndex].Cells[0].Text;

            if (mcdGridOut.Rows[rowIndex].Cells[1].Text.Trim().Equals("Layer"))
            {
                mcdType.SelectedIndex = 0;
            }
            else if (mcdGridOut.Rows[rowIndex].Cells[1].Text.Trim().Equals("Broiler"))
            {
                mcdType.SelectedIndex = 1;
            }
            else if (mcdGridOut.Rows[rowIndex].Cells[1].Text.Trim().Equals("Native"))
            {
                mcdType.SelectedIndex = 2;
            }
            else
            {
                mcdType.SelectedIndex = -1;
            }

            mcdBirthD.Text = mcdGridOut.Rows[rowIndex].Cells[2].Text;
            mcdBirthW.Text = mcdGridOut.Rows[rowIndex].Cells[3].Text;

            if (mcdGridOut.Rows[rowIndex].Cells[4].Text.Trim().Equals("45 Days"))
            {
                mcdProductType.SelectedIndex = 0;
            }
            else if (mcdGridOut.Rows[rowIndex].Cells[4].Text.Trim().Equals("9 Days"))
            {
                mcdProductType.SelectedIndex = 1;
            }
            else
            {
                mcdProductType.SelectedIndex = -1;
            }
        }

        protected void mcdClear_Click(object sender, EventArgs e)
        {
            mcdNotif.Attributes["class"] = mcdNotif.Attributes["class"].Replace("alert-warning", "alert-info").Trim();
            mcdTitle.InnerText = "Cleared!";
            mcdText.InnerText = "Manage Chicken Database textboxes and radio buttons data's is cleared.";
            mcdType.SelectedIndex = -1;
            mcdBirthD.Text = "DD/MM/YYYY";
            mcdBirthW.Text = "0";
            mcdProductType.SelectedIndex = -1;
        }

        protected void mcdSave_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("poultryUpdate", cpc);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Uid", mcdSelectedID.Value);
            cmd.Parameters.AddWithValue("@uCType", mcdType.SelectedValue);
            cmd.Parameters.AddWithValue("@uCBirth", mcdBirthD.Text);
            cmd.Parameters.AddWithValue("@uCWeight", mcdBirthW.Text);
            cmd.Parameters.AddWithValue("@uCPType", mcdProductType.SelectedValue);

            if (cpc.State == ConnectionState.Open)
            {
                cpc.Close();
            }
            cpc.Open();

            double k = cmd.ExecuteNonQuery();
            if (k != 0)
            {
                mcdNotif.Attributes["class"] = mcdNotif.Attributes["class"].Replace("alert-warning", "alert-success").Trim();
                mcdTitle.InnerText = "Data Updated!";
                mcdText.InnerText = "Data has been modified and save. To see the current updated database look in the top database table.";
                mcdType.SelectedIndex = -1;
                mcdBirthD.Text = "DD/MM/YYYY";
                mcdBirthW.Text = "0";
                mcdProductType.SelectedIndex = -1;
                GridView1.DataBind();
                mcdGridOut.DataSource = "";
                mcdGridOut.DataBind();
            }
            cpc.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Data has been updated!')", true);
        }

        protected void mcdDelete_Click1(object sender, EventArgs e)
        {
            int rowIndex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex;
            int ID = Convert.ToInt32(mcdGridOut.Rows[rowIndex].Cells[0].Text);

            SqlCommand cmd = new SqlCommand("poultryDelete", cpc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", ID);
            cmd.ExecuteNonQuery();
            cpc.Close();

            cpc.Open();
            cmd = new SqlCommand("poultrySearchBy", cpc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Cid", ID);
            mcdGridOut.DataSource = cmd.ExecuteReader();
            mcdGridOut.DataBind();
            GridView1.DataBind();
            cpc.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Data has been deleted!')", true);
        }
    }
}