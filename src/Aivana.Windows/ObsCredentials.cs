using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Localization = Aivana.Core.Localization;

namespace Aivana.Windows;

/// <summary>Stores OBS WebSocket passwords in the current Windows user's Credential Manager.</summary>
public static class ObsCredentials
{
    private const uint GenericCredential = 1;
    private const uint PersistLocalMachine = 2;
    private const int ErrorNotFound = 1168;
    private const int MaximumCredentialBlobBytes = 5 * 1024;
    private const string TargetPrefix = "Aivana.OBS:";

    /// <summary>Returns the canonical endpoint key used for the OBS credential target.</summary>
    public static string NormalizeEndpoint(string? endpoint) => ObsActionClient.NormalizeEndpoint(endpoint);

    /// <summary>Saves the OBS password for this endpoint under the current Windows user.</summary>
    public static void Save(string endpoint, string password)
    {
        ArgumentNullException.ThrowIfNull(password);
        if (password.Length == 0) throw new ArgumentException(Localization.Translate("Das OBS-Passwort darf nicht leer sein."), nameof(password));
        var blob = Encoding.Unicode.GetBytes(password);
        if (blob.Length > MaximumCredentialBlobBytes)
        {
            CryptographicOperations.ZeroMemory(blob);
            throw new ArgumentException(Localization.Translate("Das OBS-Passwort überschreitet die maximale Länge des Windows Credential Managers."), nameof(password));
        }

        var blobPointer = Marshal.AllocHGlobal(blob.Length);
        try
        {
            Marshal.Copy(blob, 0, blobPointer, blob.Length);
            var credential = new NativeCredential
            {
                Type = GenericCredential,
                TargetName = TargetPrefix + NormalizeEndpoint(endpoint),
                CredentialBlobSize = (uint)blob.Length,
                CredentialBlob = blobPointer,
                Persist = PersistLocalMachine,
                UserName = string.Empty
            };
            if (!CredWrite(ref credential, 0))
                throw CredentialError(Localization.Translate("Das OBS-Passwort konnte nicht im Windows Credential Manager gespeichert werden."));
        }
        finally
        {
            CryptographicOperations.ZeroMemory(blob);
            Marshal.Copy(blob, 0, blobPointer, blob.Length);
            Marshal.FreeHGlobal(blobPointer);
        }
    }

    /// <summary>Reads the password saved for this endpoint, or null when no password is stored.</summary>
    public static string? Read(string endpoint)
    {
        var target = TargetPrefix + NormalizeEndpoint(endpoint);
        if (!CredRead(target, GenericCredential, 0, out var credentialPointer))
        {
            var error = Marshal.GetLastWin32Error();
            if (error == ErrorNotFound) return null;
            throw CredentialError(Localization.Translate("Das OBS-Passwort konnte nicht aus dem Windows Credential Manager gelesen werden."), error);
        }

        try
        {
            var credential = Marshal.PtrToStructure<NativeCredential>(credentialPointer);
            if (credential.CredentialBlobSize > MaximumCredentialBlobBytes || credential.CredentialBlobSize % 2 != 0 ||
                (credential.CredentialBlobSize > 0 && credential.CredentialBlob == IntPtr.Zero))
                throw new InvalidOperationException(Localization.Translate("Der gespeicherte OBS-Zugang im Windows Credential Manager ist ungültig."));
            var byteCount = checked((int)credential.CredentialBlobSize);
            if (byteCount == 0) return string.Empty;
            var blob = new byte[byteCount];
            try
            {
                Marshal.Copy(credential.CredentialBlob, blob, 0, byteCount);
                return Encoding.Unicode.GetString(blob);
            }
            finally { CryptographicOperations.ZeroMemory(blob); }
        }
        finally { CredFree(credentialPointer); }
    }

    /// <summary>Deletes this endpoint's saved password; returns false if no password was stored.</summary>
    public static bool Delete(string endpoint)
    {
        var target = TargetPrefix + NormalizeEndpoint(endpoint);
        if (CredDelete(target, GenericCredential, 0)) return true;
        var error = Marshal.GetLastWin32Error();
        if (error == ErrorNotFound) return false;
        throw CredentialError(Localization.Translate("Das OBS-Passwort konnte nicht aus dem Windows Credential Manager gelöscht werden."), error);
    }

    private static InvalidOperationException CredentialError(string message, int? code = null)
    {
        var win32Error = code ?? Marshal.GetLastWin32Error();
        return new InvalidOperationException(Localization.Translate((FormattableString)$"{message} (Windows-Fehler {win32Error})."), new Win32Exception(win32Error));
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct NativeCredential
    {
        public uint Flags;
        public uint Type;
        [MarshalAs(UnmanagedType.LPWStr)] public string TargetName;
        [MarshalAs(UnmanagedType.LPWStr)] public string? Comment;
        public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        public uint CredentialBlobSize;
        public IntPtr CredentialBlob;
        public uint Persist;
        public uint AttributeCount;
        public IntPtr Attributes;
        [MarshalAs(UnmanagedType.LPWStr)] public string? TargetAlias;
        [MarshalAs(UnmanagedType.LPWStr)] public string UserName;
    }

    [DllImport("advapi32.dll", EntryPoint = "CredWriteW", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CredWrite(ref NativeCredential credential, uint flags);

    [DllImport("advapi32.dll", EntryPoint = "CredReadW", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CredRead(string target, uint type, uint flags, out IntPtr credential);

    [DllImport("advapi32.dll", EntryPoint = "CredDeleteW", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CredDelete(string target, uint type, uint flags);

    [DllImport("advapi32.dll", EntryPoint = "CredFree")]
    private static extern void CredFree(IntPtr buffer);
}
