using System;
using System.Xml;

namespace aadhaar_offlinexml_verifier_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string XMLFilePath = "D:\\Amit\\netAadhar\\offlineaadhaar20191121011352794.xml";
            string KeyFilePath = "D:\\Amit\\netAadhar\\uidai_offline_publickey_19062019.cer";

            bool isValid = AadhaarVerifier.VerifySignature(XMLFilePath, KeyFilePath);

            Console.WriteLine("\n\n\n");
            if (isValid)
            {
                Console.WriteLine("XML Validate Successfully");
            }
            else
            {
                Console.WriteLine("XML Validation Failed");
            }

            Console.ReadKey();
        }
    }
}
