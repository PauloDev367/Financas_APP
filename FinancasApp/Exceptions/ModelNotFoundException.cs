using System;

namespace FinancasApp.Exceptions;

public class ModelNotFoundException(string message) : Exception(message)
{

}
