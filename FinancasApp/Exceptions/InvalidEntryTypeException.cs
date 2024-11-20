using System;

namespace FinancasApp.Exceptions;

public class InvalidEntryTypeException(string message) : Exception(message)
{

}
