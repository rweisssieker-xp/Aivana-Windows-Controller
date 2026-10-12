using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net;

internal static class CertificateStore
{
    public static X509Certificate2 LoadOrCreate(string folder,IPAddress address)
    {
        var path=Path.Combine(folder,"certificate-"+Convert.ToHexString(SHA256.HashData(address.GetAddressBytes()))[..16]+".bin");
        if(File.Exists(path))
        {
            var clear = Protect(File.ReadAllBytes(path),false);
            X509Certificate2 certificate;
            try { certificate=X509CertificateLoader.LoadPkcs12(clear,null,X509KeyStorageFlags.UserKeySet); }
            finally { CryptographicOperations.ZeroMemory(clear); }
            if(certificate.NotAfter>DateTime.UtcNow.AddDays(1))return certificate;certificate.Dispose();
        }
        using var rsa=RSA.Create(2048);
        var request=new CertificateRequest("CN=MAKRIVO Local Panel",rsa,HashAlgorithmName.SHA256,RSASignaturePadding.Pkcs1);
        var names=new SubjectAlternativeNameBuilder();names.AddIpAddress(address);names.AddDnsName("localhost");request.CertificateExtensions.Add(names.Build());
        request.CertificateExtensions.Add(new X509BasicConstraintsExtension(false,false,0,true));
        request.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.KeyEncipherment,true));
        request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(new OidCollection { new("1.3.6.1.5.5.7.3.1") },true));
        using var fresh=request.CreateSelfSigned(DateTimeOffset.UtcNow.AddMinutes(-5),DateTimeOffset.UtcNow.AddDays(30));
        var pfx=fresh.Export(X509ContentType.Pfx);
        try { File.WriteAllBytes(path,Protect(pfx,true));return X509CertificateLoader.LoadPkcs12(pfx,null,X509KeyStorageFlags.UserKeySet); }
        finally { CryptographicOperations.ZeroMemory(pfx); }
    }
    private static byte[] Protect(byte[] input,bool encrypt)
    {
        var pointer=Marshal.AllocHGlobal(input.Length);Marshal.Copy(input,0,pointer,input.Length);var blob=new Blob { Size=input.Length,Data=pointer };Blob output=default;
        try
        {
            var success=encrypt?CryptProtectData(ref blob,null,IntPtr.Zero,IntPtr.Zero,IntPtr.Zero,1,out output):CryptUnprotectData(ref blob,IntPtr.Zero,IntPtr.Zero,IntPtr.Zero,IntPtr.Zero,1,out output);
            if(!success)throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            var bytes=new byte[output.Size];Marshal.Copy(output.Data,bytes,0,bytes.Length);return bytes;
        }
        finally { Marshal.FreeHGlobal(pointer);if(output.Data!=IntPtr.Zero)LocalFree(output.Data); }
    }
    [StructLayout(LayoutKind.Sequential)] private struct Blob { public int Size;public IntPtr Data; }
    [DllImport("crypt32.dll",CharSet=CharSet.Unicode,SetLastError=true)] private static extern bool CryptProtectData(ref Blob data,string? description,IntPtr entropy,IntPtr reserved,IntPtr prompt,int flags,out Blob output);
    [DllImport("crypt32.dll",SetLastError=true)] private static extern bool CryptUnprotectData(ref Blob data,IntPtr description,IntPtr entropy,IntPtr reserved,IntPtr prompt,int flags,out Blob output);
    [DllImport("kernel32.dll")] private static extern IntPtr LocalFree(IntPtr memory);
}
