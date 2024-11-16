using System;

namespace FinancasApp.Exceptions;

public class DuplicatedAccountBankException(string message) : Exception(message)
{

}
