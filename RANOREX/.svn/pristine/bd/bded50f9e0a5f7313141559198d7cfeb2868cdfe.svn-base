using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.CodeLibrary.Email;

namespace SendeMail
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null  || args.Length == 0  || args[0].Length == 0  || args[1].Length == 0 || args[2].Length == 0 || args[3].Length == 0 || args[4].Length == 0)
            {
                
                Console.Write("Usage: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Sendemail.exe messageBody toEmail(s):  subject fromEmail fromDisplayName attachmentPath");
                Console.ResetColor();
                return;
            }
            MailHelper.SendMessage(args[0], args[1], args[2], "relay2.atsna.atsauto.net", args[3], args[4], new[] { args[5] });
        }
    }
}
