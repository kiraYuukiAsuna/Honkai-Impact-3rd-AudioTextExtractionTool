using System.Text;

namespace AutoExtractCharacterAudioAndTextTool;

public class HashHelper
{
    public static int GetHashCodeExcel(string str)
    {
        int hashCode = 0;
        foreach (byte x in Encoding.UTF8.GetBytes(str))
        {
            hashCode = (hashCode << 5) - hashCode + x;
        }
        hashCode = (hashCode | 0) != hashCode ? hashCode >>> 0 : hashCode;
        return hashCode;
    }

    public static UInt64 GetFnv1_64Hash(string str)
    {
        UInt64 fnv164Init = 0xcbf29ce484222325;
        UInt64 fnv64Prime = 0x100000001b3;

        UInt64 hashCode = fnv164Init;
        
        foreach (var byteData in Encoding.UTF8.GetBytes(str)) 
        {
            hashCode *= fnv64Prime;
            hashCode ^= byteData;
        }

        return hashCode;
    }
    
}