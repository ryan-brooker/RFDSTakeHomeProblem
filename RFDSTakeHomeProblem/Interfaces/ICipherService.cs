namespace RFDSTakeHomeProblem.Interfaces;

public interface ICipherService
{
    string Decrypt(string cipherText);
    void ClearSensitiveData();
}
