using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace aadhaar_offlinexml_verifier_console
{
    public class AadhaarVerifier
    {
        public static bool VerifySignature(string xmlFilePath, string keyFilePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            // Extract the signature value and remove the signature node
            string signatureValue = xmlDoc.DocumentElement.ChildNodes[1].ChildNodes[1].InnerXml;
            XmlNode signatureNode = xmlDoc.DocumentElement.ChildNodes[1];
            xmlDoc.DocumentElement.RemoveChild(signatureNode);

            // Load public key from certificate
            X509Certificate2 cert = new X509Certificate2(keyFilePath, "public");
            X509CertificateParser certParser = new X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate bcCert = certParser.ReadCertificate(cert.GetRawCertData());

            // Prepare the signer
            ISigner signer = SignerUtilities.GetSigner("SHA256withRSA");
            signer.Init(false, bcCert.GetPublicKey());

            // Prepare data
            byte[] expectedSig = Convert.FromBase64String(signatureValue);
            byte[] msgBytes = Encoding.UTF8.GetBytes(xmlDoc.InnerXml);

            // Verify signature
            signer.BlockUpdate(msgBytes, 0, msgBytes.Length);
            return signer.VerifySignature(expectedSig);
        }
    }
}