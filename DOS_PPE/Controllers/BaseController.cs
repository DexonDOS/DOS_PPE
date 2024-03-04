using Microsoft.AspNetCore.Mvc;
using DOS_PPE.Data;
using DOS_PPE.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Net.Mime;

namespace Rental.Controllers
{
    public class BaseController : Controller
    {
        private readonly ApplicationDBContext _contextemp;
        private readonly SecondDbContext _context;
        public BaseController(ApplicationDBContext contextemp, SecondDbContext context)
        {
            _context = context;
            _contextemp = contextemp;
        }
        protected bool SetLoginNameInViewData()
        {
            var employee_id = HttpContext.Session.GetString("employee_id");

            var employee = _contextemp.Employee.FirstOrDefault(u => u.employee_id == employee_id);
            var permis = _context.Permission.FirstOrDefault(u => u.employee_id == employee_id);
        
            if (employee != null)
            {
                if (permis != null)
                {
                    ViewData["admin"] = "admin";
                }

                ViewData["name_en"] = employee.name_en;
                ViewData["employee_id"] = employee.employee_id;
                ViewData["profile_pic"] = employee.profile_pic;
                ViewData["dept"] = employee.department;

                SetNotification();
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void SetNotification()
        {
            string? employee_id = ViewData["employee_id"] as string;
            ViewBag.noti = "";
        }
        protected void SentMail(string dear, string email, string cc, string subject, string contant)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("helpdos@dexon-technology.com");
            message.To.Add(email);

            // ตรวจสอบว่ามีที่อยู่ CC หรือไม่
            if (!string.IsNullOrEmpty(cc))
            {
                // แยกที่อยู่ CC ด้วยเครื่องหมายจุลภาค
                string[] ccAddresses = cc.Split(',');

                // สร้างที่อยู่ CC สำหรับแต่ละที่อยู่
                foreach (var ccAddress in ccAddresses)
                {
                    message.CC.Add(ccAddress.Trim());
                }
            }

            message.Subject = subject + " | ILI Issue Tracker System";
            string htmlBody = string.Format(@"<!DOCTYPE html>
                <html>
                <head>
                <style>
                body {{
                    background-color: #F8F9F9;
                    font-family: Calibri, Arial, Helvetica, sans-serif;
                    font-size: 14px;
                    line-height: 1.4;
                    padding: 0;
                    padding-top: 15px;
                }}
                </style>
                </head>
                <body>
                    <table align=""center"" bgcolor=""white"" width=""95%"" border=""0"" cellspacing=""0"" cellpadding=""0"" style=""max-width:650px;margin:0 auto;border-width:1px 1px 0 1px;border-style:solid;border-color:#D4D9DD;"">
                        <tbody>
                            <tr>
                                <td bgcolor=""white"" style=""padding:1.5em 2em;border-bottom:1px solid #DDE3E7;border-top: 5px solid #0074a5;"">
                                    <table bgcolor=""white"" width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                        <tbody>
                                            <tr>
                                                <td valign=""top"">
                                                    <table border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td width=""100%"">
                                                                                    <h4 style=""color:#222222; font-size:20px; margin:0; line-height:25px"">Dear {0} </h4>
                                                                                </td>
                                                                                <td bgcolor=""#CDCDCD"" height=""10"" style=""color:white; font-size:12px; border-radius:2px; margin-top:3px; line-height:12px"">
                                                                                    DOS
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <br>
                                                                    <p>
                                                                        {1}
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style=""font-size:12px;color:#848484;text-align: right;"">
                                                                    <br>
                                                                    <hr>
                                                                    DEXON OMNIA SYSTEM
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </body>
                </html>", dear, contant);
            message.Body = htmlBody;
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("AKIAXJYY7PZO6IUW26MR", "BFwc7/81bMFVebjGLu4FEu9bv27wf9NS6CnVpdF0BPrn");
            smtp.Send(message);
        }
        protected void SentMailLink(string dear, string email, string cc, string subject, string contant, string link, string textlink, string filename)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("helpdos@dexon-technology.com");
            message.To.Add(email);

            // ตรวจสอบว่ามีที่อยู่ CC หรือไม่
            if (!string.IsNullOrEmpty(cc))
            {
                // แยกที่อยู่ CC ด้วยเครื่องหมายจุลภาค
                string[] ccAddresses = cc.Split(',');

                // สร้างที่อยู่ CC สำหรับแต่ละที่อยู่
                foreach (var ccAddress in ccAddresses)
                {
                    message.CC.Add(ccAddress.Trim());
                }
            }

            message.Subject = subject + " | ILI Issue Tracker System";
            string htmlBody = string.Format(@"<!DOCTYPE html>
                <html>
                <head>
                <style>
                body {{
                    background-color: #F8F9F9;
                    font-family: Calibri, Arial, Helvetica, sans-serif;
                    font-size: 14px;
                    line-height: 1.4;
                    padding: 0;
                    padding-top: 15px;
                }}
                </style>
                </head>
                <body>
                    <table align=""center"" bgcolor=""white"" width=""95%"" border=""0"" cellspacing=""0"" cellpadding=""0"" style=""max-width:650px;margin:0 auto;border-width:1px 1px 0 1px;border-style:solid;border-color:#D4D9DD;"">
                        <tbody>
                            <tr>
                                <td bgcolor=""white"" style=""padding:1.5em 2em;border-bottom:1px solid #DDE3E7;border-top: 5px solid #0074a5;"">
                                    <table bgcolor=""white"" width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                        <tbody>
                                            <tr>
                                                <td valign=""top"">
                                                    <table border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td width=""100%"">
                                                                                    <h4 style=""color:#222222; font-size:20px; margin:0; line-height:25px"">Dear {0} </h4>
                                                                                </td>
                                                                                <td bgcolor=""#CDCDCD"" height=""10"" style=""color:white; font-size:12px; border-radius:2px; margin-top:3px; line-height:12px"">
                                                                                    DOS
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <br>
                                                                    <p>
                                                                        {1}
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <table cellspacing=""0"" cellpadding=""6"">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td bgcolor=""#0074a5"" style=""color:white;border-radius:5px;padding:8px 20px;"">
                                                                            <p style=""color:white;font-size:12px;display:inline;margin:0;padding:0 5px;line-height:18px;"">
                                                                                <a href=""{2}"" style=""color:#FFF;text-decoration:none;"">{3}
                                                                            </p>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </tr>
                                                            <tr>
                                                                <td style=""font-size:12px;color:#848484;text-align: right;"">
                                                                    <br>
                                                                    <hr>
                                                                    DEXON OMNIA SYSTEM
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </body>
                </html>", dear, contant, link, textlink);
            message.Body = htmlBody;
            message.IsBodyHtml = true;

            // แนบไฟล์ PDF
            if(filename != "")
            {
                string pdfFilePath = @"C:\File\CrystalReport\"+ filename +".pdf";
                Attachment pdfAttachment = new Attachment(pdfFilePath, MediaTypeNames.Application.Pdf);
                message.Attachments.Add(pdfAttachment);
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "email-smtp.ap-southeast-1.amazonaws.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("AKIAXJYY7PZO6IUW26MR", "BFwc7/81bMFVebjGLu4FEu9bv27wf9NS6CnVpdF0BPrn");
            smtp.Send(message);
        }
        protected void writeEventLog(String Message)
        {
            StreamWriter sw = null;
            try
            {
                //SET IP ADDRESS
                var username = HttpContext.Session.GetString("employee_id");
                var ipAddress = HttpContext.Connection.RemoteIpAddress;
                var forwardedIpAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                var clientIpAddress = !string.IsNullOrEmpty(forwardedIpAddress) ? forwardedIpAddress : ipAddress.ToString();

                string Date = System.DateTime.Now.ToString("dd-MM-yyyy");
                sw = new StreamWriter("C:\\Logs\\Template\\" + Date + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " : " + username + "[" + clientIpAddress + "]" + "::" + Message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                string Date = System.DateTime.Now.ToString("dd-MM-yyyy");
                sw = new StreamWriter("C:\\Logs\\Template\\" + Date + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " : " + ex);
                sw.Flush();
                sw.Close();
            }
        }
        protected void ServiceExportPDF(string filename, string datetime, string parameter1, string parameter2, string parameter3, string parameter4, string parameter5, string parameter6, string parameter7, string dbname)
        {
            // ระบุ path ที่ตั้งอยู่ของ service.exe
            string servicePath = @"C:\ServiceExportPDF\ServiceExportPDF.exe";
            //string servicePath = @"D:\ServiceExportPDF\ServiceExportPDF\bin\Release\ServiceExportPDF.exe";

            string arguments = $"{filename} {datetime} {parameter1} {parameter2} {parameter3} {parameter4} {parameter5} {parameter6} {parameter7} {dbname}";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = servicePath,
                Arguments = arguments,
                CreateNoWindow = true, // ไม่แสดงหน้าต่างของ process
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process process = new Process { StartInfo = psi })
            {
                // เริ่มต้น service
                process.Start();

                // รอให้ .exe ทำงานเสร็จ
                process.WaitForExit();

                // ตัวอย่าง: รอให้ process ทำงานเสร็จและดึง output
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
            }
        }
    }
}
