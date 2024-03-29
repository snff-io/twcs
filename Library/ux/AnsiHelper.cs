using QRCoder;
using System.Collections;
using Microsoft.Extensions.ObjectPool;
using System.Text;
using System.Reflection.Emit;


public static class AnsiHelper
{

    public static IEnumerable<string> PairAnsi(string left, string right)
    {
        if (string.IsNullOrEmpty(left) || string.IsNullOrEmpty(right))
            return Enumerable.Empty<string>();

        var lefte = left.Split("\n");
        var righte = right.Split("\n");

        var minLength = Math.Min(lefte.Length, righte.Length);
        var pair = new List<string>();
        for (var i = 0; i < minLength; i++)
        {
            pair.Add($"{lefte[i].Trim()} {righte[i].Trim()}");
        }

        return pair;
    }

    public static string GenerateQRCode(string data)
    {
        // Colors for foreground (fg) and background (bg)
        int[] fg = { 255, 255, 255 }; // White
        int[] bg = { 32, 32, 32 };     // Dark Gray

        // Generate QR code
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.L);
        List<BitArray> matrix = qrCodeData.ModuleMatrix;

        // Construct QR code string with colored modules
        StringBuilder qrCodeString = new StringBuilder();
        for (int z = 0; z < matrix.Count - 1; z += 2)
        {
            for (int x = 0; x < matrix[z].Length; x++)
            {
                bool yu = matrix[z][x];
                bool yl = (z + 1 < matrix.Count) ? matrix[z + 1][x] : false;
                int[] cbg = bg;
                int[] cfg = bg;
                if (yu)
                {
                    cbg = fg;
                }
                if (yl)
                {
                    cfg = fg;
                }
                qrCodeString.Append($"\u001b[48;2;{cbg[0]};{cbg[1]};{cbg[2]}m\u001b[38;2;{cfg[0]};{cfg[1]};{cfg[2]}m▄\u001b[0m");
            }
            qrCodeString.AppendLine();
        }

        return qrCodeString.ToString();
    }
}

