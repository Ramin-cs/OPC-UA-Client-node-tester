using OpcUaHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Upc_UA_Client_tag_tester
{
    public partial class main_form : Form
    {
        public static OpcUaClient opcua_client = new OpcUaClient();
        public main_form()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!opcua_client.Connected)
                {
                    lw_result.Items.Add("Client not connected!");
                    lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.Red;
                    return;
                }
                else
                {

                    try
                    {
                        var value = Convert.ToString(opcua_client.ReadNode(txt_node.Text).Value);
                        lw_result.Items.Add("Node Value : " + value);
                        lw_result.Items.Add("Trying write access...");
                        int state = 0;
                        try
                        {
                            //bolean
                            opcua_client.WriteNode(txt_node.Text, !Convert.ToBoolean(value));
                            var temp = Convert.ToString(opcua_client.ReadNode(txt_node.Text).Value);
                            if (temp != value)
                            {
                                lw_result.Items.Add("Write access ✓ - Type:Boolean");
                                lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.LightGreen;
                                state = 1;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            //Int32
                            opcua_client.WriteNode(txt_node.Text, Convert.ToInt32(value+1));
                            var temp = Convert.ToString(opcua_client.ReadNode(txt_node.Text).Value);
                            if (temp != value)
                            {
                                lw_result.Items.Add("Write access ✓ - Type:Int32");
                                lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.LightGreen;
                     state=2;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            //UInt32
                            opcua_client.WriteNode(txt_node.Text, Convert.ToUInt32(value + 1));
                            var temp = Convert.ToString(opcua_client.ReadNode(txt_node.Text).Value);
                            if (temp != value)
                            {
                                lw_result.Items.Add("Write access ✓ - Type:UInt32");
                                lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.LightGreen;
                                state = 3;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        var temp2 = Convert.ToString(opcua_client.ReadNode(txt_node.Text).Value);
                        if (value==temp2)
                        {
                            lw_result.Items.Add("Writing Node Value failed!");
                            lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.Red;
                        }
                        else
                        {
                            switch (state)
                            {
                                case 1:
                                  opcua_client.WriteNode(txt_node.Text, Convert.ToBoolean(value));  
                                    break;
                                case 2:
                                    opcua_client.WriteNode(txt_node.Text, Convert.ToInt32(value ));
                                    break;
                                case 3:
                                    opcua_client.WriteNode(txt_node.Text, Convert.ToUInt32(value));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lw_result.Items.Add("Reading Node Value failed!" );
                        lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.Red;
                        lw_result.Items.Add(ex.Message);
                        lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.Red;
                    }



                }

            }
            catch (Exception ex)
            {

               lw_result.Items.Add(ex.Message);
                lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.Red;
            }

        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            try
            {
                if(btn_connect.Text=="Connect")
                {
                    opcua_client.Disconnect();
                    opcua_client.ConnectServer(textBox1.Text);
                    if (!opcua_client.Connected)
                    {
                        lw_result.Items.Add("Client connection failed!");
                        lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.Red;
                        btn_start.Enabled =txt_node.Enabled = false;
                        return;
                    }
                    else
                    {
                        lw_result.Items.Add("Client connection Successfull!");
                        btn_connect.Text = "Disconnect";
                        lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.LightGreen;
                        btn_start.Enabled = txt_node.Enabled = true;

                    }
                }
                else
                {
                    opcua_client.Disconnect();
                    lw_result.Items.Add("Disconnected!");
                    btn_connect.Text = "Connect";
                    lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.Red;
                    btn_start.Enabled = txt_node.Enabled = false;
                }
            
            }
            catch (Exception ex)
            {

                lw_result.Items.Add(ex.Message);
                lw_result.Items[lw_result.Items.Count - 1].ForeColor = Color.Red;
            }
        }
    }
}
