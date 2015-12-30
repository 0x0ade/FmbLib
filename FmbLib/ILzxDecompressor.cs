using System.IO;

public interface ILzxDecompressor {
    
    int Decompress(Stream inData, int inLen, Stream outData, int outLen);
    
}