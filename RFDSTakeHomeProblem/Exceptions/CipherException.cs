﻿namespace RFDSTakeHomeProblem.Exceptions;

public class CipherException : Exception
{
    public CipherException(string message) : base(message) { }
    public CipherException(string message, Exception innerException)
        : base(message, innerException) { }
}