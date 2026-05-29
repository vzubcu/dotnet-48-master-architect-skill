// WCF Security Binding Configuration
// BasicHttpBinding + HTTPS
var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

// WSHttpBinding + Message Security
var wsBinding = new WSHttpBinding(SecurityMode.Message);
wsBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;

// NetTcpBinding for internal high-performance
var tcpBinding = new NetTcpBinding(SecurityMode.Transport);
tcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
tcpBinding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

// Custom binding with certificate
var customBinding = new CustomBinding(
    new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressing10, Encoding.UTF8),
    new HttpsTransportBindingElement
    {
        RequireClientCertificate = true,
        MaxReceivedMessageSize = 65536
    });
