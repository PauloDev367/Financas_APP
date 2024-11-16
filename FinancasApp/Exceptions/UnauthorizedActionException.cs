using System;

namespace FinancasApp.Exceptions;

public class UnauthorizedActionException(string message) : Exception(message)
{

}
