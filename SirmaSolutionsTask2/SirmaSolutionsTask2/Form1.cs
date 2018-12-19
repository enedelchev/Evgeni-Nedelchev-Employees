using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SirmaSolutionsTask2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            var employeeList = new List<EmployeeClass>();
            var employeeCloneList = new List<EmployeeClass>();
            var numList = new List<int>();
            int flag = 0;                                           // a flag that helps preveneting the including of headers in the list of records
            string line = "";
            int empID;
            int projectId = 0;
            var dateFrom = new DateTime();
            var dateTo = new DateTime();
            CultureInfo provider = CultureInfo.InvariantCulture;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();


                    //Reading the content of the file and splitting every row by values
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            var staticArray = line.Split(',');

                            if (flag > 0)
                            {
                                staticArray[0] = staticArray[0].Trim();
                                if (!Int32.TryParse(staticArray[0], out empID))
                                {
                                    empID = 0;
                                }

                                staticArray[1] = staticArray[1].Trim();
                                if (!Int32.TryParse(staticArray[1], out projectId))
                                {
                                    projectId = 0;
                                }

                                staticArray[2] = staticArray[2].Trim();
                                if (staticArray[2].ToString() != "null")
                                {
                                    if (!DateTime.TryParse(staticArray[2], out dateFrom)){           //Checking if the string could be a valid date if yes we set the date as a value to dateFrom variable
                                        dateFrom = DateTime.ParseExact(DateTime.Now.ToString(), "mm/dd/yyyy", null);   //If not - we set the current date as a value
                                    }
                                    
                                } else {
                                    dateFrom = DateTime.ParseExact(DateTime.Now.ToString(), "mm/dd/yyyy", null);  
                                }

                                staticArray[3] = staticArray[3].Trim();
                                if (staticArray[3].ToString() != "null")
                                {
                                   /* dateFrom = DateTime.ParseExact(staticArray[3], "mm/dd/yyyy", null);*/

                                    if (!DateTime.TryParse(staticArray[3], out dateTo))
                                    {
                                        dateTo = DateTime.ParseExact(DateTime.Now.ToString(), "mm/dd/yyyy", null);
                                    }
                                } else {
                                    dateTo = DateTime.ParseExact(DateTime.Now.ToString(), "mm/dd/yyyy", null);
                                }

                                employeeList.Add(new EmployeeClass { ProjectId = projectId, EpmloyeeId = empID, DateFrom = dateFrom, DateTo = dateTo }); //list of all records in the file
                            }

                            flag++;
                        }
                    }
                }
            }

            var foundEmployeesPartners = new List<employeePartnersClass>();
            for (var i = 0; i < employeeList.Count; i++){
                for (var j = 0; j < employeeList.Count; j++){
                    if (i != j && employeeList[i].ProjectId == employeeList[j].ProjectId)    // detecting if both of the employees has worked on the same project
                    {
                        if ((employeeList[i].DateFrom <= employeeList[j].DateTo && employeeList[i].DateTo >= employeeList[j].DateFrom))  // detecting if there is a period of time both of the employees has worked together
                        {
                            DateTime bothDateFrom;
                            DateTime bothDateTo;

                            if (employeeList[i].DateFrom >= employeeList[j].DateFrom) // getting the sooner date as the start of the cooperative work
                            {
                                bothDateFrom = employeeList[i].DateFrom;
                            } else {
                                bothDateFrom = employeeList[j].DateFrom;
                            }


                            if (employeeList[i].DateTo <= employeeList[j].DateTo)             // getting the later date as the end of the cooperative work
                            {
                                bothDateTo = employeeList[i].DateTo;
                            } else {
                                bothDateTo = employeeList[j].DateFrom;
                            }

                            var isFoundEmployee = foundEmployeesPartners.Where(em => (em.EpmloyeeId == employeeList[i].EpmloyeeId && em.partnerEpmloyeeId == employeeList[j].EpmloyeeId && em.ProjectId == employeeList[i].ProjectId) || (em.EpmloyeeId == employeeList[j].EpmloyeeId && em.partnerEpmloyeeId == employeeList[i].EpmloyeeId && em.ProjectId == employeeList[i].ProjectId));                // detecting if we already have found that current couple of employees

                            if (isFoundEmployee.Count() == 0)                                           // value of 0 means that we still haven't added that couple in our list, so we add it
                            {

                                foundEmployeesPartners.Add(new employeePartnersClass {
                                        EpmloyeeId = employeeList[i].EpmloyeeId,
                                        partnerEpmloyeeId = employeeList[j].EpmloyeeId,
                                        ProjectId = employeeList[i].ProjectId,
                                        DateFrom = bothDateFrom,
                                        DateTo = bothDateTo
                                });
                            }
                        }
                    }
                }
            }

            //counting how many days every couple have worked together 
            var employeesCouplesCountDateDiff = (from emp in foundEmployeesPartners
                                    orderby (emp.DateTo - emp.DateFrom).TotalDays descending
                                    select new
                                    {
                                        emp.EpmloyeeId,
                                        emp.partnerEpmloyeeId,
                                        emp.ProjectId,
                                        TotalDays = (emp.DateTo - emp.DateFrom).TotalDays
                                    }).ToList();

            //ranking every couple on the TotalDays value 
            var employeesCouplesRankThePeriods = (from emp2 in employeesCouplesCountDateDiff
                                     select new
                                     {
                                         emp2.EpmloyeeId,
                                         emp2.partnerEpmloyeeId,
                                         emp2.ProjectId,
                                         emp2.TotalDays,
                                         Rank = (from emp3 in employeesCouplesCountDateDiff
                                                 where emp3.TotalDays > emp2.TotalDays
                                                 select emp3).Count() + 1
                                     }).ToList();

            //getting only the record that have Rank = 1
            var employeesCouplesFinalResults = (from emp4 in employeesCouplesRankThePeriods
                                     where emp4.Rank == 1
                                     select new
                                     {
                                         emp4.EpmloyeeId,
                                         emp4.partnerEpmloyeeId,
                                         emp4.ProjectId,
                                         emp4.TotalDays
                                     }).ToList();
            /*    viewEmpCouples.DataSource = employeesCouplesFinalResults;*/


            //showing the values in the datagrid

            for (var i = 0; i < employeesCouplesFinalResults.Count(); i++) {
                viewEmpCouples.Rows[i].Cells["empID_1"].Value = employeesCouplesFinalResults[i].EpmloyeeId;
                viewEmpCouples.Rows[i].Cells["empID_2"].Value = employeesCouplesFinalResults[i].partnerEpmloyeeId;
                viewEmpCouples.Rows[i].Cells["projectID"].Value = employeesCouplesFinalResults[i].ProjectId;
                viewEmpCouples.Rows[i].Cells["daysWorked"].Value = employeesCouplesFinalResults[i].TotalDays;
            }


        }
    }
}
